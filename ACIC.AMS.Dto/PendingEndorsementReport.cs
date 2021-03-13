using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class PendingEndorsementReport
    {
        public DateTime Effective { get; set; }
        public int AccountId { get; set; }
        public string Account { get; set; }
        public string DateEffectiveText { get { return this.Effective.ToShortDateString(); } }
        public string Description { get; set; }
        public string Action { get; set; }
        public string Type { get; set; }
        public string InsuredItem { get; set; }
        public int CoverageTypes { get; set; }
        public string TypeOfCoverage { get; set; }
        public decimal TotalAmount { get; set; }
        public string CarrierName { get; set; }
        public string Status { get; set; }
    }
}
