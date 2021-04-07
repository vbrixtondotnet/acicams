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
    public class ContactsApiController : ControllerBase
    {
        private readonly IContactDataStore contactDataStore;
        public ContactsApiController(IContactDataStore contactDataStore)
        {
            this.contactDataStore = contactDataStore;
        }

        [Route("contacts/titles")]
        [HttpGet]
        public IActionResult GetContactTitles(int id)
        {
            List<ContactsTitle> titles = contactDataStore.GetContactTitles();

            return Ok(titles);
        }

        [Route("contacts")]
        [HttpPost]
        public IActionResult Save([FromBody] Contact contact)
        {
            try
            {
                Contact contactResponse = contactDataStore.Save(contact);
                return Ok(contactResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [Route("contacts/{id}/notes")]
        [HttpPatch]
        public IActionResult UpdateNote([FromBody] Contact contact)
        {
            try
            {
                return Ok(contactDataStore.UpdateContactNotes(contact.ContactId, contact.Notes));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
