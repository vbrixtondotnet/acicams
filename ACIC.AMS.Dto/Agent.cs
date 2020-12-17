using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Agent
    {
        public int AgentId { get; set; }
        public bool Inactive { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public double? CommSplitNew { get; set; }
        public double? CommSplitRenew { get; set; }
        public double? CommFixedAmount { get; set; }
        public double? BrokerFeeSplit { get; set; }
        public string CommPaymentPlan { get; set; }
        public string Notes { get; set; }
    }
}
