using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_getpolicyagentscommission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetPolicyAgentCommissions
            (@PolicyId int)
            AS
            BEGIN

             DECLARE 
             @DaysToExpire int,
             @TotalEndorsementCommission decimal(18,2),
             @PolicyCommission decimal(18,2),
             @TotalCommission decimal(18,2),
             @UnearnedCommission decimal(18,2),
             @EarnedCommission decimal(18,2),
             @CommissionRate nvarchar(10)

             SELECT @CommissionRate = CONCAT(CommRate, '%') from Policy where PolicyId = @PolicyId
             SELECT @DaysToExpire = DATEDIFF(DAY, GETDATE(), Expiration) from Policy where PolicyId = @PolicyId
             SELECT @PolicyCommission = SUM(Commission) from Policy where PolicyId = @PolicyId

             SELECT @TotalEndorsementCommission =  ISNULL((select sum(Commission) from Endorsement where PolicyId = @PolicyId and [Action] = 'Add'),0) - 
             ISNULL((select sum(Commission) from Endorsement where PolicyId = @PolicyId and [Action] = 'Delete'),0)


             SET @TotalCommission = @TotalEndorsementCommission + @PolicyCommission

             SET @UnearnedCommission = ((CAST(@DaysToExpire as float) / CAST(365 as float)) * @TotalCommission)
 
             SET @EarnedCommission = @TotalCommission - @UnearnedCommission


             SELECT @CommissionRate as 'CommissionRate', @TotalCommission as 'TotalCommission', @DaysToExpire as 'DaysToExpire', @UnearnedCommission as 'UnearnedCommission', @EarnedCommission as 'EarnedCommission'


            END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
