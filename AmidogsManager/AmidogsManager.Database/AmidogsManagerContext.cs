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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().ToTable("Dog");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<DogMeeting>().ToTable("DogMeeting");

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Dog1)
                .WithMany(d => d.Matches)
                .HasForeignKey(m => m.DogId1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Dog2)
                .WithMany()
                .HasForeignKey(m => m.DogId2)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseSqlServer($"Server=amidogsmanagerresources-database-fqoxyidfrccl.chu80w8cobwx.eu-west-3.rds.amazonaws.com; Database=AmidogsManagerDB; User Id = ronnieaka; Password = ronald12");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
