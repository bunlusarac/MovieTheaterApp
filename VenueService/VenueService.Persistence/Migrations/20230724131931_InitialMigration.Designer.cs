﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VenueService.Persistence.Contexts;

#nullable disable

namespace VenueService.Persistence.Migrations
{
    [DbContext(typeof(VenueDbContext))]
    [Migration("20230724131931_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VenueService.Domain.Entities.LayoutSeat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<char>("Row")
                        .HasColumnType("character(1)");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("integer");

                    b.Property<int>("SeatType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SeatingLayoutId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeatingLayoutId");

                    b.ToTable("LayoutSeat");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Pricing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Pricing", (string)null);
                });

            modelBuilder.Entity("VenueService.Domain.Entities.SeatingLayout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<char>("LastRow")
                        .HasColumnType("character(1)");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SeatingLayout");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.SeatingState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SeatingState");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Localization")
                        .HasColumnType("integer");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SeatingStateId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TheaterId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SeatingStateId");

                    b.HasIndex("TheaterId");

                    b.ToTable("Session", (string)null);
                });

            modelBuilder.Entity("VenueService.Domain.Entities.StateSeat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Occupied")
                        .HasColumnType("boolean");

                    b.Property<char>("Row")
                        .HasColumnType("character(1)");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SeatingStateId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SeatingStateId");

                    b.ToTable("StateSeat");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Theater", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("LayoutId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid?>("VenueId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LayoutId");

                    b.HasIndex("VenueId");

                    b.ToTable("Theaters", (string)null);
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Venue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Location")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Venues", (string)null);
                });

            modelBuilder.Entity("VenueService.Domain.Entities.LayoutSeat", b =>
                {
                    b.HasOne("VenueService.Domain.Entities.SeatingLayout", null)
                        .WithMany("LayoutSeats")
                        .HasForeignKey("SeatingLayoutId");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Pricing", b =>
                {
                    b.HasOne("VenueService.Domain.Entities.Session", null)
                        .WithMany("Pricings")
                        .HasForeignKey("SessionId");

                    b.OwnsOne("VenueService.Domain.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("PricingId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer");

                            b1.HasKey("PricingId");

                            b1.ToTable("Pricing");

                            b1.WithOwner()
                                .HasForeignKey("PricingId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Session", b =>
                {
                    b.HasOne("VenueService.Domain.Entities.SeatingState", "SeatingState")
                        .WithMany()
                        .HasForeignKey("SeatingStateId");

                    b.HasOne("VenueService.Domain.Entities.Theater", null)
                        .WithMany("Sessions")
                        .HasForeignKey("TheaterId");

                    b.OwnsOne("VenueService.Domain.ValueObjects.TimeRange", "TimeRange", b1 =>
                        {
                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uuid");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("EndTime");

                            b1.HasKey("SessionId");

                            b1.ToTable("Session");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.Navigation("SeatingState");

                    b.Navigation("TimeRange");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.StateSeat", b =>
                {
                    b.HasOne("VenueService.Domain.Entities.SeatingState", null)
                        .WithMany("StateSeats")
                        .HasForeignKey("SeatingStateId");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Theater", b =>
                {
                    b.HasOne("VenueService.Domain.Entities.SeatingLayout", "Layout")
                        .WithMany()
                        .HasForeignKey("LayoutId");

                    b.HasOne("VenueService.Domain.Entities.Venue", null)
                        .WithMany("Theaters")
                        .HasForeignKey("VenueId");

                    b.Navigation("Layout");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.SeatingLayout", b =>
                {
                    b.Navigation("LayoutSeats");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.SeatingState", b =>
                {
                    b.Navigation("StateSeats");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Session", b =>
                {
                    b.Navigation("Pricings");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Theater", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("VenueService.Domain.Entities.Venue", b =>
                {
                    b.Navigation("Theaters");
                });
#pragma warning restore 612, 618
        }
    }
}
