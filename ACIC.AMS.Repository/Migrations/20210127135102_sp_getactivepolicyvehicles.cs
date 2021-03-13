using Microsoft.EntityFrameworkCore.Migrations;

namespace ACIC.AMS.Repository.Migrations
{
    public partial class sp_getactivepolicyvehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetActivePolicyVehicles
                                    (
	                                    @PolicyId int
                                    )
                                    AS 
                                    BEGIN
                                    SELECT
	                                        v.vyear as [Year], 
                                            vm.vehmakename as [Make], 
                                            v.vin as [VIN], 
                                            v.vunit as [Unit], 
                                            vt.vehtypename as [Type], 
                                            v.pdvalue as [PDValue], 
                                            CONCAT(d.FirstName, ' ', d.Middle, ' ',  d.LastName) as [Driver] 
                                    FROM   vehicle v 
                                            INNER JOIN (SELECT * 
                                                        FROM   (SELECT * 
                                                                FROM   (SELECT DISTINCT policyid, 
                                                                                        vehicleid, 
                                                                                        vin, 
                                                                                        effective AS 'AddEffective', 
                                                                                        'Add'     AS [ActionA] 
                                                                        FROM   endorsement 
                                                                        WHERE  policyid = @PolicyId 
                                                                                AND vehicleid IS NOT NULL 
                                                                                AND [action] = 'Add') a 
                                                                        LEFT JOIN (SELECT * 
                                                                                    FROM   (SELECT DISTINCT policyid  AS 'PolicyIdDelete' 
                                                                        , 
                                                        vehicleid AS 'VehicleIdDelete', 
                                                        vin       AS 'VinDelete', 
                                                        effective AS 'DeleteEffective', 
                                                        'Delete'  AS [ActionB] 
                                                        FROM   endorsement 
                                                        WHERE  policyid = @PolicyId 
                                                        AND vehicleid IS NOT NULL 
                                                        AND [action] = 'Delete') b) c 
                                                        ON a.vehicleid = c.vehicleiddelete) da 
                                                        WHERE  deleteeffective IS NULL 
                                                                OR da.addeffective > da.deleteeffective) endt 
                                                    ON endt.vehicleid = v.id 
                                            INNER JOIN ddvehicletype vt 
                                                    ON v.type = vt.vtid 
                                            INNER JOIN ddvehiclemake vm 
                                                    ON v.make = vm.vmid 
                                            LEFT JOIN Driver d 
                                                    ON d.DriverId = v.DriverId 
                                    ORDER  BY vt.VehTypeName
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
