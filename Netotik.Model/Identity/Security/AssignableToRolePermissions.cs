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



        #region ProductCategory
        public const string CanAccessProductCategory = "87";
        public static readonly PermissionModel CanAccessProductCategoryPermission = new PermissionModel { Name = CanAccessProductCategory, Description = "دسترسی به بخش دسته محصولی", Section = SectionPermisson.ProductCategory };
        #endregion

        #region DeliveryDate
        public const string CanAccessDeliveryDate = "91";
        public static readonly PermissionModel CanAccessDeliveryDatePermission = new PermissionModel { Name = CanAccessDeliveryDate, Description = "دسترسی به بخش مدت زمان های تحویل", Section = SectionPermisson.DeliveryDate };
        #endregion

        #region Discount
        public const string CanCreateDiscount = "92";
        public const string CanEditDiscount = "93";
        public const string CanDeleteDiscount = "94";
        public const string CanAccessDiscount = "95";
        public static readonly PermissionModel CanEditDiscountPermission = new PermissionModel { Name = CanCreateDiscount, Description = "ایجاد تخفیف", Section = SectionPermisson.Discount };
        public static readonly PermissionModel CanCreateDiscountPermission = new PermissionModel { Name = CanEditDiscount, Description = "ویرایش تخفیف", Section = SectionPermisson.Discount };
        public static readonly PermissionModel CanDeleteDicountPermission = new PermissionModel { Name = CanDeleteDiscount, Description = "حذف تخفیف", Section = SectionPermisson.Discount };
        public static readonly PermissionModel CanAccessDiscountPermission = new PermissionModel { Name = CanAccessDiscount, Description = "دسترسی به بخش تخفیف ها", Section = SectionPermisson.Discount };
        #endregion

        #region Manufacturer
        public const string CanAccessManufactur = "99";
        public static readonly PermissionModel CanAccessManufacturPermission = new PermissionModel { Name = CanAccessManufactur, Description = "دسترسی به بخش برندها", Section = SectionPermisson.Manufactur };
        #endregion

        #region PaymentType
        public const string CanAccessPaymentType = "103";
        public static readonly PermissionModel CanAccessPaymentTypePermission = new PermissionModel { Name = CanAccessPaymentType, Description = "ایجاد درگاههای پرداخت", Section = SectionPermisson.PaymentType };
        #endregion

        #region ProductAttribute
        public const string CanCreateProductAttribute = "104";
        public const string CanEditProductAttribute = "105";
        public const string CanDeleteProductAttribute = "106";
        public const string CanAccessProductAttribute = "107";
        public static readonly PermissionModel CanEditProductAttributePermission = new PermissionModel { Name = CanCreateProductAttribute, Description = "ایجاد مشخصات محصول", Section = SectionPermisson.ProductAttribute };
        public static readonly PermissionModel CanCreateProductAttributePermission = new PermissionModel { Name = CanEditProductAttribute, Description = "ویرایش مشخصات محصول", Section = SectionPermisson.ProductAttribute };
        public static readonly PermissionModel CanDeleteProductAttributePermission = new PermissionModel { Name = CanDeleteProductAttribute, Description = "حذف مشخصات محصول", Section = SectionPermisson.ProductAttribute };
        public static readonly PermissionModel CanAccessProductAttributePermission = new PermissionModel { Name = CanAccessProductAttribute, Description = "دسترسی به بخش مشخصات محصول", Section = SectionPermisson.ProductAttribute };
        #endregion

        #region ProductComments
        public const string CanDeleteProductComment = "108";
        public const string CanAcceptProductComment = "109";
        public const string CanDontAcceptProductComment = "110";
        public const string CanAccessProductComment = "111";
        public static readonly PermissionModel CanAccessProductCommentPermission = new PermissionModel { Name = CanAccessProductComment, Description = " دسترسی به بخش نظرات ارسالی محصول", Section = SectionPermisson.ProductComments };
        public static readonly PermissionModel CanDeleteProductCommentPermission = new PermissionModel { Name = CanDeleteProductComment, Description = "حذف نظرات ارسالی محصول", Section = SectionPermisson.ProductComments };
        public static readonly PermissionModel CanAcceptProductCommentPermission = new PermissionModel { Name = CanAcceptProductComment, Description = "تایید نظرات ارسالی محصول", Section = SectionPermisson.ProductComments };
        public static readonly PermissionModel CanDontAcceptProductCommentPermission = new PermissionModel { Name = CanDontAcceptProductComment, Description = "عدم تایید نظرات ارسالی محصول", Section = SectionPermisson.ProductComments };
        #endregion



        #region ShopShippingByWeight
        public const string CanAccessShopShippingByWeight = "115";
        public static readonly PermissionModel CanAccessShopShippingByWeightPermission = new PermissionModel { Name = CanAccessShopShippingByWeight, Description = "ایجاد شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByWeight };
        #endregion

        #region ShopShippingByMethod
        public const string CanAccessShopShippingByMethod = "119";
        public static readonly PermissionModel CanAccessShopShippingByMethodPermission = new PermissionModel { Name = CanAccessShopShippingByMethod, Description = "دسترسی به بخش شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByMethod };
        #endregion

        #region Tax
        public const string CanAccessTax = "123";
        public static readonly PermissionModel CanAccessTaxPermission = new PermissionModel { Name = CanAccessTax, Description = "دسترسی به بخش مالیات", Section = SectionPermisson.Tax };
        #endregion

        #region Product
        public const string CanCreateProduct = "128";
        public const string CanEditProduct = "129";
        public const string CanDeleteProduct = "130";
        public const string CanAcceptProduct = "131";
        public const string CanDontAcceptProduct = "132";
        public const string CanAccessProduct = "133";
        public const string CanManagePictureProduct = "134";
        public const string CanManageAttributeProduct = "135";
        public static readonly PermissionModel CanAccessProductPermission = new PermissionModel { Name = CanAccessProduct, Description = "دسترسی به بخش محصولات", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanCreateProductPermission = new PermissionModel { Name = CanCreateProduct, Description = "ایجاد محصول", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanEditProductPermission = new PermissionModel { Name = CanEditProduct, Description = "ویرایش محصول", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanDeleteProductPermission = new PermissionModel { Name = CanDeleteProduct, Description = "حذف محصول", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanAcceptProductPermission = new PermissionModel { Name = CanAcceptProduct, Description = "تایید محصول", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanDontAcceptProductPermission = new PermissionModel { Name = CanDontAcceptProduct, Description = "عدم تایید محصول", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanManagePictureProductPermission = new PermissionModel { Name = CanManagePictureProduct, Description = "مدیریت و تغییر تصاویر محصولات", Section = SectionPermisson.Product };
        public static readonly PermissionModel CanManageAttributeProductPermission = new PermissionModel { Name = CanManageAttributeProduct, Description = "مدیریت و تغییر مشخصات محصول", Section = SectionPermisson.Product };
        #endregion


        #region Advertise
        public const string CanAccessAdvertise = "136";
        public static readonly PermissionModel CanAccessAdvertisePermission = new PermissionModel { Name = CanAccessAdvertise, Description = "مدیریت بنرها", Section = SectionPermisson.Configuration };
        #endregion


        #region SuccessOrder
        public const string CanAccessSuccessOrder = "137";
        public static readonly PermissionModel CanAccessSuccessOrderPermission = new PermissionModel { Name = CanAccessSuccessOrder, Description = "مدیریت فاکتورهای پرداخت شده", Section = SectionPermisson.Factor };
        #endregion

        #region FailOrder
        public const string CanAccessFailOrder = "138";
        public static readonly PermissionModel CanAccessFailORderPermission = new PermissionModel { Name = CanAccessFailOrder, Description = "مدیریت فاکتورهای پرداخت نشده", Section = SectionPermisson.Factor };
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

                CanAccessProductCategoryPermission,

                CanAccessDeliveryDatePermission,
                
                CanAccessDiscountPermission,
                CanCreateDiscountPermission,
                CanEditDiscountPermission,
                CanDeleteDicountPermission,

                CanAccessManufacturPermission,
                
                CanAccessPaymentTypePermission,
                
                CanAccessProductAttributePermission,
                CanCreateProductAttributePermission,
                CanEditProductAttributePermission,
                CanDeleteProductAttributePermission,

                CanAccessProductCommentPermission,
                CanDeleteProductCommentPermission,
                CanAcceptProductCommentPermission,
                CanDontAcceptProductCommentPermission,

                CanAccessProductPermission,
                CanCreateProductPermission,
                CanEditProductPermission,
                CanDeleteProductPermission,
                CanManagePictureProductPermission,
                CanManageAttributeProductPermission,
                CanAcceptProductPermission,
                CanDontAcceptProductPermission,

                CanAccessShopShippingByMethodPermission,

                CanAccessShopShippingByWeightPermission,

                CanAccessTaxPermission,

                CanAccessSuccessOrderPermission,
                CanAccessFailORderPermission,

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

                CanAccessProductCategory,

                CanAccessDeliveryDate,

                CanAccessDiscount,
                CanCreateDiscount,
                CanEditDiscount,
                CanDeleteDiscount,

                CanAccessManufactur,

                CanAccessPaymentType,

                CanAccessProductAttribute,
                CanCreateProductAttribute,
                CanEditProductAttribute,
                CanDeleteProductAttribute,

                CanAccessProductComment,
                CanDeleteProductComment,
                CanAcceptProductComment,
                CanDontAcceptProductComment,

                CanAccessProduct,
                CanCreateProduct,
                CanEditProduct,
                CanDeleteProduct,
                CanManagePictureProduct,
                CanManageAttributeProduct,
                CanAcceptProduct,
                CanDontAcceptProduct,

                CanAccessShopShippingByMethod,

                CanAccessShopShippingByWeight,

                CanAccessTax,

                CanAccessSuccessOrder,
                CanAccessFailOrder,

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





        public static IEnumerable<PermissionModel> GetAllFactorPermision()
        {
            return new List<PermissionModel>
            {
                CanAccessSuccessOrderPermission,
                CanAccessFailORderPermission
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
                CanAccessProductCategoryPermission,

                CanAccessDeliveryDatePermission,

                CanAccessDiscountPermission,

                CanAccessManufacturPermission,

                CanAccessPaymentTypePermission,

                CanAccessProductPermission,

                CanAccessProductAttributePermission,

                CanAccessProductCommentPermission,

                CanAccessShopShippingByMethodPermission,

                CanAccessTaxPermission,
            };
        }
        #endregion

    }
}