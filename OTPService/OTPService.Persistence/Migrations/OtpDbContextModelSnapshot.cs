﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OTPService.Persistence.Contexts;

#nullable disable

namespace OTPService.Persistence.Migrations
{
    [DbContext(typeof(OtpDbContext))]
    partial class OtpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OTPService.Domain.Entities.OtpUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BlockExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("BlockTimeout")
                        .HasColumnType("interval");

                    b.Property<int>("DisposedAttempts")
                        .HasColumnType("integer");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("integer");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDisposed")
                        .HasColumnType("boolean");

                    b.Property<Guid>("IssuedUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("MaxDisposals")
                        .HasColumnType("integer");

                    b.Property<int>("MaxRetries")
                        .HasColumnType("integer");

                    b.Property<bool>("MfaEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OtpExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("OtpTimeWindow")
                        .HasColumnType("interval");

                    b.Property<long>("PrimaryCounter")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("PrimarySecret")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long>("SecondaryCounter")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("SecondarySecret")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("IssuedUserId")
                        .IsUnique();

                    b.ToTable("OTPUsers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
