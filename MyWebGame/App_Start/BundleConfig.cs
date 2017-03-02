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
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-{version}.intellisense.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate-vsdoc.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquerySignalR").Include(
                "~/Scripts/jquery.signalR-{version}.js",
                "~/Scripts/chat/chat.js",
                "~/Scripts/chat/templatesForChat.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryTemplate").Include(              
                "~/Scripts/chat/lib/jquery.tmpl.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/threeJs").Include(
            "~/Scripts/threeJs/lib/three.js",
            "~/Scripts/threeJs/lib/OrbitControls.js",
            "~/Scripts/threeJs/main.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/authReg").Include(
                "~/Scripts/authReg/authReg.js"
                ));
                      

            //Css
            bundles.Add(new StyleBundle("~/Content/authReg").Include(
                "~/Content/css/animate.css",
                "~/Content/css/authReg.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/reset.css",
                      "~/Content/bootstrap.css",
                      "~/Content/css/site.css",
                      "~/Content/css/media.css",
                      "~/Content/css/Chat.css"
                      ));
        }
    }
}