using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatteryPower",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Charging",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CpuHz",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FrontCameraMP",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InStorage",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MainCameraMP",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nits",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ppi",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ram",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Resolution",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScreenHz",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ScreenSize",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryPower",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Charging",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CpuHz",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "FrontCameraMP",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "InStorage",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "MainCameraMP",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Nits",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Ppi",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Resolution",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "ScreenHz",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "ScreenSize",
                table: "Phones");
        }
    }
}
