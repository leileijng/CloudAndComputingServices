using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BearerTokenAndreCAPTCHA.Controllers
{
    [System.Web.Http.Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        [RequireHttps]
        public string Get()
        {
            var userName = this.RequestContext.Principal.Identity.Name;
            return String.Format("Hello, {0}.", userName);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
