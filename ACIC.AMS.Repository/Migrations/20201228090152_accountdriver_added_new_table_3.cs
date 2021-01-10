using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class accountdriver_added_new_table_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Vehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Policy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Policy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Mga",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Mga",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EndtEstimate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "EndtEstimate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Endorsement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Endorsement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Driver",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Driver",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdVehicleType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdVehicleType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdVehicleMake",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdVehicleMake",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdUsstate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdUsstate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdCoverageType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdCoverageType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdContactsTitle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdContactsTitle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdAccountType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdAccountType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdAccountStatus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdAccountStatus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdAccountSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdAccountSource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdAccountsOperationType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdAccountsOperationType",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "DdAccountOperationRadius",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "DdAccountOperationRadius",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Commission",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Commission",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Claim",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Claim",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Carrier",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Carrier",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Bank",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Bank",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Agent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Agent",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "AccountDriver",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Account",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Policy");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Mga");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Mga");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EndtEstimate");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "EndtEstimate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Endorsement");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Endorsement");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdVehicleType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdVehicleType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdVehicleMake");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdVehicleMake");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdUsstate");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdUsstate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdCoverageType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdCoverageType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdContactsTitle");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdContactsTitle");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdAccountType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdAccountType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdAccountStatus");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdAccountStatus");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdAccountSource");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdAccountSource");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdAccountsOperationType");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdAccountsOperationType");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DdAccountOperationRadius");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "DdAccountOperationRadius");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Commission");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Commission");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Claim");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Carrier");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Carrier");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedBy",
                table: "AccountDriver",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
