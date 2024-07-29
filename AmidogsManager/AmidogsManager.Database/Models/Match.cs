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
        public string Chat { get; set; }
        public int DogId1 { get; set; }
        [ForeignKey("DogId1")]
        public Dog Dog1 { get; set; }
        public int DogId2 { get; set; }
        [ForeignKey("DogId2")]
        public Dog Dog2 { get; set; }
    }
}
