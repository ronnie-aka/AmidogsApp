using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using AmidogsManager.BusinessLogic.Services;
using AmidogsManager.Database.Models;
using AmidogsManager.Database;
using AmidogsManager.Repository.Repositories;

namespace AmidogsManager.Lambdas
{
    public class CreateMeetingFunction : BaseLambdaFunction
    {
        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Post, "/createMeeting")]
        public APIGatewayProxyResponse CreateMeeting(APIGatewayProxyRequest request)
        {
            // Inicializa el contexto y los repositorios
            var amidogsManagerContext = new AmidogsManagerContext();
            var meetingRepository = new MeetingRepository(amidogsManagerContext);
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var meetingService = new MeetingService(dogMeetingRepository, meetingRepository);

            try
            {
                // Deserializa el cuerpo de la solicitud para obtener la nueva reunión
                var meeting = JsonConvert.DeserializeObject<Meeting>(request.Body);

                // Verifica si la reunión ya existe (puedes personalizar esta lógica según tus necesidades)
                var existingMeeting = meetingService.GetMeetingById(meeting.Id);
                if (existingMeeting != null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Conflict,
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", "application/json"},
                            {"Access-Control-Allow-Origin", "*"},
                            {"Access-Control-Allow-Credentials", "true"},
                            {"Access-Control-Allow-Headers", "Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token"},
                            {"Access-Control-Allow-Methods", "POST,OPTIONS"}
                        },
                        Body = "Meeting already exists with the same ID"
                    };
                }

                // Crea la nueva reunión usando el servicio
                var creationResult = meetingService.CreateMeeting(meeting);

                if (creationResult == "CREATED")
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", "application/json"},
                            {"Access-Control-Allow-Origin", "*"},
                            {"Access-Control-Allow-Credentials", "true"},
                            {"Access-Control-Allow-Headers", "Content-Type,X-Amz-Date,Authorization,X-Api-Key,X-Amz-Security-Token"},
                            {"Access-Control-Allow-Methods", "POST,OPTIONS"}
                        },
                        Body = JsonConvert.SerializeObject(meeting)
                    };
                }
                else
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Headers = new Dictionary<string, string>
                        {
                            {"Content-Type", "application/json"},
                            {"Access-Control-Allow-Origin", "*"},
                            {"Access-Control-Allow-Credentials", "true"}
                        },
                        Body = creationResult
                    };
                }
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
                    Body = ex.Message
                };
            }
        }
    }
}
