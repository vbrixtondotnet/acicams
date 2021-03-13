using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACIC.AMS.Web.Models
{
    public class EndorsementUpdateAmount
    {
        public int EndtId { get; set; }
        public string Field { get; set; }
        public decimal Amount { get; set; }
    }
}
