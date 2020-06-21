using System.Web.Optimization;

namespace Finance
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
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

            //#if TEMPLATES

            #region Foundation Bundles

            bundles.Add(new StyleBundle("~/Content/foundation/css").Include(
                           "~/Content/foundation/foundation.css",
                           "~/Content/foundation/foundation.mvc.css",
                           "~/Content/foundation/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                          "~/Scripts/foundation/fastclick.js",
                          "~/Scripts/jquery.cookie.js",
                          "~/Scripts/foundation/foundation.js",
                          "~/Scripts/foundation/foundation.*",
                          "~/Scripts/foundation/app.js"));

            #endregion

            #region Ink Bundles

            bundles.Add(new StyleBundle("~/Content/ink/css").Include(
                           "~/Content/ink/ink.css",
                           "~/Content/ink/quick-start.css",
                           "~/Content/ink/ink-flex.css",
                           "~/Content/ink/ink-legacy.css",
                           "~/Content/ink/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/ink").Include(
                          "~/Scripts/ink/ink.*",
                          "~/Scripts/ink/ink-*",
                          "~/Scripts/ink/autoload.js",
                          "~/Scripts/ink/holder.js",
                          "~/Scripts/ink/html5shiv.js",
                          "~/Scripts/ink/html5shiv-printshiv.js",
                          "~/Scripts/ink/modernizr.js",
                          "~/Scripts/ink/modernizr-all.js",
                          "~/Scripts/ink/prettify"));

            #endregion

            //#else
        }
    }
}