using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACIC.AMS.Web.Models
{
    public class EndorsementUpdatePaymentStatusModel
    {
        public int AccountId { get; set; }
        public string Ern { get; set; }
        public int CoverageType { get; set; }
        public int PolicyId { get; set; }
        public int Status { get; set; }
    }
}
