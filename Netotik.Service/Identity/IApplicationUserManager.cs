using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Netotik.Domain.Entity;
using Netotik.Common.DataTables;
using Netotik.ViewModels.Identity.UserRouter;
using System.Web.Mvc;

namespace Netotik.Services.Identity
{

    public interface IApplicationUserManager : IDisposable
    {
        SelectList GetResellersSelectList(long? resellerId);

        long GetRoutersChargre();
        ViewModels.Identity.UserAdmin.ProfileModel GetUserAdminProfile();
        ViewModels.Identity.UserReseller.ProfileModel GetUserResellerProfile();
        ViewModels.Identity.UserRouter.ProfileModel GetUserRouterProfile(long id);

        ViewModels.Identity.UserRouter.MikrotikConfModel GetUserRouterMikrotikConf(long id);
        ViewModels.Identity.UserRouter.TelegramBotModel GetUserRouterTelegramBot(long id);

        ViewModels.Identity.UserRouter.RegisterSettingModel GetRouterRegisterSetting(long UserId);
        Task UpdateRouterRegisterSettingAsync(ViewModels.Identity.UserRouter.RegisterSettingModel model);
        Task UpdateUserAdminProfile(ViewModels.Identity.UserAdmin.ProfileModel model);
        Task UpdateUserResellerProfile(ViewModels.Identity.UserReseller.ProfileModel model);
        Task UpdateUserRouterProfile(ViewModels.Identity.UserRouter.ProfileModel model);
        Task UpdateUserClientPermissions(ViewModels.Identity.UserRouter.ProfileModel model);
        IList<ViewModels.Identity.UserAdmin.UserItem> GetListUserAdmins(RequestListModel model, out long TotalCount, out long ShowCount);
        IList<ViewModels.Identity.UserReseller.UserItem> GetListUserResellers(RequestListModel model, out long TotalCount, out long ShowCount);
        Task UpdateUserRouterMikrotikConf(ViewModels.Identity.UserRouter.MikrotikConfModel model);
        Task UpdateUserRouterTelegramBot(ViewModels.Identity.UserRouter.TelegramBotModel model);

        IList<ViewModels.Identity.UserRouter.RouterList> GetListUserRouter(long id);

        IList<ViewModels.Identity.UserRouter.RouterAdminList> GetListUserRouters(RequestListModel model, out long TotalCount, out long ShowCount);

        /// <summary>
        /// Used to hash/verify passwords
        /// </summary>
        IPasswordHasher PasswordHasher { get; set; }

        /// <summary>
        /// Used to validate users before changes are saved
        /// </summary>
        IIdentityValidator<User> UserValidator { get; set; }

        /// <summary>
        /// Used to validate passwords before persisting changes
        /// </summary>
        IIdentityValidator<string> PasswordValidator { get; set; }

        /// <summary>
        /// Used to create claims identities from users
        /// </summary>
        IClaimsIdentityFactory<User, long> ClaimsIdentityFactory { get; set; }

        /// <summary>
        /// Used to send email
        /// </summary>
        IIdentityMessageService EmailService { get; set; }

        /// <summary>
        /// Used to send a sms message
        /// </summary>
        IIdentityMessageService SmsService { get; set; }


        /// <summary>
        /// Used for generating reset password and confirmation tokens
        /// </summary>
        IUserTokenProvider<User, long> UserTokenProvider { get; set; }

        /// <summary>
        /// If true, will enable user lockout when users are created
        /// </summary>
        bool UserLockoutEnabledByDefault { get; set; }

        bool IsNationalCodeValid(string nationalCode);

        /// <summary>
        /// Number of access attempts allowed before a user is locked out (if lockout is enabled)
        /// </summary>
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        /// <summary>
        /// Default amount of time that a user is locked out for after MaxFailedAccessAttemptsBeforeLockout is reached
        /// </summary>
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }

        /// <summary>
        /// Returns true if the store is an IUserTwoFactorStore
        /// </summary>
        bool SupportsUserTwoFactor { get; }

        /// <summary>
        /// Returns true if the store is an IUserPasswordStore
        /// </summary>
        bool SupportsUserPassword { get; }

        /// <summary>
        /// Returns true if the store is an IUserSecurityStore
        /// </summary>
        bool SupportsUserSecurityStamp { get; }

