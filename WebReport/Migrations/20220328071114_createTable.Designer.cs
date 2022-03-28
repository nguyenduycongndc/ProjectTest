﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebReport.DataAccess;

namespace WebReport.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220328071114_createTable")]
    partial class createTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("WebReport.Models.Repost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Branch")
                        .HasColumnType("longtext")
                        .HasColumnName("branch");

                    b.Property<string>("Department")
                        .HasColumnType("longtext")
                        .HasColumnName("department");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("date")
                        .HasColumnType("int")
                        .HasColumnName("date");

                    b.HasKey("Id");

                    b.ToTable("REPOST");
                });
#pragma warning restore 612, 618
        }
    }
}
