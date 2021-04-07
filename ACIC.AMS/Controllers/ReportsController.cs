using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ACIC.AMS.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ReportsController : Controller
    {
        [Route("Endorsements")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Commissions")]
        public IActionResult Commissions()
        {
            return View();
        }

        [Route("AgencyReports")]
        public IActionResult AgencyReports()
        {
            return View();
        }
    }
}
