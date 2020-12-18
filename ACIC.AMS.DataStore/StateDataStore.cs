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
    public class StateDataStore : BaseDataStore, IStateDataStore
    {
        public StateDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public List<UsState> GetCities(string stateId)
        {
            List<UsState> retval = new List<UsState>();
            var dbAgents = _context.DdUsstate.Where(s => s.StateId == stateId).ToList();
            dbAgents.ForEach(st => {
                retval.Add(_mapper.Map<Dto.UsState>(st));
            });

            return retval;
        }
    }
}
