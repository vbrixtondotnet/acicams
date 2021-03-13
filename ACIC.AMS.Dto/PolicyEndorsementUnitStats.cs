using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class PolicyEndorsementUnitStats
    {
        public string Unit { get; set; }
        public decimal Initial { get; set; }
        public decimal Endorsements { get; set; }
        public decimal Current { get; set; }
    }
}
