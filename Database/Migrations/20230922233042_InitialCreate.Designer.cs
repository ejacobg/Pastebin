﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(PasteContext))]
    [Migration("20230922233042_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.Models.Paste", b =>
                {
                    b.Property<int>("PasteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Expires")
                        .HasColumnType("int");

                    b.Property<string>("Shortlink")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PasteId");

                    b.HasIndex("Shortlink")
                        .IsUnique()
                        .HasFilter("[Shortlink] IS NOT NULL");

                    b.ToTable("Pastes");
                });

            modelBuilder.Entity("Database.Models.ViewCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PasteId")
                        .HasColumnType("int");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PasteId");

                    b.ToTable("ViewCounts");
                });

            modelBuilder.Entity("Database.Models.ViewCount", b =>
                {
                    b.HasOne("Database.Models.Paste", "Paste")
                        .WithMany()
                        .HasForeignKey("PasteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paste");
                });
#pragma warning restore 612, 618
        }
    }
}
