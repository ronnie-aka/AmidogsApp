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
    public class DeleteMatchByIdFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Delete, "/deleteMatch/{matchId}")]
        public APIGatewayProxyResponse DeleteMatchById(APIGatewayProxyRequest request, int matchId)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var matchRepository = new MatchRepository(amidogsManagerContext);
            var matchService = new MatchService(matchRepository);

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
                    Body = JsonConvert.SerializeObject(matchService.DeleteMatchById(matchId))
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