using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CSCAssignment1.APIs
{
    public class TwilioController : ApiController
    {
        [HttpPost]
        [Route("api/twilio/sendMessage")]
        public IHttpActionResult sendMessage(string phoneNum, [FromBody] JObject data)
        {
            try
            {
                string accountSid = "*";
                string authToken = "*";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Today's date: " + data["date"] + ". Positives: " + data["positives"] + ". Negatives: " + data["negatives"] + ". Hospitalized: " + data["hospitalized"] + ". Deaths: " + data["death"] + ". Recovered: " + data["recovered"],
                    from: new Twilio.Types.PhoneNumber("+*"),
                    to: new Twilio.Types.PhoneNumber("+" + phoneNum)
                );
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
