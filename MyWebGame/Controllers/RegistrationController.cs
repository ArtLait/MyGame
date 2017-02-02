using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyWebGam.EF.Entity;
using MyWebGame.AddFunctionality;
using MyWebGam.Models;


namespace MyWebGam.Controllers
{
    public class RegistrationController : Controller
    {
        UserRepository repo;
        UserData testUser = new UserData();
        public RegistrationController()
       {
           repo = new UserRepository();
       }
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            User newUser = new User();
            newUser.Name = col["login"];
            int b = 15;
            var newVariable = b.GetHashCode();
            newUser.PasswordHash = CollectionOfMethods.GetHashString(col["password"]);
            newUser.Email = col["email"];
            DateTime dateNow = DateTime.Now;
            newUser.Date = dateNow;
            repo.Save(newUser);
            ViewBag.name = newUser.Name;
            ViewBag.password = newUser.PasswordHash;
            ViewBag.date = newUser.Date;
            return View();
        }
        
    }
}
