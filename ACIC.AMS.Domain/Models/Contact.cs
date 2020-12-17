using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Contact :BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public float? Title { get; set; }
        public string MblBusiness { get; set; }
        public string MblDirect { get; set; }
        public string MblMobile { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Type { get; set; }
        public string RefId { get; set; }
        public string Notes { get; set; }
    }
}
