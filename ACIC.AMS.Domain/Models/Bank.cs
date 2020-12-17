using System;
using System.Collections.Generic;

#nullable disable

namespace ACIC.AMS.Domain.Models
{
    public partial class Bank : BaseModel
    {
        public int BankId { get; set; }
        public string Type { get; set; }
        public string BankName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
    }
}
