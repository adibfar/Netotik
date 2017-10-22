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
using Netotik.Common.Caching;
using Netotik.Common.Controller;
using Netotik.Common.DataTables;
using Netotik.Services.Abstract;
using Netotik.ViewModels.Identity.UserAdmin;
using Netotik.Common.Security.RijndaelEncryption;
using System.Xml.Linq;
using Netotik.ViewModels.Identity.UserRouter;

namespace Netotik.Services.Identity
{
    public class ApplicationUserManager : UserManager<User, long>, IApplicationUserManager
    {

        #region Fields

        private readonly IIdentity _identity;
        private User _user;
        private readonly HttpContextBase _contextBase;
        private readonly IPermissionService _permissionService;
        private readonly IPermissionClientService _permissionClientService;
        private readonly IPermissionRouterService _permissionRouterService;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ILanguageService _languageService;
        private readonly ILanguageTranslationService _languageTranslationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMappingEngine _mappingEngine;

        #endregion

        #region Constructor

        public ApplicationUserManager(IIdentity identity, IPermissionRouterService permissionRouterService,
            IPermissionClientService permissionClientService, ILanguageService languageService, ILanguageTranslationService languageTranslationService,
            HttpContextBase contextBase, IPermissionService permissionService, IUserStore<User, long> userStore, IApplicationRoleManager roleManager, IUnitOfWork unitOfWork,
            IMappingEngine mappingEngine, IDataProtectionProvider dataProtectionProvider)
            : base(userStore)
        {
            _permissionRouterService = permissionRouterService;
            _permissionClientService = permissionClientService;
            _languageTranslationService = languageTranslationService;
            _languageService = languageService;
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
            var profile = _mappingEngine.Map<ViewModels.Identity.UserAdmin.ProfileModel>(GetCurrentUser());

            profile.Items = _languageService
                .All()
                .Where(x => x.Published)
                .Include(x => x.LanguageTranslationes)
                .ToList()
                .Select(x => new ProfileLanguageItem
                {
                    Language = x,
                    ShortBio = _languageTranslationService.GetLocalized(GetCurrentUserId().ToString(), x.Id, "User", "ShortBio"),
                    ShowName = _languageTranslationService.GetLocalized(GetCurrentUserId().ToString(), x.Id, "User", "ShowName")
                }).ToList();

            return profile;
        }


        public ViewModels.Identity.UserReseller.ProfileModel GetUserResellerProfile()
        {
            return _mappingEngine.Map<ViewModels.Identity.UserReseller.ProfileModel>(GetCurrentUser());
        }

        public ViewModels.Identity.UserRouter.ProfileModel GetUserRouterProfile(long id)
        {
            return _mappingEngine.Map<ViewModels.Identity.UserRouter.ProfileModel>(_users.FirstOrDefault(a => a.Id == id && !a.IsDeleted));
        }

        public ViewModels.Identity.UserRouter.MikrotikConfModel GetUserRouterMikrotikConf(long id)
        {
            return _mappingEngine.Map<ViewModels.Identity.UserRouter.MikrotikConfModel>(_users.FirstOrDefault(a => a.Id == id && !a.IsDeleted));
        }
        public ViewModels.Identity.UserRouter.TelegramBotModel GetUserRouterTelegramBot(long id)
        {
            return _mappingEngine.Map<ViewModels.Identity.UserRouter.TelegramBotModel>(_users.FirstOrDefault(a => a.Id == id && !a.IsDeleted));
        }
        public IList<ViewModels.Identity.UserRouter.RouterList> GetListUserRouter(long id)
        {
            IList<ViewModels.Identity.UserRouter.RouterList> selectedUsers = _users.Where(x => !x.IsDeleted && x.UserRouter.UserResellerId == id)
                                            .Select(x => new ViewModels.Identity.UserRouter.RouterList
                                            {
                                                Id = x.Id,
                                                RouterCode = x.UserRouter.RouterCode,
                                                Email = x.Email,
                                                FirstName = x.FirstName,
                                                IsBanned = x.IsBanned,
                                                NationalCode = x.UserRouter.NationalCode,
                                                PhoneNumber = x.PhoneNumber,
                                                UserName = x.UserName,
                                                ImageAvatar = x.PictureId.HasValue ? x.Picture.FileName : "Default.png",
                                            }).ToList();

            return selectedUsers;
        }

