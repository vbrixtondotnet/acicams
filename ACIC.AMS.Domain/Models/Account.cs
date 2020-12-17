using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Account : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public string Usdot { get; set; }
        public string StatePermit { get; set; }
        public string LegalName { get; set; }
        public string Dba { get; set; }
        public string TaxId { get; set; }
        public float? OperationType { get; set; }
        public float? OperationRadius { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string GarageAddress { get; set; }
        public string GarageCity { get; set; }
        public string GarageState { get; set; }
        public string GarageZip { get; set; }
        public string Notes { get; set; }
        public float? Source { get; set; }
        public int? YearClient { get; set; }
        public int? AgentId { get; set; }
    }
}
