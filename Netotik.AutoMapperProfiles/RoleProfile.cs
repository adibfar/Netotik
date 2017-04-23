using System.Web.Mvc;
using AutoMapper;
using System.Collections.ObjectModel;
using Netotik.Domain.Entity;
using Netotik.AutoMapperProfiles.Extentions;
using Netotik.ViewModels.Identity.Role;

namespace Netotik.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Role, RoleItem>().IgnoreAllNonExisting();

            CreateMap<RoleItem, Role>().IgnoreAllNonExisting();

            CreateMap<RoleModel, Role>()
                .ForMember(d => d.IsSystemRole, m => m.Ignore())
               .IgnoreAllNonExisting();

            CreateMap<Role, RoleModel>().IgnoreAllNonExisting();
            CreateMap<Role, SelectListItem>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Id));

            

        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }
    }
}
