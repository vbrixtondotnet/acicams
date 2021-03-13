using ACIC.AMS.Domain.Models;
using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Policy = ACIC.AMS.Dto.Policy;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IPolicyDataStore
    {
        List<CoverageType> GetAvailableCoverageTypes(int accountId);
        List<Policy> GetPolicies(int accountId);
        Policy SavePolicy(Policy policy);
        List<PolicyEndorsementUnitStats> GetPolicyEndorsementUnitStats(int accountId, int policyId);
        List<ActivePolicyVehicle> GetActivePolicyVehicles(int policyId);
        AgentCommissions GetPolicyAgentCommissions(int policyId);
        bool SetInceptionStage(int policyId, bool isInceptionStage);
        DdCoverageType GetCoverageType(int coverageTypeId);
    }
}
