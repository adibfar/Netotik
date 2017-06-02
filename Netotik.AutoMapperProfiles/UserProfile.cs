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

            CreateMap<ViewModels.Identity.UserReseller.ResellerAddModel, UserReseller>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.ResellerEditModel, UserReseller>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.RegisterViewModel, User>()
                    .ForMember(d => d.UserType, m => m.UseValue(UserType.UserReseller))
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

            CreateMap<ViewModels.Identity.UserReseller.ResellerAddModel, User>()
                    .ForMember(d => d.UserType, m => m.UseValue(UserType.UserReseller))
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


            CreateMap<ViewModels.Identity.UserReseller.ResellerEditModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
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

            CreateMap<User, ViewModels.Identity.UserReseller.ResellerEditModel>()
                .ForMember(d => d.NationalCode, m => m.MapFrom(t => t.UserReseller.NationalCode))
                .ForMember(d => d.ResellerCode, m => m.MapFrom(t => t.UserReseller.ResellerCode))
                .IgnoreAllNonExisting();

            //CreateMap<User, AddEditUserViewModel>().IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserAdmin.ProfileModel>()
                .ForMember(d => d.Facebook, m => m.MapFrom(t => t.UserAdmin.Facebook))
                .ForMember(d => d.Twitter, m => m.MapFrom(t => t.UserAdmin.Twitter))
                .ForMember(d => d.LinkedIn, m => m.MapFrom(t => t.UserAdmin.Linkedin))
                .ForMember(d => d.Instagram, m => m.MapFrom(t => t.UserAdmin.Instagram))
                .ForMember(d => d.Website, m => m.MapFrom(t => t.UserAdmin.Website))
                .ForMember(d => d.ShortBio, m => m.MapFrom(t => t.UserAdmin.ShortBio))
                .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserReseller.ProfileModel>()
                .ForMember(d => d.NationalCode, m => m.MapFrom(t=>t.UserReseller.NationalCode))
                .ForMember(d => d.ResellerCode, m => m.MapFrom(t => t.UserReseller.ResellerCode))
                .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserCompany.ProfileModel>()
              .ForMember(d => d.NationalCode, m => m.MapFrom(t => t.UserCompany.NationalCode))
              .ForMember(d => d.CompanyCode, m => m.MapFrom(t => t.UserCompany.CompanyCode))
              .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserCompany.MikrotikConfModel>()
              .ForMember(d => d.R_Host, m => m.MapFrom(t => t.UserCompany.R_Host))
              .ForMember(d => d.R_Password, m => m.MapFrom(t => t.UserCompany.R_Password))
              .ForMember(d => d.R_Port, m => m.MapFrom(t => t.UserCompany.R_Port))
              .ForMember(d => d.R_User, m => m.MapFrom(t => t.UserCompany.R_User))
              .ForMember(d => d.Userman_Customer, m => m.MapFrom(t => t.UserCompany.Userman_Customer))
              .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserAdmin.ProfileModel, UserAdmin>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserAdmin.ProfileModel, User>()
                .ForMember(d => d.UserAdmin, m => m.MapFrom(t => t))
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.ProfileModel, UserReseller>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserReseller.ProfileModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserReseller, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();


            CreateMap<ViewModels.Identity.UserCompany.ProfileModel, UserCompany>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserCompany.ProfileModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserCompany, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserCompany.MikrotikConfModel, UserCompany>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserCompany.MikrotikConfModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserCompany, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserCompany.Register, UserCompany>()
               .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserCompany.Register, User>()
                .ForMember(d => d.CreateDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EmailConfirmed, m => m.UseValue(false))
                .ForMember(d => d.IsDeleted, m => m.UseValue(false))
                .ForMember(d => d.IsBanned, m => m.UseValue(false))
                .ForMember(d => d.PhoneNumberConfirmed, m => m.UseValue(false))
                .ForMember(d => d.TwoFactorEnabled, m => m.UseValue(false))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.UserCompany, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();


        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

}
