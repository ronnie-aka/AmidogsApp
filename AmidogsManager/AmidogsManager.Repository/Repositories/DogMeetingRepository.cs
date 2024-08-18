using AmidogsManager.Database.Models;
using AmidogsManager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AmidogsManager.Repository.Repositories
{
    public class DogMeetingRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;
        public DogMeetingRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }
        public List<DogMeeting> GetDogsInMeeting(int meetingid) {
            return amidogsManagerContext.DogsMeetings.Where(d => d.MeetingId == meetingid).ToList<DogMeeting>();
        }

        public List<DogMeeting> GetDogMeetingsWithdog(int dogId)
        {
            return amidogsManagerContext.DogsMeetings.Where(d => d.DogId == dogId && d.Owner == false).ToList<DogMeeting>();
        }

        public List<DogMeeting> GetDogMeetingsWithOut(int dogId)
        {
            var meetingIdsWithDog = amidogsManagerContext.DogsMeetings
                .Where(d => d.DogId == dogId)
                .Select(d => d.MeetingId)
                .ToList();
            var result = amidogsManagerContext.DogsMeetings
                .Where(d => !meetingIdsWithDog.Contains(d.MeetingId))
                .ToList();

            return result;
        }


        public List<DogMeeting> GetDogMeetingByOwnerDog(int dogId)
        {
            return amidogsManagerContext.DogsMeetings.Where(d => d.DogId == dogId && d.Owner == true).ToList<DogMeeting>();
        }


        public void DeleteDogMeetingById(int meetingId)
        {
            List<DogMeeting> dogMeeting = amidogsManagerContext.DogsMeetings.Where(m => m.MeetingId == meetingId).ToList<DogMeeting>();
            dogMeeting.ForEach(d =>
            {
                amidogsManagerContext.DogsMeetings.Remove(d);
            });
            amidogsManagerContext.SaveChanges();
        }

        public void AddDogToMeeting(int dogId, int meetingId, bool isOwner)
        {
            var dogMeeting = new DogMeeting
            {
                DogId = dogId,
                MeetingId = meetingId,
                Owner = isOwner
            };

            amidogsManagerContext.DogsMeetings.Add(dogMeeting);
            amidogsManagerContext.SaveChanges();
        }

        public void RemoveDogFromMeeting(int dogId, int meetingId)
        {
            var dogMeeting = amidogsManagerContext.DogsMeetings
                .FirstOrDefault(d => d.DogId == dogId && d.MeetingId == meetingId);

            if (dogMeeting != null)
            {
                amidogsManagerContext.DogsMeetings.Remove(dogMeeting);
                amidogsManagerContext.SaveChanges();
            }
        }

    }
}
