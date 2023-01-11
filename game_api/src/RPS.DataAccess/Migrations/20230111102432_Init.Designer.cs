﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPS.DataAccess.Persistence;

#nullable disable

namespace RPS.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230111102432_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RPS.Core.Entities.SystemTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Importance")
                        .HasColumnType("int");

                    b.Property<bool>("IsOff")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastStopped")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72c92151-d493-4974-9e84-c00c2096c0f1"),
                            Importance = 0,
                            IsOff = false,
                            Title = "C1"
                        },
                        new
                        {
                            Id = new Guid("d7104107-ca65-4064-92c4-bc0e0f664efd"),
                            Importance = 0,
                            IsOff = false,
                            Title = "C2"
                        },
                        new
                        {
                            Id = new Guid("d01f5e00-eab7-4f77-a27b-2f351a714379"),
                            Importance = 0,
                            IsOff = false,
                            Title = "C3"
                        },
                        new
                        {
                            Id = new Guid("d395ea08-d11e-4245-be48-4446e40f5ae2"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E1"
                        },
                        new
                        {
                            Id = new Guid("145a0447-1281-441c-bdfb-09b636d129d7"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E2"
                        },
                        new
                        {
                            Id = new Guid("fd3ca987-fc42-459e-a2b7-98015c243e20"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E3"
                        },
                        new
                        {
                            Id = new Guid("5c3bdd84-8f8f-4780-8286-33ccaee032e3"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E4"
                        },
                        new
                        {
                            Id = new Guid("23763ad7-17f6-4c11-9665-cc47408ea9ee"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E5"
                        },
                        new
                        {
                            Id = new Guid("c6c18398-0424-4604-a41c-6ae293097eb0"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E6"
                        },
                        new
                        {
                            Id = new Guid("ade07bc4-2630-4ac8-b275-f5fec9db91de"),
                            Importance = 1,
                            IsOff = false,
                            Title = "E7"
                        },
                        new
                        {
                            Id = new Guid("856bd3e9-d0cf-476e-b365-31c366d22bad"),
                            Importance = 2,
                            IsOff = false,
                            Title = "I1"
                        },
                        new
                        {
                            Id = new Guid("36aeb599-113f-4448-9321-6371284b9c96"),
                            Importance = 2,
                            IsOff = false,
                            Title = "I2"
                        },
                        new
                        {
                            Id = new Guid("366687e6-85cf-4e96-9865-18ae0b504240"),
                            Importance = 2,
                            IsOff = false,
                            Title = "I3"
                        },
                        new
                        {
                            Id = new Guid("9d329a76-68d3-4fc9-9071-5071e648c14b"),
                            Importance = 2,
                            IsOff = false,
                            Title = "I4"
                        },
                        new
                        {
                            Id = new Guid("50a01356-5e97-4224-a246-3d2d7633dd9f"),
                            Importance = 2,
                            IsOff = false,
                            Title = "I5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}