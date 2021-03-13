using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Endorsement
    {
        public int EndtId { get; set; }
        public string Status { get; set; }
        public int? PolicyId { get; set; }
        public DateTime? Effective { get; set; }
        public int? AccountId { get; set; }
        public string Action { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string InsuredAddress { get; set; }
        public int? DriverId { get; set; }
        public string Vin { get; set; }
        public decimal? ProRate { get; set; }
        public decimal? Pdvalue { get; set; }
        public int? CoverageTypes { get; set; }
        public decimal? BasePerUnit { get; set; }
        public decimal? NonTaxedRateUnit { get; set; }
        public decimal? Pdrate { get; set; }
        public decimal? TrailerRate { get; set; }
        public decimal? Bfrate { get; set; }
        public decimal? Strate { get; set; }
        public decimal? Premium { get; set; }
        public decimal? Surcharge { get; set; }
        public string SurDesc { get; set; }
        public decimal? PolicyFees { get; set; }
        public decimal? Mgafees { get; set; }
        public decimal? SurplusTax { get; set; }
        public decimal? BrokerFees { get; set; }
        public decimal? EndtFee { get; set; }
        public decimal? OtherFees { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? Commission { get; set; }
        public DateTime? InvoiceRef { get; set; }
        public string FinanceRef { get; set; }
        public bool Dpreceived { get; set; }
        public bool PaidTo { get; set; }
        public string Apn { get; set; }
        public string Ern { get; set; }
        public string Notes { get; set; }
        public string EndtSource { get; set; }
        public string TrackId { get; set; }
        public string EndtTrackId { get; set; }
    }

    public enum EndorsementType
    {
        Driver
    }

    public enum EndorsementAction
    {
        ADD
    }
}
