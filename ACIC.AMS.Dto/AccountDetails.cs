using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class AccountDetails
    {
        private string garageAddress;
        private string garageCity;
        private string garageState;
        private string garageZip;
        private string dba;
        private string source;
        private string address;
        private string city;
        private string state;
        private string zip;

        public int AccountId { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string Usdot { get; set; }
        public string StatePermit { get; set; }
        public string LegalName { get; set; }
        public string Dba { get { return this.dba == null ? string.Empty : this.dba; } set { this.dba = value; } }
        public string TaxId { get; set; }
        public float? OperationTypeId { get; set; }
        public string OperationType { get; set; }
        public float? OperationRadiusId { get; set; }
        public string OperationRadius { get; set; }
        public string Address { get { return this.address == null ? string.Empty : this.address; } set { this.address = value; } }
        public string City { get { return this.city == null ? string.Empty : this.city; } set { this.city = value; } }
        public string State { get { return this.state == null ? string.Empty : this.state; } set { this.state = value; } }
        public string Zip { get { return this.zip == null ? string.Empty : this.zip; } set { this.zip = value; } }
        public string GarageAddress { get { return this.garageAddress == null ? string.Empty : this.garageAddress; } set { this.garageAddress = value; } }
        public string GarageCity { get { return this.garageCity == null ? string.Empty : this.garageCity; } set { this.garageCity = value; } }
        public string GarageState { get { return this.garageState == null ? string.Empty : this.garageState; } set { this.garageState = value; } }
        public string GarageZip { get { return this.garageZip == null ? string.Empty : this.garageZip; } set { this.garageZip = value; } }
        public string Notes { get; set; }
        public float? SourceId { get; set; }
        public string Source { get { return this.source == null ? string.Empty : this.source; } set { this.source = value; } }
        public int? YearClient { get; set; }
        public int AgentId { get; set; }
        public string Agent { get; set; }
        public DateTime? DateCreated { get; set; }
        public string MailingAddress {
            get {
                var retval = string.Empty;
                if (!string.IsNullOrEmpty(Address))
                    retval += Address;
                if (!string.IsNullOrEmpty(City))
                    retval += $",{City}";
                if (!string.IsNullOrEmpty(State))
                    retval += $",{State}";
                if (!string.IsNullOrEmpty(Zip))
                    retval += $",{Zip}";

                return retval;
            }
        }
        public string GarageAddressComplete
        {
            get
            {
                var retval = string.Empty;
                if (!string.IsNullOrEmpty(GarageAddress))
                    retval += GarageAddress;
                if (!string.IsNullOrEmpty(GarageCity))
                    retval += $",{GarageCity}";
                if (!string.IsNullOrEmpty(GarageState))
                    retval += $",{GarageState}";
                if (!string.IsNullOrEmpty(GarageZip))
                    retval += $",{GarageZip}";

                return retval;
            }
        }
    }
}
