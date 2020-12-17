using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IDriverDataStore
    {
        List<Driver> GetDrivers(int accountId);
    }
}
