using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class AvailableEndorsementVehicle
    {
        public int Id { get; set; }
        public string Vin { get; set; }
        public string VYear { get; set; }
        public double? Pdvalue { get; set; }
        public string LegalName { get; set; }
        public string Make { get; set; }
    }
}
