using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MyWebGam
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Js
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jquerySignalR").Include(
                "~/Scripts/jquery.signalR-{version}.js",             
                "~/Scripts/src/util.js",
                "~/Scripts/src/templatesForUtil.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryTemplate").Include(
                "~/Scripts/src/lib/jquery-1.5rc1.js",
                "~/Scripts/src/lib/jquery.tmpl.min.js"
                ));

            //Css
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/reset.css",
                      "~/Content/bootstrap.css",
                      "~/Content/css/site.css",
                      "~/Content/css/media.css"));
        }
    }
}