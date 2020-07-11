using CSCAssignment1.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CSCAssignment1.Controllers
{
    public class TasksController : Controller
    {
        public ActionResult Task1()
        {
            return View();
        }

        public ActionResult Task2()
        {
            return View();
        }

        public ActionResult Task3()
        {
            return View();
        }

        public ActionResult Task4()
        {
            return View();
        }

        public ActionResult Task5()
        {
            return View();
        }

        // GET: Charge
        public ActionResult Task6()
        {
            return View(new StripeChargeModel());
        }
        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Task6(StripeChargeModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var chargeId = await ProcessPayment(model);
        //    // You should do something with the chargeId --> Persist it maybe?

        //    return View();
        //}

        //private static async Task<string> ProcessPayment(StripeChargeModel model)
        //{
        //    return await Task.Run(() =>
        //    {
        //        var myCharge = new StripeChargeCreateOptions
        //        {
        //            // convert the amount of £12.50 to pennies i.e. 1250
        //            Amount = (int)(model.Amount * 100),
        //            Currency = "sgd",
        //            Description = "Description for test charge",
        //            Source = new StripeSourceOptions
        //            {
        //                TokenId = model.Token
        //            }
        //        };

        //        var chargeService = new StripeChargeService("sk_test_51Gwq3PGOehmsxlG6tAGac6flfsN8Abd03DinRcVj39s7Q46qRe9aiYsN6vx95IpBVuJGx7cgPNUKbYfBNoNKHhZY00FtIqHMRK");
        //        var stripeCharge = chargeService.Create(myCharge);

        //        return stripeCharge.Id;
        //    });
        //}

        public ActionResult Task7()
        {
            return View();
        }

        public ActionResult Task8()
        {
            return View();
        }

        public ActionResult ExtraFeature()
        {
            return View();
        }
    }
}