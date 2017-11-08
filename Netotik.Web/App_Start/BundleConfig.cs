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

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalpublic").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/public.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalpanel").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/panel.js"));

            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                        "~/Scripts/jquery.signalR-2.2.2.min.js"));



            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/PublicUI/js/vendor/modernizr-2.8.3.min.js"));


            bundles.Add(new StyleBundle("~/Content/publiccss").Include(
                                "~/Content/PublicUI/plugins/bootstrap/css/bootstrap.min.css",
                                "~/Content/PublicUI/css/essentials.css",
                                "~/Content/PublicUI/css/layout.css",
                                "~/Content/PublicUI/css/color_scheme/darkblue.css",
                                "~/Content/PublicUI/plugins/slider.revolution/css/extralayers.css",
                                "~/Content/PublicUI/plugins/slider.revolution/css/settings.css",
                                "~/Content/PublicUI/css/header-5.css",
                                "~/Content/PublicUI/css/_layout-font-rewrite.css"
                                ));



            bundles.Add(new StyleBundle("~/Content/publicrtlcss").Include(
                                "~/Content/PublicUI/plugins/bootstrap/RTL/bootstrap-rtl.min.css",
                                "~/Content/PublicUI/plugins/bootstrap/RTL/bootstrap-flipped.min.css",
                                "~/Content/PublicUI/css/layout-RTL.css",
                                "~/Content/PublicUI/css/_layout-font-rewrite.farsi.css"));





            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                       "~/Content/fonts/icomoon.css",
                       "~/Content/fonts/flag-icon-css/css/flag-icon.min.css",
                       "~/Content/css/plugins/sliders/slick/slick.css",
                       "~/Content/css/plugins/extensions/bootstrap-treeview.min.css",
                       "~/Content/css/plugins/forms/icheck/flat/_all.css",
                       "~/Content/css/plugins/forms/icheck/custom.css",
                       "~/Content/css/plugins/extensions/unslider.css",
                       "~/Content/css/plugins/forms/selects/selectize.css",
                       "~/Content/css/plugins/forms/selects/selectize.default.css",
                       "~/Content/css/plugins/forms/spinner/jquery.bootstrap-touchspin.css",
                       "~/Content/css/plugins/forms/toggle/bootstrap-switch.min.css",
                       "~/Content/css/plugins/forms/toggle/switchery.min.css",
                       "~/Content/css/plugins/tables/datatable/dataTables.bootstrap4.min.css",
                       "~/Content/css/plugins/extensions/sweetalert.css",
                       "~/Content/css/components/weather-icons/climacons.min.css").Include("~/Content/css/plugins/forms/icheck/icheck.css",new CssRewriteUrlTransform()));



            bundles.Add(new StyleBundle("~/Content/panelrtlcss").Include(
                       "~/Content/css/Rtl/vendors.min.css",
                       "~/Content/css/Rtl/app.min.css",
                       "~/Content/css/Rtl/my-style.css"));



            bundles.Add(new StyleBundle("~/Content/panelltrcss").Include(
                       "~/Content/css/Ltr/vendors.min.css",
                       "~/Content/css/Ltr/app.min.css",
                       "~/Content/css/Ltr/my-style.css"));



            bundles.Add(new ScriptBundle("~/bundles/paneljs").Include(
                                "~/Content/js/vendors.min.js",
                                "~/Content/js/plugins/ui/jquery.sticky.js",
                                "~/Content/js/plugins/forms/spinner/jquery.bootstrap-touchspin.js",
                                "~/Content/js/plugins/forms/validation/jqBootstrapValidation.js",
                                "~/Content/js/plugins/forms/toggle/bootstrap-switch.min.js",
                                "~/Content/js/plugins/forms/toggle/switchery.min.js",
                                "~/Content/js/plugins/forms/icheck/icheck.min.js",
                                "~/Content/js/components/forms/checkbox-radio.js",
                                "~/Content/js/components/extensions/block-ui.js",
                                "~/Content/js/plugins/extensions/bootstrap-treeview.min.js",
                                "~/Content/js/plugins/tables/jquery.dataTables.min.js",
                                "~/Content/js/plugins/tables/datatable/dataTables.bootstrap4.min.js",
                                "~/Content/js/app.min.js",
                                "~/Content/js/components/tooltip/tooltip.js",
                                "~/Content/js/plugins/forms/select/selectize.min.js",
                                "~/Scripts/ckeditor/ckeditor.js",
                                "~/Content/js/plugins/pickers/dateTime/moment-with-locales.min.js",
                                "~/Content/js/plugins/pickers/dateTime/bootstrap-datetimepicker.min.js",
                                "~/Content/js/plugins/extensions/sweetalert.min.js",
                                "~/Content/js/plugins/extensions/unslider-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/publicjs").Include(
                     "~/Content/PublicUI/plugins/jquery/jquery-2.2.3.min.js",
                     "~/Content/PublicUI/js/scripts.js",
                     "~/Content/PublicUI/plugins/slider.revolution/js/jquery.themepunch.tools.min.js",
                     "~/Content/PublicUI/plugins/slider.revolution/js/jquery.themepunch.revolution.min.js",
                     "~/Content/PublicUI/js/view/demo.revolution_slider.js",
                     "~/Content/PublicUI/plugins/effects/canvas.particles.js"));


        }
    }
}
