using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class VehicleHistory
    {

        public int VehicleId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Transaction { get; set; }
        public string LegalName { get; set; }
        public string DateCreatedFormatted{ get { return DateCreated.ToString("yyyy/MM/dd"); } }
    }
}
