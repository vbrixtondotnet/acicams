using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class UnearnedCommissionsDetail
    {
        public string PolicyNumber { get; set; }
        public DateTime Expiration { get; set; }
        public string CoverageTypes { get; set; }
        public string LegalName { get; set; }
        public int PolicyId { get; set; }
        public DateTime Effective { get; set; }
        public decimal DailyCommission { get; set; }
        public decimal Commission { get; set; }
        public decimal CommissionDays { get; set; }
        public decimal Earned { get; set; }
        public decimal Unearned { get; set; }
        public string Reference { get; set; }
    }
}
