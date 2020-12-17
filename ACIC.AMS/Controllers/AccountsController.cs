using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACIC.AMS.DataStore;
using ACIC.AMS.DataStore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACIC.AMS.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
