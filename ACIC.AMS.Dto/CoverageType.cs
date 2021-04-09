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
        public decimal? TotalFactor { get; set; }
        public decimal? BasePerUnit { get; set; }
        public decimal? NonTaxedRateUnit { get; set; }
        public decimal? Bfrate { get; set; }
        public decimal? Strate { get; set; }
        public decimal? Pdrate { get; set; }
        public decimal? TrailerRate { get; set; }
        public decimal? Premium { get; set; }
        public decimal? Surcharge { get; set; }
        public decimal? PolicyFees { get; set; }
        public decimal? Mgafees { get; set; }
        public decimal? SurplusTax { get; set; }
        public decimal? BrokerFees { get; set; }
        public decimal? OtherFees { get; set; }
        public decimal? TotalPremium { get; set; }
        public decimal? CommRate { get; set; }
        public decimal? Commission { get; set; }
        public decimal? AgentSplit { get; set; }
        public decimal? AgentComm { get; set; }
        public decimal? AgentBfshare { get; set; }
        public decimal? InceptionStage { get; set; }
        public decimal? PDNonOwnedTrailerRate { get; set; }
    }
}
