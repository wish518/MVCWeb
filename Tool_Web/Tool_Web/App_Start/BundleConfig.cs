using System.Web;
using System.Web.Optimization;

namespace Tool_Web
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap/bootstrap-datepicker.zh-TW.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.easyui.min.js",
                      "~/Scripts/Common/TextVaild.js",
                      "~/Scripts/Common/Common.js"));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                      "~/Scripts/vue.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap/bootstrap-datepicker3.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/GAME").Include(
                      "~/Scripts/GAME/GAME.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"));
        }
    }
}
