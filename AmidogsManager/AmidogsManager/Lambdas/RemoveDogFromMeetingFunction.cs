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
    public class RemoveDogFromMeetingFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Post, "/removeDogFromMeeting")]
        public APIGatewayProxyResponse RemoveDogFromMeeting(APIGatewayProxyRequest request)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var dogMeetingService = new DogMeetingService(dogMeetingRepository);

            try
            {
                // Deserializa el cuerpo de la solicitud para obtener los parámetros
                var requestBody = JsonConvert.DeserializeObject<RemoveDogFromMeetingRequest>(request.Body);
                if (requestBody == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Body = "Invalid request body"
                    };
                }

                // Llama al servicio para eliminar el perro de la reunión
                dogMeetingService.RemoveDogFromMeeting(requestBody.DogId, requestBody.MeetingId);

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(new { message = "Dog removed from the meeting successfully" })
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
                    Body = JsonConvert.SerializeObject(new { error = "An error occurred while removing the dog from the meeting" })
                };
            }
        }
    }

    // Clase para representar el cuerpo de la solicitud
    public class RemoveDogFromMeetingRequest
    {
        public int DogId { get; set; }
        public int MeetingId { get; set; }
    }
}
