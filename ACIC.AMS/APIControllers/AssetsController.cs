using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ACIC.AMS.Web.APIControllers
{
    [Route("scripts")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        public AssetsController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [Route("js/{viewName}/{fileName}")]
        [HttpGet]
        public IActionResult GetJs(string viewName, string fileName)
        {
            try
            {
                var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "Views", viewName, "js", fileName);

                var stream = System.IO.File.OpenRead(path);

                return new FileStreamResult(stream, "application/octet-stream");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("models/{viewName}/{fileName}")]
        [HttpGet]
        public IActionResult GetModel(string viewName, string fileName)
        {
            try
            {
                var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "Views", viewName, "js", "models", fileName);

                var stream = System.IO.File.OpenRead(path);

                return new FileStreamResult(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("services/{viewName}/{fileName}")]
        [HttpGet]
        public IActionResult GetService(string viewName, string fileName)
        {
            try
            {
                var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "Views", viewName, "js", "services", fileName);

                var stream = System.IO.File.OpenRead(path);

                return new FileStreamResult(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("components/{viewName}/{fileName}")]
        [HttpGet]
        public IActionResult GetComponent(string viewName, string fileName)
        {
            try
            {
                var path = Path.Combine(
                         Directory.GetCurrentDirectory(), "Views", viewName, "js", "components", fileName);

                var stream = System.IO.File.OpenRead(path);

                return new FileStreamResult(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("css/{viewName}/{fileName}")]
        [HttpGet]
        public IActionResult GetCss(string viewName, string fileName)
        {
            var path = Path.Combine(
                     Directory.GetCurrentDirectory(), "Views", viewName, "css", fileName);

            var stream = System.IO.File.OpenRead(path);

            return new FileStreamResult(stream, "application/octet-stream");
        }

        [Route("modals")]
        [HttpGet]
        public IActionResult HelloWorld()
        {
            var path = Path.Combine(
                       Directory.GetCurrentDirectory(), "Views", "Accounts", "Details", "_DetailsForm.cshtml");

            var stream = System.IO.File.OpenRead(path);

            return Ok(stream);
        }

    }
}
