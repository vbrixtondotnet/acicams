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
        public string CdlnumberFormatted
        {
            get
            {
                if (this.Cdlnumber != null) return Cdlnumber;

                else return string.Empty;
            }
        }
        public string CdlyearLic { get; set; }
        public string CdlyearLicFormatted
        {
            get
            {
                if (this.CdlyearLic != null) return CdlyearLic;

                else return string.Empty;
            }
        }
        public DateTime? DateHired { get; set; }
        public string DateHiredFormatted
        {
            get
            {
                if (this.DateHired != null) return Convert.ToDateTime(DateHired).ToString("yyyy/MM/dd");

                else return string.Empty;
            }
        }
        public DateTime? Terminated { get; set; }
        public string TerminatedFormatted
        {
            get
            {
                if (this.Terminated != null) return Convert.ToDateTime(Terminated).ToString("yyyy/MM/dd");

                else return string.Empty;
            }
        }
        public bool OwnerOperator { get; set; }
        public int? AccountId { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }

        public string Status { get { return Active ? "Active" : "InActive"; } }
        public string FullName { get { return $"{FirstName} {Middle} {LastName}"; } }
        public string DobFormatted
        {
            get
            {
                if (this.Dob != null) return Convert.ToDateTime(Dob).ToString("yyyy/MM/dd");

                else return string.Empty;
            }
        }
    }
}
