using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_GetAccountDetails_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    CREATE OR ALTER    PROCEDURE [dbo].[GetAccountDetails](@AccountId int)    
                                            AS    
                                            BEGIN    
                                       SELECT     
                                          [AccountId]    
                                          ,[Status] as [StatusId]    
                                          ,astatus.AccStatus as [Status]    
                                          ,[Type] as TypeId    
                                          ,atype.[AccTypes] as [Type]    
                                          ,[Usdot]    
                                          ,[StatePermit]    
                                          ,[LegalName]    
                                          ,[Dba]    
                                          ,[TaxId]    
                                          ,[OperationType] as [OperationTypeId]    
                                          ,[TypeName] as [OperationType]    
                                          ,[OperationRadius] as [OperationRadiusId]    
                                          ,[RadiusName] as [OperationRadius]    
                                          ,[Address]    
                                          ,[City]    
                                          ,[State]    
                                          ,[Zip]    
                                          ,[GarageAddress]    
                                          ,[GarageCity]    
                                          ,[GarageState]    
                                          ,[GarageZip]    
                                          ,account.[Notes]    
                                          ,source.AccSource as [Source]  
                                          ,source.Id as [SourceId]   
                                          ,[YearClient]    
                                          ,account.[AgentId]    
                                          ,CONCAT(u.FirstName,' ', u.LastName) as Agent    
                                          ,account.[DateCreated]    
                                          ,account.[DateModified]    
                                       from Account account    
                                       left join DdAccountStatus astatus on astatus.Id = account.Status    
                                       left join DdAccountType atype on atype.Id = account.Type    
                                       left join DdAccountsOperationType aotype on aotype.Id = account.OperationType    
                                       left join DdAccountOperationRadius aoradius on aoradius.Id = account.OperationRadius    
                                       left join Agent agent on agent.AgentId = account.AgentId  
									   left join [User] u on u.Id = agent.UserId
                                       left join DdAccountSource source on source.Id = account.Source    
                                       WHERE account.AccountId = @AccountId    
                                            END  
                
            ");

            migrationBuilder.Sql(@"
                                    CREATE OR ALTER PROCEDURE [dbo].[GetExpiringAccounts]  
                                    AS  
                                    BEGIN  
                                        select * from Account where AccountId in  
                                        (select DISTINCT AccountId from Policy where Expiration <= DATEADD(D, 60, GETDATE()))  
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
