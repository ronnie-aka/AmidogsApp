using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class DogMeetingService
    {
        private readonly DogMeetingRepository dogMeetingRepository;

        public DogMeetingService(DogMeetingRepository dogMeetingRepository)
        {
            this.dogMeetingRepository = dogMeetingRepository;
        }
        public void AddDogToMeeting(int dogId, int meetingId, bool isOwner)
        {
            dogMeetingRepository.AddDogToMeeting(dogId, meetingId, isOwner);
        }

        public void RemoveDogFromMeeting(int dogId, int meetingId)
        {
            dogMeetingRepository.RemoveDogFromMeeting(dogId, meetingId);
        }

    }
}
