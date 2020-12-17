using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Vehicle : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Vinid { get; set; }
        public string Vin { get; set; }
        public string VUnit { get; set; }
        public int? Type { get; set; }
        public string VYear { get; set; }
        public float? Make { get; set; }
        public string Model { get; set; }
        public double? Pdvalue { get; set; }
        public int? BankId { get; set; }
        public string AccountNo { get; set; }
        public string OwnerOperator { get; set; }
        public string Notes { get; set; }
        public int? AccountId { get; set; }
        public int? DriverId { get; set; }
        public bool PolicyInclude { get; set; }
    }
}
