using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_get_policies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER  PROCEDURE [dbo].[GetPolicies]  
                                    (@AccountId int)  
                                    AS  
                                    BEGIN  
                                    SELECT p.[PolicyId]  
                                          ,p.[PolicyNumber]  
                                          ,p.[AccountId]  
                                          ,p.[Mgaid]  
                                       ,m.Mganame  
                                          ,p.[CarrierId]  
                                       ,c.[CarrierName]  
                                          ,p.[CoverageTypes] as CoverageTypeId  
                                       ,ct.CoverageTypes as CoverageType   
                                          ,p.[BindDate]  
                                          ,p.[Effective]  
                                          ,p.[Expiration]  
                                          ,p.[DirectBill]  
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
                                          ,p.[PolicyType]  
                                          ,p.[BankId]  
                                          ,p.[AccountNo]  
                                          ,p.[Notes]  
                                          ,p.[Source]  
                                          ,p.[TrackId]  
                                          ,p.[InceptionStage]  
                                          ,p.[DateCreated]  
                                          ,p.[DateModified]  
                                          ,p.[PDNonOwnedTrailerRate]  
										  ,b.BankName
                                      FROM [dbo].[Policy] p inner join Carrier c on c.CarrierId = p.CarrierId  
                                      left join Mga m on m.Mgaid = p.Mgaid  
                                      left join DdCoverageType ct on ct.Id = p.CoverageTypes  
                                      left join Bank b on b.BankId = p.BankId  
                                      WHERE AccountId = @AccountId  
           order by ct.Id  
                                      END  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
