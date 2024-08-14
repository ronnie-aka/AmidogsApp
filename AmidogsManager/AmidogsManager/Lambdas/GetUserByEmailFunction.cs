using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.APIGatewayEvents;
using AmidogsManager.BusinessLogic.Services;
using AmidogsManager.Database;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace AmidogsManager.Lambdas
{
    public class GetUserByEmailFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Get, "/getUserByEmail/{email}")]
        public APIGatewayProxyResponse GetUserByEmail(APIGatewayProxyRequest request, string email)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var userRepository = new UserRepository(amidogsManagerContext);
            var userService = new UserService(userRepository);

            try
            {
                var user = userService.GetUserByEmail(email);
                if (user == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Body = JsonConvert.SerializeObject(new { message = "User not found" }),
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", "application/json"},
                            {"Access-Control-Allow-Origin", "*"},
                            {"Access-Control-Allow-Credentials", "true"}
                        }
                    };
                }

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(user)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = JsonConvert.SerializeObject(new { message = "An error occurred" }),
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    }
                };
            }
        }
    }
}
