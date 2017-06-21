using System.Web.Mvc;
using System.Web.Routing;

namespace OneDriveIntergration.App_Start
{
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (string.IsNullOrEmpty(session["access_token"]?.ToString()))
            {
                var redirectTraget = new RouteValueDictionary
                {
                    { "action", "Index"},
                    { "controller","Auth"}
                };

                filterContext.Result = new RedirectToRouteResult(redirectTraget);
            }
        }
    }
}