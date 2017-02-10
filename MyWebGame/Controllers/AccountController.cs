using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebGam.EF;
using MyWebGam.Models;
using System.Net;
using MyWebGam.AddFunctionality;
using System.Web.Security;
using System.Threading;
using System.Globalization;
using System.Web.Mvc.Async;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MimeKit;
using MailKit.Net.Smtp;
using MyWebGam.Service;

namespace MyWebGam.Controllers
{
    public class AccountController : UnAuthorizedController 
    {      
        UserRepository repo;
        UserForConfirmedEmailRepository repoForEmail;        
        public AccountController()
        {
            repo = new UserRepository();
            repoForEmail = new UserForConfirmedEmailRepository();            
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegisteViewModel registerData)
        {
            if (ModelState.IsValid)
            {               
                string key = CollectionOfMethods.GetHashString(registerData.Email) + CollectionOfMethods.GetHashString(registerData.Name);
                UserForConfirmedEmail newUser = new UserForConfirmedEmail()
                {                    
                    Key = key,
                    User = new User()
                    {
                        Name = registerData.Name,
                        PasswordHash = CollectionOfMethods.GetHashString(registerData.Email),
                        Email = registerData.Email,
                        Date = DateTime.UtcNow,
                        Confirmed = false
                    }
                };
                repoForEmail.Save(newUser);

                var user = new ApplicationUser() { UserName = registerData.Name};
                user.Email = registerData.Email;
                user.ConfirmedEmail = false;

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(registerData.Email, "Подтверждение регистрации", 
                    EmailService.Href(Url.Action("ConfirmEmail", "Account", new { Key = key }, Request.Url.Scheme)));

                FormsAuthentication.SetAuthCookie(registerData.Email, true);
                return RedirectToAction("WaitingForConfirm", "Account", new { Email = user.Email });
                             
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(registerData);
        }

        public ActionResult WaitingForConfirm(string Email)
        {
            ViewBag.Email = Email;
            return View();       
        }
        public ActionResult ConfirmEmail(string key)
        {
            UserForConfirmedEmail user = repoForEmail.CheckKey(key);
            string message = "Вы прошли не по своей ссылке, регистрация не подтверждена";
            if (user != null)
            {
                repoForEmail.Confirmed(user.UserId);
                message = @Resources.Web.RegistrationSuccesfull;
                repoForEmail.DeleteKey(user.Id);
            }
            ViewBag.message = message;
            return View();
        }

        public ActionResult SignIn()
        {            
            var userName = User.Identity.Name;
            
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