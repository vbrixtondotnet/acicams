using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ACIC.AMS.DataStore
{
    public class AgentDataStore : BaseDataStore, IAgentDataStore
    {
        public AgentDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }
     
        public List<Agent> GetAll()
        {
            List<Agent> retval = new List<Agent>();
            var dbAgents = _context.Agent.ToList();
            dbAgents.ForEach(ag => {
                retval.Add(_mapper.Map<Dto.Agent>(ag));
            });

            return retval;
        }

    
    }
}
