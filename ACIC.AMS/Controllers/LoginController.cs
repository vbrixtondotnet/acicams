using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ACIC.AMS.Controllers
{
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }



    }
}
