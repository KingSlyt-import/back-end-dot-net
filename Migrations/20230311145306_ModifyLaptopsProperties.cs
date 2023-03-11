using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class ModifyLaptopsProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpuSpeedBase = table.Column<double>(type: "float", nullable: false),
                    CpuSpeedBoost = table.Column<double>(type: "float", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    RamSpeed = table.Column<int>(type: "int", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ppi = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    InStorage = table.Column<int>(type: "int", nullable: false),
                    ScreenSize = table.Column<int>(type: "int", nullable: false),
                    Meta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    AccessTime = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Laptops");
        }
    }
}
