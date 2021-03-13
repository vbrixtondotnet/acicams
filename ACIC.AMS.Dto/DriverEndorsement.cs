using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class DriverEndorsement
    {
        public int DriverId { get; set; }
        public bool Excluded { get; set; }
        public string LastName { get; set; }
        public string Middle { get; set; }
        public string FirstName { get; set; }
        public DateTime? Dob { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Cdlnumber { get; set; }
        public string CdlyearLic { get; set; }
        public DateTime? DateHired { get; set; }
        public DateTime? Terminated { get; set; }
        public string DateHiredText { get; set; }
        public string TerminatedDateText { get; set; }
        public bool OwnerOperator { get; set; }
        public int? AccountId { get; set; }
        public string Notes { get; set; }
        public List<DriverCoverage> DriverCoverages {get;set;}

    }

    public class DriverCoverage
    {
        public int CoverageTypeId { get; set; }
        public decimal? Premium { get; set; }
        public decimal? PremiumTax { get; set; }
        public decimal? BrokerFee { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
