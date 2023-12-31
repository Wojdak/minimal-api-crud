﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinimalAPI.Data;

#nullable disable

namespace MinimalAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MinimalAPI.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RacingNumber")
                        .HasColumnType("int");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Max Verstappen",
                            Nationality = "Dutch",
                            RacingNumber = 1,
                            Team = "Red Bull Racing"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sergio Perez",
                            Nationality = "Mexican",
                            RacingNumber = 11,
                            Team = "Red Bull Racing"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lewis Hamilton",
                            Nationality = "British",
                            RacingNumber = 44,
                            Team = "Mercedes"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Carlos Sainz",
                            Nationality = "Spanish",
                            RacingNumber = 55,
                            Team = "Ferrari"
                        });
                });

            modelBuilder.Entity("MinimalAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "$2a$11$pi515kdmgpUzXey08Jn9zu17X4FbsG8VGkesBxj3ciZv8eXKeRs3q",
                            Role = "Administrator",
                            Username = "admin_account"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "$2a$11$pi515kdmgpUzXey08Jn9zu17X4FbsG8VGkesBxj3ciZv8eXKeRs3q",
                            Role = "Standard",
                            Username = "standard_account"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
