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
    public class MgaApiController : ControllerBase
    {
        private readonly IMgaDataStore mgaDataStore;
        public MgaApiController(IMgaDataStore mgaDataStore)
        {
            this.mgaDataStore = mgaDataStore;
        }

        [Route("mga")]
        [HttpGet]
        public IActionResult GetMgas()
        {
            List<Mga> mgas = mgaDataStore.GetMgas();

            return Ok(mgas);
        }

      



    }
}
