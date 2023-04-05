using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class modifyColumnNameAndStringArray : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPUName",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CPUType",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CpuSpeedBase",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CpuSpeedBoost",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Benchmark",
                table: "Chipsets");

            migrationBuilder.RenameColumn(
                name: "Memory",
                table: "Chipsets",
                newName: "MemoryChannels");

            migrationBuilder.AlterColumn<string>(
                name: "ScreenFeatures",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PerformanceFeatures",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Features",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DesignFeatures",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ScreenFeatures",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PerformanceFeatures",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Features",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DesignFeatures",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CpuId",
                table: "Laptops",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ChipsetPerformanceFeatures",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_CpuId",
                table: "Laptops",
                column: "CpuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Chipsets_CpuId",
                table: "Laptops",
                column: "CpuId",
                principalTable: "Chipsets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Chipsets_CpuId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_CpuId",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CpuId",
                table: "Laptops");

            migrationBuilder.RenameColumn(
                name: "MemoryChannels",
                table: "Chipsets",
                newName: "Memory");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenFeatures",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerformanceFeatures",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Features",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignFeatures",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScreenFeatures",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PerformanceFeatures",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Features",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignFeatures",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPUName",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPUType",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CpuSpeedBase",
                table: "Laptops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CpuSpeedBoost",
                table: "Laptops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "ChipsetPerformanceFeatures",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Benchmark",
                table: "Chipsets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
