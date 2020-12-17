using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ACIC.AMS.Domain.Models
{
    public abstract class BaseModel
    {
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
