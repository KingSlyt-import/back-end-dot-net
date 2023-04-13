﻿// <auto-generated />
using System;
using Back_End_Dot_Net.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Back_End_Dot_Net.Models.Chipset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessTime")
                        .HasColumnType("int");

                    b.Property<int>("CPUSocket")
                        .HasColumnType("int");

                    b.Property<int>("CPUTemp")
                        .HasColumnType("int");

                    b.Property<double>("CpuSpeedBase")
                        .HasColumnType("float");

                    b.Property<double>("CpuSpeedBoost")
                        .HasColumnType("float");

                    b.Property<double>("CpuThread")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Hide")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemoryChannels")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pci")
                        .HasColumnType("int");

                    b.Property<string>("PerformanceFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RAMSpeed")
                        .HasColumnType("int");

                    b.Property<int>("RAMVersion")
                        .HasColumnType("int");

                    b.Property<int>("TDP")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("semiconductorSize")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Chipsets");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Features", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessTime")
                        .HasColumnType("int");

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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessTime")
                        .HasColumnType("int");

                    b.Property<int>("BatteryPower")
                        .HasColumnType("int");

                    b.Property<Guid?>("CpuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesignFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("Hide")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InStorage")
                        .HasColumnType("int");

                    b.Property<bool>("MagSafe")
                        .HasColumnType("bit");

                    b.Property<string>("Manufacture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nits")
                        .HasColumnType("int");

                    b.Property<string>("PerformanceFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ppi")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Ram")
                        .HasColumnType("int");

                    b.Property<int>("RamSpeed")
                        .HasColumnType("int");

                    b.Property<string>("Resolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScreenFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScreenHz")
                        .HasColumnType("int");

                    b.Property<double>("ScreenSize")
                        .HasColumnType("float");

                    b.Property<double>("Thickness")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CpuId");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessTime")
                        .HasColumnType("int");

                    b.Property<int>("BatteryPower")
                        .HasColumnType("int");

                    b.Property<string>("CPUName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CPUSpeedBase")
                        .HasColumnType("float");

                    b.Property<double>("CPUSpeedBoost")
                        .HasColumnType("float");

                    b.Property<string>("CPUType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Charging")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesignFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FrontCameraMP")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("Hide")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InStorage")
                        .HasColumnType("int");

                    b.Property<bool>("MagSafe")
                        .HasColumnType("bit");

                    b.Property<string>("MainCameraMP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nits")
                        .HasColumnType("int");

                    b.Property<string>("PerformanceFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ppi")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("RAM")
                        .HasColumnType("int");

                    b.Property<int>("RAMSpeed")
                        .HasColumnType("int");

                    b.Property<string>("Resolution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScreenFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScreenHz")
                        .HasColumnType("int");

                    b.Property<double>("ScreenSize")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Laptop", b =>
                {
                    b.HasOne("Back_End_Dot_Net.Models.Chipset", "CPU")
                        .WithMany("Laptops")
                        .HasForeignKey("CpuId");

                    b.Navigation("CPU");
                });

            modelBuilder.Entity("Back_End_Dot_Net.Models.Chipset", b =>
                {
                    b.Navigation("Laptops");
                });
#pragma warning restore 612, 618
        }
    }
}
