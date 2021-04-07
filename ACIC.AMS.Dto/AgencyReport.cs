using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class AgencyReport
    {
        public int PolicyId { get; set; }
        public int Mgaid { get; set; }
        public int AccountId { get; set; }
        public int CarrierId { get; set; }
        public int CoverageTypeId { get; set; }
        public string Carrier { get; set; }
        public string Mga { get; set; }
        public string Account { get; set; }
        public int AgentId { get; set; }
        public string Agent { get; set; }
        public string PolicyNumber { get; set; }
        public string CoverageTypes { get; set; }
        public DateTime Effective { get; set; }
        public DateTime Expiration { get; set; }
        public decimal InitialPremium { get; set; }
        public decimal CommulativePremium { get; set; }
        public decimal InitialCommission { get; set; }
        public decimal CommulativeCommission { get; set; }
    }
}
