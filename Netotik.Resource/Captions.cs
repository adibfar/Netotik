using Netotik.Resources.Resources.Abstract;
using Netotik.Resources.Resources.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netotik.Resources
{
    public class Captions
    {
        private static IResourceProvider resourceProvider = new DbResourceProvider();


        public static string GetName(string name)
        {
            return (string)resourceProvider.GetResource(name, CultureInfo.CurrentUICulture.Name);
        }

        public static string Save
        {
            get
            {
                return (string)resourceProvider.GetResource("Save", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SaveEdit
        {
            get
            {
                return (string)resourceProvider.GetResource("SaveEdit", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Back
        {
            get
            {
                return (string)resourceProvider.GetResource("Back", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Name
        {
            get
            {
                return (string)resourceProvider.GetResource("Name", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string Email
        {
            get
            {
                return (string)resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Email2
        {
            get
            {
                return (string)resourceProvider.GetResource("Email2", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Tel
        {
            get
            {
                return (string)resourceProvider.GetResource("Tel", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Address
        {
            get
            {
                return (string)resourceProvider.GetResource("Address", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string AdminComment
        {
            get
            {
                return (string)resourceProvider.GetResource("AdminComment", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string AllowComment
        {
            get
            {
                return (string)resourceProvider.GetResource("AllowComment", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string AllowViewComments
        {
            get
            {
                return (string)resourceProvider.GetResource("AllowViewComments", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ApiCode
        {
            get
            {
                return (string)resourceProvider.GetResource("ApiCode", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Body
        {
            get
            {
                return (string)resourceProvider.GetResource("Body", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string BodyOverview
        {
            get
            {
                return (string)resourceProvider.GetResource("BodyOverview", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string CallForPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("CallForPrice", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string CanBuyIfNotInStock
        {
            get
            {
                return (string)resourceProvider.GetResource("CanBuyIfNotInStock", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string DisplayQuantityForUser
        {
            get
            {
                return (string)resourceProvider.GetResource("DisplayQuantityForUser", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Category
        {
            get
            {
                return (string)resourceProvider.GetResource("Category", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string ProductCategory
        {
            get
            {
                return (string)resourceProvider.GetResource("ProductCategory", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string City
        {
            get
            {
                return (string)resourceProvider.GetResource("City", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string CompanyName
        {
            get
            {
                return (string)resourceProvider.GetResource("CompanyName", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string ConfirmPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("ConfirmPassword", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string CountLastContent
        {
            get
            {
                return (string)resourceProvider.GetResource("CountLastContent", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string CountSpecialProduct
        {
            get
            {
                return (string)resourceProvider.GetResource("CountSpecialProduct", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string DeliveryDate
        {
            get
            {
                return (string)resourceProvider.GetResource("DeliveryDate", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Description
        {
            get
            {
                return (string)resourceProvider.GetResource("Description", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string DisableBuy
        {
            get
            {
                return (string)resourceProvider.GetResource("DisableBuy", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string DisableQuantityForUser
        {
            get
            {
                return (string)resourceProvider.GetResource("DisableQuantityForUser", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UserName
        {
            get
            {
                return (string)resourceProvider.GetResource("UserName", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string UserNameOrEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("UserNameOrEmail", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string EndDatePublish
        {
            get
            {
                return (string)resourceProvider.GetResource("EndDatePublish", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string EndTimePublish
        {
            get
            {
                return (string)resourceProvider.GetResource("EndTimePublish", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string FreeShipping
        {
            get
            {
                return (string)resourceProvider.GetResource("FreeShipping", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string FromWeight
        {
            get
            {
                return (string)resourceProvider.GetResource("FromWeight", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string GateWayUrl
        {
            get
            {
                return (string)resourceProvider.GetResource("GateWayUrl", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Image
        {
            get
            {
                return (string)resourceProvider.GetResource("Image", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ImageProfile
        {
            get
            {
                return (string)resourceProvider.GetResource("ImageProfile", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string Active
        {
            get
            {
                return (string)resourceProvider.GetResource("Active", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ActiveStatus
        {
            get
            {
                return (string)resourceProvider.GetResource("ActiveStatus", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string IsDefault
        {
            get
            {
                return (string)resourceProvider.GetResource("IsDefault", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string IsDefaultRoleRegisteredUser
        {
            get
            {
                return (string)resourceProvider.GetResource("IsDefaultRoleRegisteredUser", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string IsMinNotifyQuantityWareHouse
        {
            get
            {
                return (string)resourceProvider.GetResource("IsMinNotifyQuantityWareHouse", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string IsPasKeraye
        {
            get
            {
                return (string)resourceProvider.GetResource("IsPasKeraye", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string IsPublish
        {
            get
            {
                return (string)resourceProvider.GetResource("IsPublish", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string IsRemember
        {
            get
            {
                return (string)resourceProvider.GetResource("IsRemember", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string LastEdit
        {
            get
            {
                return (string)resourceProvider.GetResource("LastEdit", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string LastName
        {
            get
            {
                return (string)resourceProvider.GetResource("LastName", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string LastUserEdit
        {
            get
            {
                return (string)resourceProvider.GetResource("LastUserEdit", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MailServerLogin
        {
            get
            {
                return (string)resourceProvider.GetResource("MailServerLogin", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string MailServerPass
        {
            get
            {
                return (string)resourceProvider.GetResource("MailServerPass", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MailServerPort
        {
            get
            {
                return (string)resourceProvider.GetResource("MailServerPort", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MailServerUrl
        {
            get
            {
                return (string)resourceProvider.GetResource("MailServerUrl", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string Brand
        {
            get
            {
                return (string)resourceProvider.GetResource("Brand", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MaxOffPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("MaxOffPrice", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MessageText
        {
            get
            {
                return (string)resourceProvider.GetResource("MessageText", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string MetaDescription
        {
            get
            {
                return (string)resourceProvider.GetResource("MetaDescription", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MetaKeywords
        {
            get
            {
                return (string)resourceProvider.GetResource("MetaKeywords", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MetaTitle
        {
            get
            {
                return (string)resourceProvider.GetResource("MetaTitle", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string MinNotifyQuantityWareHouse
        {
            get
            {
                return (string)resourceProvider.GetResource("MinNotifyQuantityWareHouse", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MobileNumber
        {
            get
            {
                return (string)resourceProvider.GetResource("MobileNumber", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Order
        {
            get
            {
                return (string)resourceProvider.GetResource("Order", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string OrderMaximumQuantity
        {
            get
            {
                return (string)resourceProvider.GetResource("OrderMaximumQuantity", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Parent
        {
            get
            {
                return (string)resourceProvider.GetResource("Parent", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Password
        {
            get
            {
                return (string)resourceProvider.GetResource("Password", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string PostalCode
        {
            get
            {
                return (string)resourceProvider.GetResource("PostalCode", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string PriceProduct
        {
            get
            {
                return (string)resourceProvider.GetResource("PriceProduct", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string QuantityWareHouse
        {
            get
            {
                return (string)resourceProvider.GetResource("QuantityWareHouse", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SelectFile
        {
            get
            {
                return (string)resourceProvider.GetResource("SelectFile", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SendDate
        {
            get
            {
                return (string)resourceProvider.GetResource("SendDate", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string ShowOnHomePage
        {
            get
            {
                return (string)resourceProvider.GetResource("ShowOnHomePage", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ShowRss
        {
            get
            {
                return (string)resourceProvider.GetResource("ShowRss", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ShowSpecialProduct
        {
            get
            {
                return (string)resourceProvider.GetResource("ShowSpecialProduct", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string SiteName
        {
            get
            {
                return (string)resourceProvider.GetResource("SiteName", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SiteUrl
        {
            get
            {
                return (string)resourceProvider.GetResource("SiteUrl", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string SmsNumber
        {
            get
            {
                return (string)resourceProvider.GetResource("SmsNumber", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SmsPassword
        {
            get
            {
                return (string)resourceProvider.GetResource("SmsPassword", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SmsUserName
        {
            get
            {
                return (string)resourceProvider.GetResource("SmsUserName", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string StartDatePublish
        {
            get
            {
                return (string)resourceProvider.GetResource("StartDatePublish", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string StartTimePublish
        {
            get
            {
                return (string)resourceProvider.GetResource("StartTimePublish", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string State
        {
            get
            {
                return (string)resourceProvider.GetResource("State", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string StateName
        {
            get
            {
                return (string)resourceProvider.GetResource("StateName", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string Subject
        {
            get
            {
                return (string)resourceProvider.GetResource("Subject", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string SystemEmail
        {
            get
            {
                return (string)resourceProvider.GetResource("SystemEmail", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string SystemName
        {
            get
            {
                return (string)resourceProvider.GetResource("SystemName", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Tags
        {
            get
            {
                return (string)resourceProvider.GetResource("Tags", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string Tax
        {
            get
            {
                return (string)resourceProvider.GetResource("Tax", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string TaxExcemt
        {
            get
            {
                return (string)resourceProvider.GetResource("TaxExcemt", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Title
        {
            get
            {
                return (string)resourceProvider.GetResource("Title", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string ToWeight
        {
            get
            {
                return (string)resourceProvider.GetResource("ToWeight", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Url
        {
            get
            {
                return (string)resourceProvider.GetResource("Url", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UserCanRegister
        {
            get
            {
                return (string)resourceProvider.GetResource("UserCanRegister", CultureInfo.CurrentUICulture.Name);
            }

        }
        public static string VideoCode
        {
            get
            {
                return (string)resourceProvider.GetResource("VideoCode", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string WeightProduct
        {
            get
            {
                return (string)resourceProvider.GetResource("WeightProduct", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string CouponRequire
        {
            get
            {
                return (string)resourceProvider.GetResource("CouponRequire", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string CouponCode
        {
            get
            {
                return (string)resourceProvider.GetResource("CouponCode", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string DiscountType
        {
            get
            {
                return (string)resourceProvider.GetResource("DiscountType", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string UsePercentage
        {
            get
            {
                return (string)resourceProvider.GetResource("UsePercentage", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string DiscountPercentage
        {
            get
            {
                return (string)resourceProvider.GetResource("DiscountPercentage", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string DiscountAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("DiscountAmount", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MaximumDiscountAmount
        {
            get
            {
                return (string)resourceProvider.GetResource("MaximumDiscountAmount", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string MaximumDiscountQuantity
        {
            get
            {
                return (string)resourceProvider.GetResource("MaximumDiscountQuantity", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string Logout
        {
            get
            {
                return (string)resourceProvider.GetResource("Logout", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ColorCode
        {
            get
            {
                return (string)resourceProvider.GetResource("ColorCode", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string AddPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("AddPrice", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string BasePrice
        {
            get
            {
                return (string)resourceProvider.GetResource("BasePrice", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string TaxPrice
        {
            get
            {
                return (string)resourceProvider.GetResource("TaxPrice", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string Text
        {
            get
            {
                return (string)resourceProvider.GetResource("Text", CultureInfo.CurrentUICulture.Name);
            }
        }
        public static string ShowInMenu
        {
            get
            {
                return (string)resourceProvider.GetResource("ShowInMenu", CultureInfo.CurrentUICulture.Name);
            }
        }

        public static string Login
        {
            get
            {
                return (string)resourceProvider.GetResource("Login", CultureInfo.CurrentUICulture.Name);
            }
        }


    }
}
