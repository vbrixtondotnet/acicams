﻿using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IStateDataStore
    {
        List<UsState> GetCities(string stateId);
    }
}
