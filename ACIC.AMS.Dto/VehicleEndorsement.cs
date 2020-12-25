using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class VehicleEndorsement
    {
        public int Id { get; set; }
        public string Vin { get; set; }
        public string VUnit { get; set; }
        public int? Type { get; set; }
        public string VYear { get; set; }
        public float? Make { get; set; }
        public string Model { get; set; }
        public double? Pdvalue { get; set; }
        public int? BankId { get; set; }
        public string AccountNo { get; set; }
        public string OwnerOperator { get; set; }
        public string Notes { get; set; }
        public int? AccountId { get; set; }
        public int? DriverId { get; set; }
        public bool PolicyInclude { get; set; }
        public string Driver { get; set; }
        public string VehTypeName { get; set; }
        public string VehMakeName { get; set; }
        public bool DriverOwner { get; set; }
        public bool NotOnLien { get; set; }

        public List<VehicleCoverage> VehicleCoverages { get; set; }

    }

    public class VehicleCoverage
    {
        public int CoverageTypeId { get; set; }
        public double? Premium { get; set; }
        public double? PremiumTax { get; set; }
        public double? BrokerFee { get; set; }
        public double? TotalAmount { get; set; }
    }
}
