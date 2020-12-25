using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class removed_email_address_to_agent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "Agent");

            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "Agent",
                newName: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Agent",
                newName: "Inactive");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Agent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Agent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoginId",
                table: "Agent",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
