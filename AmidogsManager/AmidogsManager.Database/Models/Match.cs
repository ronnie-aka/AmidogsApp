using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Database.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }

        public virtual ICollection<DogMatch>? DogMatches { get; set; }
    }
}
