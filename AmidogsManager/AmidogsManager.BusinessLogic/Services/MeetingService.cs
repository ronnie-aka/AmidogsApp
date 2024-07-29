using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.BusinessLogic.Services
{
    public class MeetingService
    {
        private readonly MeetingRepository meetingRepository;
        private readonly DogMeetingRepository dogMeetingRepository;

        public MeetingService(DogMeetingRepository dogMeetingRepository, MeetingRepository meetingRepository)
        {
            this.dogMeetingRepository = dogMeetingRepository;
            this.meetingRepository = meetingRepository;
        }

        public List<Meeting> GetMeetingByDogId(int dogId)
        {
            try
            {
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogMeetingsByDogId(dogId);
                List<Meeting> meetings = new List<Meeting>();
                for(int i = 0; i < dogMeetings.Count; i++)
                {
                    meetings.Add(meetingRepository.GetMeetingById(dogMeetings[i].MeetingId));
                }
                return meetings;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string DeleteMeetingById(int meetingId) {
            try
            {
                dogMeetingRepository.DeleteDogMeetingById(meetingId);
                meetingRepository.DeleteMeetingById(meetingId);
                return "ELIMINADO";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
