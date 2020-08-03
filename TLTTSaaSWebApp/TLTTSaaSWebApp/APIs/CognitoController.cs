using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
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
    public class CognitoController : ApiController
    {
        const string poolId = "us-east-1_jyucfcBOf";
        const string clientId = "61esl6oat0h8e2o7gt2ij8dtp5";
        static Amazon.RegionEndpoint region = Amazon.RegionEndpoint.USEast1;

        [HttpPost]
        [Route("api/cognito/signup")]
        public async Task<IHttpActionResult> signUp([FromBody] JObject data)
        {
            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), region);
            SignUpRequest signUpRequest = new SignUpRequest()
            {
                ClientId = clientId,
                Username = data["email"].ToString(),
                Password = data["password"].ToString()
            };

            List<AttributeType> attributes = new List<AttributeType>()
                {
                    new AttributeType(){Name="email", Value = data["email"].ToString()},
                    new AttributeType(){ Name="custom:role",Value = "free"}
                };

            signUpRequest.UserAttributes = attributes;

            try
            {
                SignUpResponse result = await provider.SignUpAsync(signUpRequest);
                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("api/cognito/signin")]
        public async Task<IHttpActionResult> signIn([FromBody] JObject data)
        {
            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), region);

            CognitoUserPool userPool = new CognitoUserPool(poolId, clientId, provider);

            CognitoUser user = new CognitoUser(data["email"].ToString(), clientId, userPool, provider);

            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = data["password"].ToString()
            };

            AuthFlowResponse authResponse = null;
            try
            {
                authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);

                GetUserRequest getUserRequest = new GetUserRequest();
                getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;

                GetUserResponse getUser = await provider.GetUserAsync(getUserRequest);
                return Ok("Success!");
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
