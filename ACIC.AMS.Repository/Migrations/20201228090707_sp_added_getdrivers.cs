using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_added_getdrivers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE [dbo].GetDrivers
                                (
	                                @AccountId int
                                )
                                AS BEGIN
                                SELECT d.[DriverId]
                                        ,d.[Excluded]
                                        ,d.[LastName]
                                        ,d.[Middle]
                                        ,d.[FirstName]
                                        ,d.[Dob]
                                        ,d.[Phone]
                                        ,d.[Email]
                                        ,d.[State]
                                        ,d.[Cdlnumber]
                                        ,d.[CdlyearLic]
                                        ,d.[DateHired]
                                        ,d.[Terminated]
                                        ,d.[OwnerOperator]
                                        ,d.[AccountId]
                                        ,d.[Notes]
                                        ,d.[DateCreated]
                                        ,d.[DateModified]
                                        ,d.[CreatedBy]
                                        ,d.[ModifiedBy]
	                                    ,ad.Active
                                    FROM [dbo].[Driver] d
                                    inner join AccountDriver ad on ad.DriverId = d.DriverId
                                    WHERE ad.AccountId = @AccountId
                                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
