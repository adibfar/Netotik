using Netotik.ViewModels.Identity.Permisson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        public const string CanCreateRole = "4";
        public const string CanEditRole = "5";
        public const string CanDeleteRole = "6";
        public const string CanAccessRole = "7";
        public static readonly PermissionModel CanEditRolePermission = new PermissionModel { Name = CanCreateRole, Description = "ایجاد گروه کاربری", Section = SectionPermisson.Role };
        public static readonly PermissionModel CanCreateRolePermission = new PermissionModel { Name = CanEditRole, Description = "ویرایش گروه کاربری", Section = SectionPermisson.Role };
        public static readonly PermissionModel CanDeleteRolePermission = new PermissionModel { Name = CanDeleteRole, Description = "حذف گروه کاربری", Section = SectionPermisson.Role };
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
        public const string CanCreateContentCategory = "10";
        public const string CanEditContentCategory = "11";
        public const string CanDeleteContentCategory = "12";
        public const string CanAccessContentCategory = "13";
        public static readonly PermissionModel CanCreateContentCategoryPermission = new PermissionModel { Name = CanCreateContentCategory, Description = "ایجاد موضوع مطالب", Section = SectionPermisson.CmsContentCategory };
        public static readonly PermissionModel CanEditContentCategoryPermission = new PermissionModel { Name = CanEditContentCategory, Description = "ویرایش موضوع", Section = SectionPermisson.CmsContentCategory };
        public static readonly PermissionModel CanDeleteContentCategoryPermission = new PermissionModel { Name = CanDeleteContentCategory, Description = "حذف موضوع", Section = SectionPermisson.CmsContentCategory };
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
        public const string CanCreateTag = "66";
        public const string CanEditTag = "67";
        public const string CanDeleteTag = "68";
        public const string CanAccessTag = "69";
        public static readonly PermissionModel CanAccessTagPermission = new PermissionModel { Name = CanAccessTag, Description = "دسترسی به بخش برچسب ها", Section = SectionPermisson.CmsTag };
        public static readonly PermissionModel CanCreateTagPermission = new PermissionModel { Name = CanCreateTag, Description = "ایجاد برچسب", Section = SectionPermisson.CmsTag };
        public static readonly PermissionModel CanEditTagPermission = new PermissionModel { Name = CanEditTag, Description = "ویرایش برچسب", Section = SectionPermisson.CmsTag };
        public static readonly PermissionModel CanDeleteTagPermission = new PermissionModel { Name = CanDeleteTag, Description = "حذف برچسب", Section = SectionPermisson.CmsTag };
        #endregion

        #region Menu
        public const string CanCreateMenu = "70";
        public const string CanEditMenu = "71";
        public const string CanDeleteMenu = "72";
        public const string CanAccessMenu = "73";
        public static readonly PermissionModel CanAccessMenuPermission = new PermissionModel { Name = CanAccessMenu, Description = "دسترسی به بخش منوها", Section = SectionPermisson.ConfigurationMenu };
        public static readonly PermissionModel CanEditMenuPermission = new PermissionModel { Name = CanEditMenu, Description = "ایجاد منو", Section = SectionPermisson.ConfigurationMenu };
        public static readonly PermissionModel CanCreateMenuPermission = new PermissionModel { Name = CanCreateMenu, Description = "ویرایش منو", Section = SectionPermisson.ConfigurationMenu };
        public static readonly PermissionModel CanDeleteMenuPermission = new PermissionModel { Name = CanDeleteMenu, Description = "حذف منو", Section = SectionPermisson.ConfigurationMenu };
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
        public const string CanCreateProductCategory = "84";
        public const string CanEditProductCategory = "85";
        public const string CanDeleteProductCategory = "86";
        public const string CanAccessProductCategory = "87";
        public static readonly PermissionModel CanEditProductCategoryPermission = new PermissionModel { Name = CanCreateProductCategory, Description = "ایجاد دسته محصولی", Section = SectionPermisson.ProductCategory };
        public static readonly PermissionModel CanCreateProductCategoryPermission = new PermissionModel { Name = CanEditProductCategory, Description = "ویرایش دسته محصولی", Section = SectionPermisson.ProductCategory };
        public static readonly PermissionModel CanDeleteProductCategoryPermission = new PermissionModel { Name = CanDeleteProductCategory, Description = "حذف دسته محصولی", Section = SectionPermisson.ProductCategory };
        public static readonly PermissionModel CanAccessProductCategoryPermission = new PermissionModel { Name = CanAccessProductCategory, Description = "دسترسی به بخش دسته محصولی", Section = SectionPermisson.ProductCategory };
        #endregion

        #region DeliveryDate
        public const string CanCreateDeliveryDate = "88";
        public const string CanEditDeliveryDate = "89";
        public const string CanDeleteDeliveryDate = "90";
        public const string CanAccessDeliveryDate = "91";
        public static readonly PermissionModel CanEditDeliveryDatePermission = new PermissionModel { Name = CanCreateDeliveryDate, Description = "ایجاد مدت زمان های تحویل", Section = SectionPermisson.DeliveryDate };
        public static readonly PermissionModel CanCreateDeliveryDatePermission = new PermissionModel { Name = CanEditDeliveryDate, Description = "ویرایش مدت زمان های تحویل", Section = SectionPermisson.DeliveryDate };
        public static readonly PermissionModel CanDeleteDeliveryDatePermission = new PermissionModel { Name = CanDeleteDeliveryDate, Description = "حذف مدت زمان های تحویل", Section = SectionPermisson.DeliveryDate };
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
        public const string CanCreateManufactur = "96";
        public const string CanEditManufactur = "97";
        public const string CanDeleteManufactur = "98";
        public const string CanAccessManufactur = "99";
        public static readonly PermissionModel CanEditManufacturPermission = new PermissionModel { Name = CanCreateManufactur, Description = "ایجاد برند", Section = SectionPermisson.Manufactur };
        public static readonly PermissionModel CanCreateManufacturPermission = new PermissionModel { Name = CanEditManufactur, Description = "ویرایش برند", Section = SectionPermisson.Manufactur };
        public static readonly PermissionModel CanDeleteManufacturPermission = new PermissionModel { Name = CanDeleteManufactur, Description = "حذف برند", Section = SectionPermisson.Manufactur };
        public static readonly PermissionModel CanAccessManufacturPermission = new PermissionModel { Name = CanAccessManufactur, Description = "دسترسی به بخش برندها", Section = SectionPermisson.Manufactur };
        #endregion

        #region PaymentType
        public const string CanCreatePaymentType = "100";
        public const string CanEditPaymentType = "101";
        public const string CanDeletePaymentType = "102";
        public const string CanAccessPaymentType = "103";
        public static readonly PermissionModel CanEditPaymentTypePermission = new PermissionModel { Name = CanCreatePaymentType, Description = "ایجاد درگاههای پرداخت", Section = SectionPermisson.PaymentType };
        public static readonly PermissionModel CanCreatePaymentTypePermission = new PermissionModel { Name = CanEditPaymentType, Description = "ویرایش درگاههای پرداخت", Section = SectionPermisson.PaymentType };
        public static readonly PermissionModel CanDeletePaymentTypePermission = new PermissionModel { Name = CanDeletePaymentType, Description = "حذف درگاههای پرداخت", Section = SectionPermisson.PaymentType };
        public static readonly PermissionModel CanAccessPaymentTypePermission = new PermissionModel { Name = CanAccessPaymentType, Description = "دسترسی به بخش درگاههای پرداخت", Section = SectionPermisson.PaymentType };
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
        public const string CanCreateShopShippingByWeight = "112";
        public const string CanEditShopShippingByWeight = "113";
        public const string CanDeleteShopShippingByWeight = "114";
        public const string CanAccessShopShippingByWeight = "115";
        public static readonly PermissionModel CanEditShopShippingByWeightPermission = new PermissionModel { Name = CanCreateShopShippingByWeight, Description = "ایجاد شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByWeight };
        public static readonly PermissionModel CanCreateShopShippingByWeightPermission = new PermissionModel { Name = CanEditShopShippingByWeight, Description = "ویرایش شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByWeight };
        public static readonly PermissionModel CanDeleteShopShippingByWeightPermission = new PermissionModel { Name = CanDeleteShopShippingByWeight, Description = "حذف شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByWeight };
        public static readonly PermissionModel CanAccessShopShippingByWeightPermission = new PermissionModel { Name = CanAccessShopShippingByWeight, Description = "دسترسی به بخش شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByWeight };
        #endregion

        #region ShopShippingByMethod
        public const string CanCreateShopShippingByMethod = "116";
        public const string CanEditShopShippingByMethod = "117";
        public const string CanDeleteShopShippingByMethod = "118";
        public const string CanAccessShopShippingByMethod = "119";
        public static readonly PermissionModel CanEditShopShippingByMethodPermission = new PermissionModel { Name = CanCreateShopShippingByMethod, Description = "ایجاد روش های ارسال", Section = SectionPermisson.ShopShippingByMethod };
        public static readonly PermissionModel CanCreateShopShippingByMethodPermission = new PermissionModel { Name = CanEditShopShippingByMethod, Description = "ویرایش شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByMethod };
        public static readonly PermissionModel CanDeleteShopShippingByMethodPermission = new PermissionModel { Name = CanDeleteShopShippingByMethod, Description = "حذف شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByMethod };
        public static readonly PermissionModel CanAccessShopShippingByMethodPermission = new PermissionModel { Name = CanAccessShopShippingByMethod, Description = "دسترسی به بخش شرایط روش های ارسال", Section = SectionPermisson.ShopShippingByMethod };
        #endregion

        #region Tax
        public const string CanCreateTax = "120";
        public const string CanEditTax = "121";
        public const string CanDeleteTax = "122";
        public const string CanAccessTax = "123";
        public static readonly PermissionModel CanEditTaxPermission = new PermissionModel { Name = CanCreateTax, Description = "ایجاد مالیات", Section = SectionPermisson.Tax };
        public static readonly PermissionModel CanCreateTaxPermission = new PermissionModel { Name = CanEditTax, Description = "ویرایش مالیات", Section = SectionPermisson.Tax };
        public static readonly PermissionModel CanDeleteTaxPermission = new PermissionModel { Name = CanDeleteTax, Description = "حذف مالیات", Section = SectionPermisson.Tax };
        public static readonly PermissionModel CanAccessTaxPermission = new PermissionModel { Name = CanAccessTax, Description = "دسترسی به بخش مالیات", Section = SectionPermisson.Tax };
        #endregion


        #region WareHouse
        public const string CanCreateWareHouse = "124";
        public const string CanEditWareHouse = "125";
        public const string CanDeleteWareHouse = "126";
        public const string CanAccessWareHouse = "127";
        public static readonly PermissionModel CanEditWareHousePermission = new PermissionModel { Name = CanCreateWareHouse, Description = "ایجاد مالیات", Section = SectionPermisson.WareHouse };
        public static readonly PermissionModel CanCreateWareHousePermission = new PermissionModel { Name = CanEditWareHouse, Description = "ویرایش مالیات", Section = SectionPermisson.WareHouse };
        public static readonly PermissionModel CanDeleteWareHousePermission = new PermissionModel { Name = CanDeleteWareHouse, Description = "حذف مالیات", Section = SectionPermisson.WareHouse };
        public static readonly PermissionModel CanAccessWareHousePermission = new PermissionModel { Name = CanAccessWareHouse, Description = "دسترسی به بخش مالیات", Section = SectionPermisson.WareHouse };
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


        #region ProductGallery
        public const string CanCreateProductGallery = "139";
        public const string CanEditProductGallery = "141";
        public const string CanDeleteProductGallery = "142";
        public const string CanAccessProductGallery = "143";
        public const string CanManagePictureProductGallery = "144";
        public static readonly PermissionModel CanAccessProductGalleryPermission = new PermissionModel { Name = CanAccessProductGallery, Description = "دسترسی به بخش محصولات گالری", Section = SectionPermisson.ProductGallery };
        public static readonly PermissionModel CanCreateProductGalleryPermission = new PermissionModel { Name = CanCreateProductGallery, Description = "ایجاد محصول گالری", Section = SectionPermisson.ProductGallery };
        public static readonly PermissionModel CanEditProductGalleryPermission = new PermissionModel { Name = CanEditProductGallery, Description = "ویرایش محصول گالری", Section = SectionPermisson.ProductGallery };
        public static readonly PermissionModel CanDeleteProductGalleryPermission = new PermissionModel { Name = CanDeleteProductGallery, Description = "حذف محصول گالری", Section = SectionPermisson.ProductGallery };
        public static readonly PermissionModel CanManagePictureProductGalleryPermission = new PermissionModel { Name = CanManagePictureProductGallery, Description = "مدیریت و تغییر تصاویر محصولات گالری", Section = SectionPermisson.ProductGallery };
        #endregion


        #region ProductColor
        public const string CanCreateProductColor = "145";
        public const string CanEditProductColor = "146";
        public const string CanDeleteProductColor = "147";
        public const string CanAccessProductColor = "148";
        public static readonly PermissionModel CanAccessProductColorPermission = new PermissionModel { Name = CanAccessProductColor, Description = "دسترسی به بخش رنگ های محصولات", Section = SectionPermisson.ProductColor };
        public static readonly PermissionModel CanCreateProductColorPermission = new PermissionModel { Name = CanCreateProductColor, Description = "ایجاد رنگ", Section = SectionPermisson.ProductColor };
        public static readonly PermissionModel CanEditProductColorPermission = new PermissionModel { Name = CanEditProductColor, Description = "ویرایش رنگ", Section = SectionPermisson.ProductColor };
        public static readonly PermissionModel CanDeleteProductColorPermission = new PermissionModel { Name = CanDeleteProductColor, Description = "حذف رنگ", Section = SectionPermisson.ProductColor };
        #endregion




        #region ProductSize
        public const string CanCreateProductSize = "149";
        public const string CanEditProductSize = "150";
        public const string CanDeleteProductSize = "151";
        public const string CanAccessProductSize = "152";
        public static readonly PermissionModel CanAccessProductSizePermission = new PermissionModel { Name = CanAccessProductSize, Description = "دسترسی به بخش سایز های محصولات", Section = SectionPermisson.ProductSize };
        public static readonly PermissionModel CanCreateProductSizePermission = new PermissionModel { Name = CanCreateProductSize, Description = "ایجاد سایز", Section = SectionPermisson.ProductSize };
        public static readonly PermissionModel CanEditProductSizePermission = new PermissionModel { Name = CanEditProductSize, Description = "ویرایش سایز", Section = SectionPermisson.ProductSize };
        public static readonly PermissionModel CanDeleteProductSizePermission = new PermissionModel { Name = CanDeleteProductSize, Description = "حذف سایز", Section = SectionPermisson.ProductSize };
        #endregion



        #region Issue
        public const string CanCreateIssue = "153";
        public const string CanTrackIssue = "154";
        public const string CanDeleteIssue = "155";
        public const string CanChangeStatusIssue = "156";
        public const string CanViewAllIssue = "157";
        public const string CanAccessIssue = "158";
        public static readonly PermissionModel CanAccessIssuePermission = new PermissionModel { Name = CanAccessIssue, Description = "دسترسی به بخش وظایف", Section = SectionPermisson.Issue };
        public static readonly PermissionModel CanCreateIssuesPermission = new PermissionModel { Name = CanCreateIssue, Description = "ایجاد وظیفه", Section = SectionPermisson.Issue };
        public static readonly PermissionModel CanTrackIssuesPermission = new PermissionModel { Name = CanTrackIssue, Description = "ارسال پیام در وظیفه", Section = SectionPermisson.Issue };
        public static readonly PermissionModel CanDeleteIssuesPermission = new PermissionModel { Name = CanDeleteIssue, Description = "حذف وظیفه", Section = SectionPermisson.Issue };
        public static readonly PermissionModel CanCahngeStatusIssuesPermission = new PermissionModel { Name = CanChangeStatusIssue, Description = "تغییر وضعیت وظیفه", Section = SectionPermisson.Issue };
        public static readonly PermissionModel CanViewAllIssuesPermission = new PermissionModel { Name = CanViewAllIssue, Description = "نمایش تمامی وظایف", Section = SectionPermisson.Issue };
        #endregion


        #region Issue Label
        public const string CanAccessIssueLabel = "159";
        public static readonly PermissionModel CanAccessIssueLabelPermission = new PermissionModel { Name = CanAccessIssueLabel, Description = "دسترسی به بخش برچسب های وظایف", Section = SectionPermisson.Issue };

        #endregion



        #endregion




        #region GetAllPermisions


        public static IEnumerable<PermissionModel> GetPermision()
        {
            return new List<PermissionModel>
            {
                CanViewAdminPanelPermission,

                CanAccessContentCategoryPermisson,
                CanCreateContentCategoryPermission,
                CanEditContentCategoryPermission,
                CanDeleteContentCategoryPermission,

                CanAccessContentsPermission,
                CanViewAllContentsPermission,
                CanCreateContentsPermission,
                CanEditContentsPermission,
                CanDeleteContentsPermission,
                CanAcceptContentsPermission,
                CanDontAcceptContentsPermission,

                CanAccessTagPermission,
                CanCreateTagPermission,
                CanEditTagPermission,
                CanDeleteTagPermission,

                CanAccessCommentPermission,
                CanDeleteCommentPermission,
                CanAcceptCommentPermission,
                CanDontAcceptCommentPermission,

                CanAccessUserPermission,
                CanCreateUserPermission,
                CanEditUserPermission,
                CanDeleteUserPermission,

                CanAccessRolePermission,
                CanCreateRolePermission,
                CanEditRolePermission,
                CanDeleteRolePermission,

                CanAccessMenuPermission,
                CanCreateMenuPermission,
                CanEditMenuPermission,
                CanDeleteMenuPermission,

                CanViewAllPermissionsPermission,

                CanAccessContactUsPermission,

                CanAccessSliderPermission,

                CanAccessAdvertisePermission,

                CanAccessPublicSettingPermission,

                CanAccessLinksPermission,

                CanAccessStatePermission,

                CanAccessCityPermission,

                CanAccessProductCategoryPermission,
                CanCreateProductCategoryPermission,
                CanEditProductCategoryPermission,
                CanDeleteProductCategoryPermission,

                CanAccessDeliveryDatePermission,
                CanCreateDeliveryDatePermission,
                CanEditDeliveryDatePermission,
                CanDeleteDeliveryDatePermission,

                CanAccessDiscountPermission,
                CanCreateDiscountPermission,
                CanEditDiscountPermission,
                CanDeleteDicountPermission,

                CanAccessManufacturPermission,
                CanCreateManufacturPermission,
                CanEditManufacturPermission,
                CanDeleteManufacturPermission,

                CanAccessPaymentTypePermission,
                CanCreatePaymentTypePermission,
                CanEditPaymentTypePermission,
                CanDeletePaymentTypePermission,

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


                CanAccessProductGalleryPermission,
                CanCreateProductGalleryPermission,
                CanEditProductGalleryPermission,
                CanDeleteProductGalleryPermission,
                CanManagePictureProductGalleryPermission,

                CanAccessShopShippingByMethodPermission,
                CanCreateShopShippingByMethodPermission,
                CanEditShopShippingByMethodPermission,
                CanDeleteShopShippingByMethodPermission,

                CanAccessShopShippingByWeightPermission,
                CanCreateShopShippingByWeightPermission,
                CanEditShopShippingByWeightPermission,
                CanDeleteShopShippingByWeightPermission,

                CanAccessTaxPermission,
                CanCreateTaxPermission,
                CanEditTaxPermission,
                CanDeleteTaxPermission,

                CanAccessProductColorPermission,
                CanCreateProductColorPermission,
                CanEditProductColorPermission,
                CanDeleteProductColorPermission,

                CanAccessProductSizePermission,
                CanCreateProductSizePermission,
                CanEditProductSizePermission,
                CanDeleteProductSizePermission,

                CanAccessWareHousePermission,
                CanCreateWareHousePermission,
                CanEditWareHousePermission,
                CanDeleteWareHousePermission,

                CanAccessSuccessOrderPermission,
                CanAccessFailORderPermission,

                CanAccessIssueLabelPermission,

                CanAccessIssuePermission,
                CanViewAllIssuesPermission,
                CanCreateIssuesPermission,
                CanTrackIssuesPermission,
                CanDeleteIssuesPermission,
                CanCahngeStatusIssuesPermission,
            };
        }

        public static IEnumerable<string> GetPermisionNames()
        {
            return new List<String>
            {
                CanViewAdminPanel,

                CanAccessContentCategory,
                CanCreateContentCategory,
                CanEditContentCategory,
                CanDeleteContentCategory,

                CanAccessContent,
                CanViewAllContent,
                CanCreateContent,
                CanEditContent,
                CanDeleteContent,
                CanAcceptContent,
                CanDontAcceptContent,

                CanAccessTag,
                CanCreateTag,
                CanEditTag,
                CanDeleteTag,

                CanAccessComment,
                CanDeleteComment,
                CanAcceptComment,
                CanDontAcceptComment,

                CanAccessUser,
                CanCreateUser,
                CanEditUser,
                CanDeleteUser,

                CanAccessRole,
                CanCreateRole,
                CanEditRole,
                CanDeleteRole,

                CanAccessMenu,
                CanCreateMenu,
                CanEditMenu,
                CanDeleteMenu,

                CanViewAllPermissions,

                CanAccessContactUs,

                CanAccessSlider,

                CanAccessAdvertise,

                CanAccessPublicSetting,

                CanAccessLinks,

                CanAccessState,

                CanAccessCity,

                CanAccessProductCategory,
                CanCreateProductCategory,
                CanEditProductCategory,
                CanDeleteProductCategory,

                CanAccessDeliveryDate,
                CanCreateDeliveryDate,
                CanEditDeliveryDate,
                CanDeleteDeliveryDate,

                CanAccessDiscount,
                CanCreateDiscount,
                CanEditDiscount,
                CanDeleteDiscount,

                CanAccessManufactur,
                CanCreateManufactur,
                CanEditManufactur,
                CanDeleteManufactur,

                CanAccessPaymentType,
                CanCreatePaymentType,
                CanEditPaymentType,
                CanDeletePaymentType,

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

                CanAccessProductGallery,
                CanCreateProductGallery,
                CanEditProductGallery,
                CanDeleteProductGallery,
                CanManagePictureProductGallery,

                CanAccessShopShippingByMethod,
                CanCreateShopShippingByMethod,
                CanEditShopShippingByMethod,
                CanDeleteShopShippingByMethod,

                CanAccessShopShippingByWeight,
                CanCreateShopShippingByWeight,
                CanEditShopShippingByWeight,
                CanDeleteShopShippingByWeight,

                CanAccessTax,
                CanCreateTax,
                CanEditTax,
                CanDeleteTax,

                CanAccessProductColor,
                CanCreateProductColor,
                CanEditProductColor,
                CanDeleteProductColor,

                CanAccessProductSize,
                CanCreateProductSize,
                CanEditProductSize,
                CanDeleteProductSize,

                CanAccessWareHouse,
                CanCreateWareHouse,
                CanEditWareHouse,
                CanDeleteWareHouse,

                CanAccessSuccessOrder,
                CanAccessFailOrder,

                CanAccessIssueLabel,

                CanAccessIssue,
                CanViewAllIssue,
                CanCreateIssue,
                CanTrackIssue,
                CanDeleteIssue,
                CanChangeStatusIssue
            };
        }



        public static IEnumerable<PermissionModel> GetAllIssuePermision()
        {
            return new List<PermissionModel>
            {
                CanAccessIssueLabelPermission,

                CanAccessIssuePermission,
                CanViewAllIssuesPermission,
                CanCreateIssuesPermission,
                CanTrackIssuesPermission,
                CanDeleteIssuesPermission,
                CanCahngeStatusIssuesPermission,
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

                CanAccessProductColorPermission,

                CanAccessProductSizePermission
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

                CanAccessProductGalleryPermission,

                CanAccessProductAttributePermission,

                CanAccessProductCommentPermission,

                CanAccessShopShippingByMethodPermission,

                CanAccessTaxPermission,

                CanAccessWareHousePermission,
            };
        }
        #endregion

    }
}