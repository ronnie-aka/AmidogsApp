using AmidogsManager.Database;
using AmidogsManager.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Repository.Repositories
{
    public class MeetingRepository
    {
        private readonly AmidogsManagerContext amidogsManagerContext;

        public MeetingRepository(AmidogsManagerContext amidogsManagerContext)
        {
            this.amidogsManagerContext = amidogsManagerContext;
        }

        
        public Meeting? GetMeetingById(int meetingId)
        {
            return amidogsManagerContext.Meetings.Where(m => m.Id == meetingId).FirstOrDefault();
        }

        public void DeleteMeetingById(int meetingId)
        {
            Meeting? meeting = amidogsManagerContext.Meetings.Where(m => m.Id == meetingId).FirstOrDefault();

            if (meeting != null)
            {
                amidogsManagerContext.Meetings.Remove(meeting);
                amidogsManagerContext.SaveChanges();
            }
        }

        public void UpdateMeeting(Meeting meeting)
        {
            amidogsManagerContext.Meetings.Update(meeting);
            amidogsManagerContext.SaveChanges();
        }
        public void AddMeeting(Meeting meeting)
        {
            amidogsManagerContext.Meetings.Add(meeting);
            amidogsManagerContext.SaveChanges();
        }
    }
}
