using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IMgaDataStore
    {
        List<Mga> GetMgas();
    }
}
