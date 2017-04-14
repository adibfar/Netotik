using System.Web;
using System.Web.Optimization;

namespace Netotik.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jqueryval-default.js"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/PublicUI/js/vendor/modernizr-2.8.3.min.js"));

            //bundles.Add(new StyleBundle("~/Content/admincss").Include(
            //          "~/Content/css/bootstrap.min.css",
            //          "~/Content/css/bootstrap.rtl.min.css",
            //          "~/Content/css/plugins/toastr/toastr.min.css",
            //          "~/Content/css/plugins/iCheck/custom.css",
            //          "~/Content/css/plugins/summernote/summernote.css",
            //          "~/Content/css/plugins/summernote/summernote-bs3.css",
            //          "~/Content/css/plugins/colorpicker/bootstrap-colorpicker.min.css",
            //          "~/Content/css/plugins/jsTree/style.min.css",
            //          "~/Content/css/plugins/clockpicker/clockpicker.css",
            //          "~/Content/css/plugins/chosen/chosen.css",
            //          "~/Content/css/animate.css",
            //          "~/Content/css/style.rtl.css",
            //          "~/Content/css/fileinput.min.css",
            //          "~/Content/css/sweet-alert.css",
            //          "~/Content/css/PersianDatePicker.css"
            //          ).Include("~/Content/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));


           // bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
           //"~/Content/js/jquery-2.1.1.js",
           //"~/Content/js/bootstrap.min.js",
           //"~/Content/js/plugins/chosen/chosen.jquery.js",
           //"~/Content/js/plugins/metisMenu/jquery.metisMenu.js",
           //"~/Content/js/plugins/slimscroll/jquery.slimscroll.min.js",
           //"~/Content/js/plugins/jsTree/jstree.min.js",
           //"~/Content/js/plugins/toastr/toastr.min.js",
           //"~/Content/js/plugins/summernote/summernote.min.js",
           //"~/Content/js/plugins/colorpicker/bootstrap-colorpicker.min.js",
           //"~/Content/js/plugins/iCheck/icheck.min.js",
           //"~/Content/js/plugins/clockpicker/clockpicker.js",
           //"~/Content/js/rada.js",
           //"~/Scripts/PersianDatePicker.js",
           //"~/Scripts/sweet-alert.min.js",
           //"~/Scripts/lazysizes.js",
           //"~/Scripts/admin.js",
           //"~/Scripts/jquery-MVC-RemoveRow.js"));

        }
    }
}
