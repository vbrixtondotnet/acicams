using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class added_get_vehicles_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER  PROCEDURE [dbo].[GetVehicles]  
                                    (  
                                     @AccountId int  
                                    )  
                                    AS   
                                    BEGIN  
                                     SELECT   
                                      v.Id,  
                                      v.AccountId,  
                                      v.AccountNo,  
                                      v.BankId,  
		                              b.BankName,
                                      v.DateCreated,  
                                      v.DateModified,  
                                      v.Make,  
                                      v.Model,  
                                      v.Notes,  
                                      v.OwnerOperator,  
                                      v.Pdvalue,  
                                      v.PolicyInclude, 
		                              v.DriverId,
                                      v.Type,  
                                      v.Vin,  
                                      ISNULL(v.VUnit,'') as VUnit,  
                                      v.VYear,  
                                      m.VehMakeName,  
                                      t.VehTypeName,  
                                      CONCAT(d.FirstName, ' ', d.LastName) as Driver  
                                     FROM Vehicle v  
                                     LEFT JOIN Driver d on v.DriverId = d.DriverId  
                                     LEFT JOIN DdVehicleMake m on m.Vmid = v.Make  
                                     LEFT JOIN DdVehicleType t on t.Vtid = v.Type  
                                     LEFT JOIN Bank b on v.BankId = b.BankId  
                                     where v.AccountId = @AccountId 
                                    END  ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
