using OneDriveIntergration.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OneDriveIntergration.Controllers
{
    using Authorize = App_Start.AuthorizeAttribute;

    public class DocumentController : Controller
    {
        private IAuthenticationService _service;

        public DocumentController(IAuthenticationService service)
        {
            this._service = service;
        }

        [@Authorize]
        public async Task<ActionResult> Index()
        {
            var accessToken = Session["access_token"]?.ToString();
            var documents = await _service.GetDocuments(accessToken);

            return View(documents);
        }
    }
}