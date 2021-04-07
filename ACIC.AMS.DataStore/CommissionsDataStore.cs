using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ACIC.AMS.DataStore
{
    public class CommissionsDataStore : BaseDataStore, ICommissionsDataStore
    {
        public CommissionsDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public List<AgentCommission> GetAgentCommissions()
        {
            Dictionary<string, object> parameters = null;

            return this.ExecuteQuery<AgentCommission>("[dbo].[GetAgentCommissions]", parameters);
        }
    }
}
