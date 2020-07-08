using CSCAssignment1.App_Start;
using CSCAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CSCAssignment1.APIs
{
    public class ProductController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        [HttpGet]
        [Route("api/products")]
        public IEnumerable<Product> GetAllProducts()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 4);
            if (randomNumber >= 1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return repository.GetAll();
        }
        // ....

        [HttpGet]
        [Route("api/products/{id}")]
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpGet]
        [Route("api/products")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        [ValidateModel]
        [HttpPost]
        [Route("api/products")]
        public IHttpActionResult PostProduct([FromBody] Product item)
        {
            item = repository.Add(item);
            return Content(HttpStatusCode.Created, "Created");
        }

        [HttpPut]
        [Route("api/products/{id}")]
        public void PutProduct(int id, [FromBody] Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("api/products/{id}")]
        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
