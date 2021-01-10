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
    public class BanksApiController : ControllerBase
    {
        private readonly IBankDataStore bankDataStore;
        public BanksApiController(IBankDataStore bankDataStore)
        {
            this.bankDataStore = bankDataStore;
        }

        [Route("banks/lien-holders")]
        [HttpGet]
        public IActionResult GetVehicles()
        {
            List<Bank> banks = bankDataStore.GetLienHolders();

            return Ok(banks);
        }
        [Route("banks/premium-financers")]
        [HttpGet]
        public IActionResult GetPremiumFinancers()
        {
            List<Bank> banks = bankDataStore.GetPremiumFinancers();

            return Ok(banks);
        }





    }
}
