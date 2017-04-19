using System.Web.Mvc;
using AutoMapper;
using System.Collections.ObjectModel;
using Netotik.Domain.Entity;
using Netotik.AutoMapperProfiles.Extentions;
using Netotik.ViewModels.Identity.Role;

namespace Netotik.AutoMapperProfiles
{
    public class ContentTagProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<ContentTag, SelectListItem>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Name))
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Id));

            

        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }
    }
}
