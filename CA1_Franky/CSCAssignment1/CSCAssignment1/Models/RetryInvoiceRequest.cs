using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSCAssignment1.Models
{
    public class RetryInvoiceRequest
    {
        [JsonProperty("customerId")]
        public string Customer { get; set; }

        [JsonProperty("paymentMethodId")]
        public string PaymentMethod { get; set; }

        [JsonProperty("invoiceId")]
        public string Invoice { get; set; }
    }
}