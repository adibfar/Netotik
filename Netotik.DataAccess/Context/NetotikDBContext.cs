using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Conventions;
using Netotik.Domain.Entity;
using Netotik.Domain.EntityConfiguration;
using System.Threading.Tasks;
using EFSecondLevelCache;
using System.Reflection;
using EntityFramework.Filters;
using System.Data.Entity.Infrastructure.Interception;
using System;
using EntityFramework.BulkInsert;
using RefactorThis.GraphDiff;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Netotik.Data.Context
{
    public partial class NetotikDBContext : IdentityDbContext
        <User, Role, long, UserLogin, UserRole, UserClaim>,
        IUnitOfWork
    {

        #region Constructor


        public NetotikDBContext()
            : base("NetotikDBContext")
        {
            //this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
            //for bulk insert
            //this.Configuration.AutoDetectChangesEnabled = false;
        }

        #endregion



        public DbSet<StatisticCountry> StatisticCountries { get; set; }
        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<UserReseller> UserReselleres { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserAdmin> UserAdmines { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> AddressCities { get; set; }
        public DbSet<State> AddressStates { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentComment> ContentComments { get; set; }
        public DbSet<ImageGalleryItem> ImageGalleryItems { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Permisson> Permissons { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ContentCategory> ContentCategories { get; set; }
        public DbSet<ContentTag> ContentTags { get; set; }
        public DbSet<InboxContactUsMessage> InboxMessages { get; set; }
        public DbSet<LocaleStringResource> LocaleStringResources { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageTranslation> LanguageTranslationes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LanguageTranslationMap());
            modelBuilder.Configurations.Add(new UserResellerMap());
            modelBuilder.Configurations.Add(new UserCompanyMap());
            modelBuilder.Configurations.Add(new UserAdminMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new AddressCityMap());
            modelBuilder.Configurations.Add(new StateMap());
            modelBuilder.Configurations.Add(new ContentMap());
            modelBuilder.Configurations.Add(new ContentCommentMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new PaymentTypeMap());
            modelBuilder.Configurations.Add(new PictureMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new PermissonMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new ContentCategoryMap());
            modelBuilder.Configurations.Add(new ContentTagMap());
            modelBuilder.Configurations.Add(new InboxMessageMap());
            modelBuilder.Configurations.Add(new LocaleStringResourceMap());


            modelBuilder.Entity<UserRole>()
            .HasKey(r => new { r.UserId, r.RoleId })
            .ToTable("AspNetUserRoles");

            modelBuilder.Entity<UserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");
        }

        #region IUnitOfWork Members

        public T Update<T>(T entity, System.Linq.Expressions.Expression<Func<IUpdateConfiguration<T>, object>> mapping)
            where T : class, new()
        {

            return this.UpdateGraph(entity, mapping);

        }

        private string[] GetChangedEntityNames()
        {
            return ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added ||
                            x.State == EntityState.Modified ||
                            x.State == EntityState.Deleted)
                .Select(x => ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
                .Distinct()
                .ToArray();
        }

        public new System.Data.Entity.IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        public void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Detached;
        }
        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public override int SaveChanges()
        {
            return SaveAllChanges();
        }

        public int SaveAllChanges(bool invalidateCacheDependencies = true)
        {
            var result = base.SaveChanges();
            if (!invalidateCacheDependencies) return result;
            var changedEntityNames = GetChangedEntityNames();
            new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
            return result;
        }

        public override Task<int> SaveChangesAsync()
        {
            return SaveAllChangesAsync();
        }

        public Task<int> SaveAllChangesAsync(bool invalidateCacheDependencies = true)
        {

            try
            {
                var result = base.SaveChangesAsync();

                if (!invalidateCacheDependencies) return result;

                var changedEntityNames = GetChangedEntityNames();
                new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
                return result;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        var a = string.Format("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return Task.FromResult<int>(0);

        }



        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)Set<TEntity>()).AddRange(entities);
        }


        public void ForceDatabaseInitialize()
        {
            base.Database.Initialize(force: true);
        }

        public void EnableFiltering(string name)
        {
            this.EnableFilter(name);
        }

        public void EnableFiltering(string name, string parameter, object value)
        {
            this.EnableFilter(name).SetParameter(parameter, value);
        }

        public void DisableFiltering(string name)
        {
            this.DisableFilter(name);
        }

        public void BulkInsertData<T>(IList<T> data)
        {
            //this.BulkInsert(data);
        }

        public bool ValidateOnSaveEnabled
        {
            get { return Configuration.ValidateOnSaveEnabled; }
            set { Configuration.ValidateOnSaveEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return Configuration.ProxyCreationEnabled; }
            set { Configuration.ProxyCreationEnabled = value; }
        }

        bool IUnitOfWork.AutoDetectChangesEnabled
        {
            get { return Configuration.AutoDetectChangesEnabled; }
            set { Configuration.AutoDetectChangesEnabled = value; }
        }

        public bool ForceNoTracking { get; set; }
        #endregion

        //#region Override OnModelCreating
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    if (modelBuilder == null)
        //        throw new ArgumentNullException("modelBuilder");

        //    DbInterception.Add(new FilterInterceptor());
        //    //DbInterception.Add(new YeKeInterceptor());

        //    modelBuilder.Configurations.AddFromAssembly(typeof(AddressCityMap).GetTypeInfo().Assembly);
        //    LoadEntities(typeof(User).GetTypeInfo().Assembly, modelBuilder, "Netotik.Domain.Entity");
        //}

        //#endregion

        //#region AutoRegisterEntityType

        //public void LoadEntities(Assembly asm, DbModelBuilder modelBuilder, string nameSpace)
        //{
        //    var entityTypes = asm.GetTypes()
        //        .Where(type => type.BaseType != null &&
        //                       type.Namespace == nameSpace &&
        //                       type.BaseType == null)
        //        .ToList();

        //    var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
        //    entityTypes.ForEach(type => entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { }));
        //}


        //#endregion


    }
}
