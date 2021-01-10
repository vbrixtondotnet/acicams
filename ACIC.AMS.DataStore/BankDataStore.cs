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
    public class BankDataStore : BaseDataStore, IBankDataStore
    {
        public BankDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<Bank> GetLienHolders()
        {
            List<Bank> retval = new List<Bank>();
            var dbBanks = _context.Bank.Where(b => b.Type == "Lien Holder").ToList();
            dbBanks.ForEach(b =>
            {
                retval.Add(_mapper.Map<Bank>(b));
            });

            return retval;
        }

        public List<Bank> GetPremiumFinancers()
        {
            List<Bank> retval = new List<Bank>();
            var dbBanks = _context.Bank.Where(b => b.Type == "Premium Financer").ToList();
            dbBanks.ForEach(b =>
            {
                retval.Add(_mapper.Map<Bank>(b));
            });

            return retval;
        }
    }
}
