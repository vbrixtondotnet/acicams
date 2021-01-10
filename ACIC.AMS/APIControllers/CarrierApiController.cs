﻿using System;
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
    public class CarrierApiController : ControllerBase
    {
        private readonly ICarrierDataStore carrierDataStore;
        public CarrierApiController(ICarrierDataStore carrierDataStore)
        {
            this.carrierDataStore = carrierDataStore;
        }

        [Route("carriers")]
        [HttpGet]
        public IActionResult GetCarriers()
        {
            List<Carrier> carriers = carrierDataStore.GetCarriers();

            return Ok(carriers);
        }

      



    }
}
