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
        [Authorize]
        public ActionResult Manage()
        {
            ViewBag.Title = "Manage Talents";

            return View();
        }
    }
}