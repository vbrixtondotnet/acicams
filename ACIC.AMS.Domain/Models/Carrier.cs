using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Carrier : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string WritingState { get; set; }
        public string AgencyCode { get; set; }
        public string Ambest { get; set; }
        public string CarrierPhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
        public string RequirementsLink { get; set; }
    }
}
