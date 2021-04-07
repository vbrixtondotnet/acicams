using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class AgentCommission
    {
        public int PolicyId { get; set; }
        public int AccountId { get; set; }
        public string Account { get; set; }
        public int AgentId { get; set; }
        public string Agent { get; set; }
        public string PolicyNumber { get; set; }
        public string CoverageTypes { get; set; }
        public int CoverageTypeId { get; set; }
        public DateTime Effective { get; set; }
        public DateTime Expiration { get; set; }
        public string AgentSplit { get; set; }
        public decimal CommulativeAgencyCommission { get; set; }
        public decimal CommulativeAgencyBrokerFees { get; set; }
        public decimal CummulativeAgentCommissions { get; set; }
        public decimal CummulativeAgentBrokersFee { get; set; }
        public decimal PayableCommissions { get; set; }
        public decimal PayableBrokerFees { get; set; }
        public decimal PaidBrokerFees { get; set; }
        public decimal CommulativeAgencyTotals { get; set; }
        public decimal PayableTotals { get; set; }
        public decimal PaidTotals { get; set; }
        public string Status { get; set; }
    }
}
