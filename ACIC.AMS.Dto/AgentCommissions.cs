using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class AgentCommissions
    {
        public string CommissionRate { get; set; }
        public decimal TotalCommission { get; set; }
        public int DaysToExpire { get; set; }
        public decimal UnearnedCommission { get; set; }
        public decimal EarnedCommission { get; set; }
    }
}
