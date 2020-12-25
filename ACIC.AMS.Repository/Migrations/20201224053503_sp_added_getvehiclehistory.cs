using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_added_getvehiclehistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER   PROCEDURE [dbo].[GetVehicleHistory] (
	                                @VehicleId int
                                )
                                AS
                                BEGIN
                                select 
	                                e.VehicleId,
	                                e.DateCreated,
	                                CONCAT('Added to ', c.CoverageTypes,' [',p.PolicyNumber,']') as [Transaction],
	                                a.LegalName
                                from Endorsement e
                                inner join Policy p on e.PolicyId = p.PolicyId
                                inner join DdCoverageType c on c.Id = p.CoverageTypes
                                inner join Account a on a.AccountId = p.AccountId
                                where e.VehicleId = @VehicleId
                                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
