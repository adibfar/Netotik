using System;
using System.Collections.ObjectModel;
using AutoMapper;
using Netotik.AutoMapperProfiles.Extentions;

namespace Netotik.AutoMapperProfiles
{
    public class Configuration : Profile
    {
        protected override void Configure()
        {
            CreateMap<DateTime, string>().ConvertUsing(new ToPersianDateTimeConverter());
        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

}
