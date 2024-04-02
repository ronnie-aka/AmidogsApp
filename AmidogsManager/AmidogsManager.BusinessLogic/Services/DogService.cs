using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class DogService
    {
        private readonly DogRepository dogRepository;
        private readonly DogMeetingRepository dogMeetingRepository;

        public DogService(DogRepository dogRepository, DogMeetingRepository dogMeetingRepository)
        {
            this.dogRepository = dogRepository;
            this.dogMeetingRepository = dogMeetingRepository;
        }

        public List<Dog> GetDogsByMeetingId(int meetingId)
        {
            List<DogMeeting> dogMeeting = dogMeetingRepository.GetByMeetingId(meetingId);
            List<Dog> dogs = new List<Dog>();
            for (int i = 0; i < dogMeeting.Count; i++)
            {
                dogs.Add(dogRepository.GetById(dogMeeting[i].DogId));
            }

            return dogs;
        }

        public List<Dog> getDogsByUser(int userId)
        {
            try 
            {
                List<Dog> dogs = dogRepository.GetByUser(userId);
                return dogs;
            }     
            catch (Exception e){
                throw e; 
            }
        }
    }
}
