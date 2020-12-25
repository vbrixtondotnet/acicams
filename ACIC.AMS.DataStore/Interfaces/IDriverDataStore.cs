using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IDriverDataStore
    {
        List<Driver> GetDrivers(int accountId);

        DriverEndorsement Save(DriverEndorsement driverEndorsement);
        Driver Update(Driver driver);

        List<DriverHistory> GetDriverHistories(int driverId);

        SPRowCountResult DeleteDriver(int driverId);
        bool DriverExists(string firstName, string lastName, string cdlNumber, string state);
    }
}
