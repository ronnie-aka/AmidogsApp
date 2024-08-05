using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class DogService
    {
        private readonly DogRepository dogRepository;
        private readonly MatchRepository matchRepository;


        public DogService(DogRepository dogRepository, MatchRepository matchRepository)
        {
            this.dogRepository = dogRepository;
            this.matchRepository = matchRepository;
        }

        public Dog GetDogByUser(int userId)
        {
            try
            {
                Dog? dog = dogRepository.GetByUser(userId);
                return dog ?? throw new InvalidOperationException($"No dog found for user {userId}");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Dog> GetUnmatchedDogs(int dogId)
        {
            try
            {
                // Obtener todos los perros
                List<Dog> allDogs = dogRepository.GetAllDogs();

                // Obtener los IDs de los perros con los que ya ha hecho match
                List<int> matchedDogIds = matchRepository.GetMatchedDogIds(dogId);

                // Filtrar los perros para excluir aquellos con los que ya ha hecho match
                return allDogs.Where(d => d.Id != dogId && !matchedDogIds.Contains(d.Id)).ToList();
            }
            catch (Exception )
            {
                throw;
            }
        }
        public Dog GetDogById(int dogId)
        {
            try
            {
                Dog? dog = dogRepository.GetDogById(dogId);
                return dog ?? throw new InvalidOperationException($"No dog found with ID {dogId}");
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
