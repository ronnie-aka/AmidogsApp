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
    public class UpdateDogFunction : BaseLambdaFunction
    {
        private static readonly Dictionary<string, string> corsHeaders = new Dictionary<string, string>
        {
            {"Content-Type", "application/json"},
            {"Access-Control-Allow-Origin", "*"},
            {"Access-Control-Allow-Credentials", "true"},
            {"Access-Control-Allow-Headers", "Content-Type,Authorization,X-Amz-Date,X-Api-Key,X-Amz-Security-Token"},
            {"Access-Control-Allow-Methods", "OPTIONS,GET,POST,PUT,DELETE"}
        };

        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Put, "/UpdateDog/{dogId}")]
        public APIGatewayProxyResponse UpdateDog(APIGatewayProxyRequest request, int dogId)
        {
            var amidogsManagerContext = new AmidogsManagerContext();
            var dogRepository = new DogRepository(amidogsManagerContext);
            var matchRepository = new MatchRepository(amidogsManagerContext);
            var dogMeetingRepository = new DogMeetingRepository(amidogsManagerContext);
            var dogService = new DogService(dogRepository, matchRepository, dogMeetingRepository);

            try
            {
                // Deserializar el cuerpo de la solicitud para obtener los datos actualizados del perro
                var updatedDog = JsonConvert.DeserializeObject<Dog>(request.Body);

                // Verificar si el perro existe antes de intentar actualizar
                var existingDog = dogService.GetDogById(dogId);
                if (existingDog == null)
                {
                    return new APIGatewayProxyResponse
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Headers = corsHeaders,
                        Body = JsonConvert.SerializeObject(new { message = "Dog not found" })
                    };
                }

                // Establecer el ID del perro para asegurarse de que se actualiza el correcto
                updatedDog.Id = dogId;

                // Actualizar el perro
                dogService.UpdateDog(updatedDog);

                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Headers = corsHeaders,
                    Body = JsonConvert.SerializeObject(new { message = "Dog updated successfully" })
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Headers = corsHeaders,
                    Body = JsonConvert.SerializeObject(new { message = "Error updating dog" })
                };
            }
        }

        [LambdaFunction(Policies = "AWSLambdaBasicExecutionRole, AWSLambdaVPCAccessExecutionRole", MemorySize = 256, Timeout = 30)]
        [RestApi(LambdaHttpMethod.Options, "/UpdateDog/{dogId}")]
        public APIGatewayProxyResponse Options(APIGatewayProxyRequest request, int dogId)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Headers = corsHeaders,
                Body = JsonConvert.SerializeObject(new { message = "CORS preflight successful" })
            };
        }
    }
}
