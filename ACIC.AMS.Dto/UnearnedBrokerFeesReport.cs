using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class UnearnedBrokerFeesReport
    {
        public int AccountId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public int CoverageTypeId { get; set; }
        public DateTime Effective { get; set; }
        public decimal BrokerFees { get; set; }
        public string LegalName { get; set; }
        public string CoverageTypes { get; set; }
        public decimal DailyCommission { get; set; }
        public decimal CommissionDays { get; set; }
        public decimal Earned { get; set; }
        public decimal Unearned { get; set; }
    }
}
