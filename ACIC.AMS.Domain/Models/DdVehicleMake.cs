using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class DdVehicleMake : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vmid { get; set; }
        public string VehMakeName { get; set; }
    }
}
