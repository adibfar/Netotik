using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using Netotik.Domain.Entity;
using Netotik.ViewModels.Identity.Role;
using Netotik.Common.DataTables;

namespace Netotik.Services.Identity
{
    public interface IApplicationRoleManager : IDisposable
    {
        /// <summary>
        /// Used to validate roles before persisting changes
        /// </summary>
        IIdentityValidator<Role> RoleValidator { get; set; }



        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> CreateAsync(Role role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> UpdateAsync(Role role);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="role"/>
        /// <returns/>
        Task<IdentityResult> DeleteAsync(Role role);

        /// <summary>
        /// Returns true if the role exists
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<bool> RoleExistsAsync(string roleName);

        /// <summary>
        /// Find a role by id
        /// </summary>
        /// <param name="roleId"/>
        /// <returns/>
        Task<Role> FindByIdAsync(long roleId);

        /// <summary>
        /// Find a role by name
        /// </summary>
        /// <param name="roleName"/>
        /// <returns/>
        Task<Role> FindByNameAsync(string roleName);
        // Our new custom methods

        Role FindRoleByName(string roleName);
        IdentityResult CreateRole(Role role);
        IList<UserRole> GetUsersOfRole(string roleName);
        IList<User> GetApplicationUsersInRole(string roleName);
        IList<string> FindUserRoles(long userId);
        string[] GetRolesForUser(long userId);
        bool IsUserInRole(long userId, string roleName);
        Task<IList<Role>> GetAllRolesAsync();
        void SeedDatabase();
        Task RemoteAll();
        IList<RoleItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount);
        Task<bool> AddRole(RoleModel viewModel);
        Task<bool> EditRole(RoleModel viewModel);
        Task<RoleModel> GetRoleByIdAsync(long id);
        void AddRange(IEnumerable<Role> roles);
        Task<bool> AnyAsync();
        bool AutoCommitEnabled { get; set; }
        bool Any();
        bool ChechForExisByName(string name, long? id);
        Task RemoveById(long id);
        Task<bool> CheckRoleIsSystemRoleAsync(long id);
        Task SetRoleAsRegistrationDefaultRoleAsync(long id);
        IEnumerable<RoleItem> GetPageList(out int total, string term, int page, int count = 10);
        Task<IEnumerable<SelectListItem>> GetAllAsSelectListAsync();
        IEnumerable<long> FindUserRoleIds(long userId);
        Task<IList<string>> FindUserPermissionsAsync(long userId);
        IList<string> FindUserPermissions(long userId);
        Task<string> GetDefaultRoleForRegister();
        Task<IList<long>> FindUserRoleIdsAsync(long userId);

    }
}
