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


        public static string CompanyName
        {
            get
            {
                return resourceProvider.GetResource("CompanyName", CultureInfo.CurrentUICulture.Name) as string;
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


        public static string SmsUserName
        {
            get
            {
                return resourceProvider.GetResource("SmsUserName", CultureInfo.CurrentUICulture.Name) as string;
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


        public static string CompanyPanel
        {
            get
            {
                return resourceProvider.GetResource("CompanyPanel", CultureInfo.CurrentUICulture.Name) as string;
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

    }
}
