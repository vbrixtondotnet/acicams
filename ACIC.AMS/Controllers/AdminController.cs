using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ACIC.AMS.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Page = "Admin";
            return View();
        }
        

    }
}
