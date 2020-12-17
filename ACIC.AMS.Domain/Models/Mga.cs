using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Mga : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Mgaid { get; set; }
        public string Mganame { get; set; }
        public string WritingState { get; set; }
        public string CarrierId { get; set; }
        public string Mgaphone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public double? EndtFees { get; set; }
        public string Notes { get; set; }
    }
}
