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

        UserForConfirmedEmailRepository repoForEmail;
        UserRepository repo;

        public HomeController()
        {
            repoForEmail = new UserForConfirmedEmailRepository();
            repo = new UserRepository();           
        }
        public ActionResult StartPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StartPage(NickNameViewModel nick)
        {        
            return View(); 
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