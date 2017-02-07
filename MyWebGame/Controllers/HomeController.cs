using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebGam.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;


namespace MyWebGam.Controllers
{
    public class HomeController : UnAuthorizedController
    {       
        public ActionResult StartPage()
        {
            ViewBag.testVariable = "ok";
            return View();
        }
        [HttpPost]
        public ActionResult StartPage(NickNameViewModel nick)
        {
            return RedirectToAction("Index", "Home");
        }
        // GET: Home
        public ActionResult Index()
        {
            string result = @Resources.Web.NotAuthorized;
            if (User.Identity.IsAuthenticated)
            {
                result = @Resources.Web.UserIn + User.Identity.Name;                
            }
            ViewBag.userLogin = result;          
            return View();
        }
        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;            
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
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