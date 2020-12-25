using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_get_available_coverage_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE  OR ALTER PROCEDURE [dbo].GetAvailableCoverageTypes (  
                                    @AccountId int  
                                )  
                                AS  
                                BEGIN  
                                    select   
                                    c.Id,   
                                    c.CoverageTypes as [Name],
	                                p.[PolicyId]
                                      ,p.[PolicyNumber]
	                                  ,a.[LegalName] as Company
                                      ,p.[TotalFactor]
                                      ,p.[BasePerUnit]
                                      ,p.[NonTaxedRateUnit]
                                      ,p.[Bfrate]
                                      ,p.[Strate]
                                      ,p.[Pdrate]
                                      ,p.[TrailerRate]
                                      ,p.[Premium]
                                      ,p.[Surcharge]
                                      ,p.[PolicyFees]
                                      ,p.[Mgafees]
                                      ,p.[SurplusTax]
                                      ,p.[BrokerFees]
                                      ,p.[OtherFees]
                                      ,p.[TotalPremium]
                                      ,p.[CommRate]
                                      ,p.[Commission]
                                      ,p.[AgentSplit]
                                      ,p.[AgentComm]
                                      ,p.[AgentBfshare]
                                      ,p.[InceptionStage]
	                                  ,p.[PDNonOwnedTrailerRate]
                                from DdCoverageType c inner join Policy p  
                                left join Account a on a.AccountId=p.AccountId
                                on c.Id = p.CoverageTypes  
                                where p.AccountId = @AccountId and p.Expiration > GETDATE()  
                                END  ");
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
