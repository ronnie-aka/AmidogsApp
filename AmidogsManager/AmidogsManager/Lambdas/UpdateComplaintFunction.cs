using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.APIGatewayEvents;
using AmidogsManager.BusinessLogic.Services;
using AmidogsManager.Database;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AmidogsManager.Lambdas
{
    public class UpdateComplaintFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Get, "/updateComplaint/{userId}")]
        public APIGatewayProxyResponse UpdateComplaint(APIGatewayProxyRequest request, int userId)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var userRepository = new UserRepository(amidogsManagerContext);
            var userService = new UserService(userRepository);

            try
            {
                var resultMessage = userService.UpdateComplaintNumber(userId);
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(new { message = resultMessage })
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(new { message = "Error al realizar la denuncia" })
                };
            }
        }
    }
}
