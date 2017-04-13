using System;
using System.Collections.ObjectModel;
using AutoMapper;
using Netotik.Domain.Entity;
using Netotik.AutoMapperProfiles.Extentions;

namespace Netotik.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {

            //CreateMap<User, UserViewModel>()
            //    .ForMember(d => d.Roles, m => m.Ignore()).IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.RegisterViewModel, UserReseller>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.RegisterViewModel, User>()
                .ForMember(d => d.CreateDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EmailConfirmed, m => m.UseValue(false))
                .ForMember(d => d.IsDeleted, m => m.UseValue(false))
                .ForMember(d => d.IsBanned, m => m.UseValue(false))
                .ForMember(d => d.PhoneNumberConfirmed, m => m.UseValue(false))
                .ForMember(d => d.TwoFactorEnabled, m => m.UseValue(false))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.UserReseller, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();


            CreateMap<ViewModels.Identity.UserAdmin.AdminAddModel, User>()
                .ForMember(d => d.CreateDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EmailConfirmed, m => m.UseValue(true))
                .ForMember(d => d.IsDeleted, m => m.UseValue(false))
                .ForMember(d => d.IsBanned, m => m.UseValue(false))
                .ForMember(d => d.PhoneNumberConfirmed, m => m.UseValue(false))
                .ForMember(d => d.TwoFactorEnabled, m => m.UseValue(false))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserAdmin.AdminEditModel, User>()
                .ForMember(d => d.Picture, m => m.Ignore())
                .ForMember(d => d.PictureId, m => m.Ignore())
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserAdmin.AdminEditModel>()
                .IgnoreAllNonExisting();

            //CreateMap<User, AddEditUserViewModel>().IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserAdmin.ProfileModel>()
                .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserReseller.ProfileModel>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.ProfileModel, UserReseller>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserAdmin.ProfileModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.ProfileModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserReseller, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();


        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

}
