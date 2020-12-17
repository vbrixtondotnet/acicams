using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_getcontacts_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE [dbo].[GetContacts](@AccountId int)      
                                    AS      
                                    BEGIN       
		                               SELECT [ContactId]
										  ,[FirstName]
										  ,[MiddleName]
										  ,[LastName]
										  ,c.[Title] as TitleId
										  ,ct.TitleName as Title
										  ,[MblBusiness]
										  ,[MblDirect]
										  ,[MblMobile]
										  ,[Email1]
										  ,[Email2]
										  ,c.[Type]
										  ,[RefId]
										  ,c.[Notes]
										  ,c.[DateCreated]
										  ,c.[DateModified]
										FROM Contact c
										inner join Account on account.AccountId = c.RefId   
										left join DdContactsTitle ct on c.Title = ct.Id   
										WHERE account.AccountId = @AccountId           
                                    END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
