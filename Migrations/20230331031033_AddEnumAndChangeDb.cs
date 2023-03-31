using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_End_Dot_Net.Migrations
{
    /// <inheritdoc />
    public partial class AddEnumAndChangeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpu",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CpuHz",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Maunufacture",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Meta",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Cpu",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Meta",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CpuSpeed",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "Meta",
                table: "Chipsets");

            migrationBuilder.RenameColumn(
                name: "Ram",
                table: "Phones",
                newName: "RAM");

            migrationBuilder.RenameColumn(
                name: "NanometNumber",
                table: "Chipsets",
                newName: "semiconductorSize");

            migrationBuilder.RenameColumn(
                name: "MaxCpuSpeed",
                table: "Chipsets",
                newName: "CpuSpeedBoost");

            migrationBuilder.AlterColumn<double>(
                name: "ScreenSize",
                table: "Phones",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MainCameraMP",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CPUName",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CPUSpeedBase",
                table: "Phones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CPUSpeedBoost",
                table: "Phones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CPUType",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesignFeatures",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Features",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Phones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "MagSafe",
                table: "Phones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Manufacture",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceFeatures",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RAMSpeed",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScreenFeatures",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Phones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Phones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "ScreenSize",
                table: "Laptops",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacture",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BatteryPower",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "DesignFeatures",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Features",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Laptops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "MagSafe",
                table: "Laptops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Nits",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceFeatures",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScreenFeatures",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScreenHz",
                table: "Laptops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Thickness",
                table: "Laptops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Laptops",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "AccessTime",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacture",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CPUSocket",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPUTemp",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChipsetPerformanceFeatures",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChipsetRAMVersion",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CpuSpeedBase",
                table: "Chipsets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RAMSpeed",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TDP",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Chipsets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPUName",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CPUSpeedBase",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CPUSpeedBoost",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CPUType",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "DesignFeatures",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "MagSafe",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Manufacture",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "PerformanceFeatures",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "RAMSpeed",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "ScreenFeatures",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "BatteryPower",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CPUName",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "CPUType",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "DesignFeatures",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "MagSafe",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Nits",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "PerformanceFeatures",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "ScreenFeatures",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "ScreenHz",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "AccessTime",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CPUSocket",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "CPUTemp",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "ChipsetPerformanceFeatures",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "ChipsetRAMVersion",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "CpuSpeedBase",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "RAMSpeed",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "TDP",
                table: "Chipsets");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Chipsets");

            migrationBuilder.RenameColumn(
                name: "RAM",
                table: "Phones",
                newName: "Ram");

            migrationBuilder.RenameColumn(
                name: "semiconductorSize",
                table: "Chipsets",
                newName: "NanometNumber");

            migrationBuilder.RenameColumn(
                name: "CpuSpeedBoost",
                table: "Chipsets",
                newName: "MaxCpuSpeed");

            migrationBuilder.AlterColumn<string>(
                name: "ScreenSize",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainCameraMP",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpu",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CpuHz",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Maunufacture",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ScreenSize",
                table: "Laptops",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacture",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpu",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacture",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuSpeed",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "Chipsets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