        /// <summary>
        /// Returns true if the store is an IUserRoleStore
        /// </summary>
        bool SupportsUserRole { get; }

        /// <summary>
        /// Returns true if the store is an IUserLoginStore
        /// </summary>
        bool SupportsUserLogin { get; }

        /// <summary>
        /// Returns true if the store is an IUserEmailStore
        /// </summary>
        bool SupportsUserEmail { get; }

        /// <summary>
        /// Returns true if the store is an IUserPhoneNumberStore
        /// </summary>
        bool SupportsUserPhoneNumber { get; }

        /// <summary>
        /// Returns true if the store is an IUserClaimStore
        /// </summary>
        bool SupportsUserClaim { get; }

        /// <summary>
        /// Returns true if the store is an IUserLockoutStore
        /// </summary>
        bool SupportsUserLockout { get; }

        /// <summary>
        /// Returns true if the store is an IQueryableUserStore
        /// </summary>
        bool SupportsQueryableUsers { get; }

        /// <summary>
        /// Maps the registered two-factor authentication providers for users by their id
        /// </summary>
        IDictionary<string, IUserTokenProvider<User, long>> TwoFactorProviders { get; }

        /// <summary>
        /// Creates a ClaimsIdentity representing the user
        /// </summary>
        /// <param name="user"/><param name="authenticationType"/>
        /// <returns/>
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);

        /// <summary>
        /// Create a user with no password
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(User user);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync(User user);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="user"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync(User user);

        /// <summary>
        /// Find a user by id
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<User> FindByIdAsync(long userId);

        /// <summary>
        /// Find a user by Reseller Code
        /// </summary>
        /// <param name="Code"/>
        /// <returns/>
        Task<User> FindByResellerCodeAsync(string Code);
        Task<User> FindByRouterCodeAsync(string Code);
        Task<User> FindByRouterSMSCodeAsync(string Code);

        /// <summary>
        /// Find a user by user name
        /// </summary>
        /// <param name="userName"/>
        /// <returns/>
        Task<User> FindByNameAsync(string userName);

        /// <summary>
        /// Create a user with the given password
        /// </summary>
        /// <param name="user"/><param name="password"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(User user, string password);


        /// <summary>
        /// Return a user with the specified username and password or null if there is no match.
        /// </summary>
        /// <param name="userName"/><param name="password"/>
        /// <returns/>
        Task<User> FindAsync(string userName, string password);

        /// <summary>
        /// Returns true if the password is valid for the user
        /// </summary>
        /// <param name="user"/><param name="password"/>
        /// <returns/>
        Task<bool> CheckPasswordAsync(User user, string password);

        /// <summary>
        /// Returns true if the user has a password
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<bool> HasPasswordAsync(long userId);

        /// <summary>
        /// Add a user password only if one does not already exist
        /// </summary>
        /// <param name="userId"/><param name="password"/>
        /// <returns/>
        Task<IdentityResult> AddPasswordAsync(long userId, string password);

        /// <summary>
        /// Change a user password
        /// </summary>
        /// <param name="userId"/><param name="currentPassword"/><param name="newPassword"/>
        /// <returns/>
        Task<IdentityResult> ChangePasswordAsync(long userId, string currentPassword, string newPassword);

        /// <summary>
        /// Remove a user's password
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IdentityResult> RemovePasswordAsync(long userId);

        /// <summary>
        /// Returns the current security stamp for a user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<string> GetSecurityStampAsync(long userId);

        /// <summary>
        /// Generate a new security stamp for a user, used for SignOutEverywhere functionality
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IdentityResult> UpdateSecurityStampAsync(long userId);

        /// <summary>
        /// Generate a password reset token for the user using the UserTokenProvider
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<string> GeneratePasswordResetTokenAsync(long userId);

        /// <summary>
        /// Reset a user's password using a reset password token
        /// </summary>
        /// <param name="userId"/><param name="token"/><param name="newPassword"/>
        /// <returns/>
        Task<IdentityResult> ResetPasswordAsync(long userId, string token, string newPassword);

        /// <summary>
        /// Returns the user associated with this login
        /// </summary>
        /// <returns/>
        Task<User> FindAsync(UserLoginInfo login);

        /// <summary>
        /// Remove a user login
        /// </summary>
        /// <param name="userId"/><param name="login"/>
        /// <returns/>
        Task<IdentityResult> RemoveLoginAsync(long userId, UserLoginInfo login);

        /// <summary>
        /// Associate a login with a user
        /// </summary>
        /// <param name="userId"/><param name="login"/>
        /// <returns/>
        Task<IdentityResult> AddLoginAsync(long userId, UserLoginInfo login);

        /// <summary>
        /// Gets the logins for a user.
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IList<UserLoginInfo>> GetLoginsAsync(long userId);

        /// <summary>
        /// Add a user claim
        /// </summary>
        /// <param name="userId"/><param name="claim"/>
        /// <returns/>
        Task<IdentityResult> AddClaimAsync(long userId, Claim claim);

        /// <summary>
        /// Remove a user claim
        /// </summary>
        /// <param name="userId"/><param name="claim"/>
        /// <returns/>
        Task<IdentityResult> RemoveClaimAsync(long userId, Claim claim);

        /// <summary>
        /// Get a users's claims
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IList<Claim>> GetClaimsAsync(long userId);

        /// <summary>
        /// Add a user to a role
        /// </summary>
        /// <param name="userId"/><param name="role"/>
        /// <returns/>
        Task<IdentityResult> AddToRoleAsync(long userId, string role);

        /// <summary>
        /// Method to add user to multiple roles
        /// </summary>
        /// <param name="userId">user id</param><param name="roles">list of role names</param>
        /// <returns/>
        Task<IdentityResult> AddToRolesAsync(long userId, params string[] roles);
        SmsModel GetUserRouterSmsSettings(long id);
        Task UpdateUserRouterSmsSettingsAsync(SmsModel model);

        /// <summary>
        /// Remove user from multiple roles
        /// </summary>
        /// <param name="userId">user id</param><param name="roles">list of role names</param>
        /// <returns/>
        Task<IdentityResult> RemoveFromRolesAsync(long userId, params string[] roles);

        /// <summary>
        /// Remove a user from a role.
        /// </summary>
        /// <param name="userId"/><param name="role"/>
        /// <returns/>
        Task<IdentityResult> RemoveFromRoleAsync(long userId, string role);

        /// <summary>
        /// Returns the roles for the user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IList<string>> GetRolesAsync(long userId);

        IList<string> GetRoles(long userId);

        Task<ViewModels.Identity.UserAdmin.AdminEditModel> GetUserAdminByIdAsync(long id);
        Task<ViewModels.Identity.UserReseller.ResellerEditModel> GetUserResellerByIdAsync(long id);
        Task<ViewModels.Identity.UserRouter.RouterEditModel> GetUserRouterByIdAsync(long id);
        Task<ViewModels.Identity.UserRouter.RouterAdminEditModel> GetAdminUserRouterByIdAsync(long id);

        /// <summary>
        /// Returns true if the user is in the specified role
        /// </summary>
        /// <param name="userId"/><param name="role"/>
        /// <returns/>
        Task<bool> IsInRoleAsync(long userId, string role);

        /// <summary>
        /// Get a user's email
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<string> GetEmailAsync(long userId);

        /// <summary>
        /// Set a user's email
        /// </summary>
        /// <param name="userId"/><param name="email"/>
        /// <returns/>
        Task<IdentityResult> SetEmailAsync(long userId, string email);

        /// <summary>
        /// Find a user by his email
        /// </summary>
        /// <param name="email"/>
        /// <returns/>
        Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// Get the email confirmation token for the user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<string> GenerateEmailConfirmationTokenAsync(long userId);

        /// <summary>
        /// Confirm the user's email with confirmation token
        /// </summary>
        /// <param name="userId"/><param name="token"/>
        /// <returns/>
        Task<IdentityResult> ConfirmEmailAsync(long userId, string token);

        /// <summary>
        /// Returns true if the user's email has been confirmed
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<bool> IsEmailConfirmedAsync(long userId);

        /// <summary>
        /// Get a user's phoneNumber
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<string> GetPhoneNumberAsync(long userId);

        /// <summary>
        /// Set a user's phoneNumber
        /// </summary>
        /// <param name="userId"/><param name="phoneNumber"/>
        /// <returns/>
        Task<IdentityResult> SetPhoneNumberAsync(long userId, string phoneNumber);

        /// <summary>
        /// Set a user's phoneNumber with the verification token
        /// </summary>
        /// <param name="userId"/><param name="phoneNumber"/><param name="token"/>
        /// <returns/>
        Task<IdentityResult> ChangePhoneNumberAsync(long userId, string phoneNumber, string token);

        /// <summary>
        /// Returns true if the user's phone number has been confirmed
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<bool> IsPhoneNumberConfirmedAsync(long userId);

        /// <summary>
        /// Generate a code that the user can use to change their phone number to a specific number
        /// </summary>
        /// <param name="userId"/><param name="phoneNumber"/>
        /// <returns/>
        Task<string> GenerateChangePhoneNumberTokenAsync(long userId, string phoneNumber);

        /// <summary>
        /// Verify the code is valid for a specific user and for a specific phone number
        /// </summary>
        /// <param name="userId"/><param name="token"/><param name="phoneNumber"/>
        /// <returns/>
        Task<bool> VerifyChangePhoneNumberTokenAsync(long userId, string token, string phoneNumber);

        /// <summary>
        /// Verify a user token with the specified purpose
        /// </summary>
        /// <param name="userId"/><param name="purpose"/><param name="token"/>
        /// <returns/>
        Task<bool> VerifyUserTokenAsync(long userId, string purpose, string token);

        /// <summary>
        /// Get a user token for a specific purpose
        /// </summary>
        /// <param name="purpose"/><param name="userId"/>
        /// <returns/>
        Task<string> GenerateUserTokenAsync(string purpose, long userId);

        /// <summary>
        /// Register a two factor authentication provider with the TwoFactorProviders mapping
        /// </summary>
        /// <param name="twoFactorProvider"/><param name="provider"/>
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<User, long> provider);

        /// <summary>
        /// Returns a list of valid two factor providers for a user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IList<string>> GetValidTwoFactorProvidersAsync(long userId);

        /// <summary>
        /// Verify a two factor token with the specified provider
        /// </summary>
        /// <param name="userId"/><param name="twoFactorProvider"/><param name="token"/>
        /// <returns/>
        Task<bool> VerifyTwoFactorTokenAsync(long userId, string twoFactorProvider, string token);

        /// <summary>
        /// Get a token for a specific two factor provider
        /// </summary>
        /// <param name="userId"/><param name="twoFactorProvider"/>
        /// <returns/>
        Task<string> GenerateTwoFactorTokenAsync(long userId, string twoFactorProvider);

        /// <summary>
        /// Notify a user with a token using a specific two-factor authentication provider's Notify method
        /// </summary>
        /// <param name="userId"/><param name="twoFactorProvider"/><param name="token"/>
        /// <returns/>
        Task<IdentityResult> NotifyTwoFactorTokenAsync(long userId, string twoFactorProvider, string token);

        /// <summary>
        /// Get whether two factor authentication is enabled for a user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<bool> GetTwoFactorEnabledAsync(long userId);

        /// <summary>
        /// Set whether a user has two factor authentication enabled
        /// </summary>
        /// <param name="userId"/><param name="enabled"/>
        /// <returns/>
        Task<IdentityResult> SetTwoFactorEnabledAsync(long userId, bool enabled);

        /// <summary>
        /// Send an email to the user
        /// </summary>
        /// <param name="userId"/><param name="subject"/><param name="body"/>
        /// <returns/>
        Task SendEmailAsync(long userId, string subject, string body);

        /// <summary>
        /// Send a user a sms message
        /// </summary>
        /// <param name="userId"/><param name="message"/>
        /// <returns/>
        Task SendSmsAsync(long userId, string message);

        /// <summary>
        /// Returns true if the user is locked out
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<bool> IsLockedOutAsync(long userId);

        /// <summary>
        /// Sets whether lockout is enabled for this user
        /// </summary>
        /// <param name="userId"/><param name="enabled"/>
        /// <returns/>
        Task<IdentityResult> SetLockoutEnabledAsync(long userId, bool enabled);

        /// <summary>
        /// Returns whether lockout is enabled for the user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<bool> GetLockoutEnabledAsync(long userId);

        /// <summary>
        /// Returns when the user is no longer locked out, dates in the past are considered as not being locked out
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<DateTimeOffset> GetLockoutEndDateAsync(long userId);

        /// <summary>
        /// Sets the when a user lockout ends
        /// </summary>
        /// <param name="userId"/><param name="lockoutEnd"/>
        /// <returns/>
        Task<IdentityResult> SetLockoutEndDateAsync(long userId, DateTimeOffset lockoutEnd);

        /// <summary>
        /// Increments the access failed count for the user and if the failed access account is greater than or equal
        ///             to the MaxFailedAccessAttempsBeforeLockout, the user will be locked out for the next DefaultAccountLockoutTimeSpan
        ///             and the AccessFailedCount will be reset to 0. This is used for locking out the user account.
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IdentityResult> AccessFailedAsync(long userId);

        /// <summary>
        /// Resets the access failed count for the user to 0
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<IdentityResult> ResetAccessFailedCountAsync(long userId);

        /// <summary>
        /// Returns the number of failed access attempts for the user
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        Task<int> GetAccessFailedCountAsync(long userId);
        // Our new custom methods

        Func<CookieValidateIdentityContext, Task> OnValidateIdentity();
        Task<ClaimsIdentity> GenerateUserIdentityAsync(User applicationUser);
        Task<bool> HasPassword(long userId);
        Task<bool> HasPhoneNumber(long userId);
        Task RemoveAll();
        Task<List<User>> GetAllUsersAsync();
        List<User> GetUserRoutersWebsitesLogsActive();
        
        bool Any();
        IList<User> GetUsersWithRoleIds(params long[] ids);
        void AddRange(IEnumerable<User> users);
        bool AutoCommitEnabled { get; set; }
        void SeedDatabase();
        Task<bool> EditUser(ViewModels.Identity.UserAdmin.AdminEditModel viewModel);
        Task<bool> EditReseller(ViewModels.Identity.UserReseller.ResellerEditModel viewModel);
        Task<bool> EditRouter(ViewModels.Identity.UserRouter.RouterEditModel viewModel);
        Task<bool> EditRouter(ViewModels.Identity.UserRouter.RouterAdminEditModel viewModel);

        void SetRolesToUser(User user, IEnumerable<Role> roles);

        Task<bool> LogicalRemove(long id);
        Task<bool> ActiveUser(long id);
        Task<bool> BanneUser(long id);
        bool CheckUserNameExist(string userName, long? id);
        bool CheckAdminEmailExist(string email, long? id);
        bool CheckResellerEmailExist(string email, long? id); 
        bool CheckRouterEmailExist(string email, long? id);
        bool CheckResellerNationalCodeExist(string nCode, long? id);
        bool CheckRouterNationalCodeExist(string nCode, long? id);
        bool CheckResellerRouterNameExist(string name, long? id);
        bool SmsCodeIsValid(string RegisterWithSmsCode,long? id);
        bool CheckRouterRouterCodeExist(string name, long? id);
        bool CheckGooglePlusIdExist(string googlePlusId, long? id);
        bool CheckFacebookIdExist(string faceBookId, long? id);
        bool CheckResellerPhoneNumberExist(string phoneNumber, long? id);
        bool CheckAdminPhoneNumberExist(string phoneNumber, long? id);
        bool CheckRouterPhoneNumberExist(string phoneNumber, long? id);
        Task<string> CustomValidatePasswordAsync(string pass);
        bool CheckIsUserBanned(long id);
        bool CheckIsUserBanned(string userName);
        Task<User> AddUser(ViewModels.Identity.UserAdmin.AdminAddModel viewModel);
        Task<User> AddReseller(ViewModels.Identity.UserReseller.ResellerAddModel viewModel);
        Task<long> AddReseller(ViewModels.Identity.UserReseller.RegisterViewModel viewModel);
        Task<long> AddRouter(Netotik.ViewModels.Identity.UserRouter.Register viewModel);
        IUserEmailStore<User, long> GetEmailStore();

        bool IsEmailConfirmedByUserNameAsync(string userName);

        void EditSecurityStamp(long userId);
        bool IsEmailAvailableForConfirm(string emial);
        bool CheckIsUserBannedByEmail(string email);
        bool CheckIsUserBannedOrDelete(long id);
        bool CheckIsUserBannedOrDelete(string userName);
        bool CheckIsUserBannedOrDeleteByEmail(string email);
        User FindUserById(long id);
        User GetCurrentUser();
        Task<User> GetCurrentUserAsync();
        long GetCurrentUserId();

        IList<string> FindClientPermissions(long userId);
        IList<string> FindRouterPermissions(long userId);

        IList<User> GetbyIds(long[] ids);
        
    }

}
