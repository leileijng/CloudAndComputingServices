using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TLTTSaaSWebApp.APIs
{
    public class DynamoController : ApiController
    {
        [HttpPost]
        [Route("api/Dynamo/UploadAccount")]
        public async Task<IHttpActionResult> PostAsync([FromBody] JObject data)
        {
            using (IAmazonDynamoDB ddbClient = new AmazonDynamoDBClient())
            {
                try
                {
                    //Save user in table
                    await ddbClient.PutItemAsync(new PutItemRequest
                    {
                        TableName = "Account",
                        Item = new Dictionary<string, AttributeValue>
                        {
                            { "email",  new AttributeValue{S = data["email"].ToString() } },
                            { "customer_id",  new AttributeValue{S =  data["customer_id"].ToString()} },
                            { "price", new AttributeValue{S = data["price"].ToString() } },
                            { "last_updated", new AttributeValue{S = DateTime.Now.ToString() } },
                        }
                    });
                    return Ok("Success!");
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            }
        }

        [HttpGet]
        [Route("api/Dynamo/RetrieveAccount/{email}")]
        public async Task<IHttpActionResult> GetAsync(string email)
        {
            using (IAmazonDynamoDB ddbClient = new AmazonDynamoDBClient())
            {
                try
                {
                    Dictionary<string, AttributeValue> item = (await ddbClient.GetItemAsync(new GetItemRequest
                    {
                        TableName = "Account",
                        ConsistentRead = true,
                        Key = new Dictionary<string, AttributeValue>
                        {
                           {"email",new AttributeValue{S = email} }
                        }
                    })).Item;
                    return Ok(item);
                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());
                }
            }
        }
    }
}

