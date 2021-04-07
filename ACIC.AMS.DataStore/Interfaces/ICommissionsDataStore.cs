using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface ICommissionsDataStore
    {
        
        List<AgentCommission> GetAgentCommissions();
    }
}
