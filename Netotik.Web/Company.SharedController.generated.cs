// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace T4MVC.Company
{
    public class SharedController
    {

        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _Attachments = "_Attachments";
                public readonly string _BreadCrumb = "_BreadCrumb";
                public readonly string _CaptchaPartial = "_CaptchaPartial";
                public readonly string _CaptchaPartial2 = "_CaptchaPartial2";
                public readonly string _Head = "_Head";
                public readonly string _HeadMenu = "_HeadMenu";
                public readonly string _Layout = "_Layout";
                public readonly string _MessageResult = "_MessageResult";
                public readonly string _PicturesList = "_PicturesList";
                public readonly string _SideBarAdminMenu = "_SideBarAdminMenu";
                public readonly string _SideBarMenu = "_SideBarMenu";
                public readonly string _SingleImageForm = "_SingleImageForm";
                public readonly string Error = "Error";
            }
            public readonly string _Attachments = "~/Areas/Company/Views/Shared/_Attachments.cshtml";
            public readonly string _BreadCrumb = "~/Areas/Company/Views/Shared/_BreadCrumb.cshtml";
            public readonly string _CaptchaPartial = "~/Areas/Company/Views/Shared/_CaptchaPartial.cshtml";
            public readonly string _CaptchaPartial2 = "~/Areas/Company/Views/Shared/_CaptchaPartial2.cshtml";
            public readonly string _Head = "~/Areas/Company/Views/Shared/_Head.cshtml";
            public readonly string _HeadMenu = "~/Areas/Company/Views/Shared/_HeadMenu.cshtml";
            public readonly string _Layout = "~/Areas/Company/Views/Shared/_Layout.cshtml";
            public readonly string _MessageResult = "~/Areas/Company/Views/Shared/_MessageResult.cshtml";
            public readonly string _PicturesList = "~/Areas/Company/Views/Shared/_PicturesList.cshtml";
            public readonly string _SideBarAdminMenu = "~/Areas/Company/Views/Shared/_SideBarAdminMenu.cshtml";
            public readonly string _SideBarMenu = "~/Areas/Company/Views/Shared/_SideBarMenu.cshtml";
            public readonly string _SingleImageForm = "~/Areas/Company/Views/Shared/_SingleImageForm.cshtml";
            public readonly string Error = "~/Areas/Company/Views/Shared/Error.cshtml";
            static readonly _DisplayTemplatesClass s_DisplayTemplates = new _DisplayTemplatesClass();
            public _DisplayTemplatesClass DisplayTemplates { get { return s_DisplayTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _DisplayTemplatesClass
            {
                public readonly string Bool = "Bool";
                public readonly string CKEditor = "CKEditor";
                public readonly string DateTime = "DateTime";
                public readonly string Decimal = "Decimal";
                public readonly string File = "File";
                public readonly string Int32 = "Int32";
                public readonly string Multiline = "Multiline";
                public readonly string Password = "Password";
                public readonly string PersianDatePicker = "PersianDatePicker";
                public readonly string String = "String";
            }
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string Boolean = "Boolean";
                public readonly string ColorPicker = "ColorPicker";
                public readonly string Decimal = "Decimal";
                public readonly string DropDown = "DropDown";
                public readonly string Enum = "Enum";
                public readonly string HttpPostedFileBase = "HttpPostedFileBase";
                public readonly string Int32 = "Int32";
                public readonly string Long = "Long";
                public readonly string Multiline = "Multiline";
                public readonly string Password = "Password";
                public readonly string PersianDatePicker = "PersianDatePicker";
                public readonly string String = "String";
                public readonly string SummerNoteEditor = "SummerNoteEditor";
                public readonly string SummerNoteEditorNoLabel = "SummerNoteEditorNoLabel";
                public readonly string TimePicker = "TimePicker";
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
