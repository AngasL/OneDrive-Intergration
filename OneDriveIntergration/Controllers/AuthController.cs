using OneDriveIntergration.Services;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OneDriveIntergration.Controllers
{
    public class AuthController : Controller
    {
        private IAuthenticationService _service;

        public AuthController(IAuthenticationService service)
        {
            this._service = service;
        }

        public async Task<ActionResult> Index()
        {
            var authorizeUrl = string.Format(
                Constants.AuthorizationCodeEndpointFormat,
                Constants.AuthorizationCodeEndpoing,
                Constants.ClientId,
                HttpUtility.UrlEncode(Constants.RedirectUri),
                Constants.Scope);

            return Redirect(authorizeUrl);
        }

        public async Task<ActionResult> Authorize(string code)
        {
            var authResult = await _service.GetAuthenticationResult(code);
            Session["access_token"] = authResult.AccessToken;

            return View(authResult);
        }
    }
}