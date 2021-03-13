using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Driver
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
        public string DateHiredText
        {
            get
            {
                return DateHired != null ? Convert.ToDateTime(DateHired).ToShortDateString() : string.Empty;
            }
        }
        public DateTime? Terminated { get; set; }
        public string TerminatedDateText
        {
            get
            {
                return Terminated != null ? Convert.ToDateTime(Terminated).ToShortDateString() : string.Empty;
            }
        }
        public bool OwnerOperator { get; set; }
        public int? AccountId { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }

        public string Status { get { return Active ? "Active" : "InActive"; } set { this.Active = value == "Active"; } }
        public string FullName { get { return $"{FirstName} {Middle} {LastName}"; } }
        public string DobText
        {
            get
            {
                return Dob != null ? Convert.ToDateTime(Dob).ToShortDateString() : string.Empty;
            }
        }
    }
}
