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
                .ForMember(d => d.NationalCode, m => m.MapFrom(t => t.UserReseller.NationalCode))
                .ForMember(d => d.ResellerCode, m => m.MapFrom(t => t.UserReseller.ResellerCode))
                .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserRouter.ProfileModel>()
              .ForMember(d => d.NationalCode, m => m.MapFrom(t => t.UserRouter.NationalCode))
              .ForMember(d => d.RouterCode, m => m.MapFrom(t => t.UserRouter.RouterCode))
              .ForMember(d => d.ZarinPalMerchantId, m => m.MapFrom(t => t.UserRouter.ZarinPalMerchantId))
              .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserRouter.RouterEditModel>()
              .ForMember(d => d.NationalCode, m => m.MapFrom(t => t.UserRouter.NationalCode))
              .ForMember(d => d.RouterCode, m => m.MapFrom(t => t.UserRouter.RouterCode))
              .ForMember(d => d.R_User, m => m.MapFrom(t => t.UserRouter.R_User))
              .ForMember(d => d.R_Port, m => m.MapFrom(t => t.UserRouter.R_Port))
              .ForMember(d => d.R_Password, m => m.MapFrom(t => t.UserRouter.R_Password))
              .ForMember(d => d.R_Host, m => m.MapFrom(t => t.UserRouter.R_Host))
              .ForMember(d => d.Userman_Customer, m => m.MapFrom(t => t.UserRouter.Userman_Customer))
              .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserRouter.RouterAdminEditModel>()
              .ForMember(d => d.ResellerId, m => m.MapFrom(t => t.UserRouter.UserResellerId))
              .ForMember(d => d.NationalCode, m => m.MapFrom(t => t.UserRouter.NationalCode))
              .ForMember(d => d.RouterCode, m => m.MapFrom(t => t.UserRouter.RouterCode))
              .ForMember(d => d.R_User, m => m.MapFrom(t => t.UserRouter.R_User))
              .ForMember(d => d.R_Port, m => m.MapFrom(t => t.UserRouter.R_Port))
              .ForMember(d => d.R_Password, m => m.MapFrom(t => t.UserRouter.R_Password))
              .ForMember(d => d.R_Host, m => m.MapFrom(t => t.UserRouter.R_Host))
              .ForMember(d => d.Userman_Customer, m => m.MapFrom(t => t.UserRouter.Userman_Customer))
              .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserRouter.MikrotikConfModel>()
              .ForMember(d => d.R_Host, m => m.MapFrom(t => t.UserRouter.R_Host))
              .ForMember(d => d.R_Password, m => m.MapFrom(t => t.UserRouter.R_Password))
              .ForMember(d => d.R_Port, m => m.MapFrom(t => t.UserRouter.R_Port))
              .ForMember(d => d.R_User, m => m.MapFrom(t => t.UserRouter.R_User))
              .ForMember(d => d.Userman_Customer, m => m.MapFrom(t => t.UserRouter.Userman_Customer))
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


            CreateMap<ViewModels.Identity.UserRouter.ProfileModel, UserRouter>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.ProfileModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.SmsModel, UserRouter>()
    .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.SmsModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.MikrotikConfModel, UserRouter>()
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.MikrotikConfModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.Register, UserRouter>()
               .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.Register, User>()
                .ForMember(d => d.CreateDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.EmailConfirmed, m => m.UseValue(false))
                .ForMember(d => d.IsDeleted, m => m.UseValue(false))
                .ForMember(d => d.IsBanned, m => m.UseValue(false))
                .ForMember(d => d.PhoneNumberConfirmed, m => m.UseValue(false))
                .ForMember(d => d.TwoFactorEnabled, m => m.UseValue(false))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();


            CreateMap<ViewModels.Identity.UserRouter.RouterEditModel, UserRouter>()
             .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.RouterEditModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();


            CreateMap<ViewModels.Identity.UserRouter.RouterAdminEditModel, UserRouter>()
              .ForMember(d => d.UserResellerId, m => m.MapFrom(s => s.ResellerId))
             .IgnoreAllNonExisting();

            CreateMap<ViewModels.Identity.UserRouter.RouterAdminEditModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.UserName, m => m.MapFrom(s => s.UserName.ToLower()))
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();

            CreateMap<User, ViewModels.Identity.UserRouter.TelegramBotModel>()
                .ForMember(d => d.TelegramBotToken, m => m.MapFrom(t => t.UserRouter.UserRouterTelegram.TelegramBotToken))
                .ForMember(d => d.ContactUsNumber, m => m.MapFrom(t => t.UserRouter.UserRouterTelegram.ContactUsNumber))
                .ForMember(d => d.ContactUsMessage, m => m.MapFrom(t => t.UserRouter.UserRouterTelegram.ContactUsMessage))
                .ForMember(d => d.ContactUsLastName, m => m.MapFrom(t => t.UserRouter.UserRouterTelegram.ContactUsLastName))
                .ForMember(d => d.ContactUsFirstName, m => m.MapFrom(t => t.UserRouter.UserRouterTelegram.ContactUsFirstName))
                .ForMember(d => d.AboutMessage, m => m.MapFrom(t => t.UserRouter.UserRouterTelegram.AboutMessage))
                .IgnoreAllNonExisting();//Read From DB


            CreateMap<ViewModels.Identity.UserRouter.TelegramBotModel, UserRouterTelegram>()
                .IgnoreAllNonExisting();//Write To DB
            CreateMap<ViewModels.Identity.UserRouter.TelegramBotModel, UserRouter>()
                .ForMember(d => d.UserRouterTelegram, opt => opt.MapFrom(s => s))
                .IgnoreAllNonExisting();//Write To DB
            CreateMap<ViewModels.Identity.UserRouter.TelegramBotModel, User>()
                .ForMember(d => d.EditDate, m => m.UseValue(DateTime.Now))
                .ForMember(d => d.UserRouter, opt => opt.MapFrom(s => s))
                .ForMember(d => d.Roles, m => m.Ignore())
                .ForMember(d => d.Claims, m => m.Ignore())
                .ForMember(d => d.Logins, m => m.Ignore())
                .IgnoreAllNonExisting();//Write To DB

        }

        public override string ProfileName
        {
            get { return GetType().Name; }
        }
    }

}
