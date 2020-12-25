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
    public class UsersApiController : ControllerBase
    {
        private readonly IUserDataStore userDataStore;
        public UsersApiController(IUserDataStore userDataStore)
        {
            this.userDataStore = userDataStore;
        }

        [Route("users")]
        [HttpPost]
        public IActionResult SaveUser([FromBody] Dto.User userDto)
        {
            try
            {
                if (!userDataStore.UserExists(userDto.EmailAddress))
                {
                    User user = userDataStore.SaveUser(userDto);
                    return Ok(user);
                }
                else
                {
                    return BadRequest($"User with email address {userDto.EmailAddress} already exists.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [Route("users/{id}")]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            User user = userDataStore.GetUserById(id);

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [Route("users")]
        [HttpGet]
        public IActionResult GetUsers(bool deleted = false, bool agent = false)
        {
            List<User> users = userDataStore.GetUsers(deleted, agent);

            if (users != null)
                return Ok(users);
            else
                return NotFound();
        }

        [Route("users/{id}/active/{active}")]
        [HttpPost]
        public IActionResult SetActive(int id, bool active)
        {
            var isUserDeleted = userDataStore.SetActive(id, active);

            if (isUserDeleted)
                return Ok();
            else
                return BadRequest();
        }

        [Route("users/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id, bool active)
        {
            var isUserDeleted = userDataStore.DeleteUser(id);

            if (isUserDeleted)
                return Ok();
            else
                return BadRequest();
        }

    }
}
