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
    public class MgaDataStore : BaseDataStore, IMgaDataStore
    {
        public MgaDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Mga> GetMgas()
        {
            List<Mga> retval = new List<Mga>();
            var dbMgas = _context.Mga.ToList();
            dbMgas.ForEach(m =>
            {
                retval.Add(_mapper.Map<Mga>(m));
            });

            return retval;
        }

    }
}
