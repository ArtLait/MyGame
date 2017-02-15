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
        ResetPasswordRepository repoReset;
        public AccountController()
        {
            repo = new UserRepository();
            repoForEmail = new UserForConfirmedEmailRepository();
            repoReset = new ResetPasswordRepository();
        }     
        public ActionResult OnlyEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> OnlyEmail(OnlyEmailViewModel data)
        {
            //ViewBag.data = data.Email;
            string key = CollectionOfMethods.GetHashString(data.Email);
            if (ModelState.IsValid)
            {
                ResetPassword newData = new ResetPassword()
                {
                    Id = 4,
                    Email = data.Email,
                    Key = key,
                };
                repoReset.Save(newData);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(data.Email, @Resources.Web.ChangePasswordTheme,
                    TemplateForEmail.ForgotPassword(Url.Action("ResetPassword", "Account", new { Key = key }, Request.Url.Scheme)));
                return RedirectToAction("WaitingForConfirm");
            }
            else
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return View(data);
            }            
        }
        public ActionResult ResetPassword(string key)
        {
            // For Authorized users
            if (User.Identity.IsAuthenticated)
            {
                return View(new ResetPasswordViewModel { Email = User.Identity.Name});
            }
            //Forgot password
            ResetPassword user = repoReset.CheckKey(key);
            if (user != null)
            {                
                return View(new ResetPasswordViewModel() { Email = user.Email});
            }
            
            return View("~/Views/Account/ResetPasswordError.cshtml");
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            string email;
            if (User.Identity.IsAuthenticated) email = User.Identity.Name;
            else email = model.Email;   
            if (ModelState.IsValid)
            {
                repoReset.UpdatePassword(email, CollectionOfMethods.GetHashString(model.Password));
                return RedirectToAction("SignIn", "Account", new { message = "ChangePasswordIsSuccesfull" });
            }
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return View(model);
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
                if(repo.CheckEmailUniqueness(registerData.Email) == false)
                {
                    ModelState.AddModelError("", @Resources.Web.MailBusy);
                    return View();
                }
                string key = CollectionOfMethods.GetHashString(registerData.Email) + CollectionOfMethods.GetHashString(registerData.Name);
                UserForConfirmedEmail newUser = new UserForConfirmedEmail()
                {                    
                    Key = key,
                    User = new User()
                    {
                        Name = registerData.Name,
                        PasswordHash = CollectionOfMethods.GetHashString(registerData.Password),
                        Email = registerData.Email,
                        Date = DateTime.UtcNow,
                        Confirmed = false
                    }
                };
                repoForEmail.Save(newUser);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(registerData.Email, @Resources.Web.ConfirmEmailTheme,
                    TemplateForEmail.Registration(Url.Action("ConfirmEmail", "Account", new { Key = key }, Request.Url.Scheme)));          

                FormsAuthentication.SetAuthCookie(registerData.Email, true);                
                return View("WaitingForConfirm");
            }
            
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(registerData);
        }

        public ActionResult WaitingForConfirm()
        {            
            return View();
        }
        public ActionResult ConfirmEmail(string key)
        {
            UserForConfirmedEmail user = repoForEmail.CheckKey(key);
            string message = Resources.Web.WaitingForPasswordConfirmation;
            if (user != null)
            {
                repoForEmail.SetUserConfirmed(user.UserId);
                message = @Resources.Web.RegistrationSuccesfull;
                repoForEmail.DeleteKey(user.Id);
            }
            ViewBag.message = message;            
            return View();
        }

        public ActionResult SignIn(string message)
        {
            if (message == "ChangePasswordIsSuccesfull")
                ViewBag.ChangePasswordIsSuccesfull = @Resources.Web.ChangePasswordIsSuccesfull;
            return View();          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(AuthorizationViewModel authorizationUser)
        {
            User user = repo.GetAuthorizateUser(authorizationUser.Name,                
                    CollectionOfMethods.GetHashString(authorizationUser.Password));
             if (ModelState.IsValid)
             {                   
                if (user != null)
                {
                    if (user.Confirmed)
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, true);                    
                        return RedirectToAction("Index", "Home");
                    }
                    return View("~/Views/Account/PleaseConfirmedEmail.cshtml");
                }
                else
                {
                    ModelState.AddModelError("", Resources.Web.NoSuchUser);
                }
                return View();
            }    
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(); 
           }
        public ActionResult SignOut()
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            return Redirect(returnUrl);
        }
        
    }
}