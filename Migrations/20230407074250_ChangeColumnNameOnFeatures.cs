using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameOnFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneScreenFeatures",
                table: "Phones",
                newName: "ScreenFeatures");

            migrationBuilder.RenameColumn(
                name: "PhonePerformanceFeatures",
                table: "Phones",
                newName: "PerformanceFeatures");

            migrationBuilder.RenameColumn(
                name: "PhoneFeatures",
                table: "Phones",
                newName: "Features");

            migrationBuilder.RenameColumn(
                name: "PhoneDesignFeatures",
                table: "Phones",
                newName: "DesignFeatures");

            migrationBuilder.RenameColumn(
                name: "ChipsetRAMVersion",
                table: "Chipsets",
                newName: "RAMVersion");

            migrationBuilder.RenameColumn(
                name: "ChipsetPerformanceFeatures",
                table: "Chipsets",
                newName: "PerformanceFeatures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScreenFeatures",
                table: "Phones",
                newName: "PhoneScreenFeatures");

            migrationBuilder.RenameColumn(
                name: "PerformanceFeatures",
                table: "Phones",
                newName: "PhonePerformanceFeatures");

            migrationBuilder.RenameColumn(
                name: "Features",
                table: "Phones",
                newName: "PhoneFeatures");

            migrationBuilder.RenameColumn(
                name: "DesignFeatures",
                table: "Phones",
                newName: "PhoneDesignFeatures");

            migrationBuilder.RenameColumn(
                name: "RAMVersion",
                table: "Chipsets",
                newName: "ChipsetRAMVersion");

            migrationBuilder.RenameColumn(
                name: "PerformanceFeatures",
                table: "Chipsets",
                newName: "ChipsetPerformanceFeatures");
        }
    }
}
