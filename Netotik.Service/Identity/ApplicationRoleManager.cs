using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFSecondLevelCache;
using EntityFramework.Extensions;
using Microsoft.AspNet.Identity;
using Netotik.Domain.Entity;
using Netotik.Data;
using Netotik.Common.Security.RijndaelEncryption;
using Netotik.Common.Extensions;
using Netotik.ViewModels.Identity.Role;
using Netotik.Common.DataTables;
using System;

namespace Netotik.Services.Identity
{
    public class RoleManager : RoleManager<Role, long>, IApplicationRoleManager
    {
        #region Fields
        private readonly IMappingEngine _mappingEngine;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionService _permissionService;
        private readonly IDbSet<Role> _roles;
        private readonly IDbSet<User> _users;
        #endregion

        #region Constructor
        public RoleManager(IMappingEngine mappingEngine, IPermissionService permissionService, IUnitOfWork unitOfWork, IRoleStore<Role, long> roleStore)
            : base(roleStore)
        {
            _unitOfWork = unitOfWork;
            _roles = _unitOfWork.Set<Role>();
            _users = _unitOfWork.Set<User>();
            _permissionService = permissionService;
            _mappingEngine = mappingEngine;
            AutoCommitEnabled = true;
        }
        #endregion

        #region FindRoleByName
        public Role FindRoleByName(string roleName)
        {
            return this.FindByName(roleName); // RoleManagerExtensions
        }
        #endregion

        #region CreateRole
        public IdentityResult CreateRole(Role role)
        {
            return this.Create(role); // RoleManagerExtensions
        }
        #endregion

        #region GetUsersOfRole
        public IList<UserRole> GetUsersOfRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
        }
        #endregion

        #region GetApplicationUsersInRole
        public IList<User> GetApplicationUsersInRole(string roleName)
        {
            //var roleUserIdsQuery = from role in Roles
            //                       where role.Name == roleName
            //                       from user in role.Users
            //                       select user.UserId;

            return null; //_userManager.GetUsersWithRoleIds(roleUserIdsQuery.ToArray());
        }
        #endregion

