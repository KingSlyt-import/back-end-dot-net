using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chipsets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CPUSocket = table.Column<int>(type: "int", nullable: false),
                    CPUTemp = table.Column<int>(type: "int", nullable: false),
                    TDP = table.Column<int>(type: "int", nullable: false),
                    CpuSpeedBase = table.Column<double>(type: "float", nullable: false),
                    CpuSpeedBoost = table.Column<double>(type: "float", nullable: false),
                    CpuThread = table.Column<double>(type: "float", nullable: false),
                    semiconductorSize = table.Column<double>(type: "float", nullable: false),
                    Benchmark = table.Column<double>(type: "float", nullable: false),
                    Pci = table.Column<int>(type: "int", nullable: false),
                    Memory = table.Column<int>(type: "int", nullable: false),
                    ChipsetPerformanceFeatures = table.Column<int>(type: "int", nullable: false),
                    ChipsetRAMVersion = table.Column<int>(type: "int", nullable: false),
                    RAMSpeed = table.Column<int>(type: "int", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    AccessTime = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chipsets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    AccessTime = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPUName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPUType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CpuSpeedBase = table.Column<double>(type: "float", nullable: false),
                    CpuSpeedBoost = table.Column<double>(type: "float", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    RamSpeed = table.Column<int>(type: "int", nullable: false),
                    InStorage = table.Column<int>(type: "int", nullable: false),
                    PerformanceFeatures = table.Column<int>(type: "int", nullable: false),
                    ScreenSize = table.Column<double>(type: "float", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenHz = table.Column<int>(type: "int", nullable: false),
                    Nits = table.Column<int>(type: "int", nullable: false),
                    Ppi = table.Column<int>(type: "int", nullable: false),
                    ScreenFeatures = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Thickness = table.Column<double>(type: "float", nullable: false),
                    DesignFeatures = table.Column<int>(type: "int", nullable: false),
                    BatteryPower = table.Column<int>(type: "int", nullable: false),
                    MagSafe = table.Column<bool>(type: "bit", nullable: false),
                    Features = table.Column<int>(type: "int", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    AccessTime = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPUName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPUType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPUSpeedBase = table.Column<double>(type: "float", nullable: false),
                    CPUSpeedBoost = table.Column<double>(type: "float", nullable: false),
                    RAM = table.Column<int>(type: "int", nullable: false),
                    RAMSpeed = table.Column<int>(type: "int", nullable: false),
                    InStorage = table.Column<int>(type: "int", nullable: false),
                    PerformanceFeatures = table.Column<int>(type: "int", nullable: false),
                    ScreenSize = table.Column<double>(type: "float", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenHz = table.Column<int>(type: "int", nullable: false),
                    Nits = table.Column<int>(type: "int", nullable: false),
                    Ppi = table.Column<int>(type: "int", nullable: false),
                    ScreenFeatures = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    DesignFeatures = table.Column<int>(type: "int", nullable: false),
                    MainCameraMP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontCameraMP = table.Column<int>(type: "int", nullable: false),
                    BatteryPower = table.Column<int>(type: "int", nullable: false),
                    Charging = table.Column<int>(type: "int", nullable: false),
                    MagSafe = table.Column<bool>(type: "bit", nullable: false),
                    Features = table.Column<int>(type: "int", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    AccessTime = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chipsets");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "Phones");
        }
    }
}
