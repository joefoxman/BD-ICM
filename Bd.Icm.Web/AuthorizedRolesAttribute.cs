using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bd.Icm.Core;

namespace Bd.Icm.Web
{
    public class AuthorizedRolesAttribute
        : AuthorizationFilterAttribute
    {
        public RoleType[] Roles { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var userRoles = User.Current.Roles.Select(x => x.Role).ToArray();
            var matches = Roles.Intersect(userRoles);
            if (!matches.Any())
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                actionContext.Response.ReasonPhrase = "You are not authorized to access this resource";
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }
}
