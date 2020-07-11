using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class CreateSubscriptionRequest
    {
        [JsonProperty("paymentMethodId")]
        public string PaymentMethod { get; set; }

        [JsonProperty("customerId")]
        public string Customer { get; set; }

        [JsonProperty("priceId")]
        public string Price { get; set; }
    }
}