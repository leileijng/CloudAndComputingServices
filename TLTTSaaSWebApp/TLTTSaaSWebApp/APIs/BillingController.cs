using Newtonsoft.Json.Linq;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TLTTSaaSWebApp
{
    
    public class BillingController : ApiController
    {
        public BillingController()
        {
            StripeConfiguration.ApiKey = "sk_test_51Gwq3PGOehmsxlG6tAGac6flfsN8Abd03DinRcVj39s7Q46qRe9aiYsN6vx95IpBVuJGx7cgPNUKbYfBNoNKHhZY00FtIqHMRK";
        }

        [Route("api/billing/Config")]
        [HttpGet]
        public IHttpActionResult GetConfig()
        {
            var ConfigResponse = new ConfigResponse
            {
                PublishableKey = "pk_test_51Gwq3PGOehmsxlG6geFtIva46k5whEkw4zilkdtcyWs214rEpQ42odUiUU7Jd1AbcR3qYJsmtsnEjiiOqO3bHCVB00f32wzlPS",
            };
            return Ok(ConfigResponse);
        }

        [Route("api/billing/create-customer")]
        [HttpPost]
        public IHttpActionResult CreateCustomer([FromBody] CreateCustomerRequest req)
        {
            var options = new CustomerCreateOptions
            {
                Email = req.Email,
            };
            var service = new CustomerService();
            var customer = service.Create(options);
            var customer1 = new CreateCustomerResponse
            {
                Customer = customer,
            };
            return Ok(customer1);
        }

        [Route("api/billing/create-subscription")]
        [HttpPost]
        public IHttpActionResult CreateSubscription([FromBody] CreateSubscriptionRequest req)
        {
            // Attach payment method
            var options = new PaymentMethodAttachOptions
            {
                Customer = req.Customer,
            };
            var service = new PaymentMethodService();
            var paymentMethod = service.Attach(req.PaymentMethod, options);

            // Update customer's default invoice payment method
            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethod.Id,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(req.Customer, customerOptions);
            string newPrice;
            if (req.Price == "FREE")
            {
                newPrice = "price_1H3k4hGOehmsxlG6VM3V7aCr";
            }
            else
            {
                newPrice = "price_1H3k59GOehmsxlG6ZCM83guz";
            }
            // Create subscription
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = req.Customer,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = newPrice
                    },
                },
            };
            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var subscriptionService = new SubscriptionService();
            try
            {
                Subscription subscription = subscriptionService.Create(subscriptionOptions);
                return Ok(subscription);
            }
            catch (StripeException e)
            {
                Console.WriteLine($"Failed to create subscription.{e}");
                return BadRequest();
            }
        }
        [Route("api/billing/retry-invoice")]
        [HttpPost]
        public IHttpActionResult RetryInvoice([FromBody] RetryInvoiceRequest req)
        {
            // Attach payment method
            var options = new PaymentMethodAttachOptions
            {
                Customer = req.Customer,
            };
            var service = new PaymentMethodService();
            var paymentMethod = service.Attach(req.PaymentMethod, options);

            // Update customer's default invoice payment method
            var customerOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethod.Id,
                },
            };
            var customerService = new CustomerService();
            customerService.Update(req.Customer, customerOptions);

            var invoiceOptions = new InvoiceGetOptions();
            invoiceOptions.AddExpand("payment_intent");
            var invoiceService = new InvoiceService();
            Invoice invoice = invoiceService.Get(req.Invoice, invoiceOptions);
            return Ok(invoice);
        }
        [Route("api/billing/cancel-subscription")]
        [HttpPost]
        public IHttpActionResult CancelSubscription([FromBody] CancelSubscriptionRequest req)
        {
            var service = new SubscriptionService();
            var subscription = service.Cancel(req.Subscription, null);
            return Ok(subscription);
        }
        [Route("api/billing/retrieve-customer-payment-method")]
        [HttpPost]
        public IHttpActionResult RetrieveCustomerPaymentMethod([FromBody] RetrieveCustomerPaymentMethodRequest req)
        {
            var service = new PaymentMethodService();
            var paymentMethod = service.Get(req.PaymentMethod);
            return Ok(paymentMethod);
        }
        [Route("api/billing/update-subscription")]
        [HttpPost]
        public IHttpActionResult UpdateSubscription([FromBody] UpdateSubscriptionRequest req)
        {
            var service = new SubscriptionService();
            var subscription = service.Get(req.Subscription);
            string newPrice;
            if (req.NewPrice == "FREE")
            {
                newPrice = "price_1H3k4hGOehmsxlG6VM3V7aCr";
            }
            else
            {
                newPrice = "price_1H3k59GOehmsxlG6ZCM83guz";
            }
            var options = new SubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = false,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Id = subscription.Items.Data[0].Id,
                        Price = newPrice,
                    }
                }
            };
            var updatedSubscription = service.Update(req.Subscription, options);
            return Ok(updatedSubscription);
        }
        [Route("api/billing/retrieve-upcoming-invoice")]
        [HttpPost]
        public IHttpActionResult RetrieveUpcomingInvoice([FromBody] RetrieveUpcomingInvoiceRequest req)
        {
            var service = new SubscriptionService();
            var subscription = service.Get(req.Subscription);
            string newPrice;
            if (req.NewPrice == "FREE")
            {
                newPrice = "price_1H3k4hGOehmsxlG6VM3V7aCr";
            }
            else
            {
                newPrice = "price_1H3k59GOehmsxlG6ZCM83guz";
            }
            var invoiceService = new InvoiceService();
            var options = new UpcomingInvoiceOptions
            {
                Customer = req.Customer,
                Subscription = req.Subscription,
                SubscriptionItems = new List<InvoiceSubscriptionItemOptions>
                {
                    new InvoiceSubscriptionItemOptions
                    {
                        Id = subscription.Items.Data[0].Id,
                        Deleted = true,
                    },
                    new InvoiceSubscriptionItemOptions
                    {
                        // TODO: This should be Price, but isnt in Stripe.net yet.
                        Plan = newPrice,
                        Deleted = false,
                    },
                }
            };
            Invoice upcoming = invoiceService.Upcoming(options);
            return Ok(upcoming);
        }
        [Route("api/billing/stripe-webhook")]
        [HttpPost]
        public IHttpActionResult Webhook()
        {
            var httpContext = HttpContext.Current;
            var json = new StreamReader(httpContext.Request.InputStream).ReadToEnd();
            Event stripeEvent;
            IEnumerable<string> headerValues = Request.Headers.GetValues("Stripe-Signature");
            var id = headerValues.FirstOrDefault();
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    id,
                     "whsec_cQYt5vQPstBPqr30yrotHeNJaNfXV7S7"
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }
            
            if (stripeEvent.Type == "invoice.payment_succeeded")
            {
                // Used to provision services after the trial has ended.
                // The status of the invoice will show up as paid. Store the status in your
                // database to reference when a user accesses your service to avoid hitting rate
                // limits.
            }
            if (stripeEvent.Type == "invoice.payment_failed")
            {
                // If the payment fails or the customer does not have a valid payment method,
                // an invoice.payment_failed event is sent, the subscription becomes past_due.
                // Use this webhook to notify your user that their payment has
                // failed and to retrieve new card details.
            }
            if (stripeEvent.Type == "invoice.finalized")
            {
                // If you want to manually send out invoices to your customers
                // or store them locally to reference to avoid hitting Stripe rate limits.
            }
            if (stripeEvent.Type == "customer.subscription.created")
            {
            }
            if (stripeEvent.Type == "charge.succeeded")
            {
                //subscriptionrec sub = new subscriptionrec();
                var data = stripeEvent.ToJson();
                JObject jObject = JObject.Parse(data);
                JObject getData = JObject.Parse(jObject["data"].ToString());
                JObject getObject = JObject.Parse(getData["object"].ToString());
                JObject getName = JObject.Parse(getObject["billing_details"].ToString());
                //sub.customerid = getObject["customer"].ToString();
                //sub.name = getName["name"].ToString();
                //sub.amount = int.Parse(getObject["amount"].ToString());
                //sub.status = "Payment Successful";
                //dB.subscriptionrecs.Add(sub);
                //dB.SaveChanges();
            }
            if (stripeEvent.Type == "customer.subscription.deleted")
            {
                var data = stripeEvent.ToJson();
                JObject jObject = JObject.Parse(data);
                JObject getData = JObject.Parse(jObject["data"].ToString());
                JObject getObject = JObject.Parse(getData["object"].ToString());
                //string customerid = getObject["customer"].ToString();
                //subscriptionrec sub = dB.subscriptionrecs.Single(x => x.customerid == customerid);
                //sub.status = "Subscription Cancelled";
                //dB.SaveChanges();

            }
            if (stripeEvent.Type == "customer.subscription.trial_will_end")
            {
                // Send notification to your user that the trial will end
            }

            return Ok();
        }
        
    }
}
