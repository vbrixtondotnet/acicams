using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACIC.AMS.Web.Models
{
    public class EndorsementUpdateFinanceReferenceModel
    {
        public int AccountId { get; set; }
        public string Ern { get; set; }
        public int CoverageType { get; set; }
        public int PolicyId { get; set; }
        public string FinancingReference { get; set; }
    }
}
