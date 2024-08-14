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
        private readonly DogMeetingRepository dogMeetingRepository;


        public DogService(DogRepository dogRepository, MatchRepository matchRepository, DogMeetingRepository dogMeetingRepository)
        {
            this.dogRepository = dogRepository;
            this.matchRepository = matchRepository;
            this.dogMeetingRepository = dogMeetingRepository;
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
        public List<Dog> GetDogsInMeeting(int meetingId)
        {
            try
            {
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogsInMeeting(meetingId).ToList();
                List<Dog> dogsInMeeting = new List<Dog>();

                foreach (var dogMeeting in dogMeetings)
                {
                    Dog? dog = dogRepository.GetDogById(dogMeeting.DogId);
                    if (dog != null)
                    {
                        dogsInMeeting.Add(dog);
                    }
                }

                return dogsInMeeting;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateDog(Dog updatedDog)
        {
            try
            {
                dogRepository.UpdateDog(updatedDog);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
