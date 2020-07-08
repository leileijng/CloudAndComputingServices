using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class CancelSubscriptionRequest
    {
        [JsonProperty("subscriptionId")]
        public string Subscription { get; set; }
    }
}