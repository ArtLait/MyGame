using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.Threading;

namespace MyWebGam.Filters
{
    public class CultureAttribute :  FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cultureName = null;
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "ru";
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

    }
}