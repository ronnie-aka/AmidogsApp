using AmidogsManager.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AmidogsManager.Database
{
    public class AmidogsManagerContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DogMeeting> DogsMeetings { get; set; }
        public DbSet<DogMatch> DogsMatchs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().ToTable("Dog");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<DogMeeting>().ToTable("DogMeeting");
            modelBuilder.Entity<DogMatch>().ToTable("DogsMatchs");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer($"Server=amidogsmanagerresources-database-smzmdld0c6ss.chiugioiu1st.eu-west-1.rds.amazonaws.com; Database=AmidogsManagerDB; User Id = ronnieaka; Password = ronald12");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
