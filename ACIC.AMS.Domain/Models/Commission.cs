using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Commission : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommId { get; set; }
        public int? AccoutId { get; set; }
        public int? AgentId { get; set; }
        public string TrackId { get; set; }
        public short? SchedIndex { get; set; }
        public string Period { get; set; }
        public double? Commissions { get; set; }
        public double? BrokerFees { get; set; }
        public double? Incentives { get; set; }
        public double? Deductions { get; set; }
        public double? NetAmount { get; set; }
        public string Status { get; set; }
        public int? PaymentNo { get; set; }
        public DateTime? DatePaid { get; set; }
        public string Notes { get; set; }
    }
}
