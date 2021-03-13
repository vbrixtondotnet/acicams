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
using ACIC.AMS.Web.Models;
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
    public class EndorsementApiController : ControllerBase
    {
        private readonly IEndorsementDataStore endorsementDataStore;
        private readonly IAccountDataStore accountDataStore;
        private readonly IPolicyDataStore policyDataStore;
        public EndorsementApiController(IEndorsementDataStore endorsementDataStore, IAccountDataStore accountDataStore, IPolicyDataStore policyDataStore)
        {
            this.endorsementDataStore = endorsementDataStore;
            this.accountDataStore = accountDataStore;
            this.policyDataStore = policyDataStore;
        }

        [Route("accounts/{id}/endorsement")]
        [HttpPost]
        public IActionResult CreateEndorsement([FromBody] List<Endorsement> endorsements)
        {
            try
            {
                endorsementDataStore.SaveEndorsement(endorsements);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("accounts/{id}/endorsements/{type}/available-vehicles/{endorsementAction}")]
        [HttpGet]
        public IActionResult GetAvailableEndorsementVehicles(int id, string type, string endorsementAction)
        {
            List<AvailableEndorsementVehicle> endorsementLists = endorsementDataStore.GetAvailableEndorsementVehicles(endorsementAction, type, id);
            return Ok(endorsementLists);
        }

        [Route("endorsements/update-downpayment")]
        [HttpPatch]
        public IActionResult UpdateDownPayment([FromBody] EndorsementUpdatePaymentStatusModel endorsementUpdatePaymentStatusModel)
        {
            try
            {
                endorsementDataStore.UpdateDownPayment(endorsementUpdatePaymentStatusModel.AccountId, endorsementUpdatePaymentStatusModel.Ern, endorsementUpdatePaymentStatusModel.CoverageType, endorsementUpdatePaymentStatusModel.PolicyId, endorsementUpdatePaymentStatusModel.Status);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("endorsements/update-payment-status")]
        [HttpPatch]
        public IActionResult UpdatePaymentStatus([FromBody] EndorsementUpdatePaymentStatusModel endorsementUpdatePaymentStatusModel)
        {
            try
            {
                endorsementDataStore.UpdatePaymentStatus(endorsementUpdatePaymentStatusModel.AccountId, endorsementUpdatePaymentStatusModel.Ern, endorsementUpdatePaymentStatusModel.CoverageType, endorsementUpdatePaymentStatusModel.PolicyId, endorsementUpdatePaymentStatusModel.Status);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("endorsements/update-due-date")]
        [HttpPatch]
        public IActionResult UpdateDueDate([FromBody] EndorsementUpdateDueDateModel endorsementUpdateDueDateModel)
        {
            try
            {
                endorsementDataStore.UpdateDueDate(endorsementUpdateDueDateModel.AccountId, endorsementUpdateDueDateModel.Ern, endorsementUpdateDueDateModel.CoverageType, endorsementUpdateDueDateModel.PolicyId, endorsementUpdateDueDateModel.DueDate);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("endorsements/mark-as-paid")]
        [HttpPatch]
        public IActionResult MarkAsPaid([FromBody] EndorsementUpdateDueDateModel endorsementUpdateDueDateModel)
        {
            try
            {
                endorsementDataStore.MarkAsPaid(endorsementUpdateDueDateModel.AccountId, endorsementUpdateDueDateModel.Ern, endorsementUpdateDueDateModel.CoverageType, endorsementUpdateDueDateModel.PolicyId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("endorsements/update-finance-reference")]
        [HttpPatch]
        public IActionResult UpdateFinanceReference([FromBody] EndorsementUpdateFinanceReferenceModel endorsementUpdateFinanceReferenceModel)
        {
            try
            {
                endorsementDataStore.UpdateFinancingReference(endorsementUpdateFinanceReferenceModel.AccountId, endorsementUpdateFinanceReferenceModel.Ern, endorsementUpdateFinanceReferenceModel.CoverageType, endorsementUpdateFinanceReferenceModel.PolicyId, endorsementUpdateFinanceReferenceModel.FinancingReference);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("endorsements/update-amount")]
        [HttpPatch]
        public IActionResult UpdateAmount([FromBody] EndorsementUpdateAmount endorsementUpdateAmount)
        {
            try
            {
                endorsementDataStore.UpdateEndorsementAmount(endorsementUpdateAmount.EndtId, endorsementUpdateAmount.Field, endorsementUpdateAmount.Amount);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("endorsements/endorsement-report-items")]
        [HttpGet]
        public IActionResult GetEndorsementReportItems(int accountId, string ern, int coverageType, int policyId)
        {
            try
            {
                var accountDto = accountDataStore.Get(accountId);
                var coverageTypeDto = policyDataStore.GetCoverageType(coverageType);
                var endorsementReportItems = endorsementDataStore.GetEndorsementReportEndtItems(accountId, ern, coverageType, policyId);

                var endorsementReportEndtItemsGroup = new EndorsementReportEndtItemsGroup();
                endorsementReportEndtItemsGroup.Account = accountDto.LegalName;
                endorsementReportEndtItemsGroup.EndtNo = ern;
                endorsementReportEndtItemsGroup.CoverageType = coverageTypeDto.CoverageTypes;
                endorsementReportEndtItemsGroup.EndorsementReportEndtItems = endorsementReportItems;


                //endorsementDataStore.Get(endorsementUpdateFinanceReferenceModel.AccountId, endorsementUpdateFinanceReferenceModel.Ern, endorsementUpdateFinanceReferenceModel.CoverageType, endorsementUpdateFinanceReferenceModel.PolicyId, endorsementUpdateFinanceReferenceModel.FinancingReference);
                return Ok(endorsementReportEndtItemsGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
