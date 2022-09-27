﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220924212455_AddedPropertiesForRelevantRanking")]
    partial class AddedPropertiesForRelevantRanking
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Database.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("RelevantForRanking")
                        .HasColumnType("boolean");

                    b.Property<int>("TeamsCount")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Database.Entities.EventTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("Ranking")
                        .HasColumnType("integer");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("TeamId");

                    b.ToTable("EventTeam");
                });

            modelBuilder.Entity("Database.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("FirstPlacePercentage")
                        .HasColumnType("double precision");

                    b.Property<int>("FirstPlacesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PlayedEvents")
                        .HasColumnType("integer");

                    b.Property<int>("PlayedRelevantEvents")
                        .HasColumnType("integer");

                    b.Property<double>("RelevantFirstPlacePercentage")
                        .HasColumnType("double precision");

                    b.Property<int>("RelevantFirstPlacesCount")
                        .HasColumnType("integer");

                    b.Property<double>("RelevantSecondPlacePercentage")
                        .HasColumnType("double precision");

                    b.Property<int>("RelevantSecondPlacesCount")
                        .HasColumnType("integer");

                    b.Property<double>("RelevantThirdPlacePercentage")
                        .HasColumnType("double precision");

                    b.Property<int>("RelevantThirdPlacesCount")
                        .HasColumnType("integer");

                    b.Property<double>("RelevantTop3Percentage")
                        .HasColumnType("double precision");

                    b.Property<double>("SecondPlacePercentage")
                        .HasColumnType("double precision");

                    b.Property<int>("SecondPlacesCount")
                        .HasColumnType("integer");

                    b.Property<double>("ThirdPlacePercentage")
                        .HasColumnType("double precision");

                    b.Property<int>("ThirdPlacesCount")
                        .HasColumnType("integer");

                    b.Property<double>("Top3Percentage")
                        .HasColumnType("double precision");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("YearOfFirstEvent")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Database.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("PlayerTeam", b =>
                {
                    b.Property<int>("PlayersId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamsId")
                        .HasColumnType("integer");

                    b.HasKey("PlayersId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("PlayerTeam");
                });

            modelBuilder.Entity("Database.Entities.EventTeam", b =>
                {
                    b.HasOne("Database.Entities.Event", "Event")
                        .WithMany("Teams")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Team", "Team")
                        .WithMany("EventTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("PlayerTeam", b =>
                {
                    b.HasOne("Database.Entities.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Entities.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Entities.Event", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Database.Entities.Team", b =>
                {
                    b.Navigation("EventTeams");
                });
#pragma warning restore 612, 618
        }
    }
}