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
    public class DriversApiController : ControllerBase
    {
        private readonly IDriverDataStore driverDataStore;
        public DriversApiController(IDriverDataStore driverDataStore)
        {
            this.driverDataStore = driverDataStore;
        }

        [Route("accounts/{id}/drivers")]
        [HttpGet]
        public IActionResult GetDrivers(int id)
        {
            List<Driver> drivers = driverDataStore.GetDrivers(id);

            return Ok(drivers);
        }

    }
}
