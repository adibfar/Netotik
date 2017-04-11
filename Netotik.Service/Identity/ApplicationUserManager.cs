using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using RefactorThis.GraphDiff;
using Netotik.Domain.Entity;
using Netotik.Data;
using Netotik.Common.Extensions;
using Netotik.ViewModels.Identity.UserAdmin;
using Netotik.Common.Caching;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;

namespace Netotik.Services.Identity
{
    public class ApplicationUserManager : UserManager<User, long>, IApplicationUserManager
    {

        #region Fields

        private readonly IIdentity _identity;
        private User _user;
        private readonly HttpContextBase _contextBase;
        private readonly IPermissionService _permissionService;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMappingEngine _mappingEngine;

        #endregion

        #region Constructor

        public ApplicationUserManager(IIdentity identity,
            HttpContextBase contextBase, IPermissionService permissionService, IUserStore<User, long> userStore, IApplicationRoleManager roleManager, IUnitOfWork unitOfWork,
            IMappingEngine mappingEngine, IDataProtectionProvider dataProtectionProvider)
            : base(userStore)
        {
            _permissionService = permissionService;
            AutoCommitEnabled = true;
            _dataProtectionProvider = dataProtectionProvider;
            _mappingEngine = mappingEngine;
            _unitOfWork = unitOfWork;
            _users = _unitOfWork.Set<User>();
            _roleManager = roleManager;
            _contextBase = contextBase;
            _identity = identity;

            CreateApplicationUserManager();


        }

        #endregion


        public ViewModels.Identity.UserAdmin.ProfileModel GetUserAdminProfile()
        {
            return _mappingEngine.Map<ViewModels.Identity.UserAdmin.ProfileModel>(GetCurrentUser());
        }

        public async Task UpdateUserAdminProfile(ViewModels.Identity.UserAdmin.ProfileModel model)
        {
            var user = _users.Find(GetCurrentUserId());
            _mappingEngine.Map(model, user);
            await _unitOfWork.SaveChangesAsync();
        }
        public IList<UserItem> GetListUserAdmins(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<User> all = _users.Where(x => !x.IsDeleted).AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.FirstName.Contains(model.sSearch) ||
                    x.LastName.Contains(model.sSearch) ||
                    x.Email.Contains(model.sSearch) ||
                    x.PhoneNumber.Contains(model.sSearch))
                    .AsQueryable();
            }


            // Apply Sorting
            Func<User, string> orderingFunction = (x => model.iSortCol_0 == 1 ? x.FirstName :
                                                            model.iSortCol_0 == 2 ? x.Email : x.PhoneNumber);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new UserItem
                {
                    ImageFileName = x.PictureId.HasValue ? x.Picture.FileName : "Default.png",
                    PhoneNumber = x.PhoneNumber,
                    Name = string.Format("{0} {1}", x.FirstName, x.LastName),
                    LastLoginDate = PersianDate.ConvertDate.ToFa(x.LastLoginDate, "g"),
                    Email = x.Email,
                    Id = x.Id,
                    IsBanned = x.IsBanned,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();

        }



        #region GenerateUserIdentityAsync
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
        #endregion

        #region HasPassword

        public async Task<
            bool> HasPassword(long userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }

        #endregion

        #region HasPhoneNumber
        public async Task<bool> HasPhoneNumber(long userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }
        #endregion

