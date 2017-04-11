using DnmaGroups.Domain.Entity;
using DnmaGroups.Model.Identity.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnmaGroups.Service.Abstract
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<bool> ExistsByNameAsync(string name, int? id);
        Task<bool> ExistsBySystemNameAsync(string systemName, int? id);
        Task<IList<Role>> GetRolesbyIdsAsync(int[] ids);
        IList<Role> GetRolesbyIds(int[] ids);
        Task DisableAllDefaultRoleRegister();

        Role GetDefaultRole();
        Task<Role> GetRoleByRoleNameAsync(string roleName);
        IQueryable<RoleViewModel> GetDataTableRole(string search);

        Task Remove(int id);
    }
}
