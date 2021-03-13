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
    public class VehiclesApiController : ControllerBase
    {
        private readonly IVehicleDataStore vehicleDataStore;
        public VehiclesApiController(IVehicleDataStore vehicleDataStore)
        {
            this.vehicleDataStore = vehicleDataStore;
        }


        [Route("accounts/{id}/vehicles")]
        [HttpPost]
        public IActionResult SaveVehicle([FromBody] VehicleEndorsement vehicleEndorsement)
        {
            try
            {
                VehicleEndorsement vehicleEndorsementResponse = vehicleDataStore.Save(vehicleEndorsement);

                if (vehicleEndorsementResponse != null)
                    return Ok(vehicleEndorsementResponse);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("vehicles/makes")]
        [HttpGet]
        public IActionResult GetVehicleMakes()
        {
            List<VehicleMake> vehicleMakes = vehicleDataStore.GetVehicleMakes();

            return Ok(vehicleMakes);
        }

        [Route("vehicles/{id}/history")]
        [HttpGet]
        public IActionResult GetVehicleHistory(int id)
        {
            List<VehicleHistory> vehicleHistory = vehicleDataStore.GetVehicleHistory(id);

            return Ok(vehicleHistory);
        }

        [Route("vehicles/{id}")]
        [HttpPatch]
        public IActionResult UpdateVehicle([FromBody] Vehicle vehicle)
        {
            try
            {
                Vehicle vehicleResponse  = vehicleDataStore.UpdateVehicle(vehicle);

                if (vehicleResponse != null)
                    return Ok(vehicleResponse);

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vehicles/{id}")]
        [HttpDelete]
        public IActionResult DeleteDriver(int id)
        {
            SPRowCountResult deleteDriverResult = vehicleDataStore.DeleteVehicle(id);

            return Ok(deleteDriverResult);
        }





    }
}
