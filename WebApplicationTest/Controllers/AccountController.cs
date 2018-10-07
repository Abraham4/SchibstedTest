using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplicationTest.Models;
using DomainsServices.Interfaces;
using System.Security.Principal;
using System.Web.Security;
using System.Threading;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using System.Net;

namespace WebApplicationTest.Controllers
{


    [Authorize]
    public class AccountController : Controller
    {


        private readonly IUserDomainService _userDomainService;

        public AccountController(IUserDomainService userDomainService)
        {
            _userDomainService = userDomainService;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, bool rememberMe = false)
        {
            if (ModelState.IsValid)
            {
                var user = await _userDomainService.GetUserByNameAndPassword(model.Name, model.Password);
                if (user == null) return View();


                string userData = JsonConvert.SerializeObject(user); // Up to you to write this Serialize method
                var ticket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddMinutes(1), rememberMe, userData);
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));


                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return View(returnUrl);
                }
                else
                {
                    switch (user.Roles)
                    {
                        case "PAGE_1":
                            return RedirectToAction("PAGE1", "Home");

                        case "PAGE_2":
                            return RedirectToAction("PAGE2", "Home");

                        case "PAGE_3":
                            return RedirectToAction("PAGE3", "Home");

                        default:
                            return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "The User Name or Password is incorrect.");

            }

            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            HttpCookie currentUserCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            currentUserCookie.Expires = DateTime.Now.AddDays(-10);
            currentUserCookie.Value = null;
            HttpContext.Response.SetCookie(currentUserCookie);
            return RedirectToAction("Login","Account");
        }
    }
}