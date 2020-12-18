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
    public class AccountsApiController : ControllerBase
    {
        private readonly IAccountDataStore accountDataStore;
        private readonly IContactDataStore contactDataStore; 
        private readonly IPolicyDataStore policyDataStore;
        public AccountsApiController(IAccountDataStore accountDataStore, IContactDataStore contactDataStore, IPolicyDataStore policyDataStore)
        {
            this.accountDataStore = accountDataStore;
            this.contactDataStore = contactDataStore;
            this.policyDataStore = policyDataStore;
        }

        [Route("accounts/{id}/details")]
        [HttpGet]
        public IActionResult GetAccountDetails(int id)
        {
            AccountDetails account = accountDataStore.GetAccountDetails(id);

            if (account != null)
                return Ok(account);
            else
                return NotFound();
        }

        [Route("accounts/{id}")]
        [HttpGet]
        public IActionResult GetAccount(int id)
        {
            Account account = accountDataStore.Get(id);

            if (account != null)
                return Ok(account);
            else
                return NotFound();
        }

        [Route("accounts")]
        [HttpGet]
        public IActionResult GetAccounts(bool expiring = false)
        {
            List<Account> accounts = null;

            if (expiring)
                accounts = accountDataStore.GetExpiringAccounts();
            else
                accounts = accountDataStore.GetAll();

            if (accounts != null)
                return Ok(accounts);
            else
                return NotFound();
        }

        [Route("accounts/{id}/contacts")]
        [HttpGet]
        public IActionResult GetContacts(int id)
        {
            List<Contact> contacts = contactDataStore.GetByAccountId(id);
            return Ok(contacts);

        }

        [Route("accounts/{id}/coveragetypes")]
        [HttpGet]
        public IActionResult GetAvailableCoverageTypes(int id)
        {
            List<CoverageType> coverageTypes = policyDataStore.GetAvailableCoverageTypes(id);
            return Ok(coverageTypes);

        }

        [Route("accounts")]
        [HttpPost]
        public IActionResult SaveAccount([FromBody] Dto.Account account)
        {
            try
            {
                AccountDetails accountDetails = accountDataStore.Save(account);
                return Ok(accountDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("accounts/{id}/contacts")]
        [HttpPost]
        public IActionResult GetContacts([FromBody] Dto.Contact contact)
        {
            try
            {
                Contact contactResult = contactDataStore.Save(contact);
                return Ok(contactResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
