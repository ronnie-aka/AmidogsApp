﻿// <auto-generated />
using System;
using AmidogsManager.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AmidogsManager.Database.Migrations
{
    [DbContext(typeof(AmidogsManagerContext))]
    [Migration("20240729094407_Atributo Owner añadido en DogMeeting")]
    partial class AtributoOwnerañadidoenDogMeeting
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AmidogsManager.Database.Models.Dog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AgeCategory")
                        .HasColumnType("int");

                    b.Property<int>("Breed")
                        .HasColumnType("int");

                    b.Property<string>("DogName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Dominant")
                        .HasColumnType("bit");

                    b.Property<int>("Personaliity")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Presentation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<bool>("Sterilized")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Dog", (string)null);
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.DogMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<bool>("Owner")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DogId");

                    b.HasIndex("MeetingId");

                    b.ToTable("DogMeeting", (string)null);
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Chat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DogId1")
                        .HasColumnType("int");

                    b.Property<int>("DogId2")
                        .HasColumnType("int");

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DogId1");

                    b.HasIndex("DogId2");

                    b.ToTable("Match", (string)null);
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxParticpants")
                        .HasColumnType("int");

                    b.Property<string>("MeetingTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Meeting", (string)null);
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Complaint")
                        .HasColumnType("int");

                    b.Property<int>("DogId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.Dog", b =>
                {
                    b.HasOne("AmidogsManager.Database.Models.User", "User")
                        .WithOne("Dog")
                        .HasForeignKey("AmidogsManager.Database.Models.Dog", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.DogMeeting", b =>
                {
                    b.HasOne("AmidogsManager.Database.Models.Dog", "Dog")
                        .WithMany("DogMeeting")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AmidogsManager.Database.Models.Meeting", "Meeting")
                        .WithMany("DogMeetings")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.Match", b =>
                {
                    b.HasOne("AmidogsManager.Database.Models.Dog", "Dog1")
                        .WithMany("Matches")
                        .HasForeignKey("DogId1")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmidogsManager.Database.Models.Dog", "Dog2")
                        .WithMany()
                        .HasForeignKey("DogId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog1");

                    b.Navigation("Dog2");
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.Dog", b =>
                {
                    b.Navigation("DogMeeting");

                    b.Navigation("Matches");
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.Meeting", b =>
                {
                    b.Navigation("DogMeetings");
                });

            modelBuilder.Entity("AmidogsManager.Database.Models.User", b =>
                {
                    b.Navigation("Dog");
                });
#pragma warning restore 612, 618
        }
    }
}
