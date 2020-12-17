using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ACIC.AMS.Dto
{
    public partial class User
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string AgentId { get; set; }
        public bool Active { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
