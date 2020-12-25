using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Agent : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentId { get; set; }
        public int UserId { get; set; }
        public double? CommSplitNew { get; set; }
        public double? CommSplitRenew { get; set; }
        public double? CommFixedAmount { get; set; }
        public double? BrokerFeeSplit { get; set; }
        public string CommPaymentPlan { get; set; }
        public string Notes { get; set; }
    }
}
