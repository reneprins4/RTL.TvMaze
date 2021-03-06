﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RTL.TvMazeApp.Infrastructure.Contexts;

namespace RTL.TvMazeApp.Migrations
{
    [DbContext(typeof(TvMazeContext))]
    [Migration("20190401140022_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RTL.TvMazeApp.Domain.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Name");

                    b.Property<int>("PersonId");

                    b.Property<int>("ShowId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("RTL.TvMazeApp.Domain.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ShowId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("RTL.TvMazeApp.Domain.Models.Person", b =>
                {
                    b.HasOne("RTL.TvMazeApp.Domain.Models.Show", "Show")
                        .WithMany("Cast")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
