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

        public List<Meeting> GetMeetingsByDogId(int dogId)
        {
            try
            {
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogMeetingsByDogId(dogId);
                List<Meeting> meetings = new List<Meeting>();

                for (int i = 0; i < dogMeetings.Count; i++)
                {
                    Meeting? meeting = meetingRepository.GetMeetingById(dogMeetings[i].MeetingId);
                    if (meeting != null)
                    {
                        meetings.Add(meeting);
                    }
                }
                return meetings;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Meeting> GetMeetingsWithOutDog(int dogId)
        {
            try
            {
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogMeetingsWithOut(dogId);
                List<Meeting> meetings = new List<Meeting>();


                for (int i = 0; i < dogMeetings.Count; i++)
                {
                   Meeting? meeting = meetingRepository.GetMeetingById(dogMeetings[i].MeetingId);
                   if (meeting != null)
                    {
                        meetings.Add(meeting);
                    }
                }
                return meetings;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Meeting> GetMeetingsByOwnerDog(int dogId)
        {
            try
            {
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogMeetingByOwnerDog(dogId);
                List<Meeting> meetings = new List<Meeting>();
                for (int i = 0; i < dogMeetings.Count; i++)
                {
                    Meeting? meeting = meetingRepository.GetMeetingById(dogMeetings[i].MeetingId);
                    if (meeting != null)
                    {
                        meetings.Add(meeting);
                    }
                }
                return meetings;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteMeetingById(int meetingId) {
            try
            {
                dogMeetingRepository.DeleteDogMeetingById(meetingId);
                meetingRepository.DeleteMeetingById(meetingId);
                return "ELIMINADO";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateMeeting(int meetingId, Meeting updatedMeeting)
        {
            try
            {
                Meeting? existingMeeting = meetingRepository.GetMeetingById(meetingId);
                if (existingMeeting == null)
                {
                    return "Meeting not found";
                }

                // Actualizamos los campos del meeting existente con los datos del updatedMeeting.
                existingMeeting.MeetingTitle = updatedMeeting.MeetingTitle;
                existingMeeting.MaxParticpants = updatedMeeting.MaxParticpants;
                existingMeeting.Description = updatedMeeting.Description;
                existingMeeting.Date = updatedMeeting.Date;
                existingMeeting.Location = updatedMeeting.Location;
                // Añadir más campos según sea necesario

                meetingRepository.UpdateMeeting(existingMeeting);
                return "UPDATED";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
