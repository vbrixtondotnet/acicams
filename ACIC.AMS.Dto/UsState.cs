using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class UsState
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string CityAscii { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public double? CountyFips { get; set; }
        public string CountyName { get; set; }
        public double? CountyFipsAll { get; set; }
        public string CountyNameAll { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public double? Population { get; set; }
        public double? Density { get; set; }
        public string Source { get; set; }
        public string Military { get; set; }
        public string Incorporated { get; set; }
        public string Timezone { get; set; }
        public double? Ranking { get; set; }
        public string Zips { get; set; }
    }
}
