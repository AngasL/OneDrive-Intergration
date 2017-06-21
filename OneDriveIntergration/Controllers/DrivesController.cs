using OneDriveIntergration.Models;
using OneDriveIntergration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OneDriveIntergration.Controllers
{
    using Authorize = App_Start.AuthorizeAttribute;

    [@Authorize]
    public class DrivesController : Controller
    {
        private readonly IAuthenticationService _service;

        private string accessToken
        {
            get
            {
                return Session["access_token"].ToString();
            }
        }

        public DrivesController(IAuthenticationService service)
        {
            this._service = service;
        }

        // GET: Drives
        public ActionResult Index()
        {
            var graphServices = GetGraphApis();

            return View(graphServices);
        }

        public async Task<JsonResult> GetDefaultDrive()
        {
            var defaultDrive = await _service.GetDefaultDrive(accessToken);

            return Json(defaultDrive, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ListAvailableDrives()
        {
            var drives = await _service.ListAvailableDrives(accessToken);

            return Json(drives, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ListSharedFiles()
        {
            var sharedDrives = await _service.ListSharedDrives(accessToken);

            return Json(sharedDrives, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ListRecentFiles()
        {
            var sharedDrives = await _service.ListRecentFiles(accessToken);

            return Json(sharedDrives, JsonRequestBehavior.AllowGet);
        }

        private IList<GraphService> GetGraphApis()
        {
            return new List<GraphService>
            {
                new GraphService
                {
                    Name="Get the default drive for a user on OneDrive",
                    Description = "Get metadata about a user's default drive on OneDrive.",
                    HttpRequest ="GET /drive",
                    RequestBody="",
                    Link = "GetDefaultDrive",
                    HttpResponse = "",
                },
                new GraphService
                {
                    Name="List available drives",
                    Description = "List the available drives for a user (OneDrive) or SharePoint site (document libraries).",
                    HttpRequest ="GET /drives",
                    RequestBody="",
                    Link = "ListAvailableDrives",
                    HttpResponse = "",
                },
                new GraphService
                {
                    Name="List available drives",
                    Description = "List the available drives for a user (OneDrive) or SharePoint site (document libraries).",
                    HttpRequest ="GET /drives",
                    RequestBody="",
                    Link = "ListAvailableDrives",
                    HttpResponse = "",
                }
            };
        }
    }
}