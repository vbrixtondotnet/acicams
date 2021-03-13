using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Policy
    {
        decimal? premium;
        decimal? totalPremium;
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public int? AccountId { get; set; }
        public int? Mgaid { get; set; }
        public string Mganame { get; set; }
        public int? CarrierId { get; set; }
        public string CarrierName { get; set; }
        public float? CoverageTypeId { get; set; }
        public string CoverageType { get; set; }
        public DateTime? BindDate { get; set; }
        public DateTime? Effective {get;set; }
        public DateTime? Expiration { get; set; }
        public string EffectiveDateText
        {
            get
            {
                return Effective != null ? Convert.ToDateTime(Effective).ToShortDateString() : string.Empty;
            }
        }
        public string ExpirationDateText
        {
            get
            {
                return Expiration != null ? Convert.ToDateTime(Expiration).ToShortDateString() : string.Empty;
            }
        }
        public bool DirectBill { get; set; }
        public decimal? TotalFactor { get; set; }
        public decimal? BasePerUnit { get; set; }
        public decimal? NonTaxedRateUnit { get; set; }
        public decimal? Bfrate { get; set; }
        public decimal? Strate { get; set; }
        public decimal? Pdrate { get; set; }
        public decimal? TrailerRate { get; set; }
        public decimal? PDNonOwnedTrailerRate { get; set; }
        public decimal? TrailerInterchangeRate { get; set; }
        public decimal? Premium { get { return this.premium == null ? 0 : this.premium; } set { this.premium = value; } }
        public decimal? Surcharge { get; set; }
        public decimal? PolicyFees { get; set; }
        public decimal? Mgafees { get; set; }
        public decimal? SurplusTax { get; set; }
        public decimal? BrokerFees { get; set; }
        public decimal? OtherFees { get; set; }
        public decimal? TotalPremium { get { return this.totalPremium == null ? 0 : this.totalPremium; } set { this.totalPremium = value; } }
        public decimal? CommRate { get; set; }
        public decimal? Commission { get; set; }
        public string AgentSplit { get; set; }
        public decimal? AgentComm { get; set; }
        public decimal? AgentBfshare { get; set; }
        public string PolicyType { get; set; }
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public string TrackId { get; set; }
        public bool InceptionStage { get; set; }
    }
}
