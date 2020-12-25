using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class CoverageType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }

        public string PolicyNumber { get; set; }

        public int PolicyId { get; set; }
        public double? TotalFactor { get; set; }
        public double? BasePerUnit { get; set; }
        public double? NonTaxedRateUnit { get; set; }
        public double? Bfrate { get; set; }
        public double? Strate { get; set; }
        public double? Pdrate { get; set; }
        public double? TrailerRate { get; set; }
        public double? Premium { get; set; }
        public double? Surcharge { get; set; }
        public double? PolicyFees { get; set; }
        public double? Mgafees { get; set; }
        public double? SurplusTax { get; set; }
        public double? BrokerFees { get; set; }
        public double? OtherFees { get; set; }
        public double? TotalPremium { get; set; }
        public double? CommRate { get; set; }
        public double? Commission { get; set; }
        public double? AgentSplit { get; set; }
        public double? AgentComm { get; set; }
        public double? AgentBfshare { get; set; }
        public double? InceptionStage { get; set; }
        public double? PDNonOwnedTrailerRate { get; set; }
    }
}
