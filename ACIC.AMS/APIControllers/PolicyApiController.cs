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
        [HttpGet]
        public IActionResult GetPolicies(int id)
        {
            List<Policy> policies = policyDataStore.GetPolicies(id);
            return Ok(policies);

        }

        [Route("accounts/{id}/coveragetypes")]
        [HttpGet]
        public IActionResult GetAvailableCoverageTypes(int id)
        {
            List<CoverageType> coverageTypes = policyDataStore.GetAvailableCoverageTypes(id);
            return Ok(coverageTypes);

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

    }
}
