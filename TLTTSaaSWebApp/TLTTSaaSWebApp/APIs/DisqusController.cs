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
                var disqus = new DisqusApi(DisqusAuthMethod.SecretKey, "sK1KCvZWQi0AdyjTMlabYSokcxsOWNNNF48fXpVK4EbU79z0unBTJr3fNXq8ubAW");
                var request = DisqusForumCreateRequest.New(id, id);
                var response = await disqus.Forums
                                .CreateAsync(DisqusAccessToken.Create("d9f8c170020040769ff7b7e92e81565f"), request)
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
