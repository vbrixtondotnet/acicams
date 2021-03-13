using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class EndorsementReportEndtItemsGroup
    {
        public string Account { get; set; }
        public string EndtNo { get; set; }
        public string CoverageType { get; set; }
        public List<EndorsementReportEndtItems> EndorsementReportEndtItems { get; set; }
        public decimal TotalPremium { get { return this.EndorsementReportEndtItems.Sum(x => x.Premium); } }
        public decimal TotalSurcharge { get { return this.EndorsementReportEndtItems.Sum(x => x.Surcharge); } }
        public decimal TotalSurplusTaxes { get { return this.EndorsementReportEndtItems.Sum(x => x.SurplusTax); } }
        public decimal TotalEndtFees { get { return this.EndorsementReportEndtItems.Sum(x => x.EndtFee); } }
        public decimal TotalNonTaxedFees { get { return this.EndorsementReportEndtItems.Sum(x => x.NonTaxedRateUnit); } }
        public decimal TotalOtherFees { get { return this.EndorsementReportEndtItems.Sum(x => x.OtherFees); } }
        public decimal TotalCommissions { get { return this.EndorsementReportEndtItems.Sum(x => x.Commission); } }
        public decimal TotalNetAmount { get { return this.EndorsementReportEndtItems.Sum(x => x.TotalAmount); } }

    }

    public class EndorsementReportEndtItems
    {
        public int EndtId { get; set; }
        public string Reference { get; set; }
        public decimal Premium { get; set; }
        public decimal Surcharge { get; set; }
        public decimal SurplusTax { get; set; }
        public decimal EndtFee { get; set; }
        public decimal NonTaxedRateUnit { get; set; }
        public decimal OtherFees { get; set; }
        public decimal Commission { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
