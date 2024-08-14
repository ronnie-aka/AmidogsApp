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
using AmidogsManager.Lambdas;
using Newtonsoft.Json;

namespace AmidogsManager.Lambdas
{
    public class GetUnmatchedDogsFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Get, "/getUnmatchedDogs/{dogId}")]
        public APIGatewayProxyResponse GetUnmatchedDogs(APIGatewayProxyRequest request, int dogId)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var dogRespository = new DogRepository(amidogsManagerContext);
            var matchRepository = new MatchRepository(amidogsManagerContext);
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var dogServices = new DogService(dogRespository, matchRepository, dogMeetingRepository);

            try
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                {
                    {"Content-Type", "application/json"},
                    {"Access-Control-Allow-Origin", "*"},
                    {"Access-Control-Allow-Credentials", "true"}
                },
                    Body = JsonConvert.SerializeObject(dogServices.GetUnmatchedDogs(dogId))
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
        }
    }
}