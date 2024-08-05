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
using AmidogsManager.Database.Models;

namespace AmidogsManager.Lambdas
{
    public class UpdateMeetingFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Put, "/UpdateMeeting/{meetingId}")]
        public APIGatewayProxyResponse UpdateMeeting(APIGatewayProxyRequest request, int meetingId)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var meetingRepository = new MeetingRepository(amidogsManagerContext);
            var meetingServices = new MeetingService(dogMeetingRepository, meetingRepository);

            try
            {
                // Deserializar el cuerpo de la solicitud para obtener los datos actualizados de la quedada
                var updatedMeeting = JsonConvert.DeserializeObject<Meeting>(request.Body);

                string result = meetingServices.UpdateMeeting(meetingId, updatedMeeting);

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)(result == "UPDATED" ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(new { message = result })
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(new { message = "Error updating meeting" })
                };
            }
        }
    }
}
