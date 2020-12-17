using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class Driver
    {
        DateTime? dateHired;
        string dateHiredString;
        string cdlyearLic;
        DateTime? terminated;
        string terminatedString;

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
        public string CdlyearLic { get { return this.cdlyearLic; } set { this.cdlyearLic = value == null ? string.Empty : value; } }
        public DateTime? DateHired
        {
            get { return dateHired; }
            set
            {
                this.dateHired = value;
                this.dateHiredString = value == null ? string.Empty : value.ToString();
            }
        }
        public string DateHiredString { get { return this.dateHiredString; } }
        public DateTime? Terminated { get {
                return this.terminated;
            } set {
                this.terminated = value;
                this.terminatedString = value == null ? string.Empty : value.ToString();
            } }
        public string TerminatedString { get { return this.terminatedString; } }
        public bool OwnerOperator { get; set; }
        public int? AccountId { get; set; }
        public string Notes { get; set; }
    }
}
