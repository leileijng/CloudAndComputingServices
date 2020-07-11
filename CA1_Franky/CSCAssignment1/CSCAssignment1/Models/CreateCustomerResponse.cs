using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stripe;

namespace CSCAssignment1.Models
{
    public class CreateCustomerResponse
    {
        [JsonProperty("customer")]
        public Customer Customer { get; set; }
    }
}
