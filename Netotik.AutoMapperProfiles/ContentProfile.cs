using System.Web.Mvc;
using AutoMapper;
using System.Collections.ObjectModel;
using Netotik.Domain.Entity;
using Netotik.AutoMapperProfiles.Extentions;
using Netotik.ViewModels.CMS.Content;

namespace Netotik.AutoMapperProfiles
{
    public class ContentProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Content, ContentModel>().IgnoreAllNonExisting();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }
    }
}
