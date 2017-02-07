﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyWebGam.Controllers
{
    public class UnAuthorizedController : Controller
    {
        // GET: UnAuthorized
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureNameKey = null;
            HttpCookie cultureCookie = Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureNameKey = cultureCookie.Value;
            else
                cultureNameKey = "ru";
            List<string> cultures = new List<string>() { "ru", "en" };
            Dictionary<string, string> culturesMap = new Dictionary<string, string>();
            culturesMap.Add("ru", "RU-ru");
            culturesMap.Add("en", "EN-US");
            //if (!cultures.Contains(cultureName))
            if (!culturesMap.ContainsKey(cultureNameKey))
            {
                cultureNameKey = "ru";
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culturesMap[cultureNameKey]);// CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture; //CultureInfo.CreateSpecificCulture(cultureName);                                   
            return base.BeginExecuteCore(callback, state);
        }
    }
}