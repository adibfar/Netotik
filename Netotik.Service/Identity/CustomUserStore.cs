using System.Data.Entity;
using System.Threading.Tasks;
using Netotik.Data.Context;
using Netotik.Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Netotik.Services;

namespace Netotik.Services.Identity
{
    public class CustomUserStore : UserStore<User, Role, long, UserLogin, UserRole, UserClaim>, ICustomUserStore
    {
        private readonly DbSet<User> _users;

        public CustomUserStore(NetotikDBContext dbContext)
            : base(dbContext)
        {
            _users = (DbSet<User>)dbContext.Set<User>();
        }

        public override Task<User> FindByIdAsync(long userId)
        {
            return _users.FindAsync(userId);
        }
    }
}
