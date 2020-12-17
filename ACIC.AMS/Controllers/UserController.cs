using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ACIC.AMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDataStore userDataStore;
        public UserController(IUserDataStore userDataStore)
        {
            this.userDataStore = userDataStore;
        }
        public IActionResult Profile(int id)
        {
            if (id != 0)
            {
                var user = userDataStore.GetUserById(id);

                return View(user);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Profile([FromForm] Dto.User userModel)
        {
            User user = userDataStore.SaveUser(userModel);
            return View(user);
        }
    }
}
