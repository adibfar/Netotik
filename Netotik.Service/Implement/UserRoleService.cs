using DnmaGroups.Common.Security;
using DnmaGroups.DataAccess;
using DnmaGroups.Domain.Entity;
using DnmaGroups.Service.Abstract;
using DnmaGroups.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DnmaGroups.Model.Identity.Role;

namespace DnmaGroups.Service.Implement
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(IUnitOfWork unit)
            : base(unit)
        {

        }
        public async Task<bool> ExistsByNameAsync(string name, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(name) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(name));
        }

        public async Task<bool> ExistsBySystemNameAsync(string systemName, int? id)
        {
            if (id.HasValue)
                return await dbSet.AnyAsync(x => x.Name.Equals(systemName) && x.Id != id.Value);
            return await dbSet.AnyAsync(x => x.Name.Equals(systemName));
        }

        public async Task<IList<Role>> GetRolesbyIdsAsync(int[] ids)
        {
            var roles = await dbSet.ToListAsync();
            IList<Role> list = new List<Role>();
            foreach (var item in ids)
            {
                var role = roles.FirstOrDefault(x => x.Id == item);
                if (role != null)
                    list.Add(role);
            }
            return list;
        }

        public IList<Role> GetRolesbyIds(int[] ids)
        {
            var roles = dbSet.ToList();
            IList<Role> list = new List<Role>();
            foreach (var item in ids)
            {
                var role = roles.FirstOrDefault(x => x.Id == item);
                if (role != null)
                    list.Add(role);
            }
            return list;
        }


        public async Task DisableAllDefaultRoleRegister()
        {
            var roles = await dbSet.Where(x => x.IsDefaultForRegister).ToListAsync();
            roles.ForEach(x => x.IsDefaultForRegister = false);

        }

        public async Task<Role> GetRoleByRoleNameAsync(string roleName)
        {
            return await dbSet.SingleAsync(x => x.Name.Equals(roleName));
        }


        public IQueryable<RoleViewModel> GetDataTableRole(string search)
        {
            IQueryable<RoleViewModel> selectedUsers = dbSet
                                            .OrderByDescending(x => x.Name)
                                            .Select(x => new RoleViewModel
                                            {
                                                Id = x.Id,
                                                IsSystemRole = x.IsSystemRole,
                                                //Active = x.Active,
                                                //FreeShipping = x.FreeShipping,
                                                //TaxExemt = x.TaxExemt,
                                                //IsDefaultRoleRegisteredUser = x.IsDefaultRoleRegisteredUser,
                                                //Name = x.Name,
                                                //SystemName = x.SystemName
                                            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                selectedUsers = selectedUsers.Where(x => x.Name.Contains(search) || x.Name.Contains(search)).AsQueryable();

            return selectedUsers;
        }
        public async Task Remove(int id)
        {
            var _Role = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (_Role != null)
                Remove(_Role);
        }


        public Role GetDefaultRole()
        {
            return dbSet.FirstOrDefault(x => x.IsDefaultForRegister);
        }
    }
}
