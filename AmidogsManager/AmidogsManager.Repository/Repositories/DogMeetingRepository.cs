using AmidogsManager.Database.Models;
using AmidogsManager.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Repository.Repositories
{
    public class DogMeetingRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;
        public DogMeetingRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }

        public List<DogMeeting> GetDogMeetingsByDogId(int dogId)
        {
            return amidogsManagerContext.DogsMeetings.Where(d => d.DogId == dogId).ToList<DogMeeting>();
        }

        public List<DogMeeting> GetDogMeetingsWithOut(int dogId)
        {
            return amidogsManagerContext.DogsMeetings.Where(d => d.DogId != dogId).ToList<DogMeeting>();
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
    }
}
