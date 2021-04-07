using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ACIC.AMS.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    public class CommissionsController : Controller
    {
        [Route("AgentCommissions")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
