using CSCAssignment1.Filters;
using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CSCAssignment1.APIs
{
    public class TalentsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [RequireHttps]
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [RequireHttps]
        [Route("api/talents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

    }
}
