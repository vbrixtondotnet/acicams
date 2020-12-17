using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Driver : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverId { get; set; }
        public bool Excluded { get; set; }
        public string LastName { get; set; }
        public string Middle { get; set; }
        public string FirstName { get; set; }
        public DateTime? Dob { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Cdlnumber { get; set; }
        public string CdlyearLic { get; set; }
        public DateTime? DateHired { get; set; }
        public DateTime? Terminated { get; set; }
        public bool OwnerOperator { get; set; }
        public int? AccountId { get; set; }
        public string Notes { get; set; }
    }
}
