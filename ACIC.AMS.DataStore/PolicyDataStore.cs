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
    public class PolicyDataStore : BaseDataStore, IPolicyDataStore
    {
        public PolicyDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public List<CoverageType> GetAvailableCoverageTypes(int accountId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);

            return this.ExecuteQuery<CoverageType>("[dbo].[GetAvailableCoverageTypes]", parameters);
        }

        public List<Policy> GetPolicies(int accountId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);

            return this.ExecuteQuery<Policy>("[dbo].[GetPolicies]", parameters);
        }

        public Policy SavePolicy(Policy policy)
        {
            Domain.Models.Policy dbPolicy = _mapper.Map<Domain.Models.Policy>(policy);
            dbPolicy.DateCreated = DateTime.Now;
            _context.Policy.Add(dbPolicy);
            _context.SaveChanges();

            policy.PolicyId = dbPolicy.PolicyId;
            return policy;
        }
    }
}
