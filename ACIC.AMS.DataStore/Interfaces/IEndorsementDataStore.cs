using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IEndorsementDataStore
    {
        Endorsement SaveDriverEndorsement(int accountId, int driverId, List<DriverCoverage> driverCoverages);
    }
}
