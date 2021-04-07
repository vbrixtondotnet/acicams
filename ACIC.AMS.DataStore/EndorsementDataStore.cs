using ACIC.AMS.DataStore.Interfaces;
using ACIC.AMS.Dto;
using ACIC.AMS.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ACIC.AMS.DataStore
{
    public class EndorsementDataStore : BaseDataStore, IEndorsementDataStore
    {
        public EndorsementDataStore(ACICDBContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public List<AvailableEndorsementVehicle> GetAvailableEndorsementVehicles(string action, string type, int accountId)
        {
            var retval = new List<AvailableEndorsementVehicle>();
            var parameters = new Dictionary<string, object>();
            parameters.Add("@Type", $"'{type}'");
            parameters.Add("@AccountId", accountId);

            retval = action == "ADD" ? this.ExecuteQuery<AvailableEndorsementVehicle>("[dbo].[GetAvailableVehiclesForNewEndorment]", parameters) : this.ExecuteQuery<AvailableEndorsementVehicle>("[dbo].[GetDeletedVehiclesForNewEndorment]", parameters);

            return retval;
        }

        public List<EndorsementList> GetEndorsements(int accountId, string type, string search = "")
        {
            var retval = new List<EndorsementList>();
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AccountId", accountId);
            parameters.Add("@Type", $"'{type}'");

            var endorsementList = this.ExecuteQuery<EndorsementList>("[dbo].[GetEndorsements]", parameters);
            if (string.IsNullOrEmpty(search))
                retval = endorsementList;
            else
            {
                List<EndorsementList> retvalFiltered = new List<EndorsementList>();
                foreach(var endorsement in endorsementList)
                {
                    if(endorsement.Vin != null && endorsement.Vin.ToLower().Contains(search.ToLower()) || endorsement.Description != null && endorsement.Description.ToLower().Contains(search.ToLower()))
                    {
                        retvalFiltered.Add(endorsement);
                    }
                }
                retval = retvalFiltered;
            }


            return retval;
        }

        public List<ReceivedEndorsementReport> GetReceivedEndorsementReport()
        {
            return this.ExecuteQuery<ReceivedEndorsementReport>("SELECT * FROM ReceivedEndorsementReportView ORDER BY LegalName, CoverageTypes");
        }
        public List<PayableEndorsementReport> GetPayableEndorsementReports()
        {
            List<PayableEndorsementReport> retval = new List<PayableEndorsementReport>();

            var noDueData = this.ExecuteQuery<PayableEndorsementReport>("SELECT * FROM PayableEndorsementReportView where PayableAmount > 0 and PaymentStatus = 0 and DueInDays IS NULL ORDER BY DueInDays, LegalName, CoverageTypes");
            var withPastDue = this.ExecuteQuery<PayableEndorsementReport>("SELECT * FROM PayableEndorsementReportView where PayableAmount > 0 and PaymentStatus = 0 and DueInDays < 0 ORDER BY DueInDays, LegalName, CoverageTypes");
            var withDueData = this.ExecuteQuery<PayableEndorsementReport>("SELECT * FROM PayableEndorsementReportView where PayableAmount > 0 and PaymentStatus = 0 and DueInDays > 0 ORDER BY DueInDays DESC, LegalName, CoverageTypes");

            retval.AddRange(noDueData);
            retval.AddRange(withPastDue);
            retval.AddRange(withDueData);

            return retval;
        }
        public Endorsement SaveDriverEndorsement(int accountId, int driverId, List<DriverCoverage> driverCoverages, string action = "ADD", string description = "Initial Binding", string status = "Reconciled")
        {
            foreach(var coverage in driverCoverages)
            {
                // select latest policy under coverage type
                var policy = _context.Policy.Where(p => p.AccountId == accountId && p.CoverageTypes == coverage.CoverageTypeId && p.Expiration > DateTime.Now).FirstOrDefault();
                if(policy != null)
                {
                    var dbEndorsement = new Domain.Models.Endorsement
                    {
                        AccountId = accountId,
                        PolicyId = policy.PolicyId,
                        Action = action,
                        Type = "Driver",
                        Effective = policy.Effective,
                        CoverageTypes = coverage.CoverageTypeId,
                        Description = description,
                        DriverId = driverId,
                        Premium = coverage.Premium,
                        SurplusTax = coverage.PremiumTax,
                        BrokerFees = coverage.BrokerFee,
                        TotalPremium = coverage.TotalAmount,
                        Status = status,
                        DateCreated = DateTime.Now
                    };

                    _context.Endorsement.Add(dbEndorsement);
                    _context.SaveChanges();
                }
            }
            return new Endorsement();
        }

        public async Task SaveEndorsement(List<Endorsement> endorsements)
        {
            foreach (var endorsement in endorsements)
            {
                var dbEndorsement = new Domain.Models.Endorsement
                {
                    AccountId = endorsement.AccountId,
                    PolicyId = endorsement.PolicyId,
                    Action = endorsement.Action,
                    Type = endorsement.Type,
                    Effective = endorsement.Effective,
                    CoverageTypes = endorsement.CoverageTypes,
                    Description = endorsement.Description,
                    DriverId = endorsement.DriverId,
                    Premium = endorsement.Premium,
                    SurplusTax = endorsement.SurplusTax,
                    BrokerFees = endorsement.BrokerFees,
                    TotalPremium = endorsement.TotalPremium,
                    Status = endorsement.Status,
                    ProRate = endorsement.ProRate,
                    BasePerUnit = endorsement.BasePerUnit,
                    Surcharge = endorsement.Surcharge,
                    OtherFees = endorsement.OtherFees,
                    EndtFee = endorsement.EndtFee,
                    DateCreated = DateTime.Now
                };

                _context.Endorsement.Add(dbEndorsement);
            }
           await _context.SaveChangesAsync();
        }

        public Endorsement SaveVehicleEndorsement(int accountId, int vehicleId, string type, List<VehicleCoverage> vehicleCoverages, string action = "ADD", string description = "Initial Binding", string status = "Reconciled")
        {
            foreach (var coverage in vehicleCoverages)
            {
                // select latest policy under coverage type
                var policy = _context.Policy.Where(p => p.AccountId == accountId && p.CoverageTypes == coverage.CoverageTypeId && p.Expiration > DateTime.Now).FirstOrDefault();
                if (policy != null)
                {
                    var dbEndorsement = new Domain.Models.Endorsement
                    {
                        AccountId = accountId,
                        PolicyId = policy.PolicyId,
                        Action = action,
                        Type = type,
                        Effective = policy.Effective,
                        CoverageTypes = coverage.CoverageTypeId,
                        Description = description,
                        Premium = coverage.Premium,
                        SurplusTax = coverage.PremiumTax,
                        BrokerFees = coverage.BrokerFee,
                        TotalPremium = coverage.TotalAmount,
                        Status = status,
                        VehicleId = vehicleId,
                        DateCreated = DateTime.Now
                    };

                    _context.Endorsement.Add(dbEndorsement);
                    _context.SaveChanges();
                }
            }
            return new Endorsement();
        }

        public void UpdateDownPayment(int accountId, string ern, int coverageType, int policyId, int status)
        {
            var sql = $"UPDATE Endorsement SET Dpreceived = {status} where AccountId = {accountId} and Ern = '{ern}' and CoverageTypes = {coverageType} and PolicyId = {policyId}";
            this.ExecuteQuery<int>(sql).FirstOrDefault();

        }

        public void UpdateFinancingReference(int accountId, string ern, int coverageType, int policyId, string financingReference)
        {
            var sql = $"UPDATE Endorsement SET FinanceRef = '{financingReference}' where AccountId = {accountId} and Ern = '{ern}' and CoverageTypes = {coverageType} and PolicyId = {policyId}";
            this.ExecuteQuery<int>(sql).FirstOrDefault();
        }

        public void UpdatePaymentStatus(int accountId, string ern, int coverageType, int policyId, int status)
        {
            var sql = $"UPDATE Endorsement SET PaidTo = {status} where AccountId = {accountId} and Ern = '{ern}' and CoverageTypes = {coverageType} and PolicyId = {policyId}";
            this.ExecuteQuery<int>(sql).FirstOrDefault();
        }

        public void UpdateDueDate(int accountId, string ern, int coverageType, int policyId, string dueDate)
        {
            var sql = $"UPDATE Endorsement SET InvoiceRef = '{dueDate}' where AccountId = {accountId} and Ern = '{ern}' and CoverageTypes = {coverageType} and PolicyId = {policyId}";
            this.ExecuteQuery<int>(sql).FirstOrDefault();
        }

        public void MarkAsPaid(int accountId, string ern, int coverageType, int policyId)
        {
            var sql = $"UPDATE Endorsement SET PaidTo = 1 where AccountId = {accountId} and Ern = '{ern}' and CoverageTypes = {coverageType} and PolicyId = {policyId}";
            this.ExecuteQuery<int>(sql).FirstOrDefault();
        }

        public void UpdateEndorsementAmount(int id, string field, decimal value)
        {
            var sql = $"UPDATE Endorsement SET {field} = {value} where EndtId = {id}"; 
            this.ExecuteQuery<int>(sql).FirstOrDefault();
        }

        public List<EndorsementReportEndtItems> GetEndorsementReportEndtItems(int accountId, string ern, int coverageType, int policyId)
        {
            var sql = $@"SELECT *,
                                Premium + Surcharge + SurplusTax + EndtFee + NonTaxedRateUnit + OtherFees + Commission as TotalAmount
                            FROM(
                            select
                                EndtId,
                                ISNULL(RIGHT(Vin, 4),'') as Reference,
                                CAST(ISNULL(Premium, 0) as decimal(10, 2)) as Premium,
                                CAST(ISNULL(Surcharge, 0) as decimal(10, 2)) as Surcharge,
                                CAST(ISNULL(SurplusTax, 0) as decimal(10, 2)) as SurplusTax,
                                CAST(ISNULL(EndtFee, 0) as decimal(10, 2)) as EndtFee,
                                CAST(ISNULL(NonTaxedRateUnit, 0) as decimal(10, 2)) as NonTaxedRateUnit,
                                CAST(ISNULL(OtherFees, 0) as decimal(10, 2)) as OtherFees,
                                CAST(ISNULL(Commission, 0) as decimal(10, 2)) as Commission
                            from Endorsement where AccountId = {accountId} and ern = '{ern}' and CoverageTypes = {coverageType} and PolicyId = {policyId}
                            ) a";
            return this.ExecuteQuery<EndorsementReportEndtItems>(sql);
        }

        public List<PendingEndorsementReport> GetPendingEndorsementReports()
        {

            var sql = $@"select 
	                        e.*, 
	                        a.LegalName as 'Account' , 
	                        c.CarrierName,
	                        CASE WHEN e.DriverId IS NOT NULL THEN CONCAT(d.FirstName, ' ', d.Middle, ' ', d.LastName)
	                        ELSE e.Vin END AS InsuredItem,
	                        CASE e.CoverageTypes WHEN  1 THEN 'AL'
	                        WHEN 2 THEN 'MTC'
	                        WHEN 3 THEN 'PD'
	                        END AS TypeOfCoverage,
	                        CAST(ISNULL(e.Premium,0) + ISNULL(e.Surcharge,0) + ISNULL(e.PolicyFees,0) + ISNULL(e.Mgafees,0) + ISNULL(e.SurplusTax,0) + ISNULL(e.BrokerFees,0) + ISNULL(e.EndtFee,0) + ISNULL(e.OtherFees,0) + ISNULL(e.TotalPremium,0) + ISNULL(e.Commission,0) as decimal(10,2)) as TotalAmount

                        from endorsement e 
                        left join account a on e.AccountId = a.AccountId
                        left join Policy p on p.PolicyId = e.PolicyId
                        left join Carrier c on c.CarrierId = p.CarrierId
                        LEFT JOIN Driver d on d.DriverId = e.DriverId
                        where e.Status = 'Follow-up' OR e.Status = 'Sent'
                        and e.PolicyId IS NOT NULL";
            return this.ExecuteQuery<PendingEndorsementReport>(sql);
        }

        public List<UnearnedCommissionsReport> GetUnearnedCommissionsReport(DateTime asOf)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AsOf", $"'{asOf.ToShortDateString()}'");

            return this.ExecuteQuery<UnearnedCommissionsReport>("[dbo].[GetUnearnedCommissions]", parameters);
        }

        public List<UnearnedBrokerFeesReport> GetUnearnedBrokerFeesReport(DateTime asOf)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@AsOf", $"'{asOf.ToShortDateString()}'");

            return this.ExecuteQuery<UnearnedBrokerFeesReport>("[dbo].[GetUnearnedBrokerFees]", parameters);
        }

        public List<UnearnedCommissionsDetail> GetUnearnedCommissionsDetail(int policyId, int coverageType, DateTime asOf)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@PolicyId", policyId);
            parameters.Add("@CoverageType", coverageType);
            parameters.Add("@AsOf", $"'{asOf.ToShortDateString()}'");

            return this.ExecuteQuery<UnearnedCommissionsDetail>("[dbo].[GetUnearnedCommissionDetails]", parameters);
        }

        public List<UnearnedBrokerFeesDetail> GetUnearnedBrokerFeesDetail(int policyId, int coverageType, DateTime asOf)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@PolicyId", policyId);
            parameters.Add("@CoverageType", coverageType);
            parameters.Add("@AsOf", $"'{asOf.ToShortDateString()}'");

            return this.ExecuteQuery<UnearnedBrokerFeesDetail>("[dbo].[GetUnearnedBrokerFeesDetails]", parameters);
        }

        public List<AgencyReport> GetAgencyReport(DateTime? from, DateTime? to)
        {
            Dictionary<string, object> parameters = null;

            if (from != null && to != null)
            {
                parameters = new Dictionary<string, object>();
                parameters.Add("@From", $"'{((DateTime)from).ToShortDateString()}'");
                parameters.Add("@To", $"'{((DateTime)to).ToShortDateString()}'");
            }
            
            return this.ExecuteQuery<AgencyReport>("[dbo].[GetAgencyReports]", parameters);
        }

    }
}
