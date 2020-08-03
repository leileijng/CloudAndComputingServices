using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TLTTSaaSWebApp.APIs
{
    public class CognitoController : ApiController
    {
        const string poolId = "us-east-1_XxNsHESMF";
        const string clientId = "7qnpq5ps6ofs0fabpphek0tg9t";
        static Amazon.RegionEndpoint region = Amazon.RegionEndpoint.USEast1;

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("signUp")]
        public async Task<JsonResult> signUp([FromBody] string email, [FromBody] string username, [FromBody] string password)
        {
            return Json(await SignUpUser(email, username, password), JsonRequestBehavior.AllowGet);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("signIn")]
        public async Task<JsonResult> signIn([FromBody] string email, [FromBody] string username, [FromBody] string password)
        {
            return Json(await SignInUser(email, username, password), JsonRequestBehavior.AllowGet);
        }

        public static async Task<List<object>> SignUpUser(string email, string userName, string password)
        {
            List<object> recordList = new List<object>();

            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), region);
            SignUpRequest signUpRequest = new SignUpRequest()
            {
                ClientId = clientId,
                Username = userName,
                Password = password
            };

            List<AttributeType> attributes = new List<AttributeType>()
            {
                new AttributeType(){ Name="custom:UserStatus",Value = "freeUser"}
            };

            signUpRequest.UserAttributes = attributes;

            try
            {
                SignUpResponse result = await provider.SignUpAsync(signUpRequest);
                recordList.Add(new
                {
                    data = "successfully signed up!",
                    returnType = "pass"
                });
                return recordList;
            }
            catch (Exception ex)
            {
                recordList.Add(new
                {
                    data = ex.Message,
                    returnType = "fail"
                });
                return recordList;
            }
        }

        public static async Task<List<object>> SignInUser(string email, string userName, string password)
        {
            List<object> recordList = new List<object>();

            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), region);

            CognitoUserPool userPool = new CognitoUserPool(poolId, clientId, provider);

            CognitoUser user = new CognitoUser(userName, clientId, userPool, provider);

            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = password
            };

            AuthFlowResponse authResponse = null;
            try
            {
                authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                recordList.Add(new
                {
                    data = ex.Message,
                    returnType = "fail"
                });
                return recordList;
            }

            GetUserRequest getUserRequest = new GetUserRequest();
            getUserRequest.AccessToken = authResponse.AuthenticationResult.AccessToken;

            GetUserResponse getUser = await provider.GetUserAsync(getUserRequest);
            recordList.Add(new
            {
                data = getUser,
                returnType = "pass"
            });
            return recordList;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("setSession")]
        public bool setSession([FromBody] string userName, [FromBody] string userType)
        {
            Session["username"] = userName;
            Session["usertype"] = userType;

            return true;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getData")]
        public string getData()
        {
            string ConnectionFormat = "Server={0}; Database={1}; Uid=admin; Pwd=password";
            string Database = "Talent";
            string Server = "csc.cv5e97rh2o9r.us-east-1.rds.amazonaws.com,1433";
            //SqlConnection con = new SqlConnection(@"Data Source=rm-6gjt0i37hchv8r98ono.mssql.ap-south-1.rds.aliyuncs.com,3433;Initial Catalog=mydb;User ID=testuser;Password=Testuser1234");

            //string.Format(ConnectionFormat, Database, Server)
            //"Server=csc.cv5e97rh2o9r.us-east-1.rds.amazonaws.com,1433;Database=Talent;Uid=admin;Pwd=password"
            try
            {
                using (SqlConnection connection = new SqlConnection(string.Format(ConnectionFormat, Server, Database)))
                {
                    Console.WriteLine(connection.ConnectionString);
                    connection.Open();
                    Console.WriteLine("Success");
                    connection.Close();
                }
                return "1";
            }
    }
}
