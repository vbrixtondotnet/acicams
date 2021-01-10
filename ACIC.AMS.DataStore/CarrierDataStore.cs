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
    public class CarrierDataStore : BaseDataStore, ICarrierDataStore
    {
        public CarrierDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Carrier> GetCarriers()
        {
            List<Carrier> retval = new List<Carrier>();
            var dbCarriers = _context.Carrier.ToList();
            dbCarriers.ForEach(c =>
            {
                retval.Add(_mapper.Map<Carrier>(c));
            });

            return retval;
        }

    }
}
