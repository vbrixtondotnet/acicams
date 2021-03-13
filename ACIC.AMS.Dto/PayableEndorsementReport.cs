using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class PayableEndorsementReport
    {
        public int AccountId { get; set; }
        public string LegalName { get; set; }
        public string Ern { get; set; }
        public int PolicyId { get; set; }
        public string Mganame { get; set; }
        public string FinanceRef { get; set; }
        public int CoverageTypes { get; set; }
        public string CoverageTypeDesc { get; set; }
        public string CarrierName { get; set; }
        public decimal Premium { get; set; }
        public decimal SLTaxes { get; set; }
        public decimal Fees { get; set; }
        public decimal Commission { get; set; }
        public decimal PayableAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public bool DownPayment { get; set; }
        public DateTime? DueDate { get; set; }
        public int? DueInDays { get; set; }
    }
}
