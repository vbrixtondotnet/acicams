var EndorsementPendingReportComponent = {
    pendingReportData: null,
    accounts: null,
    carriers: null,
    getPendingEndorsementReport: function () {
      
        ReportService.getPendingEndorsementsReportItems(function (data) {
            EndorsementPendingReportComponent.pendingReportData = data;
            EndorsementPendingReportComponent.renderPendingReport(EndorsementPendingReportComponent.pendingReportData);
            EndorsementPendingReportComponent.getUniqueAccounts();
            EndorsementPendingReportComponent.getUniqueCarriers();
            EndorsementPendingReportComponent.getUniqueItemTypes();
        });
    },
    filterPendingReports: function () {
        var accountId = $("#slcPendingAccount").val();
        var coverageType = $("#slcPendingCoverageTypes").val();
        var itemType = $("#slcPendingItemType").val();
        var carrier = $("#slcPendingCarrier").val();

        var filteredReportData = [];
        var filteredReportDataByAccount = [];
       
        if (EndorsementPendingReportComponent.pendingReportData != null && EndorsementPendingReportComponent.pendingReportData.length > 0) {
            for (var i = 0; i < EndorsementPendingReportComponent.pendingReportData.length; i++) {
                var reportData = EndorsementPendingReportComponent.pendingReportData[i];
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
                    if (reportData.coverageTypes == coverageType) {
                        filteredReportDataByCoverageType.push(reportData);
                    }
                }
                else {
                    filteredReportDataByCoverageType.push(reportData);
                }
            }

            var filteredReportDataByItemType = [];
            for (var i = 0; i < filteredReportDataByCoverageType.length; i++) {
                var reportData = filteredReportDataByCoverageType[i];
                if (itemType != "") {
                    if (reportData.type == itemType) {
                        filteredReportDataByItemType.push(reportData);
                    }
                }
                else {
                    filteredReportDataByItemType.push(reportData);
                }
            }

            var filteredReportDataByCarrier = [];
            for (var i = 0; i < filteredReportDataByItemType.length; i++) {
                var reportData = filteredReportDataByItemType[i];
                if (carrier != "") {
                    if (reportData.carrierName == carrier) {
                        filteredReportDataByCarrier.push(reportData);
                    }
                }
                else {
                    filteredReportDataByCarrier.push(reportData);
                }
            }

            filteredReportData = filteredReportDataByCarrier;
        }

        EndorsementPendingReportComponent.renderPendingReport(filteredReportData);
    },
    getUniqueAccounts: function () {
        $("#slcPendingAccount").html('<option value="">&nbsp</option>');
        var retval = [];
        if (EndorsementPendingReportComponent.pendingReportData != null && EndorsementPendingReportComponent.pendingReportData.length > 0) {

            for (var i = 0; i < EndorsementPendingReportComponent.pendingReportData.length; i++) {
                var reportData = EndorsementPendingReportComponent.pendingReportData[i];
                var account = retval.find((p) => { return p["accountId"] === reportData.accountId });
                if (!account) retval.push(reportData);
            }
        }

        EndorsementPendingReportComponent.accounts = retval;
        for (var i = 0; i < EndorsementPendingReportComponent.accounts.length; i++) {
            var account = EndorsementPendingReportComponent.accounts[i];
            $("#slcPendingAccount").append('<option value="' + account.accountId + '">' + account.account + '</option>');
        }
    },
    getUniqueCarriers: function () {
        $("#slcPendingCarrier").html('<option value="">&nbsp</option>');
        var retval = [];
        if (EndorsementPendingReportComponent.pendingReportData != null && EndorsementPendingReportComponent.pendingReportData.length > 0) {

            for (var i = 0; i < EndorsementPendingReportComponent.pendingReportData.length; i++) {
                var reportData = EndorsementPendingReportComponent.pendingReportData[i];
                var account = retval.find((p) => { return p["carrierName"] === reportData.carrierName });
                if (!account) retval.push(reportData);
            }
        }

        EndorsementPendingReportComponent.accounts = retval;
        for (var i = 0; i < EndorsementPendingReportComponent.accounts.length; i++) {
            var account = EndorsementPendingReportComponent.accounts[i];
            $("#slcPendingCarrier").append('<option value="' + account.carrierName + '">' + account.carrierName + '</option>');
        }
    },
    getUniqueItemTypes: function () {
        $("#slcPendingItemType").html('<option value="">&nbsp</option>');
        var retval = [];
        if (EndorsementPendingReportComponent.pendingReportData != null && EndorsementPendingReportComponent.pendingReportData.length > 0) {

            for (var i = 0; i < EndorsementPendingReportComponent.pendingReportData.length; i++) {
                var reportData = EndorsementPendingReportComponent.pendingReportData[i];
                var account = retval.find((p) => { return p["type"] === reportData.type });
                if (!account) retval.push(reportData);
            }
        }

        EndorsementPendingReportComponent.accounts = retval;
        for (var i = 0; i < EndorsementPendingReportComponent.accounts.length; i++) {
            var account = EndorsementPendingReportComponent.accounts[i];
            $("#slcPendingItemType").append('<option value="' + account.type + '">' + account.type + '</option>');
        }
    },
    renderPendingReport: function (data) {
        $("#pendingReportTable").html('');

        var totalAmount = 0;
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var report = data[i];
                var description = report.description == null ? '' : report.description;
                var amount = report.totalAmount == 0 ? '' : report.totalAmount;
                var typeOfCoverage = report.typeOfCoverage == null ? '' : report.typeOfCoverage;
                totalAmount += report.totalAmount;

                var reportRow = `<div class="col-md-12 table-row-pending">
                                    <div class="col-md-1" style="width:8%; text-align:center"><span>12/01/2020</span></div>
                                    <div class="col-md-1" style="width:6%; text-align:left"><span>`+report.action+`</span></div>
                                    <div class="col-md-1" style="width:12%; text-align:left"><span>`+ description +`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:left"><span>`+ report.type +`</span></div>
                                    <div class="col-md-1" style="width:10%; text-align:center"><span>`+ report.insuredItem +`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+ typeOfCoverage +`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+ amount +`</span></div>
                                    <div class="col-md-1" style="width:12%; text-align:left"><span>`+ report.account +`</span></div>
                                    <div class="col-md-1" style="width:17%; text-align:left"><span>`+ report.carrierName +`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+report.status+`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"></div>
                                 </div>`;
                $("#pendingReportTable").append(reportRow);
            }
        }
        $("#lblTotalPendingAmount").html(totalAmount.toFixed(2));
    },
    init: function () {
        EndorsementPendingReportComponent.getPendingEndorsementReport();
        $("#btnExportPendingReport").on("click", function () {
            window.open('/api/reports/endorsements/pending/download', '_blank');
        });
        $("html").on("change", "#slcPendingAccount, #slcPendingCoverageTypes, #slcPendingItemType, #slcPendingCarrier", function () {
            EndorsementPendingReportComponent.filterPendingReports();
        });
    },
}