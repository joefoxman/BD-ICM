using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using Bd.Icm.Core;
using Csla;
using NLog;

namespace Bd.Icm.Web.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected AppUserState AppUserState = new AppUserState();
        protected Logger Logger = LogManager.GetCurrentClassLogger();

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var appUserState = new AppUserState();
            if (User is ClaimsPrincipal)
            {
                var user = User as ClaimsPrincipal;
                var claims = user.Claims.ToList();

                var userStateString = GetClaim(claims, "userState");
                if (!string.IsNullOrWhiteSpace(userStateString))
                {
                    appUserState.FromString(userStateString);
                    BdPrincipal.LoadPrincipal(appUserState.UserName);
                }
                AppUserState = appUserState;

            }
        }

        public static string GetClaim(List<Claim> claims, string key)
        {
            var claim = claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value ?? null;
        }

        protected void AddModelErrors(IBusinessBase obj)
        {
            if (obj.GetBrokenRules().ErrorCount <= 0) return;
            foreach (var error in obj.GetBrokenRules())
            {
                ModelState.AddModelError(error.Property, error.Description);
            }
        }
    }
}
