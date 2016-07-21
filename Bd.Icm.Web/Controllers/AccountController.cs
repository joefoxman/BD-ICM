using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Bd.Icm.Models;
using Csla;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NLog;

namespace Bd.Icm.Web.Controllers
{
    public class AccountController : Controller
    {
        private Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult LogOff()
        {
            IdentitySignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string username, string password, bool rememberMe, string returnUrl)
        {
            User user = null;
            try
            {
                Logger.Trace($"Login:{username}|{password}|{Icm.User.HashPassword(password)}");
                user = Icm.User.ValidateLogin(username, password);
            }
            catch (DataPortalException dpex)
            {
                if (dpex.BusinessException is AuthenticationException)
                {
                    ModelState.AddModelError("login", "Username or password is incorrect.");
                    return View(new LoginModel());
                }
                if (dpex.BusinessException is AccountDisabledException)
                {
                    ModelState.AddModelError("login", "Account is disabled.");
                    return View(new LoginModel());
                }
                throw;
            }

            var appUserState = new AppUserState()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };

            BdPrincipal.Login(username, password);

            IdentitySignIn(appUserState, null, rememberMe);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home", null);
        }

        #region [SignIn and SignOut]

        public void IdentitySignIn(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, appUserState.UserName));

            claims.Add(new Claim("userState", appUserState.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        #endregion
    }
}