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
    public class ReportsApiController : ControllerBase
    {
        private readonly IEndorsementDataStore endorsementDataStore;
        private readonly IAccountDataStore accountDataStore;
        private readonly IPolicyDataStore policyDataStore;
        public ReportsApiController(IEndorsementDataStore endorsementDataStore, IAccountDataStore accountDataStore, IPolicyDataStore policyDataStore)
        {
            this.endorsementDataStore = endorsementDataStore;
            this.accountDataStore = accountDataStore;
            this.policyDataStore = policyDataStore;
        }

        [Route("reports/endorsements/received")]
        [HttpGet]
        public IActionResult GetReceivedEndorsements()
        {
            try
            {
                var response = endorsementDataStore.GetReceivedEndorsementReport();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("reports/endorsements/pending")]
        [HttpGet]
        public IActionResult GetPendingEndorsements()
        {
            try
            {
                var response = endorsementDataStore.GetPendingEndorsementReports();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("reports/endorsements/payable")]
        [HttpGet]
        public IActionResult GetPayableEndorsements()
        {
            try
            {
                var response = endorsementDataStore.GetPayableEndorsementReports();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("reports/endorsements/received/download")]
        [HttpGet]
        public IActionResult DownloadExcelDocument()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"endorsements-received-{DateTime.Now.ToShortDateString()}.xlsx";
            var endorsements = endorsementDataStore.GetReceivedEndorsementReport();
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Endorsements-Received");
                    worksheet.Cell(1, 1).Value = "Account ID";
                    worksheet.Cell(1, 2).Value = "Carrier Name";
                    worksheet.Cell(1, 3).Value = "Endorsement No";
                    worksheet.Cell(1, 4).Value = "Premium";
                    worksheet.Cell(1, 5).Value = "SL Taxes";
                    worksheet.Cell(1, 6).Value = "Fees";
                    worksheet.Cell(1, 7).Value = "Commission";
                    worksheet.Cell(1, 8).Value = "Payable Amount";
                    worksheet.Cell(1, 9).Value = "Down Payment";
                    worksheet.Cell(1, 10).Value = "Payment Status";
                    worksheet.Cell(1, 11).Value = "Finance Reference";
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

                    for (int index = 1; index <= endorsements.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = endorsements[index - 1].AccountId;
                        worksheet.Cell(index + 1, 2).Value = endorsements[index - 1].CarrierName;
                        worksheet.Cell(index + 1, 3).Value = endorsements[index - 1].Ern;
                        worksheet.Cell(index + 1, 4).Value = endorsements[index - 1].Premium;
                        worksheet.Cell(index + 1, 5).Value = endorsements[index - 1].SLTaxes;
                        worksheet.Cell(index + 1, 6).Value = endorsements[index - 1].Fees;
                        worksheet.Cell(index + 1, 7).Value = endorsements[index - 1].Commission;
                        worksheet.Cell(index + 1, 8).Value = endorsements[index - 1].PayableAmount;
                        worksheet.Cell(index + 1, 9).Value = endorsements[index - 1].DownPayment;
                        worksheet.Cell(index + 1, 10).Value = endorsements[index - 1].PaymentStatus;
                        worksheet.Cell(index + 1, 11).Value = endorsements[index - 1].FinanceRef;
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

        [Route("reports/endorsements/pending/download")]
        [HttpGet]
        public IActionResult DownloadPendingReport()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"endorsements-pending-{DateTime.Now.ToShortDateString()}.xlsx";
            var endorsements = endorsementDataStore.GetPendingEndorsementReports();
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Endorsements-Pending");
                    worksheet.Cell(1, 1).Value = "Date Effective";
                    worksheet.Cell(1, 2).Value = "Action";
                    worksheet.Cell(1, 3).Value = "Description";
                    worksheet.Cell(1, 4).Value = "Type";
                    worksheet.Cell(1, 5).Value = "Insured Item";
                    worksheet.Cell(1, 6).Value = "Type Of Coverage";
                    worksheet.Cell(1, 7).Value = "Amount";
                    worksheet.Cell(1, 8).Value = "Account";
                    worksheet.Cell(1, 9).Value = "Carrier";
                    worksheet.Cell(1, 10).Value = "Status";
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

                    for (int index = 1; index <= endorsements.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = endorsements[index - 1].DateEffectiveText;
                        worksheet.Cell(index + 1, 2).Value = endorsements[index - 1].Action;
                        worksheet.Cell(index + 1, 3).Value = endorsements[index - 1].Description;
                        worksheet.Cell(index + 1, 4).Value = endorsements[index - 1].Type;
                        worksheet.Cell(index + 1, 5).Value = endorsements[index - 1].InsuredItem;
                        worksheet.Cell(index + 1, 6).Value = endorsements[index - 1].TypeOfCoverage;
                        worksheet.Cell(index + 1, 7).Value = endorsements[index - 1].TotalAmount;
                        worksheet.Cell(index + 1, 8).Value = endorsements[index - 1].Account;
                        worksheet.Cell(index + 1, 9).Value = endorsements[index - 1].CarrierName;
                        worksheet.Cell(index + 1, 10).Value = endorsements[index - 1].Status;
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

        [Route("reports/endorsements/payable/download")]
        [HttpGet]
        public IActionResult DownloadPayableReport()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"endorsements-payable-{DateTime.Now.ToShortDateString()}.xlsx";
            var endorsements = endorsementDataStore.GetPayableEndorsementReports();
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Endorsements-Payable");
                    worksheet.Cell(1, 1).Value = "Account";
                    worksheet.Cell(1, 2).Value = "Carrier / MGA";
                    worksheet.Cell(1, 3).Value = "Endt No";
                    worksheet.Cell(1, 4).Value = "Premium";
                    worksheet.Cell(1, 5).Value = "SL Taxes";
                    worksheet.Cell(1, 6).Value = "Fees";
                    worksheet.Cell(1, 7).Value = "Commission";
                    worksheet.Cell(1, 8).Value = "Payable Amount";
                    worksheet.Cell(1, 9).Value = "Due Date";
                    worksheet.Cell(1, 10).Value = "Remarks";
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

                    for (int index = 1; index <= endorsements.Count; index++)
                    {
                        var remarks = endorsements[index - 1].DueInDays == null ? "No Due Date Assigned" : $"Due In {endorsements[index - 1].DueInDays} days";

                        worksheet.Cell(index + 1, 1).Value = endorsements[index - 1].LegalName;
                        worksheet.Cell(index + 1, 2).Value = endorsements[index - 1].CarrierName + " / " + endorsements[index - 1].Mganame;
                        worksheet.Cell(index + 1, 3).Value = endorsements[index - 1].Ern;
                        worksheet.Cell(index + 1, 4).Value = endorsements[index - 1].Premium;
                        worksheet.Cell(index + 1, 5).Value = endorsements[index - 1].SLTaxes;
                        worksheet.Cell(index + 1, 6).Value = endorsements[index - 1].Fees;
                        worksheet.Cell(index + 1, 7).Value = endorsements[index - 1].Commission;
                        worksheet.Cell(index + 1, 8).Value = endorsements[index - 1].PayableAmount;
                        worksheet.Cell(index + 1, 9).Value = endorsements[index - 1].DueDate;
                        worksheet.Cell(index + 1, 10).Value = remarks;
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

        [Route("reports/endorsements/endorsement-report-items/download")]
        [HttpGet]
        public IActionResult DownloadEndorsementGroupItems(int accountId, string ern, int coverageType, int policyId)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"endorsement-received-report-items-{accountId}-{ern}-{coverageType}-{policyId}-{DateTime.Now.ToShortDateString()}.xlsx";
            var accountDto = accountDataStore.Get(accountId);
            var coverageTypeDto = policyDataStore.GetCoverageType(coverageType);
            var endorsements = endorsementDataStore.GetEndorsementReportEndtItems(accountId, ern, coverageType, policyId);
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Endorsements-Received-Items");
                    worksheet.Cell(1, 1).Value = "Account";
                    worksheet.Cell(1, 2).Value = "Endt No";
                    worksheet.Cell(1, 3).Value = "Coverage Type";
                    worksheet.Cell(1, 4).Value = "Premium";
                    worksheet.Cell(1, 5).Value = "Surcharge";
                    worksheet.Cell(1, 6).Value = "Surplus Tax";
                    worksheet.Cell(1, 7).Value = "Endt Fees";
                    worksheet.Cell(1, 8).Value = "Non Taxed Fees";
                    worksheet.Cell(1, 9).Value = "Other Fees";
                    worksheet.Cell(1, 10).Value = "Commissions";
                    worksheet.Cell(1, 11).Value = "Net Amount";
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

                    for (int index = 1; index <= endorsements.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = accountDto.LegalName;
                        worksheet.Cell(index + 1, 2).Value = ern;
                        worksheet.Cell(index + 1, 3).Value = coverageTypeDto.CoverageTypes;
                        worksheet.Cell(index + 1, 4).Value = endorsements[index - 1].Premium;
                        worksheet.Cell(index + 1, 5).Value = endorsements[index - 1].Surcharge;
                        worksheet.Cell(index + 1, 6).Value = endorsements[index - 1].SurplusTax;
                        worksheet.Cell(index + 1, 7).Value = endorsements[index - 1].EndtFee;
                        worksheet.Cell(index + 1, 8).Value = endorsements[index - 1].NonTaxedRateUnit;
                        worksheet.Cell(index + 1, 9).Value = endorsements[index - 1].OtherFees;
                        worksheet.Cell(index + 1, 10).Value = endorsements[index - 1].Commission;
                        worksheet.Cell(index + 1, 11).Value = endorsements[index - 1].TotalAmount;
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
