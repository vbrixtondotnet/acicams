using ACIC.AMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ACIC.AMS.DataStore.Interfaces
{
    public interface IEndorsementDataStore
    {
        Endorsement SaveDriverEndorsement(int accountId, int driverId, List<DriverCoverage> driverCoverages, string action = "ADD", string description = "Initial Binding", string status = "Reconciled");
        Endorsement SaveVehicleEndorsement(int accountId, int vehicleId, string type, List<VehicleCoverage> vehicleCoverages, string action = "ADD", string description = "Initial Binding", string status = "Reconciled");
        List<EndorsementList> GetEndorsements(int accountId, string type, string search = null);

        Task SaveEndorsement(List<Endorsement> endorsements);
        List<AvailableEndorsementVehicle> GetAvailableEndorsementVehicles(string action, string type, int accountId);
        void UpdateDownPayment(int accountId, string ern, int coverageType, int policyId, int status);
        void UpdatePaymentStatus(int accountId, string ern, int coverageType, int policyId, int status);
        void UpdateFinancingReference(int accountId, string ern, int coverageType, int policyId, string financingReference);
        List<EndorsementReportEndtItems> GetEndorsementReportEndtItems(int accountId, string ern, int coverageType, int policyId);
        void UpdateEndorsementAmount(int id, string field, decimal value);
        List<ReceivedEndorsementReport> GetReceivedEndorsementReport();
        List<PendingEndorsementReport> GetPendingEndorsementReports();
        List<PayableEndorsementReport> GetPayableEndorsementReports();
        List<UnearnedCommissionsReport> GetUnearnedCommissionsReport(DateTime asOf);
        List<UnearnedBrokerFeesReport> GetUnearnedBrokerFeesReport(DateTime asOf);
        List<UnearnedCommissionsDetail> GetUnearnedCommissionsDetail(int policyId, int coverageType, DateTime asOf);
        List<UnearnedBrokerFeesDetail> GetUnearnedBrokerFeesDetail(int policyId, int coverageType, DateTime asOf);
        List<AgencyReport> GetAgencyReport(DateTime? from, DateTime? to);
        void UpdateDueDate(int accountId, string ern, int coverageType, int policyId, string dueDate);
        void MarkAsPaid(int accountId, string ern, int coverageType, int policyId);


    }
}
