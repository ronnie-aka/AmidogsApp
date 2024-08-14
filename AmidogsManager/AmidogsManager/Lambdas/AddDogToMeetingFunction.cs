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
    public class AddDogToMeetingFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Post, "/addDogToMeeting")]
        public APIGatewayProxyResponse AddDogToMeeting(APIGatewayProxyRequest request)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var dogMeetingService = new DogMeetingService(dogMeetingRepository);

            try
            {
                // Deserializa el cuerpo de la solicitud para obtener los parámetros
                var requestBody = JsonConvert.DeserializeObject<AddDogToMeetingRequest>(request.Body);
                if (requestBody == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", "application/json"},
                            {"Access-Control-Allow-Origin", "*"},   // CORS header
                            {"Access-Control-Allow-Credentials", "true"}  // CORS header
                        },
                        Body = "Invalid request body"
                    };
                }

                // Llama al servicio para agregar el perro a la reunión
                dogMeetingService.AddDogToMeeting(requestBody.DogId, requestBody.MeetingId, requestBody.IsOwner);

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json"},
                        {"Access-Control-Allow-Origin", "*"},
                        {"Access-Control-Allow-Credentials", "true"}
                    },
                    Body = JsonConvert.SerializeObject(new { message = "Dog added to the meeting successfully" })
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
                        {"Access-Control-Allow-Origin", "*"},   // CORS header
                        {"Access-Control-Allow-Credentials", "true"}  // CORS header
                    },
                    Body = "An error occurred"
                };
            }
        }
    }

    // Clase para representar el cuerpo de la solicitud
    public class AddDogToMeetingRequest
    {
        public int DogId { get; set; }
        public int MeetingId { get; set; }
        public bool IsOwner { get; set; }
    }
}
