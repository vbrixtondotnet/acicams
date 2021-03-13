using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_endorsement_id_to_estimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Effective",
                table: "EndtEstimate",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndtId",
                table: "EndtEstimate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndtId",
                table: "EndtEstimate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Effective",
                table: "EndtEstimate",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
