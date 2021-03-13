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
        [Column(TypeName = "Date")]
        public DateTime? BindDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Effective { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Expiration { get; set; }
        public bool DirectBill { get; set; }
        public decimal? TotalFactor { get; set; }
        public decimal? BasePerUnit { get; set; }
        public decimal? NonTaxedRateUnit { get; set; }
        public decimal? Bfrate { get; set; }
        public decimal? Strate { get; set; }
        public decimal? Pdrate { get; set; }
        public decimal? TrailerRate { get; set; }
        public decimal? TrailerInterchangeRate { get; set; }
        public decimal? PDNonOwnedTrailerRate { get; set; }
        public decimal? Premium { get; set; }
        public decimal? Surcharge { get; set; }
        public decimal? PolicyFees { get; set; }
        public decimal? Mgafees { get; set; }
        public decimal? SurplusTax { get; set; }
        public decimal? BrokerFees { get; set; }
        public decimal? OtherFees { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? CommRate { get; set; }
        public decimal? Commission { get; set; }
        public string AgentSplit { get; set; }
        public decimal? AgentComm { get; set; }
        public decimal? AgentBfshare { get; set; }
        public string PolicyType { get; set; }
        public int? BankId { get; set; }
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string Source { get; set; }
        public string TrackId { get; set; }
        public bool InceptionStage { get; set; }
    }
}
