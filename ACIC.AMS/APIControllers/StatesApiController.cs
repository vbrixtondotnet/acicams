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
    public class StatesApiController : ControllerBase
    {
        private readonly IStateDataStore addressDataStore;
        public StatesApiController(IStateDataStore addressDataStore)
        {
            this.addressDataStore = addressDataStore;
        }

        [Route("states/{stateid}")]
        [HttpGet]
        public IActionResult GetCities(string stateid)
        {
            List<DdUsState> states = addressDataStore.GetCities(stateid);

            if (states != null)
                return Ok(states);
            else
                return NotFound();
        }

    }
}
