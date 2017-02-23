using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebGam.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using MyWebGam.Service;
using MyWebGam.EF;


namespace MyWebGam.Controllers
{
    public class HomeController : UnAuthorizedController
    {
        public HomeController()
        {               
        }
        // GET: Home
        public ActionResult Index(string message)
        {
            string result = @Resources.Web.NotAuthorized;            
            if (User.Identity.IsAuthenticated)
            {
                result = @Resources.Web.UserIn + User.Identity.Name;                
            }
            ViewBag.userLogin = result;
            ViewBag.message = message;
            return View();
        }
        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;            
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                return Redirect(returnUrl);
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            
            return Redirect(returnUrl);
        }
    }
}