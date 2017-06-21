using OneDriveIntergration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneDriveIntergration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var catetories = GetCatogroies();

            return View(catetories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private Dictionary<string, string> GetCatogroies()
        {
            return new Dictionary<string, string>
            {
                {"Drives", "Drives"},
                {"File handlers", "FileHandlers"},
                {"Items", "Items"},
                {"Misc", "Misc"} ,
                {"Sharing", "Sharing"},
                {"Sites", "Sites" },
                {"WebHooks", "WebHooks" },
                {"Resources", "Resources"} ,
                { "Facets", "Facets"}
            };
        }
    }
}