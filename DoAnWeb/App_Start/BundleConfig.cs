using System.Web;
using System.Web.Optimization;

namespace DoAnWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/bundles/adminCss").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/bootstrap.min.css",
                       //"~/Content/all.min.css",
                       "~/Content/fontawesome.min.css",
                       "~/Content/sb-admin.css"));

            bundles.Add(new StyleBundle("~/bundles/adminTableCss").Include(
                      "~/Content/dataTables.bootstrap4.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminJs").Include(
                       //"~/Scripts/jquery-3.3.1.min.js",
                       "~/Scripts/bootstrap.bundle.min.js",
                       "~/Scripts/jquery.easing.min.js",
                       "~/Scripts/sb-admin.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminTableJs").Include(
                       "~/Scripts/jquery-3.3.1.js",
                       "~/Scripts/jquery.dataTables.js",
                       "~/Scripts/dataTables.bootstrap4.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));
        }
    }
}
