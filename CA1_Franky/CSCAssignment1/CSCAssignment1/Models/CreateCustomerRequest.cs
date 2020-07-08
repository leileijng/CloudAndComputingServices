using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class CreateCustomerRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}