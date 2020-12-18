using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_delete_driver_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE [dbo].DeleteDriver (
	                                @DriverId int
                                )
                                AS
                                BEGIN
	                                DELETE FROM Endorsement where DriverId = @DriverId;
	                                DELETE FROM Driver Where DriverId = @DriverId;
	                                SELECT @@ROWCOUNT as RowsAffected
                                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
