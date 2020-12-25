using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_get_agents_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE [dbo].GetAgents
                                    AS 
                                    BEGIN
	                                   SELECT 
										u.FirstName,
										u.LastName,
										u.MiddleName,
										u.EmailAddress,
										u.DateCreated,
										u.DateModified,
										u.Active,
										u.Password,
										a.UserId,
										a.AgentId as Id,
										a.BrokerFeeSplit,
										a.CommFixedAmount,
										a.CommPaymentPlan,
										a.CommSplitNew,
										a.CommSplitRenew
	
										FROM [user] u 
										inner join Agent a on a.UserId = u.Id
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
