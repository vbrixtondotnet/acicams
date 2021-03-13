using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class ActivePolicyVehicle
    {
        public string Year { get; set; }
        public string Make { get; set; }
        public string VIN { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public double PDValue { get; set; }
        public string Driver { get; set; }
    }
}
