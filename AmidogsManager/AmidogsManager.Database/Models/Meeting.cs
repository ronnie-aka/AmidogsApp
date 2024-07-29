using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Database.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string MeetingName { get; set; }
        public int MaxParticpants { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<DogMeeting>? DogMeetings { get; set; }

        public Meeting()
        {
            DogMeetings = new List<DogMeeting>();
        }
    }
}
