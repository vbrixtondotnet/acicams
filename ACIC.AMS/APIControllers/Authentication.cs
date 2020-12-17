using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ACIC.AMS.Web.APIControllers
{
    [Route("api")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly IUserDataStore userDataStore;
        private readonly IConfiguration configuration;
        public Authentication(IUserDataStore userDataStore, IConfiguration configuration)
        {
            this.userDataStore = userDataStore;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [Route("authentication")]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticationRequest authRequest)
        {
            User user = userDataStore.Login(authRequest.UserName, authRequest.Password);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                AuthenticationResponse response = new AuthenticationResponse { Token = tokenString, User = user };
                return Ok(response);
            }
            else
                return Unauthorized();
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
