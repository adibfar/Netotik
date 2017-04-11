using Netotik.Domain.Entity;
using Microsoft.AspNet.Identity;
using Netotik.Services.Identity;

namespace Netotik.Services.Identity
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<Role, long> _roleStore;

        public CustomRoleStore(IRoleStore<Role, long> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}