        #region OnValidateIdentity
        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return CustomSecurityStampValidator.OnValidateIdentity(
                         validateInterval: TimeSpan.FromMinutes(0),
                         regenerateIdentityCallback: GenerateUserIdentityAsync,
                         getUserIdCallback: identity => identity.GetUserId<long>());
        }

        #endregion

        #region SeedDatabase

        public async void SeedDatabase()
        {
            var now = DateTime.Now;
            const string systemAdminUserName = "adibfar";
            const string systemAdminEmail = "pouriya.adibfar@gmail.com";
            const string systemAdminPassword = "adibfar";
            var newUser = this.FindByName(systemAdminUserName);
            if (newUser == null)
            {
                newUser = new User
                {
                    FirstName = "کاربر",
                    LastName = "مدیر",
                    UserName = systemAdminUserName.ToLower(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    TwoFactorEnabled = false,
                    Email = systemAdminEmail,
                    CreateDate = now,
                    EditDate = now,
                    IsDeleted = false,
                    IsBanned = false,
                    UserType = UserType.UserAdmin
                };
                var result = await this.CreateAsync(newUser, systemAdminPassword);
                this.SetLockoutEnabled(newUser.Id, false);
            }
            var userRoles = _roleManager.FindUserRoles(newUser.Id);
            if (userRoles.Any(a => a == StandardRoles.SuperAdministrators)) return;
            this.AddToRole(newUser.Id, StandardRoles.SuperAdministrators);
        }

        #endregion

        #region GenerateUserIdentityAsync
        private static async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
        #endregion

        #region GetAllUsersAsync
        public Task<List<User>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }
        #endregion

        #region CreateApplicationUserManager

        private void CreateApplicationUserManager()
        {
            ClaimsIdentityFactory = new CustomClaimsIdentityFactory();

            UserValidator = new CustomUserValidator<User, long>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            //RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser, int>
            //{
            //    MessageFormat = "کد فعال سازی شما {0} است"
            //});
            //RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser, int>
            //{
            //    Subject = "کد فعال سازی",
            //    BodyFormat = "کد فعال سازی شما {0} است"
            //});


            if (_dataProtectionProvider == null) return;

            var dataProtector = _dataProtectionProvider.Create("Asp.net Identity");
            UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtector);

        }
        #endregion

        #region DeleteAll
        public async Task RemoveAll()
        {
            await Users.DeleteAsync();
        }
        #endregion

        #region GetUsersWithRoleIds
        public IList<User> GetUsersWithRoleIds(params long[] ids)
        {
            return Users.Where(applicationUser => ids.Contains(applicationUser.Id))
                .ToList();
        }
        #endregion

        #region AutoCommitEnabled
        public bool AutoCommitEnabled { get; set; }

        #endregion

        #region Any
        public bool Any()
        {
            return _users.Any();
        }
        #endregion

        #region AddRange
        public void AddRange(IEnumerable<User> users)
        {
            _unitOfWork.AddThisRange(users);
        }
        #endregion

        #region GetUserByRoles
        public async Task<AdminEditModel> GetUserByRolesAsync(long id)
        {
            var userWithRoles = await
                 _users.AsNoTracking()
                     .Include(a => a.Roles)
                     .FirstOrDefaultAsync(a => a.Id == id);
            return _mappingEngine.Map<AdminEditModel>(userWithRoles);
        }

        #endregion

        #region EditUser
        public async Task<bool> EditUser(AdminEditModel viewModel)
        {
            var passwordModify = false;

            if (viewModel.RoleIds == null || viewModel.RoleIds.Length < 1) return await Task.FromResult(false);

            var user = _users.Find(viewModel.Id);
            //_unitOfWork.MarkAsDetached(user);

            _mappingEngine.Map(viewModel, user);
            if (viewModel.Picture != null)
                user.Picture = viewModel.Picture;

            //var emailModify = viewModel.Email != user.Email;

            //if (emailModify)
            //{
            //    user.EmailConfirmed = false;
            //}

            user.Roles.Clear();
            viewModel.RoleIds.ToList().ForEach(roleId => user.Roles.Add(new UserRole { RoleId = roleId, UserId = user.Id }));

            //user.Picture = viewModel.Picture;
            //_unitOfWork.Update(user, a => a.AssociatedCollection(u => u.Roles));



            //if (passwordModify || emailModify)
            if (passwordModify)
                this.UpdateSecurityStamp(user.Id);
            //else
            //    await _unitOfWork.SaveAllChangesAsync();

            await _unitOfWork.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        #endregion

        #region SetRolesToUser
        public void SetRolesToUser(User user, IEnumerable<Role> roles)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AddUser
        public async Task<User> AddUser(AdminAddModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            viewModel.RoleIds.ToList().ForEach(roleId => user.Roles.Add(new UserRole { RoleId = roleId }));
            await CreateAsync(user, viewModel.Password);
            return user;
        }
        public async Task<long> AddReseller(ViewModels.Identity.UserReseller.RegisterViewModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            user.UserType = UserType.UserReseller;

            await CreateAsync(user, viewModel.Password);

            return user.Id;

        }
        #endregion

        #region Validations

        public bool CheckUserNameExist(string userName, long? id)
        {
            return id == null
                ? _users.Any(a => a.UserName == userName.ToLower())
                : _users.Any(a => a.UserName == userName.ToLower() && a.Id != id.Value);
        }


        public bool CheckEmailExist(string email, long? id)
        {
            return id == null
               ? _users.Any(a => a.Email == email.ToLower())
               : _users.Any(a => a.Email == email.ToLower() && a.Id != id.Value);
        }

        public bool CheckNationalCodeExist(string nCode, long? id)
        {
            return id == null
               ? _users.Any(a => a.UserReseller.NationalCode == nCode)
               : _users.Any(a => a.UserReseller.NationalCode == nCode && a.Id != id.Value);
        }
        public bool CheckResellerCompanyNameExist(string name, long? id)
        {
            return id == null
               ? _users.Any(a => a.UserReseller.ResellerCode == name.ToLower())
               : _users.Any(a => a.UserReseller.ResellerCode == name.ToLower() && a.Id != id.Value);
        }

        public bool CheckGooglePlusIdExist(string googlePlusId, long? id)
        {
            return false;
            //return id == null
            //        ? _users.Any(a => a.GooglePlusId == googlePlusId)
            //        : _users.Any(a => a.GooglePlusId == googlePlusId && a.Id != id.Value);
        }

        public bool CheckFacebookIdExist(string faceBookId, long? id)
        {
            return false;
            //return id == null
            //  ? _users.Any(a => a.FaceBookId == faceBookId)
            //  : _users.Any(a => a.FaceBookId == faceBookId && a.Id != id.Value);
        }

        public bool CheckIsPhoneNumberAvailable(string phoneNumber, long? id)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.Id != id.Value);
        }
        #endregion

        #region override GetRolesAsync
        public async override Task<IList<string>> GetRolesAsync(long userId)
        {

            var userPermissions = await _roleManager.FindUserPermissionsAsync(userId);
            ////todo: any permission form other sections
            return userPermissions;
        }
        #endregion


        #region CustomValidatePasswordAsync
        public async Task<string> CustomValidatePasswordAsync(string pass)
        {
            var result = await PasswordValidator.ValidateAsync(pass);
            return result.Errors.GetUserManagerErros();
        }
        #endregion


        #region GetEmailStore
        public IUserEmailStore<User, long> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<User, long>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }
            return cast;
        }

        #endregion

        #region Private
        //private static string[] SplitString(string dependencies)
        //{
        //    if (dependencies == null) return new string[0];
        //    var split = from dependency in dependencies.Split(',')
        //                let lowerDependency = dependency.ToLower()
        //                where !string.IsNullOrEmpty(lowerDependency)
        //                select lowerDependency;
        //    return split.ToArray();
        //}
        #endregion

        #region IsEmailConfirmedByUserNameAsync
        public bool IsEmailConfirmedByUserNameAsync(string userName)
        {
            return _users.Any(a => a.UserName == userName.ToLower() && a.EmailConfirmed);

        }

        #endregion

        #region IsEmailAvailableForConfirm
        public bool IsEmailAvailableForConfirm(string email)
        {
            return _users.Any(a => a.Email == email);
        }
        #endregion

        #region EditSecurityStamp
        public void EditSecurityStamp(long userId)
        {
            this.UpdateSecurityStamp(userId);
        }
        #endregion

        #region FindUserById
        public User FindUserById(long id)
        {
            return this.FindById(id);
        }
        #endregion

        #region FindUserByResellerCodeAsync
        public Task<User> FindByResellerCodeAsync(string Code)
        {
            return _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.IsBanned && x.EmailConfirmed && x.UserType == UserType.UserReseller && x.UserReseller.ResellerCode == Code);
        }
        #endregion

        #region CurrentUser
        public User GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return _user ?? (_user = await FindByIdAsync(GetCurrentUserId()));
        }

        public long GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId<long>();
        }
        #endregion

        public IList<User> GetbyIds(long[] ids)
        {
            var tag = _users.ToList();
            var list = new List<User>();
            if (ids != null)
            {
                foreach (var item in ids)
                {
                    var t = tag.FirstOrDefault(x => x.Id == item && !x.IsDeleted);
                    if (t != null)
                        list.Add(t);
                }
            }
            return list;
        }



        #region ChechIsBanneduser
        public bool CheckIsUserBanned(long id)
        {
            return _users.Any(a => a.Id == id && (a.IsBanned));
        }

        public bool CheckIsUserBanned(string userName)
        {
            return _users.Any(a => a.UserName == userName.ToLower() && (a.IsBanned));
        }
        public bool CheckIsUserBannedByEmail(string email)
        {
            return _users.Any(a => a.Email == email && (a.IsBanned));
        }


        #endregion

        public async Task<bool> LogicalRemove(long id)
        {
            var key = id.ToString(CultureInfo.InvariantCulture) + "_roles";
            _contextBase.InvalidateCache(key);
            var result = await _users.Where(a => a.Id == id).UpdateAsync(a => new User { IsDeleted = true });
            return result > 0;
        }
        public bool CheckIsUserBannedOrDelete(long id)
        {
            return _users.Any(a => a.Id == id && (a.IsBanned || a.IsDeleted));
        }

        public bool CheckIsUserBannedOrDelete(string userName)
        {
            return _users.Any(a => a.UserName == userName.ToLower() && (a.IsBanned || a.IsDeleted));
        }

        public bool CheckIsUserBannedOrDeleteByEmail(string email)
        {
            return _users.Any(a => a.Email == email && (a.IsBanned || a.IsDeleted));
        }


    }
}