        public IList<RouterAdminList> GetListUserRouters(RequestListModel model, out long TotalCount, out long ShowCount)
        {

            IQueryable<User> all = _users
                .Where(x => !x.IsDeleted && x.UserType == UserType.UserRouter)
                .AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.
                    Where(x => x.FirstName.Contains(model.sSearch) ||
                    x.UserReseller.User.FirstName.Contains(model.sSearch) ||
                    x.UserReseller.User.LastName.Contains(model.sSearch) ||
                    x.Email.Contains(model.sSearch) ||
                    x.PhoneNumber.Contains(model.sSearch))
                    .AsQueryable();
            }


            // Apply Sorting
            Func<User, string> orderingFunction = (x => model.iSortCol_0 == 2 ? x.FirstName : x.Email);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            var test = all.ToList();
            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new ViewModels.Identity.UserRouter.RouterAdminList
                {
                    ImageFileName = x.PictureId.HasValue ? x.Picture.FileName : "Default.png",
                    PhoneNumber = x.PhoneNumber,
                    ResellerName = string.Format("{0} {1}", x.UserRouter.UserReseller.User.FirstName, x.UserRouter.UserReseller.User.LastName),
                    ResellerId = x.UserRouter.UserResellerId,
                    Name = x.FirstName,
                    LastLoginDate = PersianDate.ConvertDate.ToFa(x.LastLoginDate, "g"),
                    Email = x.Email,
                    Id = x.Id,
                    IsBanned = x.IsBanned,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();
        }
        

        ViewModels.Identity.UserRouter.RegisterSettingModel IApplicationUserManager.GetRouterRegisterSetting(long UserId)
        {
            var user = _users.FirstOrDefault(x => x.Id == UserId);
            var model = new ViewModels.Identity.UserRouter.RegisterSettingModel();

            model.Id = UserId;
            model.Age = user.UserRouter.UserRouterRegisterSetting.Age;
            model.BirthDate = user.UserRouter.UserRouterRegisterSetting.BirthDate;
            model.Email = user.UserRouter.UserRouterRegisterSetting.Email;
            model.IsMale = user.UserRouter.UserRouterRegisterSetting.IsMale;
            model.MobileNumber = user.UserRouter.UserRouterRegisterSetting.MobileNumber;
            model.Name = user.UserRouter.UserRouterRegisterSetting.Name;
            model.NationalCode = user.UserRouter.UserRouterRegisterSetting.NationalCode;
            model.Password = user.UserRouter.UserRouterRegisterSetting.Password;
            model.PasswordConfirm = user.UserRouter.UserRouterRegisterSetting.PasswordConfirm;
            model.Username = user.UserRouter.UserRouterRegisterSetting.Username;
            model.SendEmailUserPass = user.UserRouter.UserRouterRegisterSetting.SendEmailUserPass;
            model.SendSmsUserPass = user.UserRouter.RegisterFormSms;

            return model;
        }

