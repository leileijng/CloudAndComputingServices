using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class ConfigResponse
    {
        [JsonProperty("publishableKey")]
        public string PublishableKey { get; set; }
    }
}