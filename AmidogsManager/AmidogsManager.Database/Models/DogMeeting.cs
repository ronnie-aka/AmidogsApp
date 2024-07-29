using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Database.Models
{
    public class DogMeeting
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        [ForeignKey("DogId")]
        public virtual Dog Dog { get; set; }
        public int MeetingId { get; set; }
        [ForeignKey("MeetingId")]
        public virtual Meeting Meeting { get; set; }    
    }
}
