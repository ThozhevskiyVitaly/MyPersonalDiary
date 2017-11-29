using System.Web;
using System.Web.Optimization;

namespace MyPersonalDiary
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Diary/js").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js",
                      "~/Scripts/modernizr-*",
                      "~/Scripts/jquery-1.10.2.min.js"));
            bundles.Add(new StyleBundle("~/Content/Diary/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/Account/js").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/Content/Account/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/account.css"));
        }
    }
}
