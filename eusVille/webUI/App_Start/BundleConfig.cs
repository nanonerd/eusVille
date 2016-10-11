using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace webUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new JsxBundle("~/bundles/main").Include(
                    //"~/Scripts/external/react/react.js",    // Bundler will auto minify = prod version                      
                    "~/Scripts/external/kartikRating/js/star-rating.min.js",
                    "~/Scripts/external/react/showdown.js",
                    "~/Scripts/internal/eus/eusAPI.js",
                    "~/Scripts/internal/eus/eusCommon.js" ,                      
                    "~/Scripts/internal/eusVote/TopicAnswers.jsx"
                    )); 

            bundles.Add(new ScriptBundle("~/bundles/react").Include(
                    "~/Scripts/external/react/react.js",    // Bundler will auto minify = prod version
                    "~/Scripts/external/react/JSXTransformer.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/external/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/external/jQuery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/external/modernizer/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/external/bootstrap/bootstrap.js",
                      "~/Scripts/external/respond/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/eus").Include(
                      "~/Scripts/internal/eus/eus*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));                        

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
