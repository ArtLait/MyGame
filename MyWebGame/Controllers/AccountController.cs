using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebGam.EF.Entity;
using MyWebGam.Models;
using System.Net;
using MyWebGam.AddFunctionality;
using System.Web.Security;
using MyWebGam.Filters;

namespace MyWebGam.Controllers
{
    [Culture]
    public class AccountController : Controller
    {
        UserRepository repo;
        // GET: Account
        public AccountController()
        {
            repo = new UserRepository();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisteViewModel registerData)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
                newUser.Name = registerData.Name;
                int b = 15;
                var newVariable = b.GetHashCode();
                newUser.PasswordHash = CollectionOfMethods.GetHashString(registerData.Password);
                newUser.Email = registerData.Email;
                DateTime dateNow = DateTime.Now;
                newUser.Date = dateNow;
                repo.Save(newUser);
                ViewBag.name = newUser.Name;
                ViewBag.password = newUser.PasswordHash;
                ViewBag.date = newUser.Date;
                return RedirectToAction("SignIn", "Account");
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(registerData);

        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(AuthorizationViewModel authorizationUser)
        {
            if (ModelState.IsValid)
            {
                if (repo.GetAuthorizateUser(authorizationUser.Name,
                    CollectionOfMethods.GetHashString(authorizationUser.Password)) != null)
                {
                    ViewBag.userIn = "User in";
                    FormsAuthentication.SetAuthCookie(authorizationUser.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем - нету");
                }
                return View();
            }     
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View();            

        }
    }
}