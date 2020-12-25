using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Agent
    {
        public int AgentId { get; set; }
        public int UserId { get; set; }
        public double? CommSplitNew { get; set; }
        public double? CommSplitRenew { get; set; }
        public double? CommFixedAmount { get; set; }
        public double? BrokerFeeSplit { get; set; }
        public string CommPaymentPlan { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
