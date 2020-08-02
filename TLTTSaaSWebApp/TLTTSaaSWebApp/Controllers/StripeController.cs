using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSCAssignment1.Controllers
{
    public class StripeController : Controller
    {
        
        public ActionResult Account()
        {
            return View();
        }
        public ActionResult Prices()
        {
            return View();
        }
        public ActionResult Comment()
        {
            return View();
        }
    }
}