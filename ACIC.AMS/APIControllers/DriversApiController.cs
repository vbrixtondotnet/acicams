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

        [Route("drivers/{id}/history")]
        [HttpGet]
        public IActionResult GetDriverHistory(int id)
        {
            List<DriverHistory> driverHistory = driverDataStore.GetDriverHistories(id);

            return Ok(driverHistory);
        }

        [Route("drivers/{id}")]
        [HttpDelete]
        public IActionResult DeleteDriver(int id)
        {
            SPRowCountResult deleteDriverResult = driverDataStore.DeleteDriver(id);

            return Ok(deleteDriverResult);
        }

        [Route("drivers")]
        [HttpPatch]
        public IActionResult EditDriver([FromBody] Driver driverDto)
        {
            try
            {
                Driver driverResult = driverDataStore.Update(driverDto);

                return Ok(driverResult);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Route("accounts/{id}/drivers")]
        [HttpPost]
        public IActionResult SaveNewDriver([FromBody] DriverEndorsement driverEndorsement)
        {
            try
            {
                if(!driverDataStore.DriverExists(driverEndorsement.FirstName, driverEndorsement.LastName, driverEndorsement.Cdlnumber, driverEndorsement.State))
                {
                    DriverEndorsement driverEndorsementResponse = driverDataStore.Save(driverEndorsement);

                    if (driverEndorsementResponse != null)
                        return Ok(driverEndorsementResponse);
                }
                else
                {
                    return BadRequest($"The Driver {driverEndorsement.FirstName} {driverEndorsement.LastName} with license number {driverEndorsement.Cdlnumber}, State: {driverEndorsement.State} already exist!");
                }

                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
