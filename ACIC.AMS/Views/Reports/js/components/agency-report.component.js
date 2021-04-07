var AgencyReportComponent = {
    agencyReportData: null,
    accounts: null,
    carriers: null,
    mgas: null,
    agents: null,
    coverageTypes: null,
    getAgencyReports: function () {

        var from = $("#txtAgencyFrom").val();
        var to = $("#txtAgencyTo").val();
        ReportService.getAgencyReports(from, to, function (data) {
            AgencyReportComponent.agencyReportData = data; 
            AgencyReportComponent.renderAgencyReport(AgencyReportComponent.agencyReportData);
            AgencyReportComponent.getUniqueAccounts();
            AgencyReportComponent.getUniqueCarriers();
            AgencyReportComponent.getUniqueMgas();
            AgencyReportComponent.getUniqueAgents();
            AgencyReportComponent.getUniqueCoverageTypes();
            
        });
    },
    filterDates: function () {
        var from = $("#txtAgencyFrom").val();
        var to = $("#txtAgencyTo").val();

        if (from != '' && to != '') {
            AgencyReportComponent.getAgencyReports();
        }
    },
    filterPendingReports: function () {
        var carrierId = $("#slcAgencyCarrier").val();
        var mgaId = $("#slcAgencyMGA").val();
        var accountId = $("#slcAgencyAccount").val();
        var agentId = $("#slcAgencyAgent").val();
        var agentId = $("#slcAgencyAgent").val();
        var policyNumber = $("#txtAgencyPolicyNumber").val();
        var coverageType = $("#slcAgencyCoverageType").val();
        var status = $("#slcAgencyStatus").val();

        var filteredReportData = [];
        var filteredReportDataByAccount = [];
       
        if (AgencyReportComponent.agencyReportData != null && AgencyReportComponent.agencyReportData.length > 0) {
            for (var i = 0; i < AgencyReportComponent.agencyReportData.length; i++) {
                var reportData = AgencyReportComponent.agencyReportData[i];
                if (accountId != "") {
                    if (reportData.accountId == accountId) {
                        filteredReportDataByAccount.push(reportData);
                    }
                }
                else {
                    filteredReportDataByAccount.push(reportData);
                }
            }

            var filteredReportDataByCoverageType = [];
            for (var i = 0; i < filteredReportDataByAccount.length; i++) {
                var reportData = filteredReportDataByAccount[i];
                if (coverageType != "") {
                    if (reportData.coverageTypeId == parseInt(coverageType)) {
                        filteredReportDataByCoverageType.push(reportData);
                    }
                }
                else {
                    filteredReportDataByCoverageType.push(reportData);
                }
            }

            var filteredReportDataByCarrier= [];
            for (var i = 0; i < filteredReportDataByCoverageType.length; i++) {
                var reportData = filteredReportDataByCoverageType[i];
                if (carrierId != "") {
                    if (reportData.carrierId == parseInt(carrierId)) {
                        filteredReportDataByCarrier.push(reportData);
                    }
                }
                else {
                    filteredReportDataByCarrier.push(reportData);
                }
            }

            var filteredReportDataByMga = [];
            for (var i = 0; i < filteredReportDataByCarrier.length; i++) {
                var reportData = filteredReportDataByCarrier[i];
                if (mgaId != "") {
                    if (reportData.mgaid == parseInt(mgaId)) {
                        filteredReportDataByMga.push(reportData);
                    }
                }
                else {
                    filteredReportDataByMga.push(reportData);
                }
            }

            var filteredReportDataByAgent = [];
            for (var i = 0; i < filteredReportDataByMga.length; i++) {
                var reportData = filteredReportDataByMga[i];
                if (agentId != "") {
                    if (reportData.agentId == parseInt(agentId)) {
                        filteredReportDataByAgent.push(reportData);
                    }
                }
                else {
                    filteredReportDataByAgent.push(reportData);
                }
            }

            var filteredReportDataByPolicy = [];
            for (var i = 0; i < filteredReportDataByAgent.length; i++) {
                var reportData = filteredReportDataByAgent[i];
                if (policyNumber != "") {
                    if (reportData.policyNumber.toLowerCase().indexOf(policyNumber.toLowerCase()) > -1) {
                        filteredReportDataByPolicy.push(reportData);
                    }
                }
                else {
                    filteredReportDataByPolicy.push(reportData);
                }
            }
            var filteredReportDataByStatus = [];
            for (var i = 0; i < filteredReportDataByPolicy.length; i++) {
                var reportData = filteredReportDataByPolicy[i];
                if (status != "") {
                    var statusId = parseInt(status);
                    switch (statusId) {
                        case 1:
                            filteredReportDataByStatus.push(reportData);
                            break;
                        case 2:
                            if (reportData.commulativePremium < reportData.initialPremium || reportData.commulativeCommission < reportData.initialCommission) {
                                filteredReportDataByStatus.push(reportData);
                            }
                            break;
                        case 3:
                            if (reportData.commulativePremium >= reportData.initialPremium || reportData.commulativeCommission >= reportData.initialCommission) {
                                filteredReportDataByStatus.push(reportData);
                            }
                            break;
                    }
                }
                else {
                    filteredReportDataByStatus.push(reportData);
                }
            }
            filteredReportData = filteredReportDataByStatus;
        }

        AgencyReportComponent.renderAgencyReport(filteredReportData);
    },
    getUniqueAccounts: function () {
        $("#slcAgencyAccount").html('<option value="">&nbsp</option>');
        var retval = [];
        if (AgencyReportComponent.agencyReportData != null && AgencyReportComponent.agencyReportData.length > 0) {
            for (var i = 0; i < AgencyReportComponent.agencyReportData.length; i++) {
                var reportData = AgencyReportComponent.agencyReportData[i];
                var account = retval.find((p) => { return p["accountId"] === reportData.accountId });
                if (!account) retval.push(reportData);
            }
        }

        AgencyReportComponent.accounts = retval;
        for (var i = 0; i < AgencyReportComponent.accounts.length; i++) {
            var account = AgencyReportComponent.accounts[i];
            $("#slcAgencyAccount").append('<option value="' + account.accountId + '">' + account.account + '</option>');
        }
    },
    getUniqueCarriers: function () {
        $("#slcAgencyCarrier").html('<option value="">&nbsp</option>');
        var retval = [];
        if (AgencyReportComponent.agencyReportData != null && AgencyReportComponent.agencyReportData.length > 0) {

            for (var i = 0; i < AgencyReportComponent.agencyReportData.length; i++) {
                var reportData = AgencyReportComponent.agencyReportData[i];
                var account = retval.find((p) => { return p["carrierId"] === reportData.carrierId });
                if (!account && reportData.carrier != '' ) retval.push(reportData);
            }
        }

        AgencyReportComponent.carriers = retval;
        for (var i = 0; i < AgencyReportComponent.carriers.length; i++) {
            var account = AgencyReportComponent.carriers[i];
            $("#slcAgencyCarrier").append('<option value="' + account.carrierId + '">' + account.carrier + '</option>');
        }
    },
    getUniqueMgas: function () {
        $("#slcAgencyMGA").html('<option value="">&nbsp</option>');
        var retval = [];
        if (AgencyReportComponent.agencyReportData != null && AgencyReportComponent.agencyReportData.length > 0) {

            for (var i = 0; i < AgencyReportComponent.agencyReportData.length; i++) {
                var reportData = AgencyReportComponent.agencyReportData[i];
                var account = retval.find((p) => { return p["mgaid"] === reportData.mgaid });
                if (!account && reportData.mga != '') retval.push(reportData);
            }
        }

        AgencyReportComponent.mgas = retval;
        for (var i = 0; i < AgencyReportComponent.mgas.length; i++) {
            var account = AgencyReportComponent.mgas[i];
            $("#slcAgencyMGA").append('<option value="' + account.mgaid + '">' + account.mga + '</option>');
        }
    },
    getUniqueAgents: function () {
        $("#slcAgencyAgent").html('<option value="">&nbsp</option>');
        var retval = [];

        if (AgencyReportComponent.agencyReportData != null && AgencyReportComponent.agencyReportData.length > 0) {

            for (var i = 0; i < AgencyReportComponent.agencyReportData.length; i++) {
                var reportData = AgencyReportComponent.agencyReportData[i];
                var account = retval.find((p) => { return p["agentId"] === reportData.agentId });
                if (!account) retval.push(reportData);
            }
        }

        AgencyReportComponent.agents = retval;
        for (var i = 0; i < AgencyReportComponent.agents.length; i++) {
            var account = AgencyReportComponent.agents[i];
            $("#slcAgencyAgent").append('<option value="' + account.agentId + '">' + account.agent + '</option>');
        }
    },
    getUniqueCoverageTypes: function () {
        $("#slcAgencyCoverageType").html('<option value="">&nbsp</option>');
        var retval = [];
        if (AgencyReportComponent.agencyReportData != null && AgencyReportComponent.agencyReportData.length > 0) {

            for (var i = 0; i < AgencyReportComponent.agencyReportData.length; i++) {
                var reportData = AgencyReportComponent.agencyReportData[i];
                var account = retval.find((p) => { return p["coverageTypeId"] === reportData.coverageTypeId });
                if (!account) retval.push(reportData);
            }
        }

        AgencyReportComponent.coverageTypes = retval;
        for (var i = 0; i < AgencyReportComponent.coverageTypes.length; i++) {
            var account = AgencyReportComponent.coverageTypes[i];
            $("#slcAgencyCoverageType").append('<option value="' + account.coverageTypeId + '">' + account.coverageTypes + '</option>');
        }
    },
    renderAgencyReport: function (data) {
        $("#agencyReportTable").html('');

        var totalAmount = 0;
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var report = data[i];
                var carrierMga = report.carrier + '<br/>' + report.mga;
                var account = report.account;
                var policyType = report.policyNumber + '<br/>' + report.coverageTypes;
                var effective = ValidationService.formatDate(report.effective) + '<br/>' + ValidationService.formatDate(report.expiration);
                var initialPremium = ValidationService.formatMoney(report.initialPremium);
                var commulativePremium = ValidationService.formatMoney(report.commulativePremium);
                var initialCommission = ValidationService.formatMoney(report.initialCommission);
                var commulativeCommission = ValidationService.formatMoney(report.commulativeCommission);
                var agent = report.agent;
                totalAmount += report.totalAmount;

                var reportRow = `<div class="col-md-12 table-row agencyreport">
                                    <div class="col-md-1" style="width:15%; text-align:left">`+ carrierMga+`</div>
                                    <div class="col-md-1" style="width:15%; text-align:left; padding-left:18px;">`+ account +`</div>
                                    <div class="col-md-1" style="width:10%; text-align:left">`+ policyType +`</div>
                                    <div class="col-md-1" style="width:7%; text-align:left">`+ effective +`</div>
                                    <div class="col-md-1" style="width:8%; text-align:left;padding-left:8px;">`+ initialPremium +`</div>
                                    <div class="col-md-1" style="width:8%; text-align:left;padding-left:8px;">`+ commulativePremium +`</div>
                                    <div class="col-md-1" style="width:8%; text-align:left;padding-left:8px;">`+ initialCommission +`</div>
                                    <div class="col-md-1" style="width:8%; text-align:left;padding-left:8px;">`+ commulativeCommission +`</div>
                                    <div class="col-md-1" style="width:14%; text-align:center">`+ agent +`</div>
                                    <div class="col-md-1" style="width:7%; text-align:center">&nbsp;</div>
                                 </div>`;
                $("#agencyReportTable").append(reportRow);
            }
        }
        $("#totalRecordsAgencyReports").html(data.length);
        //$("#lblTotalPendingAmount").html(totalAmount.toFixed(2));
    },

    init: function () {
        AgencyReportComponent.getAgencyReports();
        $("#btnExportAgencyReport").on("click", function () {
            var from = $("#txtAgencyFrom").val();
            var to = $("#txtAgencyTo").val();
            window.open('/api/reports/agency/download?from=' + from + '&to=' + to, '_blank');
        });
        $("html").on("change", "#slcPendingAccount, #slcPendingCoverageTypes, #slcPendingItemType, #slcPendingCarrier", function () {
            EndorsementPendingReportComponent.filterPendingReports();
        });
        $("#txtAgencyFrom,#txtAgencyTo").on("change", function () {
            AgencyReportComponent.filterDates();
        });
        $("#btnClearAgencyFilters").on("click", function () {
            location.reload();
        });
        $("#slcAgencyCarrier,#slcAgencyAccount,#slcAgencyCoverageType,#slcAgencyMGA,#slcAgencyAgent,#slcAgencyStatus").on("change", function () {
            AgencyReportComponent.filterPendingReports();
        });
        $("#txtAgencyPolicyNumber").on("keyup", function () {
            AgencyReportComponent.filterPendingReports();
        });
    },
}