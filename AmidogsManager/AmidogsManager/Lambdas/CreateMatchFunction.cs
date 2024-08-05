using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.APIGatewayEvents;
using AmidogsManager.BusinessLogic.Services;
using AmidogsManager.Database;
using AmidogsManager.Repository.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using AmidogsManager.Database.Models;

namespace AmidogsManager.Lambdas
{
    public class CreateMatchFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Post, "/createMatch")]
        public APIGatewayProxyResponse CreateMatch(APIGatewayProxyRequest request)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var matchRepository = new MatchRepository(amidogsManagerContext);
            var matchService = new MatchService(matchRepository);

            try
            {
                var match = JsonConvert.DeserializeObject<Match>(request.Body);

                if (matchService.GetMatchByDogs(match.DogId1, match.DogId2) is Match existingMatch)
                {
                    if ((existingMatch.DogId1 == match.DogId1 && existingMatch.LikeDog2) ||
                        (existingMatch.DogId2 == match.DogId1 && existingMatch.LikeDog1))
                    {
                        match.LikeDog1 = true;
                        match.LikeDog2 = true;
                        match.MatchDate = DateTime.Now;
                    }
                }

                var createdMatch = matchService.CreateOrUpdateMatch(match);

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(createdMatch)
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
