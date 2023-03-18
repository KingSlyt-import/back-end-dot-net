using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class AddCpuDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chipsets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpuSpeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpuThread = table.Column<double>(type: "float", nullable: false),
                    MaxCpuSpeed = table.Column<double>(type: "float", nullable: false),
                    NanometNumber = table.Column<double>(type: "float", nullable: false),
                    Benchmark = table.Column<double>(type: "float", nullable: false),
                    Pci = table.Column<int>(type: "int", nullable: false),
                    Memory = table.Column<int>(type: "int", nullable: false),
                    Meta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false),
                    AccessTime = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chipsets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chipsets");
        }
    }
}
