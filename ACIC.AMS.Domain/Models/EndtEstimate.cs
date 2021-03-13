using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class EndtEstimate : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstEndtId { get; set; }
        public int EndtId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Effective { get; set; }
        public double? ProRate { get; set; }
        public double? Pdvalue { get; set; }
        public double? BasePerUnit { get; set; }
        public double? NonTaxedRateUnit { get; set; }
        public double? Pdrate { get; set; }
        public double? Bfrate { get; set; }
        public double? Strate { get; set; }
        public double? Premium { get; set; }
        public double? Surcharge { get; set; }
        public double? PolicyFees { get; set; }
        public double? Mgafees { get; set; }
        public double? SurplusTax { get; set; }
        public double? BrokerFees { get; set; }
        public double? EndtFee { get; set; }
        public double? OtherFees { get; set; }
        public double? TotalPremium { get; set; }
        public string EndtTrackId { get; set; }
    }
}
