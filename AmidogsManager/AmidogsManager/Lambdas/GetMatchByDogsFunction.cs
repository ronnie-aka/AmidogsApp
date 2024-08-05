using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using AmidogsManager.BusinessLogic.Services;
using AmidogsManager.Database;
using AmidogsManager.Repository.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace AmidogsManager.Lambdas
{
    public class GetMatchByDogsFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Get, "/getMatchByDogs/{dog1Id}/{dog2Id}")]
        public APIGatewayProxyResponse GetMatchByDogs(APIGatewayProxyRequest request, int dog1Id, int dog2Id)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var matchRepository = new MatchRepository(amidogsManagerContext);
            var matchService = new MatchService(matchRepository);

            try
            {
                var match = matchService.GetMatchByDogs(dog1Id, dog2Id);
                if (match == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                        Body = "Match not found"
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
                    Body = JsonConvert.SerializeObject(match)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Body = ex.Message
                };
            }
        }
    }
}
