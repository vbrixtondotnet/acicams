using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Policy : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public int? AccountId { get; set; }
        public int? Mgaid { get; set; }
        public int? CarrierId { get; set; }
        public float? CoverageTypes { get; set; }
        public DateTime? BindDate { get; set; }
        public DateTime? Effective { get; set; }
        public DateTime? Expiration { get; set; }
        public bool DirectBill { get; set; }
        public double? TotalFactor { get; set; }
        public double? BasePerUnit { get; set; }
        public double? NonTaxedRateUnit { get; set; }
        public double? Bfrate { get; set; }
        public double? Strate { get; set; }
        public double? Pdrate { get; set; }
        public double? TrailerRate { get; set; }
        public double? TrailerInterchangeRate { get; set; }
        public double? PDNonOwnedTrailerRate { get; set; }
        public double? Premium { get; set; }
        public double? Surcharge { get; set; }
        public double? PolicyFees { get; set; }
        public double? Mgafees { get; set; }
        public double? SurplusTax { get; set; }
        public double? BrokerFees { get; set; }
        public double? OtherFees { get; set; }
        public double? TotalPremium { get; set; }
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
