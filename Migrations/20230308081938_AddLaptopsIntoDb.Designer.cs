﻿// <auto-generated />
using System;
using Back_End_Dot_Net.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230308081938_AddLaptopsIntoDb")]
    partial class AddLaptopsIntoDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Back_End_Dot_Net.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Hide")
                        .HasColumnType("bit");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessTime")
                        .HasColumnType("int");

                    b.Property<string>("Cpu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CpuSpeed")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Hide")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InStorage")
                        .HasColumnType("int");

                    b.Property<string>("Maunufacture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ppi")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Ram")
                        .HasColumnType("int");

                    b.Property<int>("RamSpeed")
                        .HasColumnType("int");

                    b.Property<int>("Resolution")
                        .HasColumnType("int");

                    b.Property<int>("ScreenSize")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessTime")
                        .HasColumnType("int");

                    b.Property<int>("BatteryPower")
                        .HasColumnType("int");

                    b.Property<int>("Charging")
                        .HasColumnType("int");

                    b.Property<string>("Cpu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CpuHz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FrontCameraMP")
                        .HasColumnType("int");

                    b.Property<bool>("Hide")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InStorage")
                        .HasColumnType("int");

                    b.Property<string>("MainCameraMP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maunufacture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nits")
                        .HasColumnType("int");

                    b.Property<int>("Ppi")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Ram")
                        .HasColumnType("int");

                    b.Property<int>("Resolution")
                        .HasColumnType("int");

                    b.Property<int>("ScreenHz")
                        .HasColumnType("int");

                    b.Property<string>("ScreenSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}