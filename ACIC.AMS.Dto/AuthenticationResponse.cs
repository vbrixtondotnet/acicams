using System;
using System.Collections.Generic;
using System.Text;

namespace ACIC.AMS.Dto
{
    public class AuthenticationResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
