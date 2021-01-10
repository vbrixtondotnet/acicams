using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Policy
    {
        double? premium;
        double? totalPremium;
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
        public DateTime? Effective {get;set;}
        public string EffectiveString
        {
            get
            {
                return this.Effective != null ? ((DateTime)this.Effective).ToString("yyyy/mm/dd") : string.Empty;
            }
        }
        public DateTime? Expiration { get; set; }
        public string ExpirationString
        {
            get
            {
                return this.Expiration != null ? ((DateTime)this.Expiration).ToString("yyyy/mm/dd") : string.Empty;
            }
        }
        public bool DirectBill { get; set; }
        public double? TotalFactor { get; set; }
        public double? BasePerUnit { get; set; }
        public double? NonTaxedRateUnit { get; set; }
        public double? Bfrate { get; set; }
        public double? Strate { get; set; }
        public double? Pdrate { get; set; }
        public double? TrailerRate { get; set; }
        public double? PDNonOwnedTrailerRate { get; set; }
        public double? TrailerInterchangeRate { get; set; }
        public double? Premium { get { return this.premium == null ? 0 : this.premium; } set { this.premium = value; } }
        public double? Surcharge { get; set; }
        public double? PolicyFees { get; set; }
        public double? Mgafees { get; set; }
        public double? SurplusTax { get; set; }
        public double? BrokerFees { get; set; }
        public double? OtherFees { get; set; }
        public double? TotalPremium { get { return this.totalPremium == null ? 0 : this.totalPremium; } set { this.totalPremium = value; } }
        public double? CommRate { get; set; }
        public double? Commission { get; set; }
        public string AgentSplit { get; set; }
        public double? AgentComm { get; set; }
        public double? AgentBfshare { get; set; }
        public string PolicyType { get; set; }
        public int? BankId { get; set; }
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public string TrackId { get; set; }
        public bool InceptionStage { get; set; }
    }
}
