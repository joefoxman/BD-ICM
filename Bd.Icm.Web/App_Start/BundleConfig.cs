using System.Web;
using System.Web.Optimization;

namespace Bd.Icm.Web
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

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-sanitize.js",
                "~/Scripts/angular-local-storage.js",
                "~/Scripts/angular-file-saver.bundle.js",
                "~/Scripts/angular-ui-router.js",
                "~/Scripts/angular-toastr.min.js",
                "~/Scripts/angular-toastr.tpls.min.js",
                "~/Scripts/angular-ui/ui-bootstrap.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/app/app.module.js",
                "~/app/app.core.module.js",
                "~/app/app.config.js",
                "~/app/app.run.js",
                "~/app/app.enums.js")
                .IncludeDirectory("~/app", "*module.js", true)
                .IncludeDirectory("~/app", "*filter.js", true)
                .IncludeDirectory("~/app", "*routes.js", true)
                .IncludeDirectory("~/app", "*service.js", true)
                .IncludeDirectory("~/app", "*model.js", true)
                .IncludeDirectory("~/app", "*controller.js", true)
                .IncludeDirectory("~/app/directives", "*.js")
                .IncludeDirectory("~/app/thirdparty", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                    "~/Scripts/underscore.min.js",
                        "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                "~/Content/angular-toastr.min.css",
                      "~/Content/spinkit.css",
                "~/Content/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}
