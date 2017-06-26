using Netotik.ViewModels.Identity.Permisson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Netotik.ViewModels.Identity.Security
{

    public static class AssignableToRolePermissions
    {

        #region Fields

        private static Lazy<IEnumerable<PermissionModel>> _permissionsLazy =
            new Lazy<IEnumerable<PermissionModel>>(GetPermision, LazyThreadSafetyMode.ExecutionAndPublication);

        private static Lazy<IEnumerable<string>> _permissionNamesLazy = new Lazy<IEnumerable<string>>(
            GetPermisionNames, LazyThreadSafetyMode.ExecutionAndPublication);
        #endregion



        #region GetAsSelectedList

        public static IEnumerable<SelectListItem> GetAsSelectListItems()
        {
            return Permissions.Select(a => new SelectListItem { Text = a.Description, Value = a.Name }).ToList();
        }
        #endregion


        #region Properties
        public static IEnumerable<PermissionModel> Permissions
        {
            get
            {
                return _permissionsLazy.Value;
            }
        }

        public static IEnumerable<string> PermissionNames
        {
            get
            {
                return _permissionNamesLazy.Value;
            }
        }

        #endregion


        #region RolePerssiones


        #region User
        public const string CanCreateUser = "0";
        public const string CanEditUser = "1";
        public const string CanDeleteUser = "2";
        public const string CanAccessUser = "3";
        public static readonly PermissionModel CanEditUserPermission = new PermissionModel { Name = CanCreateUser, Description = "تعریف کاربر جدید", Section = SectionPermisson.User };
        public static readonly PermissionModel CanCreateUserPermission = new PermissionModel { Name = CanEditUser, Description = "ویرایش کاربران", Section = SectionPermisson.User };
        public static readonly PermissionModel CanDeleteUserPermission = new PermissionModel { Name = CanDeleteUser, Description = "حذف کاربران", Section = SectionPermisson.User };
        public static readonly PermissionModel CanAccessUserPermission = new PermissionModel { Name = CanAccessUser, Description = "دسترسی به بخش کاربران", Section = SectionPermisson.User };
        #endregion

        #region Role
        public const string CanAccessRole = "7";
        public static readonly PermissionModel CanAccessRolePermission = new PermissionModel { Name = CanAccessRole, Description = "دسترسی به بخش گروه های کاربری", Section = SectionPermisson.Role };
        #endregion

        #region Permisson
        public const string CanViewAllPermissions = "8";
        public static readonly PermissionModel CanViewAllPermissionsPermission = new PermissionModel { Name = CanViewAllPermissions, Description = "دسترسی به  جدول مجوز ها", Section = SectionPermisson.Permisson };
        #endregion

        #region AdminPanel
        public const string CanViewAdminPanel = "9";
        public static readonly PermissionModel CanViewAdminPanelPermission = new PermissionModel { Name = CanViewAdminPanel, Description = "دسترسی به پنل مدیریت", Section = SectionPermisson.AdminPanel };
        #endregion

        #region CMS ContentCategory
        public const string CanAccessContentCategory = "13";
        public static readonly PermissionModel CanAccessContentCategoryPermisson = new PermissionModel { Name = CanAccessContentCategory, Description = "دسترسی به بخش موضوعات مطالب", Section = SectionPermisson.CmsContentCategory };
        #endregion

        #region CMS Contets
        public const string CanCreateContent = "36";
        public const string CanEditContent = "37";
        public const string CanDeleteContent = "38";
        public const string CanAcceptContent = "39";
        public const string CanDontAcceptContent = "40";
        public const string CanViewAllContent = "41";
        public const string CanAccessContent = "42";
        public static readonly PermissionModel CanAccessContentsPermission = new PermissionModel { Name = CanAccessContent, Description = "دسترسی به بخش مطالب", Section = SectionPermisson.CmsContent };
        public static readonly PermissionModel CanCreateContentsPermission = new PermissionModel { Name = CanCreateContent, Description = "ایجاد مطلب", Section = SectionPermisson.CmsContent };
        public static readonly PermissionModel CanEditContentsPermission = new PermissionModel { Name = CanEditContent, Description = "ویرایش مطلب", Section = SectionPermisson.CmsContent };
        public static readonly PermissionModel CanDeleteContentsPermission = new PermissionModel { Name = CanDeleteContent, Description = "حذف مطلب", Section = SectionPermisson.CmsContent };
        public static readonly PermissionModel CanAcceptContentsPermission = new PermissionModel { Name = CanAcceptContent, Description = "تایید مطلب", Section = SectionPermisson.CmsContent };
        public static readonly PermissionModel CanDontAcceptContentsPermission = new PermissionModel { Name = CanDontAcceptContent, Description = "عدم تایید مطلب", Section = SectionPermisson.CmsContent };
        public static readonly PermissionModel CanViewAllContentsPermission = new PermissionModel { Name = CanViewAllContent, Description = "نمایش تمامی مطالب", Section = SectionPermisson.CmsContent };
        #endregion

        #region CMS Tag
        public const string CanAccessTag = "69";
        public static readonly PermissionModel CanAccessTagPermission = new PermissionModel { Name = CanAccessTag, Description = "دسترسی به بخش برچسب ها", Section = SectionPermisson.CmsTag };
        #endregion

        #region Menu
        public const string CanAccessMenu = "73";
        public static readonly PermissionModel CanAccessMenuPermission = new PermissionModel { Name = CanAccessMenu, Description = "دسترسی به بخش منوها", Section = SectionPermisson.ConfigurationMenu };
        #endregion

        #region Comments
        public const string CanDeleteComment = "74";
        public const string CanAcceptComment = "75";
        public const string CanDontAcceptComment = "76";
        public const string CanAccessComment = "77";
        public static readonly PermissionModel CanAccessCommentPermission = new PermissionModel { Name = CanAccessComment, Description = "دسترسی به بخش نظرات ارسالی", Section = SectionPermisson.CmsComments };
        public static readonly PermissionModel CanDeleteCommentPermission = new PermissionModel { Name = CanDeleteComment, Description = "حذف نظرات ارسالی", Section = SectionPermisson.CmsComments };
        public static readonly PermissionModel CanAcceptCommentPermission = new PermissionModel { Name = CanAcceptComment, Description = "تایید نظرات ارسالی", Section = SectionPermisson.CmsComments };
        public static readonly PermissionModel CanDontAcceptCommentPermission = new PermissionModel { Name = CanDontAcceptComment, Description = "عدم تایید نظرات ارسالی", Section = SectionPermisson.CmsComments };
        #endregion

        #region ContactUs
        public const string CanAccessContactUs = "78";
        public static readonly PermissionModel CanAccessContactUsPermission = new PermissionModel { Name = CanAccessContactUs, Description = "دسترسی به پیام های دریافتی تماس با ما", Section = SectionPermisson.ConfigurationContactUs };
        #endregion

        #region Slider
        public const string CanAccessSlider = "79";
        public static readonly PermissionModel CanAccessSliderPermission = new PermissionModel { Name = CanAccessSlider, Description = "مدیریت اسلایدر", Section = SectionPermisson.Configuration };
        #endregion

        #region PublicSetting
        public const string CanAccessPublicSetting = "80";
        public static readonly PermissionModel CanAccessPublicSettingPermission = new PermissionModel { Name = CanAccessPublicSetting, Description = "مدیریت تنظیمات عمومی سایت", Section = SectionPermisson.Configuration };
        #endregion

        #region Links
        public const string CanAccessLinks = "81";
        public static readonly PermissionModel CanAccessLinksPermission = new PermissionModel { Name = CanAccessLinks, Description = "مدیریت پیوند ها", Section = SectionPermisson.Configuration };
        #endregion

        #region State
        public const string CanAccessState = "82";
        public static readonly PermissionModel CanAccessStatePermission = new PermissionModel { Name = CanAccessState, Description = "مدیریت استان ها", Section = SectionPermisson.Configuration };
        #endregion


        #region City
        public const string CanAccessCity = "83";
        public static readonly PermissionModel CanAccessCityPermission = new PermissionModel { Name = CanAccessCity, Description = "مدیریت شهرها", Section = SectionPermisson.Configuration };
        #endregion


        #region PaymentType
        public const string CanAccessPaymentType = "103";
        public static readonly PermissionModel CanAccessPaymentTypePermission = new PermissionModel { Name = CanAccessPaymentType, Description = "ایجاد درگاههای پرداخت", Section = SectionPermisson.PaymentType };
        #endregion

        #region Advertise
        public const string CanAccessAdvertise = "136";
        public static readonly PermissionModel CanAccessAdvertisePermission = new PermissionModel { Name = CanAccessAdvertise, Description = "مدیریت بنرها", Section = SectionPermisson.Configuration };
        #endregion

        #region Ticket
        public const string CanCreateTicket = "153";
        public const string CanTrackTicket = "154";
        public const string CanDeleteTicket = "155";
        public const string CanChangeStatusTicket = "156";
        public const string CanViewAllTicket = "157";
        public const string CanAccessTicket = "158";
        public static readonly PermissionModel CanAccessTicketPermission = new PermissionModel { Name = CanAccessTicket, Description = "دسترسی به بخش تیکت", Section = SectionPermisson.Ticket };
        public static readonly PermissionModel CanCreateTicketPermission = new PermissionModel { Name = CanCreateTicket, Description = "ایجاد تیکت", Section = SectionPermisson.Ticket };
        public static readonly PermissionModel CanTrackTicketPermission = new PermissionModel { Name = CanTrackTicket, Description = "ارسال پیام در تیکت", Section = SectionPermisson.Ticket };
        public static readonly PermissionModel CanDeleteTicketPermission = new PermissionModel { Name = CanDeleteTicket, Description = "حذف تیکت", Section = SectionPermisson.Ticket };
        public static readonly PermissionModel CanCahngeStatusTicketPermission = new PermissionModel { Name = CanChangeStatusTicket, Description = "تغییر وضعیت تیکت", Section = SectionPermisson.Ticket };
        public static readonly PermissionModel CanViewAllTicketPermission = new PermissionModel { Name = CanViewAllTicket, Description = "نمایش تمامی تیکت", Section = SectionPermisson.Ticket };
        #endregion


        #region Ticket Tag
        public const string CanAccessTicketTag = "159";
        public static readonly PermissionModel CanAccessTicketTagPermission = new PermissionModel { Name = CanAccessTicketTag, Description = "دسترسی به بخش برچسب های تیکت", Section = SectionPermisson.Ticket };

        #endregion


        #endregion




        #region GetAllPermisions


        public static IEnumerable<PermissionModel> GetPermision()
        {
            return new List<PermissionModel>
            {
                CanViewAdminPanelPermission,

                CanAccessContentCategoryPermisson,

                CanAccessContentsPermission,
                CanViewAllContentsPermission,
                CanCreateContentsPermission,
                CanEditContentsPermission,
                CanDeleteContentsPermission,
                CanAcceptContentsPermission,
                CanDontAcceptContentsPermission,

                CanAccessTagPermission,

                CanAccessCommentPermission,
                CanDeleteCommentPermission,
                CanAcceptCommentPermission,
                CanDontAcceptCommentPermission,

                CanAccessUserPermission,
                CanCreateUserPermission,
                CanEditUserPermission,
                CanDeleteUserPermission,

                CanAccessRolePermission,

                CanAccessMenuPermission,

                CanViewAllPermissionsPermission,

                CanAccessContactUsPermission,

                CanAccessSliderPermission,

                CanAccessAdvertisePermission,

                CanAccessPublicSettingPermission,

                CanAccessLinksPermission,

                CanAccessStatePermission,

                CanAccessCityPermission,

                CanAccessPaymentTypePermission,
                
                CanAccessTicketTagPermission,

                CanAccessTicketPermission,
                CanViewAllTicketPermission,
                CanCreateTicketPermission,
                CanTrackTicketPermission,
                CanDeleteTicketPermission,
                CanCahngeStatusTicketPermission,
            };
        }

        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                CanViewAdminPanel,

                CanAccessContentCategory,

                CanAccessContent,
                CanViewAllContent,
                CanCreateContent,
                CanEditContent,
                CanDeleteContent,
                CanAcceptContent,
                CanDontAcceptContent,

                CanAccessTag,

                CanAccessComment,
                CanDeleteComment,
                CanAcceptComment,
                CanDontAcceptComment,

                CanAccessUser,
                CanCreateUser,
                CanEditUser,
                CanDeleteUser,

                CanAccessRole,

                CanAccessMenu,

                CanViewAllPermissions,

                CanAccessContactUs,

                CanAccessSlider,

                CanAccessAdvertise,

                CanAccessPublicSetting,

                CanAccessLinks,

                CanAccessState,

                CanAccessCity,

                CanAccessPaymentType,

                CanAccessTicketTag,

                CanAccessTicket,
                CanViewAllTicket,
                CanCreateTicket,
                CanTrackTicket,
                CanDeleteTicket,
                CanChangeStatusTicket
            };
        }



        public static IEnumerable<PermissionModel> GetAllIssuePermision()
        {
            return new List<PermissionModel>
            {
                CanAccessTicketTagPermission,

                CanAccessTicketPermission,
                CanViewAllTicketPermission,
                CanCreateTicketPermission,
                CanTrackTicketPermission,
                CanDeleteTicketPermission,
                CanCahngeStatusTicketPermission,
            };
        }


        public static IEnumerable<PermissionModel> GetAllConfigurationPermision()
        {
            return new List<PermissionModel>
            {
                CanAccessMenuPermission,

                CanAccessLinksPermission,

                CanAccessCityPermission,

                CanAccessStatePermission,

                CanAccessPublicSettingPermission,
            };
        }




        public static IEnumerable<PermissionModel> GetAllUserPermision()
        {
            return new List<PermissionModel>
            {

                CanAccessUserPermission,
                CanAccessRolePermission,
                CanViewAllPermissionsPermission
            };
        }




        

        public static IEnumerable<PermissionModel> GetAllCmsSectionPermision()
        {
            return new List<PermissionModel>
            {
                CanAccessContentCategoryPermisson,

                CanAccessSliderPermission,

                CanAccessAdvertisePermission,

                CanAccessContentsPermission,

                CanAccessTagPermission,

                CanAccessCommentPermission,

                CanAccessContactUsPermission
            };
        }



        public static IEnumerable<PermissionModel> GetAllShopSectionPermision()
        {
            return new List<PermissionModel>
            {

                CanAccessPaymentTypePermission,
            };
        }
        #endregion

    }
}