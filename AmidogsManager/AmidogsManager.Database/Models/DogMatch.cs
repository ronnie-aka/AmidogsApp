using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Database.Models
{
    public class DogMatch
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public int MatchId { get; set;  }
        public Dog Dog { get; set; }
        public Match Match { get; set; }   
    }
}
