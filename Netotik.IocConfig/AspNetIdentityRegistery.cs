using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Netotik.Data.Context;
using Netotik.Domain.Entity;
using Netotik.Services.Identity;
using StructureMap.Configuration.DSL;
using StructureMap.Web;
using Netotik.Data;

namespace Netotik.IocConfig
{
    public class AspNetIdentityRegistery : Registry
    {
        public AspNetIdentityRegistery()
        {
            For<NetotikDBContext>().HybridHttpOrThreadLocalScoped()
                      .Use(context => (NetotikDBContext)context.GetInstance<IUnitOfWork>());
            For<DbContext>().HybridHttpOrThreadLocalScoped()
                 .Use(context => (NetotikDBContext)context.GetInstance<IUnitOfWork>());

            For<IUserStore<User, long>>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<CustomUserStore>();

            For<IRoleStore<Role, long>>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<RoleStore<Role, long, UserRole>>();

            For<IAuthenticationManager>()
                 .Use(() => HttpContext.Current.GetOwinContext().Authentication);

            For<IApplicationSignInManager>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<ApplicationSignInManager>();

            For<IApplicationRoleManager>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<RoleManager>();

            For<IApplicationUserManager>().HybridHttpOrThreadLocalScoped()
               .Use<ApplicationUserManager>();

            For<ApplicationUserManager>().HybridHttpOrThreadLocalScoped()
                 .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

            For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

            For<ICustomUserStore>()
                  .HybridHttpOrThreadLocalScoped()
                  .Use<CustomUserStore>();

            Policies.SetAllProperties(y =>
            {
                y.OfType<IApplicationUserManager>();
                y.OfType<IAuthenticationManager>();
            });
        }
    }

}
