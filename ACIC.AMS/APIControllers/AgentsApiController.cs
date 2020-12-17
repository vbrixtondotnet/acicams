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
        public AgentsApiController(IAgentDataStore agentDataStore)
        {
            this.agentDataStore = agentDataStore;
        }

        [Route("agents")]
        [HttpGet]
        public IActionResult GetAgents()
        {
            List<Agent> agents = agentDataStore.GetAll();

            if (agents != null)
                return Ok(agents);
            else
                return NotFound();
        }

    }
}