        #region FindUserRoles
        public IList<string> FindUserRoles(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).Select(a => a.Name).ToList();
        }

        public IEnumerable<long> FindUserRoleIds(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.Select(a => a.Id).ToList();
        }

        public async Task<IList<long>> FindUserRoleIdsAsync(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return await userRolesQuery.Select(a => a.Id).ToListAsync();
        }


        public async Task<IList<string>> FindUserPermissionsAsync(long userId)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user.UserType == UserType.UserCompany) return new List<string> { "Company" };
            else if (user.UserType == UserType.UserReseller) return new List<string> { "Reseller" };
            else if (user.UserType == UserType.Client) return new List<string> { "Client" };

            var userRolesQuery = from role in Roles
                                 from users in role.Users
                                 where users.UserId == userId
                                 select role;

            IEncryptSettingsProvider settings = new EncryptSettingsProvider();

            var roles = await userRolesQuery.AsNoTracking().ToListAsync();

            IList<string> roleList = new List<string>();
            foreach (var item in roles)
            {
                var encrypter = new RijndaelStringEncrypter(settings, item.SecurityStamp);
                roleList.Add(encrypter.Decrypt(item.Permissions));
            }

            return _permissionService.GetUserPermissionsAsList(roleList.Select(XElement.Parse).ToList());
        }

        public IList<string> FindUserPermissions(long userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            IEncryptSettingsProvider settings = new EncryptSettingsProvider();

            var roles = userRolesQuery.AsNoTracking().ToList();

            IList<string> roleList = new List<string>();
            foreach (var item in roles)
            {
                var encrypter = new RijndaelStringEncrypter(settings, item.SecurityStamp);
                roleList.Add(encrypter.Decrypt(item.Permissions));
            }

            return _permissionService.GetUserPermissionsAsList(roleList.Select(XElement.Parse).ToList());
        }
        #endregion

        #region GetRolesForUser
        public string[] GetRolesForUser(long userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new string[] { };
            }

            return roles.ToArray();
        }

        #endregion

        #region IsUserInRole
        public bool IsUserInRole(long userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        #endregion

        #region GetAllRolesAsync
        public async Task<IList<Role>> GetAllRolesAsync()
        {
            return await Roles.ToListAsync();
        }

        #endregion

        #region SeedDatabase
        /// <summary>
        /// for instal permissions with roles
        /// </summary>
        public void SeedDatabase()
        {
            var standardRoles = StandardRoles.SystemRolesWithPermissions;


            IEncryptSettingsProvider settings = new EncryptSettingsProvider();



            foreach (var role in standardRoles.Select(record => this.FindByName(record.RoleName) ?? new Role
            {
                Name = record.RoleName,
                IsSystemRole = true,
                Permissions = record.Permissions != null ?
                    _permissionService.GetPermissionsAsXml(record.Permissions.Select(a => a.Name).ToArray()).ToString() : _permissionService.GetPermissionsAsXml(new string[] { "null" }).ToString()
            }).Where(role => role.Id == 0))
            {
                var securityTamp = System.Guid.NewGuid().ToString();
                var encrypter = new RijndaelStringEncrypter(settings, securityTamp);
                role.Permissions = encrypter.Encrypt(role.Permissions);
                role.SecurityStamp = securityTamp;
                _roles.Add(role);
            }

            _unitOfWork.SaveChanges();
        }

        #endregion

        #region DeleteAll
        public async Task RemoteAll()
        {
            await Roles.DeleteAsync();
        }
        #endregion

        #region GetList
        public IList<RoleItem> GetList(RequestListModel model, out long TotalCount, out long ShowCount)
        {
            IQueryable<Role> all = _roles.AsQueryable();
            TotalCount = all.LongCount();

            // Apply Filtering
            if (!string.IsNullOrEmpty(model.sSearch))
            {
                all = all.Where(x => x.Name.Contains(model.sSearch)).AsQueryable();
            }


            // Apply Sorting
            Func<Role, string> orderingFunction = (x => x.Name);
            // asc or desc
            all = model.sSortDir_0 == "asc" ? all.OrderBy(orderingFunction).AsQueryable() : all.OrderByDescending(orderingFunction).AsQueryable();

            ShowCount = all.Count();
            return all.AsEnumerable().Skip(model.iDisplayStart).Take(model.iDisplayLength).ToList()
                .Select((x, index) => new RoleItem
                {
                    Name = x.Name,
                    Description = x.Description,
                    IsDefaultForRegister = x.IsDefaultForRegister,
                    IsSystemRole = x.IsSystemRole,
                    Id = x.Id,
                    RowNumber = model.iDisplayStart + index + 1
                })
                .ToList();

        }

        #endregion

        #region AddRole
        public Task<bool> AddRole(RoleModel viewModel)
        {

            var XmlPermissions = "";

            var role = _mappingEngine.Map<Role>(viewModel);
            if (viewModel.PermissionNames == null || viewModel.PermissionNames.Length < 1)
                XmlPermissions = _permissionService.GetPermissionsAsXml("null").ToString();
            else XmlPermissions = _permissionService.GetPermissionsAsXml(viewModel.PermissionNames).ToString();

            var securityTamp = System.Guid.NewGuid().ToString();
            IEncryptSettingsProvider settings = new EncryptSettingsProvider();
            var encrypter = new RijndaelStringEncrypter(settings, securityTamp);


            role.Permissions = encrypter.Encrypt(XmlPermissions);
            role.SecurityStamp = securityTamp;
            _roles.Add(role);
            encrypter.Dispose();

            return Task.FromResult(true);
        }
        #endregion

        #region GetRoleByPermissions
        public async Task<RoleModel> GetRoleByIdAsync(long id)
        {
            var role = await _roles.FirstOrDefaultAsync(r => r.Id == id);
            var viewModel = _mappingEngine.Map<RoleModel>(role);


            var securityTamp = role.SecurityStamp;
            IEncryptSettingsProvider settings = new EncryptSettingsProvider();
            var encrypter = new RijndaelStringEncrypter(settings, securityTamp);


            viewModel.PermissionNames = _permissionService.GetUserPermissionsAsList(XElement.Parse(encrypter.Decrypt(role.Permissions))).ToArray();

            return viewModel;
        }

        #endregion

        #region EditRole

        public async Task<bool> EditRole(RoleModel viewModel)
        {
            var role = await _roles.FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            var securityTamp = role.SecurityStamp;
            IEncryptSettingsProvider settings = new EncryptSettingsProvider();
            var encrypter = new RijndaelStringEncrypter(settings, securityTamp);


            _mappingEngine.Map(viewModel, role);

            if (viewModel.PermissionNames == null || viewModel.PermissionNames.Length < 1)
                role.Permissions = _permissionService.GetPermissionsAsXml("null").ToString(); 
            else role.Permissions = encrypter.Encrypt(_permissionService.GetPermissionsAsXml(viewModel.PermissionNames).ToString());

            return true;

        }
        #endregion

        #region AddRange
        public void AddRange(IEnumerable<Role> roles)
        {
            _unitOfWork.AddThisRange(roles);
        }
        #endregion

        #region AnyAsync
        public Task<bool> AnyAsync()
        {
            return _roles.AnyAsync();
        }
        public bool Any()
        {
            return _roles.Any();
        }
        #endregion

        #region AutoCommitEnabled
        public bool AutoCommitEnabled { get; set; }
        #endregion

        #region ChechForExisByName
        public bool ChechForExisByName(string name, long? id)
        {
            var roles = _roles.ToList();
            return id == null ? roles.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName())
                : roles.Any(a => a.Name.GetFriendlyPersianName() == name.GetFriendlyPersianName() && id.Value != a.Id);
        }
        #endregion

        #region GetPageList
        public IEnumerable<RoleItem> GetPageList(out int total, string term, int page, int count = 10)
        {
            var roles = _roles.AsNoTracking().OrderBy(a => a.Id).AsQueryable();
            if (!string.IsNullOrEmpty(term))
                roles = roles.Where(a => a.Name.Contains(term));
            total = roles.FutureCount();
            var result =
                roles.Skip((page - 1) * count).Take(count).Project(_mappingEngine).To<RoleItem>().Future().ToList();

            return result;
        }
        #endregion

        #region RemoveById
        public async Task RemoveById(long id)
        {
            await _roles.Where(a => a.Id == id).DeleteAsync();
        }

        #endregion

        #region CheckRoleIsSystemRoleAsync
        public async Task<bool> CheckRoleIsSystemRoleAsync(long id)
        {
            return await _roles.AnyAsync(a => a.Id == id && a.IsSystemRole);
        }
        #endregion

        #region SetRoleAsRegistrationDefaultRoleAsync
        public async Task SetRoleAsRegistrationDefaultRoleAsync(long id)
        {
            _unitOfWork.EnableFiltering("IsDefaultRegisteRole");
            var role = await _roles.FirstOrDefaultAsync();
            _unitOfWork.DisableFiltering("IsDefaultRegisteRole");
            await _roles.Where(a => a.Id == id).UpdateAsync(a => new Role { IsDefaultForRegister = true });
        }

        #endregion

        #region GetAllAsSelectList
        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListAsync()
        {
            var roles = await _roles.AsNoTracking().Project(_mappingEngine).To<SelectListItem>().Cacheable().ToListAsync();
            return roles;
        }
        #endregion

        #region GetDefaultRoleForRegister
        public Task<string> GetDefaultRoleForRegister()
        {
            return _roles.Where(a => a.IsDefaultForRegister).Select(a => a.Name).FirstOrDefaultAsync();
        }
        #endregion
    }
}
