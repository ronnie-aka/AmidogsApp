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

        public List<DogMeeting> GetByMeetingId(int meetingId)
        {
            return amidogsManagerContext.DogsMeetings.Where(dm => dm.MeetingId == meetingId).ToList<DogMeeting>();
        }
    }
}
