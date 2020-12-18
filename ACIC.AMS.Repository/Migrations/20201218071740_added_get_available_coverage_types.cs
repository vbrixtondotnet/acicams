using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_get_available_coverage_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE [dbo].GetAvailableCoverageTypes (
	                                @AccountId int
                                )
                                AS
                                BEGIN
                                select 
	                                c.Id, 
	                                c.CoverageTypes 

                                from DdCoverageType c inner join Policy p
                                on c.Id = p.CoverageTypes
                                where p.AccountId = @AccountId and p.Expiration > GETDATE()
                                END");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
