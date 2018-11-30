using System.Web;
using System.Web.Optimization;

namespace LICTProject
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

            bundles.Add(new StyleBundle("~/Login/css").Include(
               "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
               "~/dist/css/AdminLTE.min.css",
               "~/plugins/iCheck/flat/blue.css",
               "~/bower_components/font-awesome/css/font-awesome.min.css",
               "~/bower_components/Ionicons/css/ionicons.min.css",
               "~/plugins/iCheck/square/blue.css"// for register

               ));

            bundles.Add(new ScriptBundle("~/Login/js").Include(
                        "~/bower_components/jquery/dist/jquery.min.js",
                        "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                        "~/plugins/iCheck/icheck.min.js"
                        ));

        }
    }
}
