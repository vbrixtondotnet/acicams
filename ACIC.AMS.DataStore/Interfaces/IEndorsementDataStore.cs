using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IEndorsementDataStore
    {
        Endorsement SaveDriverEndorsement(int accountId, int driverId, List<DriverCoverage> driverCoverages);
        Endorsement SaveVehicleEndorsement(int accountId, int vehicleId, string type, List<VehicleCoverage> vehicleCoverages);
    }
}
