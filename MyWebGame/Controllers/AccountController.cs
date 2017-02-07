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
using System.Threading;
using System.Globalization;
using System.Web.Mvc.Async;

namespace MyWebGam.Controllers
{
    public class AccountController : UnAuthorizedController 
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
                User newUser = new User
                {
                    Name = registerData.Name,
                    PasswordHash = CollectionOfMethods.GetHashString(registerData.Password),
                    Email = registerData.Email,
                    Date  = DateTime.UtcNow
                };
                
                repo.Save(newUser);               
                FormsAuthentication.SetAuthCookie(newUser.Name, true);
                return RedirectToAction("Index", "Home");
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