        public async Task UpdateRouterRegisterSettingAsync(ViewModels.Identity.UserRouter.RegisterSettingModel model)
        {
            var user = _users.Find(model.Id);
            user.UserRouter.UserRouterRegisterSetting.Age = model.Age;
            user.UserRouter.UserRouterRegisterSetting.BirthDate = model.BirthDate;
            user.UserRouter.UserRouterRegisterSetting.Email = model.Email;
            user.UserRouter.UserRouterRegisterSetting.IsMale = model.IsMale;
            user.UserRouter.UserRouterRegisterSetting.MobileNumber = model.MobileNumber;
            user.UserRouter.UserRouterRegisterSetting.Name = model.Name;
            user.UserRouter.UserRouterRegisterSetting.NationalCode = model.NationalCode;
            user.UserRouter.UserRouterRegisterSetting.Password = model.Password;
            user.UserRouter.UserRouterRegisterSetting.PasswordConfirm = model.PasswordConfirm;
            user.UserRouter.UserRouterRegisterSetting.Username = model.Username;
            user.UserRouter.UserRouterRegisterSetting.SendEmailUserPass = model.SendEmailUserPass;
            user.UserRouter.RegisterFormSms = model.SendSmsUserPass;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserAdminProfile(ViewModels.Identity.UserAdmin.ProfileModel model)
        {
            var user = _users.Find(GetCurrentUserId());
            var id = user.Id.ToString();
            var trans = await _languageTranslationService.All().Where(x => x.EntityId == id).ToListAsync();
            _mappingEngine.Map(model, user);



            for (int a = 0; a < model.LanguageIds.Length; a++)
            {
                var showName = trans.FirstOrDefault(x => x.ObjectName == "User" && x.PropertyName == "ShowName" && x.LanguageId == model.LanguageIds[a]);
                if (showName == null)
                {
                    _languageTranslationService.Add(new LanguageTranslation
                    {
                        EntityId = id,
                        LanguageId = model.LanguageIds[a],
                        ObjectName = "User",
                        PropertyName = "ShowName",
                        Value = model.ShowNames[a]
                    });
                }
                else
                {
                    showName.Value = model.ShowNames[a];
                }

                var shortBio = trans.FirstOrDefault(x => x.ObjectName == "User" && x.PropertyName == "ShortBio" && x.LanguageId == model.LanguageIds[a]);
                if (showName == null)
                {
                    _languageTranslationService.Add(new LanguageTranslation
                    {
                        EntityId = id,
                        LanguageId = model.LanguageIds[a],
                        ObjectName = "User",
                        PropertyName = "ShortBio",
                        Value = model.ShowNames[a]
                    });
                }
                else
                {
                    showName.Value = model.ShowNames[a];
                }

            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateUserResellerProfile(ViewModels.Identity.UserReseller.ProfileModel model)
        {
            var user = _users.Find(GetCurrentUserId());
            var emailModify = model.Email != user.Email;

            if (emailModify)
            {
                user.EmailConfirmed = false;
            }
            _mappingEngine.Map(model, user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserClientPermissions(ViewModels.Identity.UserRouter.ProfileModel model)
        {
            var user = _users.Find(model.Id);

            var XmlClientPermissions = "";
            if (model.ClientPermissionNames == null || model.ClientPermissionNames.Length < 1)
                XmlClientPermissions = _permissionClientService.GetPermissionsAsXml("null").ToString();
            else XmlClientPermissions = _permissionClientService.GetPermissionsAsXml(model.ClientPermissionNames).ToString();

            user.UserRouter.ClientPermissions = XmlClientPermissions;

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserRouterProfile(ViewModels.Identity.UserRouter.ProfileModel model)
        {
            var user = _users.Find(model.Id);

            _mappingEngine.Map(model, user);

            var emailModify = model.Email != user.Email;

            if (emailModify)
            {
                user.EmailConfirmed = false;
            }

            //var XmlClientPermissions = "";
            //if (model.ClientPermissionNames == null || model.ClientPermissionNames.Length < 1)
            //    XmlClientPermissions = _permissionClientService.GetPermissionsAsXml("null").ToString();
            //else XmlClientPermissions = _permissionClientService.GetPermissionsAsXml(model.ClientPermissionNames).ToString();

            //user.UserRouter.ClientPermissions = XmlClientPermissions;

            //var XmlCompanyPermissions = "";
            //if (model.RouterPermissionNames == null || model.RouterPermissionNames.Length < 1)
            //    XmlCompanyPermissions = _permissionCompanyService.GetPermissionsAsXml("null").ToString();
            //else XmlCompanyPermissions = _permissionCompanyService.GetPermissionsAsXml(model.RouterPermissionNames).ToString();

            //user.UserRouter.CompanyPermissions= XmlCompanyPermissions;

            await _unitOfWork.SaveChangesAsync();
        }
        public IList<ViewModels.Identity.UserAdmin.UserItem> GetListUserAdmins(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<User> all = _users.Where(x => !x.IsDeleted && x.UserType == UserType.UserAdmin).AsQueryable();
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
                .Select((x, index) => new ViewModels.Identity.UserAdmin.UserItem
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

        public async Task UpdateUserRouterMikrotikConf(ViewModels.Identity.UserRouter.MikrotikConfModel model)
        {
            var user = _users.Find(model.Id);
            _mappingEngine.Map(model, user);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateUserRouterTelegramBot(ViewModels.Identity.UserRouter.TelegramBotModel model)
        {
            var user = _users.Find(model.Id);
            _mappingEngine.Map(model, user);
            await _unitOfWork.SaveChangesAsync();
        }
        public IList<ViewModels.Identity.UserReseller.UserItem> GetListUserResellers(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<User> all = _users.Where(x => !x.IsDeleted && x.UserType == UserType.UserReseller).AsQueryable();
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
                .Select((x, index) => new ViewModels.Identity.UserReseller.UserItem
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
                RequireUniqueEmail = false,
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
        public async Task<ViewModels.Identity.UserAdmin.AdminEditModel> GetUserAdminByIdAsync(long id)
        {
            var userWithRoles = await
                 _users.AsNoTracking()
                     .Include(a => a.Roles)
                     .FirstOrDefaultAsync(a => a.Id == id);
            return _mappingEngine.Map<ViewModels.Identity.UserAdmin.AdminEditModel>(userWithRoles);
        }

        public async Task<ViewModels.Identity.UserReseller.ResellerEditModel> GetUserResellerByIdAsync(long id)
        {
            var userWithRoles = await
                 _users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

            return _mappingEngine.Map<ViewModels.Identity.UserReseller.ResellerEditModel>(userWithRoles);
        }

        public async Task<ViewModels.Identity.UserRouter.RouterEditModel> GetUserRouterByIdAsync(long id)
        {
            var userWithRoles = await
                 _users.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

            return _mappingEngine.Map<ViewModels.Identity.UserRouter.RouterEditModel>(userWithRoles);
        }
        #endregion

        #region EditUser
        public async Task<bool> EditUser(ViewModels.Identity.UserAdmin.AdminEditModel viewModel)
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

        public async Task<bool> EditReseller(ViewModels.Identity.UserReseller.ResellerEditModel viewModel)
        {
            var passwordModify = false;

            var user = _users.Find(viewModel.Id);
            //_unitOfWork.MarkAsDetached(user);

            _mappingEngine.Map(viewModel, user);
            if (viewModel.Picture != null)
                user.Picture = viewModel.Picture;

            var emailModify = viewModel.Email != user.Email;

            if (emailModify)
            {
                user.EmailConfirmed = false;
            }

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
        public async Task<bool> EditRouter(ViewModels.Identity.UserRouter.RouterEditModel viewModel)
        {
            var passwordModify = false;

            var user = _users.Find(viewModel.Id);
            //_unitOfWork.MarkAsDetached(user);

            _mappingEngine.Map(viewModel, user);
            if (viewModel.Picture != null)
                user.Picture = viewModel.Picture;

            var emailModify = viewModel.Email != user.Email;

            if (emailModify)
            {
                user.EmailConfirmed = false;
            }

            //user.Picture = viewModel.Picture;
            //_unitOfWork.Update(user, a => a.AssociatedCollection(u => u.Roles));

            var XmlClientPermissions = "";
            if (viewModel.ClientPermissionNames == null || viewModel.ClientPermissionNames.Length < 1)
                XmlClientPermissions = _permissionClientService.GetPermissionsAsXml("null").ToString();
            else XmlClientPermissions = _permissionClientService.GetPermissionsAsXml(viewModel.ClientPermissionNames).ToString();

            user.UserRouter.ClientPermissions = XmlClientPermissions;

            var XmlRouterPermissions = "";
            if (viewModel.RouterPermissionNames == null || viewModel.RouterPermissionNames.Length < 1)
                XmlRouterPermissions = _permissionRouterService.GetPermissionsAsXml("null").ToString();
            else XmlRouterPermissions = _permissionRouterService.GetPermissionsAsXml(viewModel.RouterPermissionNames).ToString();

            user.UserRouter.RouterPermissions = XmlRouterPermissions;

            //if (passwordModify || emailModify)
            if (passwordModify)
                this.UpdateSecurityStamp(user.Id);
            //else
            //    await _unitOfWork.SaveAllChangesAsync();

            await _unitOfWork.SaveChangesAsync();
            return await Task.FromResult(true);
        }


        #region SetRolesToUser
        public void SetRolesToUser(User user, IEnumerable<Role> roles)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AddUser
        public async Task<User> AddUser(ViewModels.Identity.UserAdmin.AdminAddModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            viewModel.RoleIds.ToList().ForEach(roleId => user.Roles.Add(new UserRole { RoleId = roleId }));
            await CreateAsync(user, viewModel.Password);
            return user;
        }
        public async Task<User> AddReseller(ViewModels.Identity.UserReseller.ResellerAddModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            await CreateAsync(user, viewModel.Password);
            return user;
        }
        public async Task<long> AddReseller(ViewModels.Identity.UserReseller.RegisterViewModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            await CreateAsync(user, viewModel.Password);
            return user.Id;
        }
        public async Task<long> AddRouter(ViewModels.Identity.UserRouter.Register viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);


            var XmlClientPermissions = "";
            if (viewModel.ClientPermissionNames == null || viewModel.ClientPermissionNames.Length < 1)
                XmlClientPermissions = _permissionClientService.GetPermissionsAsXml("null").ToString();
            else XmlClientPermissions = _permissionClientService.GetPermissionsAsXml(viewModel.ClientPermissionNames).ToString();

            user.UserRouter.ClientPermissions = XmlClientPermissions;

            var XmlRouterPermissions = "";
            if (viewModel.RouterPermissionNames == null || viewModel.RouterPermissionNames.Length < 1)
                XmlRouterPermissions = _permissionRouterService.GetPermissionsAsXml("null").ToString();
            else XmlRouterPermissions = _permissionClientService.GetPermissionsAsXml(viewModel.RouterPermissionNames).ToString();

            user.UserRouter.RouterPermissions = XmlRouterPermissions;

            user.UserType = UserType.UserRouter;
            user.EmailConfirmed = false;
            await CreateAsync(user, viewModel.Password);
            return user.Id;


        }
        #endregion

        #region Validations

        public bool CheckUserNameExist(string userName, long? id)
        {
            return id == null
                ? _users.Any(a => a.UserName == userName.ToLower() && !a.IsDeleted)
                : _users.Any(a => a.UserName == userName.ToLower() && !a.IsDeleted && a.Id != id.Value);
        }


        public bool CheckResellerEmailExist(string email, long? id)
        {/*
            return id == null
               ? _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.UserType == UserType.UserReseller)
               : _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.Id != id.Value && a.UserType == UserType.UserReseller);
            */
            return id == null
               ? _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted)
               : _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.Id != id.Value);
        }

        public bool CheckAdminEmailExist(string email, long? id)
        {/*
            return id == null
               ? _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.UserType == UserType.UserAdmin)
               : _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.UserType == UserType.UserAdmin && a.Id != id.Value);
            */
            return id == null
               ? _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted)
               : _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.Id != id.Value);
        }
        public bool CheckRouterEmailExist(string email, long? id)
        {/*
            return id == null
               ? _users.Any(a => a.Email.ToLower() == email.ToLower() && a.UserType == UserType.UserRouter && !a.IsDeleted)
               : _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.UserType == UserType.UserRouter && a.Id != id.Value);
            */
            return id == null
               ? _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted)
               : _users.Any(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted && a.Id != id.Value);
        }
        public bool CheckResellerNationalCodeExist(string nCode, long? id)
        {
            return id == null
               ? _users.Any(a => a.UserReseller.NationalCode == nCode && !a.IsDeleted)
               : _users.Any(a => a.UserReseller.NationalCode == nCode && !a.IsDeleted && a.Id != id.Value);
        }

        public bool CheckRouterNationalCodeExist(string nCode, long? id, long? resellerid)
        {
            return id == null
               ? _users.Any(a => a.UserRouter.NationalCode == nCode && a.UserRouter.UserResellerId == resellerid && !a.IsDeleted)
               : _users.Any(a => a.UserRouter.NationalCode == nCode && a.UserRouter.UserResellerId == resellerid && !a.IsDeleted && a.Id != id.Value);
        }

        public bool SmsCodeIsValid(string RegisterWithSmsCode, long? id)
        {
            return id == null
               ? _users.Any(a => a.UserRouter.RegisterWithSmsCode == RegisterWithSmsCode && !a.IsDeleted)
               : _users.Any(a => a.UserRouter.RegisterWithSmsCode == RegisterWithSmsCode && !a.IsDeleted && a.Id != id.Value);
        }
        public bool CheckResellerRouterNameExist(string name, long? id)
        {
            return id == null
               ? _users.Any(a => a.UserReseller.ResellerCode == name.ToLower() && !a.IsDeleted)
               : _users.Any(a => a.UserReseller.ResellerCode == name.ToLower() && !a.IsDeleted && a.Id != id.Value);
        }
        public bool CheckRouterRouterNameExist(string name, long? id, long? resellerid)
        {
            //return id == null
            //   ? _users.Any(a => a.UserRouter.RouterCode == name.ToLower() && a.UserRouter.UserResellerId == resellerid && !a.IsDeleted)
            //   : _users.Any(a => a.UserRouter.RouterCode == name.ToLower() && a.UserRouter.UserResellerId == resellerid && !a.IsDeleted && a.Id != id.Value);
            return id == null
               ? _users.Any(a => a.UserRouter.RouterCode == name.ToLower() && !a.IsDeleted)
               : _users.Any(a => a.UserRouter.RouterCode == name.ToLower() && !a.IsDeleted && a.Id != id.Value);
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

        public bool CheckResellerPhoneNumberExist(string phoneNumber, long? id)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber && a.UserType == UserType.UserReseller && !a.IsDeleted)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.UserType == UserType.UserReseller && !a.IsDeleted && a.Id != id.Value);
        }
        public bool CheckAdminPhoneNumberExist(string phoneNumber, long? id)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber && a.UserType == UserType.UserAdmin && !a.IsDeleted)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.UserType == UserType.UserAdmin && !a.IsDeleted && a.Id != id.Value);
        }
        public bool CheckRouterPhoneNumberExist(string phoneNumber, long? id, long? resellerid)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber && a.UserType == UserType.UserRouter && a.UserRouter.UserResellerId == resellerid && !a.IsDeleted)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.UserType == UserType.UserRouter && a.UserRouter.UserResellerId == resellerid && !a.IsDeleted && a.Id != id.Value);
        }
        #endregion

        #region override GetRolesAsync
        public async override Task<IList<string>> GetRolesAsync(long userId)
        {

            var userPermissions = await _roleManager.FindUserPermissionsAsync(userId);
            ////todo: any permission form other sections
            return userPermissions;
        }

        public IList<string> GetRoles(long userId)
        {

            var userPermissions = _roleManager.FindUserPermissions(userId);
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
            //return _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.IsBanned && x.EmailConfirmed && x.UserType == UserType.UserReseller && x.UserReseller.ResellerCode == Code);
            return _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.UserType == UserType.UserReseller && x.UserReseller.ResellerCode == Code);
        }
        public async Task<User> FindByRouterCodeAsync(string Code)
        {
            //return _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.IsBanned && x.EmailConfirmed && x.UserType == UserType.UserReseller && x.UserReseller.ResellerCode == Code);
            return await _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.UserType == UserType.UserRouter && x.UserRouter.RouterCode == Code);
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

        public async Task<bool> ActiveUser(long id)
        {
            var key = id.ToString(CultureInfo.InvariantCulture) + "_roles";
            _contextBase.InvalidateCache(key);
            var result = await _users.Where(a => a.Id == id).UpdateAsync(a => new User { IsBanned = false });
            return result > 0;
        }
        public async Task<bool> BanneUser(long id)
        {
            var key = id.ToString(CultureInfo.InvariantCulture) + "_roles";
            _contextBase.InvalidateCache(key);
            var result = await _users.Where(a => a.Id == id).UpdateAsync(a => new User { IsBanned = true });
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

        public bool IsNationalCodeValid(string nationalCode)
        {
            if (nationalCode.Length == 10)
            {
                if (nationalCode == "1111111111" ||
                    nationalCode == "0000000000" ||
                    nationalCode == "2222222222" ||
                    nationalCode == "3333333333" ||
                    nationalCode == "4444444444" ||
                    nationalCode == "5555555555" ||
                    nationalCode == "6666666666" ||
                    nationalCode == "7777777777" ||
                    nationalCode == "8888888888" ||
                    nationalCode == "9999999999" ||
                    nationalCode == "0123456789"
                    )
                {
                    //Response.Write("كد ملي صحيح نمي باشد");          
                    return false;
                }

                int c = Convert.ToInt16(nationalCode.Substring(9, 1));

                int n = Convert.ToInt16(nationalCode.Substring(0, 1)) * 10 +
                     Convert.ToInt16(nationalCode.Substring(1, 1)) * 9 +
                     Convert.ToInt16(nationalCode.Substring(2, 1)) * 8 +
                     Convert.ToInt16(nationalCode.Substring(3, 1)) * 7 +
                     Convert.ToInt16(nationalCode.Substring(4, 1)) * 6 +
                     Convert.ToInt16(nationalCode.Substring(5, 1)) * 5 +
                     Convert.ToInt16(nationalCode.Substring(6, 1)) * 4 +
                     Convert.ToInt16(nationalCode.Substring(7, 1)) * 3 +
                     Convert.ToInt16(nationalCode.Substring(8, 1)) * 2;
                int r = n - (n / 11) * 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r))
                {
                    //Response.Write(" کد ملی صحیح است");                
                    return true;
                }
                else
                {
                    //Response.Write("كد ملي صحيح نمي باشد");         
                    return false;
                }
            }
            else
            {
                //Response.Write("طول کد ملی وارد شده باید 10 کاراکتر باشد");           
                return false;
            }
        }


        public IList<string> FindClientPermissions(long userId)
        {
            var user = _users.Where(x => x.Id == userId).Include(x => x.UserRouter).FirstOrDefault();
            if (user.UserRouter.ClientPermissions == null)
                return new[] { "" };
            return _permissionClientService.GetPermissionsAsList(XElement.Parse(user.UserRouter.ClientPermissions)).ToList();
        }


        public IList<string> FindRouterPermissions(long userId)
        {
            var user = _users.Where(x => x.Id == userId).Include(x => x.UserRouter).FirstOrDefault();
            if (user.UserRouter.RouterPermissions == null)
                return new[] { "" };

            return _permissionRouterService.GetPermissionsAsList(XElement.Parse(user.UserRouter.RouterPermissions)).ToList();
        }

        public List<User> GetUserRoutersWebsitesLogsActive()
        {
            var users = _users.Where(x => x.UserType == UserType.UserRouter && !x.IsDeleted && x.UserRouter.WebsitesLogs).ToList();//شرط فعال بودن گزینه لاگ گیری در دیتابیس
            return users;
        }
        public SmsModel GetUserRouterSmsSettings(long id)
        {
            return _users.Where(x => x.UserType == UserType.UserRouter && x.Id == id).Select(x => new SmsModel
            {
                Id = x.Id,
                RegisterFormSms = x.UserRouter.RegisterFormSms,
                RegisterWithSms = x.UserRouter.RegisterWithSms,
                RegisterWithSmsCode = x.UserRouter.RegisterWithSmsCode,
                SmsActive = x.UserRouter.SmsActive,
                SmsAdminChangeAdminPassword = x.UserRouter.SmsAdminChangeAdminPassword,
                SmsAdminChangeUserPassword = x.UserRouter.SmsAdminChangeUserPassword,
                SmsAdminLogins = x.UserRouter.SmsAdminLogins,
                SmsCharge = x.UserRouter.SmsCharge,
                SmsUserAfterChangePackage = x.UserRouter.SmsUserAfterChangePackage,
                SmsUserAfterCreateWithAdmin = x.UserRouter.SmsUserAfterCreateWithAdmin,
                SmsUserAfterDelete = x.UserRouter.SmsUserAfterDelete,
                SmsUserAfterResetCounter = x.UserRouter.SmsUserAfterResetCounter,
                SmsUserhangeUserPassword = x.UserRouter.SmsUserhangeUserPassword,
                RegisterWithSmsMessage = x.UserRouter.RegisterWithSmsMessage,
                RegisterWithSmsRouterProfile = x.UserRouter.RegisterWithSmsRouterProfile,
                SmsIfErrorInSms = x.UserRouter.SmsIfErrorInSms
            }).FirstOrDefault();
        }

        public async Task UpdateUserRouterSmsSettingsAsync(SmsModel model)
        {
            var user = _users.Find(model.Id);
            _mappingEngine.Map(model, user);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<User> FindByRouterSMSCodeAsync(string Code)
        {
            //return _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.IsBanned && x.EmailConfirmed && x.UserType == UserType.UserReseller && x.UserReseller.ResellerCode == Code);
            return await _users.FirstOrDefaultAsync(x => !x.IsDeleted && x.UserType == UserType.UserRouter && x.UserRouter.RegisterWithSmsCode == Code);
        }

        public long GetRoutersChargre()
        {
            return _users.Where(x => !x.IsDeleted && x.UserType == UserType.UserRouter).Sum(x => x.UserRouter.SmsCharge);
        }

    }
}
