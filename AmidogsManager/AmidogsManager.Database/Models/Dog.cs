﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmidogsManager.Database.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DogName { get; set; }
        public bool Genre { get; set; }
        public bool Castrated { get; set; }
        public bool Dominant { get; set; }
        public Breed Breed { get; set;}
        public AgeCategory AgeCategory { get; set; }
        public Personaliity Personaliity { get; set; }
        public Size Size { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<DogMatch> DogMatch { get; set; }
        public virtual ICollection<DogMeeting> DogMeeting { get; set; }

    }

    public enum Breed
    {
       Labrador, 
       Pastor_Alemán,
       Golden_Retrierver,
       Bulldog_Francés,
       Beagle,
       Boxer,
       Teckel,
       Caniche,
       Chihuahua,
       Husky,
       Doberman,
       Rottwiler,
       ShihTzu,
       Yorkshire,
       Pomerania,
       Shiba_Inu,
       Cocker_Spaniel,
       Pug,
       Mastín
    }
    public enum AgeCategory { 
        Joven,
        Adulto,
        Senior
    }

    public enum Personaliity
    {
        Tímido,
        Sociable,
        Enérgico, 
        Tranquilo,
        Miedoso,
        Curioso
    }
    public enum Size
    {
        Pequeño,
        Mediano,
        Grande,
        Gigante
    }
}
