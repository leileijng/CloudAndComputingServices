using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace TalentSearchWebAPI.Controllers
{
    public class TalentsController : Controller
    {
        public ActionResult Manage()
        {
            ViewBag.Title = "Manage Talents";

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Add Talents";

            return View();
        }

        public ActionResult Update()
        {
            ViewBag.Title = "Update Talents";

            return View();
        }

        public ActionResult Details()
        {
            ViewBag.Title = "Talent's Detail";

            return View();
        }
    }
}
