using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Endorsement : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int? VehicleId { get; set; }
        public string Vin { get; set; }
        public double? ProRate { get; set; }
        public double? Pdvalue { get; set; }
        public int? CoverageTypes { get; set; }
        public double? BasePerUnit { get; set; }
        public double? NonTaxedRateUnit { get; set; }
        public double? Pdrate { get; set; }
        public double? TrailerRate { get; set; }
        public double? Bfrate { get; set; }
        public double? Strate { get; set; }
        public double? Premium { get; set; }
        public double? Surcharge { get; set; }
        public string SurDesc { get; set; }
        public double? PolicyFees { get; set; }
        public double? Mgafees { get; set; }
        public double? SurplusTax { get; set; }
        public double? BrokerFees { get; set; }
        public double? EndtFee { get; set; }
        public double? OtherFees { get; set; }
        public double? TotalPremium { get; set; }
        public double? Commission { get; set; }
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
}
