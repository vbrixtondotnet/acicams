using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class AgentsApiController : ControllerBase
    {
        private readonly IAgentDataStore agentDataStore;
        private readonly IUserDataStore userDataStore;
        public AgentsApiController(IAgentDataStore agentDataStore, IUserDataStore userDataStore)
        {
            this.agentDataStore = agentDataStore;
            this.userDataStore = userDataStore;
        }

        [Route("agents")]
        [HttpGet]
        public IActionResult GetAgents()
        {
            List<AgentUser> agents = agentDataStore.GetAll();

            if (agents != null)
                return Ok(agents);
            else
                return NotFound();
        }

        [Route("agents")]
        [HttpPost]
        public IActionResult SaveAgent(AgentUser agentUser)
        {
            try
            {
                if (agentUser.Id == 0)
                {
                    if (!userDataStore.UserExists(agentUser.EmailAddress))
                    {
                        AgentUser agentUserResponse = agentDataStore.Save(agentUser);

                        if (agentUser != null)
                            return Ok(agentUser);
                    }
                    else
                    {
                        return BadRequest($"Agent with email address {agentUser.EmailAddress} already exists.");
                    }
                }
                else
                {
                    AgentUser agentUserResponse = agentDataStore.Save(agentUser);

                    if (agentUser != null)
                        return Ok(agentUser);

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();

        }

    }
}
