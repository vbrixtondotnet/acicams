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
using ACIC.AMS.Domain.Models;
using Policy = ACIC.AMS.Dto.Policy;

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

        public List<PolicyEndorsementUnitStats> GetPolicyEndorsementUnitStats(int accountId, int policyId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);
            parameters.Add("@PolicyId", policyId);

            return this.ExecuteQuery<PolicyEndorsementUnitStats>("[dbo].[GetPolicyEndorsementUnitStats]", parameters);
        }

        public Policy SavePolicy(Policy policy)
        {

            if(policy.PolicyId == 0)
            {
                Domain.Models.Policy dbPolicy = _mapper.Map<Domain.Models.Policy>(policy);
                dbPolicy.DateCreated = DateTime.Now;
                dbPolicy.CoverageTypes = policy.CoverageTypeId;
                _context.Policy.Add(dbPolicy);
            }
            else
            {
                var dbPolicy = _context.Policy.Where(a => a.PolicyId == policy.PolicyId).AsNoTracking().FirstOrDefault();
                var updatedPolicy = _mapper.Map<Domain.Models.Policy>(policy);
                updatedPolicy.DateModified = DateTime.Now;
                updatedPolicy.DateCreated = dbPolicy.DateCreated;
                updatedPolicy.CreatedBy = dbPolicy.CreatedBy;
                updatedPolicy.CoverageTypes = policy.CoverageTypeId;
                _context.Policy.Update(updatedPolicy);
            }

            _context.SaveChanges();

            return policy;
        }

        public List<ActivePolicyVehicle> GetActivePolicyVehicles(int policyId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@PolicyId", policyId);

            return this.ExecuteQuery<ActivePolicyVehicle>("[dbo].[GetActivePolicyVehicles]", parameters);
        }

        public PolicyAgentCommissions GetPolicyAgentCommissions(int policyId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@PolicyId", policyId);

            return this.ExecuteQuery<PolicyAgentCommissions>("[dbo].[GetPolicyAgentCommissions]", parameters).FirstOrDefault();
        }

        public bool SetInceptionStage(int policyId, bool isInceptionStage)
        {
            var policy = _context.Policy.Where(p => p.PolicyId == policyId).FirstOrDefault();
            if (policy != null)
            {
                policy.InceptionStage = isInceptionStage;
                _context.Policy.Update(policy);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public DdCoverageType GetCoverageType(int coverageTypeId)
        {
            return _context.DdCoverageType.Where(c => c.Id == coverageTypeId).FirstOrDefault();
        }
    }
}
