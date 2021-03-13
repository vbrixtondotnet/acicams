using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ClosedXML.Excel;
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
        private readonly IEndorsementDataStore endorsementDataStore;
        private readonly IDriverDataStore driverDataStore;
        private readonly IVehicleDataStore vehicleDataStore;
        public AccountsApiController(IAccountDataStore accountDataStore, IContactDataStore contactDataStore, IPolicyDataStore policyDataStore, IEndorsementDataStore endorsementDataStore, IDriverDataStore driverDataStore, IVehicleDataStore vehicleDataStore)
        {
            this.accountDataStore = accountDataStore;
            this.contactDataStore = contactDataStore;
            this.policyDataStore = policyDataStore;
            this.endorsementDataStore = endorsementDataStore;
            this.driverDataStore = driverDataStore;
            this.vehicleDataStore = vehicleDataStore;
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

        [Route("accounts/{id}/coveragetypes")]
        [HttpGet]
        public IActionResult GetAvailableCoverageTypes(int id)
        {
            List<CoverageType> coverageTypes = policyDataStore.GetAvailableCoverageTypes(id);
            return Ok(coverageTypes);

        }

        [Route("accounts/{id}/endorsements/{type}")]
        [HttpGet]
        public IActionResult GetEndorsements(int id, string type, string searchText = "")
        {
            List<EndorsementList> endorsementLists = endorsementDataStore.GetEndorsements(id, type, searchText);
            return Ok(endorsementLists);
        }



        [Route("accounts/{id}/policy")]
        [HttpGet]
        public IActionResult GetPolicies(int id)
        {
            List<Policy> policies = policyDataStore.GetPolicies(id);
            return Ok(policies);

        }

        [Route("accounts/{id}/drivers")]
        [HttpGet]
        public IActionResult GetDriversByAccount(int id)
        {
            List<Driver> drivers = driverDataStore.GetDriversByAccount(id);

            return Ok(drivers);
        }

        [Route("accounts/{id}/vehicles")]
        [HttpGet]
        public IActionResult GetVehicles(int id)
        {
            List<Vehicle> vehicles = vehicleDataStore.GetVehicles(id);

            return Ok(vehicles);
        }



        [Route("accounts/{id}/endorsements/download")]
        [HttpGet]
        public IActionResult DownloadExcelDocument(int id)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"endorsements-{id}.xlsx";
            var endorsements = endorsementDataStore.GetEndorsements(id, "all");
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Endorsements");
                    worksheet.Cell(1, 1).Value = "Effective";
                    worksheet.Cell(1, 2).Value = "Description";
                    worksheet.Cell(1, 3).Value = "Action";
                    worksheet.Cell(1, 4).Value = "Year";
                    worksheet.Cell(1, 5).Value = "Make";
                    worksheet.Cell(1, 6).Value = "VIN";
                    worksheet.Cell(1, 7).Value = "PD Value";
                    worksheet.Cell(1, 8).Value = "Surcharge";
                    worksheet.Cell(1, 9).Value = "AL";
                    worksheet.Cell(1, 10).Value = "MTC";
                    worksheet.Cell(1, 11).Value = "APD";
                    worksheet.Cell(1, 12).Value = "Broker Fees";
                    worksheet.Cell(1, 13).Value = "Endt Fees";
                    worksheet.Cell(1, 14).Value = "Total Amount";
                    worksheet.Cell(1, 15).Value = "Status";
                    worksheet.Cell(1, 16).Value = "Variance";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 2).Style.Font.Bold = true;
                    worksheet.Cell(1, 3).Style.Font.Bold = true;
                    worksheet.Cell(1, 4).Style.Font.Bold = true;
                    worksheet.Cell(1, 5).Style.Font.Bold = true;
                    worksheet.Cell(1, 6).Style.Font.Bold = true;
                    worksheet.Cell(1, 7).Style.Font.Bold = true;
                    worksheet.Cell(1, 8).Style.Font.Bold = true;
                    worksheet.Cell(1, 9).Style.Font.Bold = true;
                    worksheet.Cell(1, 10).Style.Font.Bold = true;
                    worksheet.Cell(1, 11).Style.Font.Bold = true;
                    worksheet.Cell(1, 12).Style.Font.Bold = true;
                    worksheet.Cell(1, 13).Style.Font.Bold = true;
                    worksheet.Cell(1, 14).Style.Font.Bold = true;
                    worksheet.Cell(1, 15).Style.Font.Bold = true;
                    worksheet.Cell(1, 16).Style.Font.Bold = true;
                    for (int index = 1; index <= endorsements.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = endorsements[index - 1].Effective;
                        worksheet.Cell(index + 1, 2).Value = endorsements[index - 1].Description;
                        worksheet.Cell(index + 1, 3).Value = endorsements[index - 1].Action;
                        worksheet.Cell(index + 1, 4).Value = endorsements[index - 1].Year;
                        worksheet.Cell(index + 1, 5).Value = endorsements[index - 1].Make;
                        worksheet.Cell(index + 1, 6).Value = endorsements[index - 1].Vin;
                        worksheet.Cell(index + 1, 7).Value = endorsements[index - 1].Pdvalue;
                        worksheet.Cell(index + 1, 8).Value = endorsements[index - 1].Surcharge;
                        worksheet.Cell(index + 1, 9).Value = endorsements[index - 1].AL;
                        worksheet.Cell(index + 1, 10).Value = endorsements[index - 1].MTC;
                        worksheet.Cell(index + 1, 11).Value = endorsements[index - 1].APD;
                        worksheet.Cell(index + 1, 12).Value = endorsements[index - 1].BrokerFees;
                        worksheet.Cell(index + 1, 13).Value = endorsements[index - 1].EndtFees;
                        worksheet.Cell(index + 1, 14).Value = endorsements[index - 1].TotalAmount;
                        worksheet.Cell(index + 1, 15).Value = endorsements[index - 1].Status;
                        worksheet.Cell(index + 1, 16).Value = endorsements[index - 1].Variance;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
