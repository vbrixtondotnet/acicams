using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_added_delete_vehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE   PROCEDURE [dbo].DeleteVehicle (  
                                    @VehicleId int  
                                )  
                                AS  
                                BEGIN  
                                    DELETE FROM Endorsement where VehicleId = @VehicleId;  
                                    DELETE FROM Vehicle Where Id = @VehicleId;  
                                    SELECT @@ROWCOUNT as RowsAffected  
                                END  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
