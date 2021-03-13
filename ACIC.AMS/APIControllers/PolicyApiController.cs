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
    public class PolicyApiController : ControllerBase
    {
        private readonly IPolicyDataStore policyDataStore;
        public PolicyApiController(IPolicyDataStore policyDataStore)
        {
            this.policyDataStore = policyDataStore;
        }

       
        [Route("accounts/{id}/policy")]
        [HttpPost]
        public IActionResult SavePolicy([FromBody] Policy policy)
        {
            try
            {
                var retval = policyDataStore.SavePolicy(policy);
                return Ok(retval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("accounts/{id}/policy/{policyId}/policy-endorsement-unit-stats")]
        [HttpGet]
        public IActionResult GetPolicyEndorsementUnitStats(int id, int policyId)
        {
            try
            {
                var retval = policyDataStore.GetPolicyEndorsementUnitStats(id, policyId);
                return Ok(retval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("policy/{id}/active-vehicles")]
        [HttpGet]
        public IActionResult GetActivePolicyVehicles(int id)
        {
            try
            {
                var retval = policyDataStore.GetActivePolicyVehicles(id);
                return Ok(retval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("policy/{id}/agent-comissions")]
        [HttpGet]
        public IActionResult GetPolicyAgentCommissions(int id)
        {
            try
            {
                var retval = policyDataStore.GetPolicyAgentCommissions(id);
                return Ok(retval);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("policy/{id}/inception/{inception}")]
        [HttpPost]
        public IActionResult SetInceptionStage(int id, bool inception)
        {
            try
            {
                policyDataStore.SetInceptionStage(id, inception);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
