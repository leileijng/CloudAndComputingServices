using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace TLTTSaaSWebApp.Controllers
{
    public class TalentsController : Controller
    {
        public ActionResult Manage()
        {
            ViewBag.Title = "Manage Talents";

            return View();
        }
        [Authorize(Roles = "Premium")]
        public ActionResult Details()
        {
            ViewBag.Title = "Talent's Detail";

            return View();
        }
        public ActionResult Glacier()
        {
            ViewBag.Title = "Upload Archive";

            return View();
        }
        public ActionResult Charts()
        {
            ViewBag.Title = "Charts";

            return View();
        }
    }
}