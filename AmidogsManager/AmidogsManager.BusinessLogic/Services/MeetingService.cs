using AmidogsManager.Database.Models;
using AmidogsManager.Repository.Interfaces;
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
        private readonly IMeetingRepository meetingRepository;
        private readonly IDogMeetingRepository dogMeetingRepository;

        public MeetingService(IDogMeetingRepository dogMeetingRepository, IMeetingRepository meetingRepository)
        {
            this.dogMeetingRepository = dogMeetingRepository;
            this.meetingRepository = meetingRepository;
        }
        public Meeting? GetMeetingById(int meetingId) {
            return meetingRepository.GetMeetingById(meetingId);
        }   

        public List<Meeting> GetMeetingsWithDog(int dogId)
        {
            try
            {
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogMeetingsWithdog(dogId);
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
                // Obtén las reuniones sin el perro especificado
                List<DogMeeting> dogMeetings = dogMeetingRepository.GetDogMeetingsWithOut(dogId);

                // Usa un HashSet para almacenar MeetingId únicos
                HashSet<int> meetingIds = new HashSet<int>(dogMeetings.Select(dm => dm.MeetingId));

                // Crea una lista para las reuniones únicas
                List<Meeting> meetings = new List<Meeting>();

                // Obtén reuniones basadas en los MeetingId únicos
                foreach (int meetingId in meetingIds)
                {
                    Meeting? meeting = meetingRepository.GetMeetingById(meetingId);
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
        public string CreateMeeting(Meeting newMeeting)
        {
            try
            {
                // Verificar si ya existe una reunión con el mismo título y fecha
                var existingMeeting = meetingRepository.GetMeetingById(newMeeting.Id);
                if (existingMeeting != null)
                {
                    return "Meeting already exists with the same ID";
                }

                // Aquí podrías realizar más validaciones si es necesario

                // Agregar la nueva reunión
                meetingRepository.AddMeeting(newMeeting);

                return "CREATED";
            }
            catch (Exception ex)
            {
                // Manejar la excepción (podrías registrar el error o tomar otra acción)
                return $"Error: {ex.Message}";
            }
        }
    }
}
