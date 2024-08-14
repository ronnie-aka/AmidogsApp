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
    public class GetMeetingsWithOutDogFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Get, "/getMeetingsWithOutDog/{dogId}")]
        public APIGatewayProxyResponse GetMeetingsWithOutDog(APIGatewayProxyRequest request, int dogId)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var meetingRepository = new MeetingRepository(amidogsManagerContext);
            var meetingServices = new MeetingService(dogMeetingRepository, meetingRepository);

            try
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                {
                    {"Content-Type", "application/json"},
                    {"Access-Control-Allow-Origin", "*"},
                    {"Access-Control-Allow-Credentials", "true"},
                    {"Access-Control-Allow-Methods", "GET, OPTIONS"},
                    {"Access-Control-Allow-Headers", "Content-Type, Authorization"}
                },
                    Body = JsonConvert.SerializeObject(meetingServices.GetMeetingsWithOutDog(dogId))
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
                        {"Access-Control-Allow-Origin", "*"}
                    }
                };
            }
        }
    }
}