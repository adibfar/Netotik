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

        public static string GetName(string key)
        {
            return resourceProvider.GetResource(key, CultureInfo.CurrentUICulture.Name) as string;
        }

        public static string AccountLockMessage
        {
            get
            {
                return resourceProvider.GetResource("AccountLockMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string CantLogin
        {
            get
            {
                return resourceProvider.GetResource("CantLogin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Save
        {
            get
            {
                return resourceProvider.GetResource("Save", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SaveEdit
        {
            get
            {
                return resourceProvider.GetResource("SaveEdit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Close
        {
            get
            {
                return resourceProvider.GetResource("Close", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Back
        {
            get
            {
                return resourceProvider.GetResource("Back", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Name
        {
            get
            {
                return resourceProvider.GetResource("Name", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string FirstName
        {
            get
            {
                return resourceProvider.GetResource("FirstName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LastName
        {
            get
            {
                return resourceProvider.GetResource("LastName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UserName
        {
            get
            {
                return resourceProvider.GetResource("UserName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UserNameOrEmail
        {
            get
            {
                return resourceProvider.GetResource("UserNameOrEmail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NationalCode
        {
            get
            {
                return resourceProvider.GetResource("NationalCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Email
        {
            get
            {
                return resourceProvider.GetResource("Email", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Tel
        {
            get
            {
                return resourceProvider.GetResource("Tel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Address
        {
            get
            {
                return resourceProvider.GetResource("Address", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string PostalCode
        {
            get
            {
                return resourceProvider.GetResource("PostalCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AdminComment
        {
            get
            {
                return resourceProvider.GetResource("AdminComment", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AllowComment
        {
            get
            {
                return resourceProvider.GetResource("AllowComment", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AllowViewComments
        {
            get
            {
                return resourceProvider.GetResource("AllowViewComments", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ApiCode
        {
            get
            {
                return resourceProvider.GetResource("ApiCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Body
        {
            get
            {
                return resourceProvider.GetResource("Body", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string BodyOverview
        {
            get
            {
                return resourceProvider.GetResource("BodyOverview", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Category
        {
            get
            {
                return resourceProvider.GetResource("Category", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Group
        {
            get
            {
                return resourceProvider.GetResource("Group", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string City
        {
            get
            {
                return resourceProvider.GetResource("City", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RouterName
        {
            get
            {
                return resourceProvider.GetResource("RouterName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string OldPassword
        {
            get
            {
                return resourceProvider.GetResource("OldPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Password
        {
            get
            {
                return resourceProvider.GetResource("Password", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewPassword
        {
            get
            {
                return resourceProvider.GetResource("NewPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ConfirmPassword
        {
            get
            {
                return resourceProvider.GetResource("ConfirmPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CountLastContent
        {
            get
            {
                return resourceProvider.GetResource("CountLastContent", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Description
        {
            get
            {
                return resourceProvider.GetResource("Description", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DisableBuy
        {
            get
            {
                return resourceProvider.GetResource("DisableBuy", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Date
        {
            get
            {
                return resourceProvider.GetResource("Date", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string StartDatePublish
        {
            get
            {
                return resourceProvider.GetResource("StartDatePublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string StartTimePublish
        {
            get
            {
                return resourceProvider.GetResource("StartTimePublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EndDatePublish
        {
            get
            {
                return resourceProvider.GetResource("EndDatePublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EndTimePublish
        {
            get
            {
                return resourceProvider.GetResource("EndTimePublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string GateWayUrl
        {
            get
            {
                return resourceProvider.GetResource("GateWayUrl", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Image
        {
            get
            {
                return resourceProvider.GetResource("Image", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ImageProfile
        {
            get
            {
                return resourceProvider.GetResource("ImageProfile", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Active
        {
            get
            {
                return resourceProvider.GetResource("Active", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ActiveStatus
        {
            get
            {
                return resourceProvider.GetResource("ActiveStatus", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IsDefault
        {
            get
            {
                return resourceProvider.GetResource("IsDefault", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IsDefaultRoleRegisteredUser
        {
            get
            {
                return resourceProvider.GetResource("IsDefaultRoleRegisteredUser", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Text
        {
            get
            {
                return resourceProvider.GetResource("Text", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ShowInMenu
        {
            get
            {
                return resourceProvider.GetResource("ShowInMenu", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IsPasKeraye
        {
            get
            {
                return resourceProvider.GetResource("IsPasKeraye", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IsPublish
        {
            get
            {
                return resourceProvider.GetResource("IsPublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RememberMe
        {
            get
            {
                return resourceProvider.GetResource("RememberMe", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LastEdit
        {
            get
            {
                return resourceProvider.GetResource("LastEdit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LastUserEdit
        {
            get
            {
                return resourceProvider.GetResource("LastUserEdit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MailServerLogin
        {
            get
            {
                return resourceProvider.GetResource("MailServerLogin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MailServerPass
        {
            get
            {
                return resourceProvider.GetResource("MailServerPass", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MailServerPort
        {
            get
            {
                return resourceProvider.GetResource("MailServerPort", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MailServerUrl
        {
            get
            {
                return resourceProvider.GetResource("MailServerUrl", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Brand
        {
            get
            {
                return resourceProvider.GetResource("Brand", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MessageText
        {
            get
            {
                return resourceProvider.GetResource("MessageText", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Message
        {
            get
            {
                return resourceProvider.GetResource("Message", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MetaDescription
        {
            get
            {
                return resourceProvider.GetResource("MetaDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MetaKeywords
        {
            get
            {
                return resourceProvider.GetResource("MetaKeywords", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MetaTitle
        {
            get
            {
                return resourceProvider.GetResource("MetaTitle", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MobileNumber
        {
            get
            {
                return resourceProvider.GetResource("MobileNumber", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DisplayOrder
        {
            get
            {
                return resourceProvider.GetResource("DisplayOrder", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Parent
        {
            get
            {
                return resourceProvider.GetResource("Parent", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CouponRequire
        {
            get
            {
                return resourceProvider.GetResource("CouponRequire", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CouponCode
        {
            get
            {
                return resourceProvider.GetResource("CouponCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MaximumDiscountAmount
        {
            get
            {
                return resourceProvider.GetResource("MaximumDiscountAmount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DiscountPercentage
        {
            get
            {
                return resourceProvider.GetResource("DiscountPercentage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UsePercentage
        {
            get
            {
                return resourceProvider.GetResource("UsePercentage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DiscountType
        {
            get
            {
                return resourceProvider.GetResource("DiscountType", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MaximumDiscountQuantity
        {
            get
            {
                return resourceProvider.GetResource("MaximumDiscountQuantity", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DiscountAmount
        {
            get
            {
                return resourceProvider.GetResource("DiscountAmount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string PriceProduct
        {
            get
            {
                return resourceProvider.GetResource("PriceProduct", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string QuantityWareHouse
        {
            get
            {
                return resourceProvider.GetResource("QuantityWareHouse", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SelectFile
        {
            get
            {
                return resourceProvider.GetResource("SelectFile", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string HomePageTitle
        {
            get
            {
                return resourceProvider.GetResource("HomePageTitle", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string HomePageDescription
        {
            get
            {
                return resourceProvider.GetResource("HomePageDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string HomePageKeywords
        {
            get
            {
                return resourceProvider.GetResource("HomePageKeywords", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SiteUrl
        {
            get
            {
                return resourceProvider.GetResource("SiteUrl", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SmsNumber
        {
            get
            {
                return resourceProvider.GetResource("SmsNumber", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SmsPassword
        {
            get
            {
                return resourceProvider.GetResource("SmsPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SmsUsername
        {
            get
            {
                return resourceProvider.GetResource("SmsUsername", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string State
        {
            get
            {
                return resourceProvider.GetResource("State", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string StateName
        {
            get
            {
                return resourceProvider.GetResource("StateName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Subject
        {
            get
            {
                return resourceProvider.GetResource("Subject", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SystemEmail
        {
            get
            {
                return resourceProvider.GetResource("SystemEmail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SystemName
        {
            get
            {
                return resourceProvider.GetResource("SystemName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Tags
        {
            get
            {
                return resourceProvider.GetResource("Tags", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Tax
        {
            get
            {
                return resourceProvider.GetResource("Tax", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string TaxExcemt
        {
            get
            {
                return resourceProvider.GetResource("TaxExcemt", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Title
        {
            get
            {
                return resourceProvider.GetResource("Title", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Url
        {
            get
            {
                return resourceProvider.GetResource("Url", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UserCanRegister
        {
            get
            {
                return resourceProvider.GetResource("UserCanRegister", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Logout
        {
            get
            {
                return resourceProvider.GetResource("Logout", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Login
        {
            get
            {
                return resourceProvider.GetResource("Login", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string HasSideBar
        {
            get
            {
                return resourceProvider.GetResource("HasSideBar", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DontShowBlog
        {
            get
            {
                return resourceProvider.GetResource("DontShowBlog", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DontShowImageDetail
        {
            get
            {
                return resourceProvider.GetResource("DontShowImageDetail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Language
        {
            get
            {
                return resourceProvider.GetResource("Language", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ImageAlt
        {
            get
            {
                return resourceProvider.GetResource("ImageAlt", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Icon
        {
            get
            {
                return resourceProvider.GetResource("Icon", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Location
        {
            get
            {
                return resourceProvider.GetResource("Location", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LanguageCulture
        {
            get
            {
                return resourceProvider.GetResource("LanguageCulture", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UrlCode
        {
            get
            {
                return resourceProvider.GetResource("UrlCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Flag
        {
            get
            {
                return resourceProvider.GetResource("Flag", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IsRtl
        {
            get
            {
                return resourceProvider.GetResource("IsRtl", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string XmlResource
        {
            get
            {
                return resourceProvider.GetResource("XmlResource", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Facebook
        {
            get
            {
                return resourceProvider.GetResource("Facebook", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Linkedin
        {
            get
            {
                return resourceProvider.GetResource("Linkedin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Twitter
        {
            get
            {
                return resourceProvider.GetResource("Twitter", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Website
        {
            get
            {
                return resourceProvider.GetResource("Website", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Instagram
        {
            get
            {
                return resourceProvider.GetResource("Instagram", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ShortBio
        {
            get
            {
                return resourceProvider.GetResource("ShortBio", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RouterAddress
        {
            get
            {
                return resourceProvider.GetResource("RouterAddress", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RouterUsername
        {
            get
            {
                return resourceProvider.GetResource("RouterUsername", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RouterPassword
        {
            get
            {
                return resourceProvider.GetResource("RouterPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ApiPort
        {
            get
            {
                return resourceProvider.GetResource("ApiPort", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UsermanCustomer
        {
            get
            {
                return resourceProvider.GetResource("UsermanCustomer", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IsCloudActive
        {
            get
            {
                return resourceProvider.GetResource("IsCloudActive", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Dashboard
        {
            get
            {
                return resourceProvider.GetResource("Dashboard", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Content
        {
            get
            {
                return resourceProvider.GetResource("Content", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Subjects
        {
            get
            {
                return resourceProvider.GetResource("Subjects", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Contents
        {
            get
            {
                return resourceProvider.GetResource("Contents", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Sliders
        {
            get
            {
                return resourceProvider.GetResource("Sliders", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Menues
        {
            get
            {
                return resourceProvider.GetResource("Menues", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string IndexSections
        {
            get
            {
                return resourceProvider.GetResource("IndexSections", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Comments
        {
            get
            {
                return resourceProvider.GetResource("Comments", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ContactUsMessages
        {
            get
            {
                return resourceProvider.GetResource("ContactUsMessages", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Shop
        {
            get
            {
                return resourceProvider.GetResource("Shop", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Accounting
        {
            get
            {
                return resourceProvider.GetResource("Accounting", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Support
        {
            get
            {
                return resourceProvider.GetResource("Support", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UsersRoles
        {
            get
            {
                return resourceProvider.GetResource("UsersRoles", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Users
        {
            get
            {
                return resourceProvider.GetResource("Users", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Roles
        {
            get
            {
                return resourceProvider.GetResource("Roles", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Resellers
        {
            get
            {
                return resourceProvider.GetResource("Resellers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Configuration
        {
            get
            {
                return resourceProvider.GetResource("Configuration", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string PublicConfig
        {
            get
            {
                return resourceProvider.GetResource("PublicConfig", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string States
        {
            get
            {
                return resourceProvider.GetResource("States", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Cities
        {
            get
            {
                return resourceProvider.GetResource("Cities", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Languages
        {
            get
            {
                return resourceProvider.GetResource("Languages", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ViewStatistics
        {
            get
            {
                return resourceProvider.GetResource("ViewStatistics", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ViewAllStatistics
        {
            get
            {
                return resourceProvider.GetResource("ViewAllStatistics", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ViewCountryStatistics
        {
            get
            {
                return resourceProvider.GetResource("ViewCountryStatistics", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Send
        {
            get
            {
                return resourceProvider.GetResource("Send", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SendImage
        {
            get
            {
                return resourceProvider.GetResource("SendImage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SendingData
        {
            get
            {
                return resourceProvider.GetResource("SendingData", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Profile
        {
            get
            {
                return resourceProvider.GetResource("Profile", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewCity
        {
            get
            {
                return resourceProvider.GetResource("NewCity", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditCity
        {
            get
            {
                return resourceProvider.GetResource("EditCity", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditComment
        {
            get
            {
                return resourceProvider.GetResource("EditComment", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ReplyComment
        {
            get
            {
                return resourceProvider.GetResource("ReplyComment", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewContent
        {
            get
            {
                return resourceProvider.GetResource("NewContent", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditContents
        {
            get
            {
                return resourceProvider.GetResource("EditContents", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewSubject
        {
            get
            {
                return resourceProvider.GetResource("NewSubject", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditSubject
        {
            get
            {
                return resourceProvider.GetResource("EditSubject", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewTag
        {
            get
            {
                return resourceProvider.GetResource("NewTag", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditTag
        {
            get
            {
                return resourceProvider.GetResource("EditTag", CultureInfo.CurrentUICulture.Name) as string;
            }
        }



        public static string Newest
        {
            get
            {
                return resourceProvider.GetResource("Newest", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewIndexSectoin
        {
            get
            {
                return resourceProvider.GetResource("NewIndexSectoin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string EditIndexSectoin
        {
            get
            {
                return resourceProvider.GetResource("EditIndexSectoin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewMenu
        {
            get
            {
                return resourceProvider.GetResource("NewMenu", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditMenu
        {
            get
            {
                return resourceProvider.GetResource("EditMenu", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewLanguage
        {
            get
            {
                return resourceProvider.GetResource("NewLanguage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditLanguage
        {
            get
            {
                return resourceProvider.GetResource("EditLanguage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewLanguageWithXml
        {
            get
            {
                return resourceProvider.GetResource("NewLanguageWithXml", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditString
        {
            get
            {
                return resourceProvider.GetResource("EditString", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewRole
        {
            get
            {
                return resourceProvider.GetResource("NewRole", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditRole
        {
            get
            {
                return resourceProvider.GetResource("EditRole", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewState
        {
            get
            {
                return resourceProvider.GetResource("NewState", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditState
        {
            get
            {
                return resourceProvider.GetResource("EditState", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewSlider
        {
            get
            {
                return resourceProvider.GetResource("NewSlider", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditSlider
        {
            get
            {
                return resourceProvider.GetResource("EditSlider", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewUser
        {
            get
            {
                return resourceProvider.GetResource("NewUser", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditUser
        {
            get
            {
                return resourceProvider.GetResource("EditUser", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NewReseller
        {
            get
            {
                return resourceProvider.GetResource("NewReseller", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EditReseller
        {
            get
            {
                return resourceProvider.GetResource("EditReseller", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ActivationAccount
        {
            get
            {
                return resourceProvider.GetResource("ActivationAccount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Sended
        {
            get
            {
                return resourceProvider.GetResource("Sended", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Submit
        {
            get
            {
                return resourceProvider.GetResource("Submit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SubmitRequest
        {
            get
            {
                return resourceProvider.GetResource("SubmitRequest", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Register
        {
            get
            {
                return resourceProvider.GetResource("Register", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LoginToSystem
        {
            get
            {
                return resourceProvider.GetResource("LoginToSystem", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Comment
        {
            get
            {
                return resourceProvider.GetResource("Comment", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Count
        {
            get
            {
                return resourceProvider.GetResource("Count", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ContinueContent
        {
            get
            {
                return resourceProvider.GetResource("ContinueContent", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Populars
        {
            get
            {
                return resourceProvider.GetResource("Populars", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LastContent
        {
            get
            {
                return resourceProvider.GetResource("LastContent", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string HomeMenu
        {
            get
            {
                return resourceProvider.GetResource("HomeMenu", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AdminPanel
        {
            get
            {
                return resourceProvider.GetResource("AdminPanel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ResellerPanel
        {
            get
            {
                return resourceProvider.GetResource("ResellerPanel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RouterPanel
        {
            get
            {
                return resourceProvider.GetResource("RouterPanel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UserPanel
        {
            get
            {
                return resourceProvider.GetResource("UserPanel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Netotik
        {
            get
            {
                return resourceProvider.GetResource("Netotik", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ContactUs
        {
            get
            {
                return resourceProvider.GetResource("ContactUs", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Blog
        {
            get
            {
                return resourceProvider.GetResource("Blog", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Management
        {
            get
            {
                return resourceProvider.GetResource("Management", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Control
        {
            get
            {
                return resourceProvider.GetResource("Control", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Network
        {
            get
            {
                return resourceProvider.GetResource("Network", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ForgetIt
        {
            get
            {
                return resourceProvider.GetResource("ForgetIt", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LoginToUserAccount
        {
            get
            {
                return resourceProvider.GetResource("LoginToUserAccount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string WatchIntroduceNetotikVideo
        {
            get
            {
                return resourceProvider.GetResource("WatchIntroduceNetotikVideo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ChangePassword
        {
            get
            {
                return resourceProvider.GetResource("ChangePassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string OnlineUsers
        {
            get
            {
                return resourceProvider.GetResource("OnlineUsers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ResellerCode
        {
            get
            {
                return resourceProvider.GetResource("ResellerCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string DontHaveAccount
        {
            get
            {
                return resourceProvider.GetResource("DontHaveAccount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NetotikDescribtion
        {
            get
            {
                return resourceProvider.GetResource("NetotikDescribtion", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CopyRight
        {
            get
            {
                return resourceProvider.GetResource("CopyRight", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CitiesList
        {
            get
            {
                return resourceProvider.GetResource("CitiesList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CommentsList
        {
            get
            {
                return resourceProvider.GetResource("CommentsList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MessagesList
        {
            get
            {
                return resourceProvider.GetResource("MessagesList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SubjectsList
        {
            get
            {
                return resourceProvider.GetResource("SubjectsList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ContentsList
        {
            get
            {
                return resourceProvider.GetResource("ContentsList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string TagsList
        {
            get
            {
                return resourceProvider.GetResource("TagsList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LanguagesList
        {
            get
            {
                return resourceProvider.GetResource("LanguagesList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MenuesList
        {
            get
            {
                return resourceProvider.GetResource("MenuesList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RolesList
        {
            get
            {
                return resourceProvider.GetResource("RolesList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SlidersList
        {
            get
            {
                return resourceProvider.GetResource("SlidersList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UsersList
        {
            get
            {
                return resourceProvider.GetResource("UsersList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ResellersList
        {
            get
            {
                return resourceProvider.GetResource("ResellersList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string StatesList
        {
            get
            {
                return resourceProvider.GetResource("StatesList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Seo
        {
            get
            {
                return resourceProvider.GetResource("Seo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AddError
        {
            get
            {
                return resourceProvider.GetResource("AddError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AddSuccess
        {
            get
            {
                return resourceProvider.GetResource("AddSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string AdminUserCreated
        {
            get
            {
                return resourceProvider.GetResource("AdminUserCreated", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string PasswordEasy
        {
            get
            {
                return resourceProvider.GetResource("PasswordEasy", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string WillSendForgetPasswrordMessage
        {
            get
            {
                return resourceProvider.GetResource("WillSendForgetPasswrordMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string WillSendActivationAccountMessage
        {
            get
            {
                return resourceProvider.GetResource("WillSendActivationAccountMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ChangePasswordSuccess
        {
            get
            {
                return resourceProvider.GetResource("ChangePasswordSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ForgetPasswordEmailSended
        {
            get
            {
                return resourceProvider.GetResource("ForgetPasswordEmailSended", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ChangePasswordFail
        {
            get
            {
                return resourceProvider.GetResource("ChangePasswordFail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ConfirmPasswordNotValid
        {
            get
            {
                return resourceProvider.GetResource("ConfirmPasswordNotValid", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ExistError
        {
            get
            {
                return resourceProvider.GetResource("ExistError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string NotValidError
        {
            get
            {
                return resourceProvider.GetResource("NotValidError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RequiredError
        {
            get
            {
                return resourceProvider.GetResource("RequiredError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MaxLengthError
        {
            get
            {
                return resourceProvider.GetResource("MaxLengthError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MinLengthError
        {
            get
            {
                return resourceProvider.GetResource("MinLengthError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RangeError
        {
            get
            {
                return resourceProvider.GetResource("RangeError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LengthError
        {
            get
            {
                return resourceProvider.GetResource("LengthError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string InvalidDataError
        {
            get
            {
                return resourceProvider.GetResource("InvalidDataError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LoginFail
        {
            get
            {
                return resourceProvider.GetResource("LoginFail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MissionFail
        {
            get
            {
                return resourceProvider.GetResource("MissionFail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string MissionSuccess
        {
            get
            {
                return resourceProvider.GetResource("MissionSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RemoveError
        {
            get
            {
                return resourceProvider.GetResource("RemoveError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RemoveSuccess
        {
            get
            {
                return resourceProvider.GetResource("RemoveSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SelectRole
        {
            get
            {
                return resourceProvider.GetResource("SelectRole", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SettingUpdateError
        {
            get
            {
                return resourceProvider.GetResource("SettingUpdateError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SettingUpdateSuccess
        {
            get
            {
                return resourceProvider.GetResource("SettingUpdateSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UpdateError
        {
            get
            {
                return resourceProvider.GetResource("UpdateError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UpdateSettingFail
        {
            get
            {
                return resourceProvider.GetResource("UpdateSettingFail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UpdateSettingSuccess
        {
            get
            {
                return resourceProvider.GetResource("UpdateSettingSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UpdateSuccess
        {
            get
            {
                return resourceProvider.GetResource("UpdateSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UserIsBanned
        {
            get
            {
                return resourceProvider.GetResource("UserIsBanned", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UserNameOrEmailReqired
        {
            get
            {
                return resourceProvider.GetResource("UserNameOrEmailReqired", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string JustEnglishNumeric
        {
            get
            {
                return resourceProvider.GetResource("JustEnglishNumeric", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ConfirmEmailMessage
        {
            get
            {
                return resourceProvider.GetResource("ConfirmEmailMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CountCommentSended
        {
            get
            {
                return resourceProvider.GetResource("CountCommentSended", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string RegisterDone
        {
            get
            {
                return resourceProvider.GetResource("RegisterDone", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string PrivacyMessage
        {
            get
            {
                return resourceProvider.GetResource("PrivacyMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string UsernameOrPasswordWrong
        {
            get
            {
                return resourceProvider.GetResource("UsernameOrPasswordWrong", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ErrorLogin
        {
            get
            {
                return resourceProvider.GetResource("ErrorLogin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ActivationError
        {
            get
            {
                return resourceProvider.GetResource("ActivationError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ForgetPasswordMailMessage
        {
            get
            {
                return resourceProvider.GetResource("ForgetPasswordMailMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ForgetPasswordMailSubject
        {
            get
            {
                return resourceProvider.GetResource("ForgetPasswordMailSubject", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string EmailNotFound
        {
            get
            {
                return resourceProvider.GetResource("EmailNotFound", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string YourAccountIsBlock
        {
            get
            {
                return resourceProvider.GetResource("YourAccountIsBlock", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ActivationMailMessage
        {
            get
            {
                return resourceProvider.GetResource("ActivationMailMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string ActivationMailSubject
        {
            get
            {
                return resourceProvider.GetResource("ActivationMailSubject", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CommentRegisterSuccess
        {
            get
            {
                return resourceProvider.GetResource("CommentRegisterSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string CommentRegisterFail
        {
            get
            {
                return resourceProvider.GetResource("CommentRegisterFail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Thanks
        {
            get
            {
                return resourceProvider.GetResource("Thanks", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string SelectRoles
        {
            get
            {
                return resourceProvider.GetResource("SelectRoles", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ResellerLogin
        {
            get
            {
                return resourceProvider.GetResource("ResellerLogin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Footer
        {
            get
            {
                return resourceProvider.GetResource("Footer", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Header
        {
            get
            {
                return resourceProvider.GetResource("Header", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Default
        {
            get
            {
                return resourceProvider.GetResource("Default", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Enable
        {
            get
            {
                return resourceProvider.GetResource("Enable", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Disable
        {
            get
            {
                return resourceProvider.GetResource("Disable", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Enabled
        {
            get
            {
                return resourceProvider.GetResource("Enabled", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Disabled
        {
            get
            {
                return resourceProvider.GetResource("Disabled", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Accepted
        {
            get
            {
                return resourceProvider.GetResource("Accepted", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string WaitingForAccept
        {
            get
            {
                return resourceProvider.GetResource("WaitingForAccept", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string NotAccept
        {
            get
            {
                return resourceProvider.GetResource("NotAccept", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ShowMessage
        {
            get
            {
                return resourceProvider.GetResource("ShowMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string NotPublish
        {
            get
            {
                return resourceProvider.GetResource("NotPublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Answer
        {
            get
            {
                return resourceProvider.GetResource("Answer", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Accept
        {
            get
            {
                return resourceProvider.GetResource("Accept", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string DontAccept
        {
            get
            {
                return resourceProvider.GetResource("DontAccept", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string AcceptPublish
        {
            get
            {
                return resourceProvider.GetResource("AcceptPublish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Published
        {
            get
            {
                return resourceProvider.GetResource("Published", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Action
        {
            get
            {
                return resourceProvider.GetResource("Action", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Edit
        {
            get
            {
                return resourceProvider.GetResource("Edit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Remove
        {
            get
            {
                return resourceProvider.GetResource("Remove", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Texts
        {
            get
            {
                return resourceProvider.GetResource("Texts", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string DownloadLanguage
        {
            get
            {
                return resourceProvider.GetResource("DownloadLanguage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Systemical
        {
            get
            {
                return resourceProvider.GetResource("Systemical", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ShowName
        {
            get
            {
                return resourceProvider.GetResource("ShowName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ActiveYourAccount
        {
            get
            {
                return resourceProvider.GetResource("ActiveYourAccount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ChangePlan
        {
            get
            {
                return resourceProvider.GetResource("ChangePlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string About
        {
            get
            {
                return resourceProvider.GetResource("About", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string FoodMenu
        {
            get
            {
                return resourceProvider.GetResource("FoodMenu", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Details
        {
            get
            {
                return resourceProvider.GetResource("Details", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TimeValidity
        {
            get
            {
                return resourceProvider.GetResource("TimeValidity", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TillDate
        {
            get
            {
                return resourceProvider.GetResource("TillDate", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Online
        {
            get
            {
                return resourceProvider.GetResource("Online", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PlanName
        {
            get
            {
                return resourceProvider.GetResource("PlanName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Enrollment
        {
            get
            {
                return resourceProvider.GetResource("Enrollment", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string All
        {
            get
            {
                return resourceProvider.GetResource("All", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Remain
        {
            get
            {
                return resourceProvider.GetResource("Remain", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Used
        {
            get
            {
                return resourceProvider.GetResource("Used", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ConnectionHistory
        {
            get
            {
                return resourceProvider.GetResource("ConnectionHistory", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ConnectionDetails
        {
            get
            {
                return resourceProvider.GetResource("ConnectionDetails", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ChartOfMonthAndYear
        {
            get
            {
                return resourceProvider.GetResource("ChartOfMonthAndYear", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ChartOf7LastDays
        {
            get
            {
                return resourceProvider.GetResource("ChartOf7LastDays", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string OneDayAgo
        {
            get
            {
                return resourceProvider.GetResource("OneDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TwoDayAgo
        {
            get
            {
                return resourceProvider.GetResource("TwoDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ThreeDayAgo
        {
            get
            {
                return resourceProvider.GetResource("ThreeDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FourDayAgo
        {
            get
            {
                return resourceProvider.GetResource("FourDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FiveDayAgo
        {
            get
            {
                return resourceProvider.GetResource("FiveDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SixDayAgo
        {
            get
            {
                return resourceProvider.GetResource("SixDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SevenDayAgo
        {
            get
            {
                return resourceProvider.GetResource("SevenDayAgo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Download
        {
            get
            {
                return resourceProvider.GetResource("Download", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Upload
        {
            get
            {
                return resourceProvider.GetResource("Upload", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string JustEnglish
        {
            get
            {
                return resourceProvider.GetResource("JustEnglish", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MakeCard
        {
            get
            {
                return resourceProvider.GetResource("MakeCard", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SelectPlan
        {
            get
            {
                return resourceProvider.GetResource("SelectPlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string BuyPlan
        {
            get
            {
                return resourceProvider.GetResource("BuyPlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string BuyPackage
        {
            get
            {
                return resourceProvider.GetResource("BuyPackage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Charts
        {
            get
            {
                return resourceProvider.GetResource("Charts", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ChartOf30LastDays
        {
            get
            {
                return resourceProvider.GetResource("ChartOf30LastDays", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ChartOf365LastDays
        {
            get
            {
                return resourceProvider.GetResource("ChartOf365LastDays", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ChartOf30LastSession
        {
            get
            {
                return resourceProvider.GetResource("ChartOf30LastSession", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Properties
        {
            get
            {
                return resourceProvider.GetResource("Properties", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Connections
        {
            get
            {
                return resourceProvider.GetResource("Connections", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Time
        {
            get
            {
                return resourceProvider.GetResource("Time", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Traffic
        {
            get
            {
                return resourceProvider.GetResource("Traffic", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PrintReport
        {
            get
            {
                return resourceProvider.GetResource("PrintReport", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string Secend
        {
            get
            {
                return resourceProvider.GetResource("Secend", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Minute
        {
            get
            {
                return resourceProvider.GetResource("Minute", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Hour
        {
            get
            {
                return resourceProvider.GetResource("Hour", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Day
        {
            get
            {
                return resourceProvider.GetResource("Day", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Week
        {
            get
            {
                return resourceProvider.GetResource("Week", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Month
        {
            get
            {
                return resourceProvider.GetResource("Month", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UploadSpeedLimit
        {
            get
            {
                return resourceProvider.GetResource("UploadSpeedLimit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string DownloadSpeedLimit
        {
            get
            {
                return resourceProvider.GetResource("DownloadSpeedLimit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TrafficRemain
        {
            get
            {
                return resourceProvider.GetResource("TrafficRemain", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string DownloadUploadTrafficLimit
        {
            get
            {
                return resourceProvider.GetResource("DownloadUploadTrafficLimit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UploadTrafficLimit
        {
            get
            {
                return resourceProvider.GetResource("UploadTrafficLimit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string DownloadTrafficLimit
        {
            get
            {
                return resourceProvider.GetResource("DownloadTrafficLimit", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ShowAll
        {
            get
            {
                return resourceProvider.GetResource("ShowAll", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Hide
        {
            get
            {
                return resourceProvider.GetResource("Hide", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string EditTable
        {
            get
            {
                return resourceProvider.GetResource("EditTable", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Error
        {
            get
            {
                return resourceProvider.GetResource("Error", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string IPPORTClientError
        {
            get
            {
                return resourceProvider.GetResource("IPPORTClientError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UserPasswordClientError
        {
            get
            {
                return resourceProvider.GetResource("UserPasswordClientError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UsermanagerClientError
        {
            get
            {
                return resourceProvider.GetResource("UsermanagerClientError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Inaccessible
        {
            get
            {
                return resourceProvider.GetResource("Inaccessible", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Saturday
        {
            get
            {
                return resourceProvider.GetResource("Saturday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Sunday
        {
            get
            {
                return resourceProvider.GetResource("Sunday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Monday
        {
            get
            {
                return resourceProvider.GetResource("Monday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Tuesday
        {
            get
            {
                return resourceProvider.GetResource("Tuesday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Wednesday
        {
            get
            {
                return resourceProvider.GetResource("Wednesday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Thursday
        {
            get
            {
                return resourceProvider.GetResource("Thursday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Friday
        {
            get
            {
                return resourceProvider.GetResource("Friday", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NoConnection
        {
            get
            {
                return resourceProvider.GetResource("NoConnection", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Unlimited
        {
            get
            {
                return resourceProvider.GetResource("Unlimited", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Approximate
        {
            get
            {
                return resourceProvider.GetResource("Approximate", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Information
        {
            get
            {
                return resourceProvider.GetResource("Information", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string User
        {
            get
            {
                return resourceProvider.GetResource("User", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterInformation
        {
            get
            {
                return resourceProvider.GetResource("RouterInformation", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ClientPanelPermission
        {
            get
            {
                return resourceProvider.GetResource("ClientPanelPermission", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterLoginLink
        {
            get
            {
                return resourceProvider.GetResource("RouterLoginLink", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterLoginMessage
        {
            get
            {
                return resourceProvider.GetResource("RouterLoginMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FirstNameAndLastName
        {
            get
            {
                return resourceProvider.GetResource("FirstNameAndLastName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string CreateSuccsessGoToEmail
        {
            get
            {
                return resourceProvider.GetResource("CreateSuccsessGoToEmail", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikConfig
        {
            get
            {
                return resourceProvider.GetResource("MikrotikConfig", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Attention
        {
            get
            {
                return resourceProvider.GetResource("Attention", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterPasswordEmptyInformation
        {
            get
            {
                return resourceProvider.GetResource("RouterPasswordEmptyInformation", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UserManager
        {
            get
            {
                return resourceProvider.GetResource("UserManager", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PlanList
        {
            get
            {
                return resourceProvider.GetResource("PlanList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NewPlan
        {
            get
            {
                return resourceProvider.GetResource("NewPlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Actions
        {
            get
            {
                return resourceProvider.GetResource("Actions", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Hotspot
        {
            get
            {
                return resourceProvider.GetResource("Hotspot", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Access
        {
            get
            {
                return resourceProvider.GetResource("Access", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Price
        {
            get
            {
                return resourceProvider.GetResource("Price", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Toman
        {
            get
            {
                return resourceProvider.GetResource("Toman", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SharedUsers
        {
            get
            {
                return resourceProvider.GetResource("SharedUsers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string StartAt
        {
            get
            {
                return resourceProvider.GetResource("StartAt", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FirstConnection
        {
            get
            {
                return resourceProvider.GetResource("FirstConnection", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PlanBind
        {
            get
            {
                return resourceProvider.GetResource("PlanBind", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FromTime
        {
            get
            {
                return resourceProvider.GetResource("FromTime", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TillTime
        {
            get
            {
                return resourceProvider.GetResource("TillTime", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string WeekDays
        {
            get
            {
                return resourceProvider.GetResource("WeekDays", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ConnectionOnlineTime
        {
            get
            {
                return resourceProvider.GetResource("ConnectionOnlineTime", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string AccountValidity
        {
            get
            {
                return resourceProvider.GetResource("AccountValidity", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ZeroToUnlimited
        {
            get
            {
                return resourceProvider.GetResource("ZeroToUnlimited", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TerminateConnection
        {
            get
            {
                return resourceProvider.GetResource("TerminateConnection", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Reset
        {
            get
            {
                return resourceProvider.GetResource("Reset", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PrintList
        {
            get
            {
                return resourceProvider.GetResource("PrintList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NewMultiUser
        {
            get
            {
                return resourceProvider.GetResource("NewMultiUser", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string LastConnection
        {
            get
            {
                return resourceProvider.GetResource("LastConnection", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterModel
        {
            get
            {
                return resourceProvider.GetResource("RouterModel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NotDetect
        {
            get
            {
                return resourceProvider.GetResource("NotDetect", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string DeviceName
        {
            get
            {
                return resourceProvider.GetResource("DeviceName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string LicenseVersion
        {
            get
            {
                return resourceProvider.GetResource("LicenseVersion", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MoreInfo
        {
            get
            {
                return resourceProvider.GetResource("MoreInfo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SoftwareSerial
        {
            get
            {
                return resourceProvider.GetResource("SoftwareSerial", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterTime
        {
            get
            {
                return resourceProvider.GetResource("RouterTime", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterDate
        {
            get
            {
                return resourceProvider.GetResource("RouterDate", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterAndServerDateEqual
        {
            get
            {
                return resourceProvider.GetResource("RouterAndServerDateEqual", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ServerDate
        {
            get
            {
                return resourceProvider.GetResource("ServerDate", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string CoreCount
        {
            get
            {
                return resourceProvider.GetResource("CoreCount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Free
        {
            get
            {
                return resourceProvider.GetResource("Free", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string CheckIt
        {
            get
            {
                return resourceProvider.GetResource("CheckIt", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UpdateIsInstall
        {
            get
            {
                return resourceProvider.GetResource("UpdateIsInstall", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UpdateTo
        {
            get
            {
                return resourceProvider.GetResource("UpdateTo", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterOSVersion
        {
            get
            {
                return resourceProvider.GetResource("RouterOSVersion", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UpTime
        {
            get
            {
                return resourceProvider.GetResource("UpTime", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Structure
        {
            get
            {
                return resourceProvider.GetResource("Structure", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string HotspotOnlineUsers
        {
            get
            {
                return resourceProvider.GetResource("HotspotOnlineUsers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UsersAccess
        {
            get
            {
                return resourceProvider.GetResource("UsersAccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string HotspotUserAccessDescription
        {
            get
            {
                return resourceProvider.GetResource("HotspotUserAccessDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string New
        {
            get
            {
                return resourceProvider.GetResource("New", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string AddressesAccess
        {
            get
            {
                return resourceProvider.GetResource("AddressesAccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string HotspotAddressesAccessDescription
        {
            get
            {
                return resourceProvider.GetResource("HotspotAddressesAccessDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Add
        {
            get
            {
                return resourceProvider.GetResource("Add", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Reboot
        {
            get
            {
                return resourceProvider.GetResource("Reboot", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRebootDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRebootDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Update
        {
            get
            {
                return resourceProvider.GetResource("Update", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveLogs
        {
            get
            {
                return resourceProvider.GetResource("RemoveLogs", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRemoveLogsDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRemoveLogsDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikReset
        {
            get
            {
                return resourceProvider.GetResource("MikrotikReset", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikResetDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikResetDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string DefualtConfiguration
        {
            get
            {
                return resourceProvider.GetResource("DefualtConfiguration", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikBackup
        {
            get
            {
                return resourceProvider.GetResource("MikrotikBackup", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikBackupDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikBackupDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRestore
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRestore", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRestoreDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRestoreDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SelectBackup
        {
            get
            {
                return resourceProvider.GetResource("SelectBackup", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikResetUsermanagerDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikResetUsermanagerDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikResetUsermanager
        {
            get
            {
                return resourceProvider.GetResource("MikrotikResetUsermanager", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveUsermanagerDB
        {
            get
            {
                return resourceProvider.GetResource("RemoveUsermanagerDB", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveUsermanagerSession
        {
            get
            {
                return resourceProvider.GetResource("RemoveUsermanagerSession", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveUsermanagerHistory
        {
            get
            {
                return resourceProvider.GetResource("RemoveUsermanagerHistory", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveUsermanagerPlan
        {
            get
            {
                return resourceProvider.GetResource("RemoveUsermanagerPlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveUsermanagerUsers
        {
            get
            {
                return resourceProvider.GetResource("RemoveUsermanagerUsers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RemoveUsermanagerLogs
        {
            get
            {
                return resourceProvider.GetResource("RemoveUsermanagerLogs", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikUsermanagerBackup
        {
            get
            {
                return resourceProvider.GetResource("MikrotikUsermanagerBackup", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikUsermanagerBackupDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikUsermanagerBackupDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikUsermanagerRestore
        {
            get
            {
                return resourceProvider.GetResource("MikrotikUsermanagerRestore", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikUsermanagerRestoreDesctiption
        {
            get
            {
                return resourceProvider.GetResource("MikrotikUsermanagerRestoreDesctiption", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string LeaveBlankAllPort
        {
            get
            {
                return resourceProvider.GetResource("LeaveBlankAllPort", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string HotspotAddressesAccessAdd
        {
            get
            {
                return resourceProvider.GetResource("HotspotAddressesAccessAdd", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string HotspotUserAccessAdd
        {
            get
            {
                return resourceProvider.GetResource("HotspotUserAccessAdd", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Router
        {
            get
            {
                return resourceProvider.GetResource("Router", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NothingFound
        {
            get
            {
                return resourceProvider.GetResource("NothingFound", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikBackupMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikBackupMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikUsermanagerBackupMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikUsermanagerBackupMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRemoveLogsMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRemoveLogsMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikResetUsermanagerMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikResetUsermanagerMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ValidateError
        {
            get
            {
                return resourceProvider.GetResource("ValidateError", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikUpdateStartMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikUpdateStartMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRebootMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRebootMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikResetMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikResetMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRestoreUsermanagerMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRestoreUsermanagerMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MikrotikRestoreMessage
        {
            get
            {
                return resourceProvider.GetResource("MikrotikRestoreMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string OnlineTime
        {
            get
            {
                return resourceProvider.GetResource("OnlineTime", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Reports
        {
            get
            {
                return resourceProvider.GetResource("Reports", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UsermanagerTrafficUsageDescription
        {
            get
            {
                return resourceProvider.GetResource("UsermanagerTrafficUsageDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UsermanagerTrafficUsage
        {
            get
            {
                return resourceProvider.GetResource("UsermanagerTrafficUsage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MaxTimeConnectedDescription
        {
            get
            {
                return resourceProvider.GetResource("MaxTimeConnectedDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MaxTimeConnected
        {
            get
            {
                return resourceProvider.GetResource("MaxTimeConnected", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MinTrafficUsageDescription
        {
            get
            {
                return resourceProvider.GetResource("MinTrafficUsageDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MinTrafficUsage
        {
            get
            {
                return resourceProvider.GetResource("MinTrafficUsage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MaxTrafficUsageDescription
        {
            get
            {
                return resourceProvider.GetResource("MaxTrafficUsageDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MaxTrafficUsage
        {
            get
            {
                return resourceProvider.GetResource("MaxTrafficUsage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Logs
        {
            get
            {
                return resourceProvider.GetResource("Logs", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UsermanagerLogsMessage
        {
            get
            {
                return resourceProvider.GetResource("UsermanagerLogsMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ReportOfSessions
        {
            get
            {
                return resourceProvider.GetResource("ReportOfSessions", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string ReportOfSessionsDescription
        {
            get
            {
                return resourceProvider.GetResource("ReportOfSessionsDescription", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        //<!--======================Messages Ehsan EM===========================-->
        public static string LastPlanChanges
        {
            get
            {
                return resourceProvider.GetResource("LastPlanChanges", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PlanCount
        {
            get
            {
                return resourceProvider.GetResource("PlanCount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UsersCount
        {
            get
            {
                return resourceProvider.GetResource("UsersCount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string RegisterSetting
        {
            get
            {
                return resourceProvider.GetResource("RegisterSetting", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SendEmailUserPass
        {
            get
            {
                return resourceProvider.GetResource("SendEmailUserPass", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SendSmsUserPass
        {
            get
            {
                return resourceProvider.GetResource("SendSmsUserPass", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Age
        {
            get
            {
                return resourceProvider.GetResource("Age", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string BirthDate
        {
            get
            {
                return resourceProvider.GetResource("BirthDate", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string IsMale
        {
            get
            {
                return resourceProvider.GetResource("IsMale", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ShowType
        {
            get
            {
                return resourceProvider.GetResource("ShowType", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string RequiredType
        {
            get
            {
                return resourceProvider.GetResource("RequiredType", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string NoneType
        {
            get
            {
                return resourceProvider.GetResource("NoneType", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsRouterPasswordChange
        {
            get
            {
                return resourceProvider.GetResource("SmsRouterPasswordChange", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsRouterLogins
        {
            get
            {
                return resourceProvider.GetResource("SmsRouterLogins", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserBuyPlan
        {
            get
            {
                return resourceProvider.GetResource("SmsUserBuyPlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserPasswordChange
        {
            get
            {
                return resourceProvider.GetResource("SmsUserPasswordChange", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserAccountRemoved
        {
            get
            {
                return resourceProvider.GetResource("SmsUserAccountRemoved", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserAccountCreated
        {
            get
            {
                return resourceProvider.GetResource("SmsUserAccountCreated", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsCharge
        {
            get
            {
                return resourceProvider.GetResource("SmsCharge", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RegisterWithSms
        {
            get
            {
                return resourceProvider.GetResource("RegisterWithSms", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RegisterWithSmsCode
        {
            get
            {
                return resourceProvider.GetResource("RegisterWithSmsCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsAdminChangeUserPassword
        {
            get
            {
                return resourceProvider.GetResource("SmsAdminChangeUserPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserhangeUserPassword
        {
            get
            {
                return resourceProvider.GetResource("SmsUserhangeUserPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsAdminChangeAdminPassword
        {
            get
            {
                return resourceProvider.GetResource("SmsAdminChangeAdminPassword", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsAdminLogins
        {
            get
            {
                return resourceProvider.GetResource("SmsAdminLogins", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RegisterFormSms
        {
            get
            {
                return resourceProvider.GetResource("RegisterFormSms", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserAfterCreateWithAdmin
        {
            get
            {
                return resourceProvider.GetResource("SmsUserAfterCreateWithAdmin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsActive
        {
            get
            {
                return resourceProvider.GetResource("SmsActive", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserAfterResetCounter
        {
            get
            {
                return resourceProvider.GetResource("SmsUserAfterResetCounter", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserAfterChangePlan
        {
            get
            {
                return resourceProvider.GetResource("SmsUserAfterChangePlan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsUserAfterDelete
        {
            get
            {
                return resourceProvider.GetResource("SmsUserAfterDelete", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsYourAccountCounterReset
        {
            get
            {
                return resourceProvider.GetResource("SmsYourAccountCounterReset", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsIfErrorInSms
        {
            get
            {
                return resourceProvider.GetResource("SmsIfErrorInSms", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RegisterWithSmsMessage
        {
            get
            {
                return resourceProvider.GetResource("RegisterWithSmsMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RegisterWithSmsRouterProfile
        {
            get
            {
                return resourceProvider.GetResource("RegisterWithSmsRouterProfile", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ContactInformation
        {
            get
            {
                return resourceProvider.GetResource("ContactInformation", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsConfig
        {
            get
            {
                return resourceProvider.GetResource("SmsConfig", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsPackage
        {
            get
            {
                return resourceProvider.GetResource("SmsPackage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Paid
        {
            get
            {
                return resourceProvider.GetResource("Paid", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NotPaid
        {
            get
            {
                return resourceProvider.GetResource("NotPaid", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UnsuccessfulPaid
        {
            get
            {
                return resourceProvider.GetResource("UnsuccessfulPaid", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string TransactionNumber
        {
            get
            {
                return resourceProvider.GetResource("TransactionNumber", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FactorPrice
        {
            get
            {
                return resourceProvider.GetResource("FactorPrice", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FactorDate
        {
            get
            {
                return resourceProvider.GetResource("FactorDate", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FactorStatus
        {
            get
            {
                return resourceProvider.GetResource("FactorStatus", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string AdminFactorPriceChartHeader
        {
            get
            {
                return resourceProvider.GetResource("AdminFactorPriceChartHeader", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string AdminFactorCountChartHeader
        {
            get
            {
                return resourceProvider.GetResource("AdminFactorCountChartHeader", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Factores
        {
            get
            {
                return resourceProvider.GetResource("Factores", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsCount
        {
            get
            {
                return resourceProvider.GetResource("SmsCount", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string UnitSmsPrice
        {
            get
            {
                return resourceProvider.GetResource("UnitSmsPrice", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NewPackage
        {
            get
            {
                return resourceProvider.GetResource("NewPackage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string AdminSmsLogChart
        {
            get
            {
                return resourceProvider.GetResource("AdminSmsLogChart", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string FarapayamakCharge
        {
            get
            {
                return resourceProvider.GetResource("FarapayamakCharge", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string AllUserCharge
        {
            get
            {
                return resourceProvider.GetResource("AllUserCharge", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsSendedByPanels
        {
            get
            {
                return resourceProvider.GetResource("SmsSendedByPanels", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsSendedBySystem
        {
            get
            {
                return resourceProvider.GetResource("SmsSendedBySystem", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string SmsManagement
        {
            get
            {
                return resourceProvider.GetResource("SmsManagement", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsPackages
        {
            get
            {
                return resourceProvider.GetResource("SmsPackages", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string FactorList
        {
            get
            {
                return resourceProvider.GetResource("FactorList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PaymentGatewayNotFound
        {
            get
            {
                return resourceProvider.GetResource("PaymentGatewayNotFound", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PaymentSuccess
        {
            get
            {
                return resourceProvider.GetResource("PaymentSuccess", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string BuyPackageFactorSmsToUser
        {
            get
            {
                return resourceProvider.GetResource("BuyPackageFactorSmsToUser", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string PaymentUnsuccesfulMesssage
        {
            get
            {
                return resourceProvider.GetResource("PaymentUnsuccesfulMesssage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string PaymentErrorMessage
        {
            get
            {
                return resourceProvider.GetResource("PaymentErrorMessage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string FinancialManagement
        {
            get
            {
                return resourceProvider.GetResource("FinancialManagement", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PaymentTypesManagement
        {
            get
            {
                return resourceProvider.GetResource("PaymentTypesManagement", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string PaymentTypes
        {
            get
            {
                return resourceProvider.GetResource("PaymentTypes", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string SmsPanel
        {
            get
            {
                return resourceProvider.GetResource("SmsPanel", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string EmailConfirmed
        {
            get
            {
                return resourceProvider.GetResource("EmailConfirmed", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string IsBanned
        {
            get
            {
                return resourceProvider.GetResource("IsBanned", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TodayVisitors
        {
            get
            {
                return resourceProvider.GetResource("TodayVisitors", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string TodayViews
        {
            get
            {
                return resourceProvider.GetResource("TodayViews", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Visitors
        {
            get
            {
                return resourceProvider.GetResource("Visitors", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Views
        {
            get
            {
                return resourceProvider.GetResource("Views", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string PastTwentyDaysViews
        {
            get
            {
                return resourceProvider.GetResource("PastTwentyDaysViews", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Browsers
        {
            get
            {
                return resourceProvider.GetResource("Browsers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Countries
        {
            get
            {
                return resourceProvider.GetResource("Countries", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Unknow
        {
            get
            {
                return resourceProvider.GetResource("Unknow", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string OpertaingSystems
        {
            get
            {
                return resourceProvider.GetResource("OpertaingSystems", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MostPagesVisted
        {
            get
            {
                return resourceProvider.GetResource("MostPagesVisted", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string MostReferrers
        {
            get
            {
                return resourceProvider.GetResource("MostReferrers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string XmlNotValid
        {
            get
            {
                return resourceProvider.GetResource("XmlNotValid", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string CompanyName
        {
            get
            {
                return resourceProvider.GetResource("CompanyName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string Routers
        {
            get
            {
                return resourceProvider.GetResource("Routers", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string RouterList
        {
            get
            {
                return resourceProvider.GetResource("RouterList", CultureInfo.CurrentUICulture.Name) as string;
            }
        }
        public static string NewRouter
        {
            get
            {
                return resourceProvider.GetResource("NewRouter", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string EditRouter
        {
            get
            {
                return resourceProvider.GetResource("EditRouter", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string BussinesName
        {
            get
            {
                return resourceProvider.GetResource("BussinesName", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string RouterCode
        {
            get
            {
                return resourceProvider.GetResource("RouterCode", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


        public static string LogoImage
        {
            get
            {
                return resourceProvider.GetResource("LogoImage", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string Reseller
        {
            get
            {
                return resourceProvider.GetResource("Reseller", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string ContactusSlogan
        {
            get
            {
                return resourceProvider.GetResource("ContactusSlogan", CultureInfo.CurrentUICulture.Name) as string;
            }
        }

        public static string LastLogin
        {
            get
            {
                return resourceProvider.GetResource("LastLogin", CultureInfo.CurrentUICulture.Name) as string;
            }
        }


    }
}
