using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class EndorsementList
    {
        public DateTime Effective { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Vin { get; set; }
        public double? Pdvalue { get; set; }
        public double? Surcharge { get; set; }
        public double? AL { get; set; }
        public double? MTC { get; set; }
        public double? APD { get; set; }
        public double? BrokerFees { get; set; }
        public double? EndtFees { get; set; }
        public double? TotalAmount { get; set; }
        public string Status { get; set; }
        public double? Variance { get; set; }
    }
}
