using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Claim : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? LossDate { get; set; }
        public DateTime? ReportedDate { get; set; }
        public float? LossType { get; set; }
        public int? CarrierId { get; set; }
        public int? PolicyId { get; set; }
        public int? DriverId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactId { get; set; }
        public int? ClaimNumber { get; set; }
        public string ClaimStatus { get; set; }
        public double? PaidOut { get; set; }
        public string Notes { get; set; }
    }
}
