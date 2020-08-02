using Disqus.NET;
using Disqus.NET.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TLTTSaaSWebApp.APIs
{
    public class DisqusController : ApiController
    {
        [HttpPost]
        [Route("api/disqus/createforum")]
        public async Task<IHttpActionResult> createForum(string id)
        {
            try
            {
                var disqus = new DisqusApi(DisqusAuthMethod.SecretKey, "XedGyVIwIrInXSosRK1nTQNj5oSvPfMrY1MP941uQJkUhNOU6usidjNPk1ddy8tc");
                var request = DisqusForumCreateRequest.New(id, id);
                var response = await disqus.Forums
                                .CreateAsync(DisqusAccessToken.Create("e536487e23e24f23bb306391858ceb5f"), request)
                                .ConfigureAwait(false);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
