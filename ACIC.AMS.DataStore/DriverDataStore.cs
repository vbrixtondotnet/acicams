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
    public class DriverDataStore : BaseDataStore, IDriverDataStore
    {
        public DriverDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }
     
        public List<Driver> GetDrivers(int accountId)
        {
            List<Driver> retval = new List<Driver>();
            var dbDrivers = _context.Driver.Where(s => s.AccountId == accountId).ToList();
            dbDrivers.ForEach(d => {
                retval.Add(_mapper.Map<Dto.Driver>(d));
            });

            return retval;
        }
    }
}
