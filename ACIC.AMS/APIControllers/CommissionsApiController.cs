using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ACIC.AMS.Web.APIControllers
{
    [Route("api")]
    [ApiController]
    public class CommissionsApiController : ControllerBase
    {
        private readonly ICommissionsDataStore commissionsDataStore;
        public CommissionsApiController(ICommissionsDataStore commissionsDataStore)
        {
            this.commissionsDataStore = commissionsDataStore;
        }

        [Route("commissions/agent")]
        [HttpGet]
        public IActionResult GetAgentCommissions()
        {
            try
            {
                var response = commissionsDataStore.GetAgentCommissions();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
