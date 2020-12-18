using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IPolicyDataStore
    {
        List<CoverageType> GetAvailableCoverageTypes(int accountId);
    }
}
