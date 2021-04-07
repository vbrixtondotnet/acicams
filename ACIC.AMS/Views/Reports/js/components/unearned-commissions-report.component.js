var UnearnedCommissionsReportComponent = {
    unearnedReportData: null,
    accounts: [],
    selectedReport: null,
    selectedGroupId: null,
    getUnearnedReport: function () {
        var asOf = $("#txtComissionsDateAsOf").val() == '' ? new Date().toLocaleDateString() : $("#txtComissionsDateAsOf").val();
        
        ReportService.getUnearnedCommissionsReportItems(asOf, function (data) {
            UnearnedCommissionsReportComponent.unearnedReportData = data;
            UnearnedCommissionsReportComponent.renderCommissionsReport(UnearnedCommissionsReportComponent.unearnedReportData);
            UnearnedCommissionsReportComponent.getUniqueAccounts();
        });
    },
    filterReport: function () {
        var accountId = $("#slcCommissionsAccount").val();
        var coverageType = $("#slcComissionsCoverageTypes").val();
        var policyNumber = $("#txtComissionsPolicyNumber").val().trim();
        var status = $("#slcCommissionStatus").val();

        var filteredReportData = [];
        var filteredReportDataByAccount = [];

        var reportDataSet = UnearnedCommissionsReportComponent.unearnedReportData;
        if (reportDataSet != null && reportDataSet.length > 0) {

            for (var i = 0; i < reportDataSet.length; i++) {
                var reportData = reportDataSet[i];
                if (accountId != "") {
                    if (reportData.accountId == accountId) {
                        filteredReportDataByAccount.push(reportData);
                    }
                }
                else {
                    filteredReportDataByAccount.push(reportData);
                }
            }

            //filter by coverage types
            var filteredReportDataByCategory = [];
            for (var i = 0; i < filteredReportDataByAccount.length; i++) {
                var reportData = filteredReportDataByAccount[i];
                if (coverageType != "") {
                    if (reportData.coverageTypeId == parseInt(coverageType)) {
                        filteredReportDataByCategory.push(reportData);
                    }
                }
                else {
                    filteredReportDataByCategory.push(reportData);
                }
            }

            var filteredReportDataByEndt = [];
            for (var i = 0; i < filteredReportDataByCategory.length; i++) {
                var reportData = filteredReportDataByCategory[i];
                if (policyNumber != "") {
                    if (reportData.policyNumber == policyNumber) {
                        filteredReportDataByEndt.push(reportData);
                    }
                }
                else {
                    filteredReportDataByEndt.push(reportData);
                }
            }

            var filteredReportDataByStatus = [];
            for (var i = 0; i < filteredReportDataByEndt.length; i++) {
                var reportData = filteredReportDataByEndt[i];
                if (status == "") {
                    filteredReportDataByStatus.push(reportData);
                }
                else {
                    if (status == "1") {
                        if (reportData.commission > 0) 
                            filteredReportDataByStatus.push(reportData);
                    }
                    else {
                        if (reportData.commission <= 0) 
                            filteredReportDataByStatus.push(reportData);
                    }
                }
            }

            filteredReportData = filteredReportDataByStatus;
        }

        UnearnedCommissionsReportComponent.renderCommissionsReport(filteredReportData);
    },
    renderCommissionsReport: function (reportData) {
        $("#comissionsUnearnedReportTable").html('');
        $("#thCommulativeCommissions").html('');
        $("#thUnearned").html('');
        $("#thEarned").html('');

        if (reportData != null && reportData.length > 0) {
           
            var totalCummulative = 0;
            var totalUnearned = 0;
            var totalEarned = 0;
            for (var i = 0; i < reportData.length; i++) {
                var report = reportData[i];
                var reportId = report.accountId + '-' + report.coverageTypeId + '-' + report.policyId;
                totalCummulative += report.commission;
                totalUnearned += report.unearned;
                totalEarned += report.earned;
                var policyNumber = report.policyNumber == null ? '' : report.policyNumber;

                var reportRow = `<div class="col-md-12 table-row">
                                    <div class="col-md-1" style="width:20%; text-align:left">`+ report.legalName+`</div>
                                    <div class="col-md-1" style="width:15%; text-align:left">`+ report.coverageTypes +`</div>
                                    <div class="col-md-1" style="width:10%; text-align:left">`+ policyNumber +`</div>
                                    <div class="col-md-1" style="width:15%; text-align:center">`+ ValidationService.formatDate(report.effective) +`</div>
                                    <div class="col-md-1" style="width:14%; text-align:center">`+ ValidationService.formatMoney(report.commission) +`</div>
                                    <div class="col-md-1" style="width:7%; text-align:center">`+ ValidationService.formatMoney(report.unearned) +`</div>
                                    <div class="col-md-1" style="width:7%; text-align:center">`+ ValidationService.formatMoney(report.earned) +`</div>
                                    <div class="col-md-1" style="width:12%; text-align:center;padding-top:5px !important;"><button data-id="`+ reportId + `" class="btn btn-info commission-details" style="padding:5px;font-size:12px;">Detailed</button></div>
                                </div>`;
                $("#comissionsUnearnedReportTable").append(reportRow);
            }

            $("#thCommulativeCommissions").html(ValidationService.formatMoney(totalCummulative.toFixed(2)));
            $("#thUnearned").html(ValidationService.formatMoney(totalUnearned.toFixed(2)));
            $("#thEarned").html(ValidationService.formatMoney(totalEarned.toFixed(2)));
        }
    },
    getUniqueAccounts: function () {
        $("#slcCommissionsAccount").html('<option value="">&nbsp</option>');
        var retval = [];
        var reportDataSet = UnearnedCommissionsReportComponent.unearnedReportData;
        if (reportDataSet != null && reportDataSet.length > 0) {

            for (var i = 0; i < reportDataSet.length; i++) {
                var reportData = reportDataSet[i];
                var account = retval.find((p) => { return p["accountId"] === reportData.accountId });
                if (!account) retval.push(reportData);
            }
        }

        UnearnedCommissionsReportComponent.accounts = retval;
        for (var i = 0; i < UnearnedCommissionsReportComponent.accounts.length; i++) {
            var account = UnearnedCommissionsReportComponent.accounts[i];
            $("#slcCommissionsAccount").append('<option value="' + account.accountId + '">' + account.legalName + '</option>');
        }
    },
    viewDetails: function (reportId) {
        var asOf = $("#txtComissionsDateAsOf").val() == '' ? new Date().toLocaleDateString() : $("#txtComissionsDateAsOf").val();
        var policyId = reportId[2];
        var coverageType = reportId[1];
        $("#commissionDetailItems").html('');
        $("#commissionDetailItemsPreloader").removeClass('hide');
        $("#lblCommissionDetailsAccount").html('');
        $("#lblCommissionDetailsExpiration").html('');
        $("#lblCommissionDetailsPolicyNo").html('');
        $("#lblCommissionDetailsCoverageType").html('');
        ReportService.getUnearnedCommissionsReportDetails(policyId, coverageType, asOf, function (data) {

            if (data != null && data.length > 0) {
                var headerRow = data[0];
                debugger;
                $("#lblCommissionDetailsAccount").html(headerRow.legalName);
                $("#lblCommissionDetailsExpiration").html(ValidationService.formatDate(headerRow.expiration));
                $("#lblCommissionDetailsPolicyNo").html(headerRow.policyNumber);
                $("#lblCommissionDetailsCoverageType").html(headerRow.coverageTypes);

                for (var i = 0; i < data.length; i++) {
                    var detail = data[i];

                    var row = `<div class="col-md-12 table-row-popup" style="margin-top:5px;">
                                    <div class="col-md-2 text-center"><small>` + ValidationService.formatDate(detail.effective) + `</small></div>
                                    <div class="col-md-1 text-center"><small>` + ValidationService.formatMoney(detail.commission) +`</small></div>
                                    <div class="col-md-2 text-center"><small>` + ValidationService.formatMoney(detail.dailyCommission) +`</small></div>
                                    <div class="col-md-1 text-center"><small>` + ValidationService.formatMoney(detail.unearned) +`</small></div>
                                    <div class="col-md-1 text-center"><small>` + ValidationService.formatMoney(detail.earned) +`</small></div>
                                    <div class="col-md-2 text-center"><small>` + detail.reference +`</small></div>
                                    <div class="col-md-3 text-center" style="padding:0 !important;"></div>
                                </div>`;
                    $("#commissionDetailItems").append(row);
                }
            }
            $("#commissionDetailItemsPreloader").addClass('hide');
        });
        $("#mdlCommissionDetails").modal({
            backdrop: 'static'
        });
    },
    init: function () {
        var today = new Date();
        var todayStr = today.toLocaleDateString().split('/');
        var dateStr = todayStr[2] + '-' + (parseInt(todayStr[1]) < 10 ? ('0' + todayStr[1]) : (todayStr[1])) + '-' + (parseInt(todayStr[0]) < 10 ? ('0' + todayStr[0]) : todayStr[0]);
        
        $("#txtComissionsDateAsOf").val(dateStr);

        $("#btnClearCommissionsFilters").on("click", function () {
            $("#slcComissionsCoverageTypes").val("");
            $("#txtComissionsPolicyNumber").val("");
            $("#txtComissionsDateAsOf").val('');
            $("#slcCommissionStatus").val('');
            $("#slcCommissionsAccount").val("").trigger('change');
        });

        $("#btnExportCommissionsReport").on("click", function () {
            var asOf = $("#txtComissionsDateAsOf").val() == '' ? new Date().toLocaleDateString() : $("#txtComissionsDateAsOf").val();
            window.open('/api/reports/commissions/unearned/download?asOf=' + asOf, '_blank');
        });

        $("#txtComissionsDateAsOf").on('change', function () {
            UnearnedCommissionsReportComponent.getUnearnedReport();
        });

        $("#txtComissionsPolicyNumber").on("keyup", function () {
            UnearnedCommissionsReportComponent.filterReport();
        });

        $("#slcCommissionsAccount, #slcComissionsCoverageTypes, #slcCommissionStatus").on("change", function () {
            UnearnedCommissionsReportComponent.filterReport();
        });

        $("html").on("click", 'button.commission-details', function () {
            var dataId = $(this).attr('data-id');
            var reportId = dataId.split('-');

            UnearnedCommissionsReportComponent.viewDetails(reportId);
        });

        $('a.commission[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("href") // activated tab
            if (target == "#tbCommissions") {
                UnearnedCommissionsReportComponent.getUnearnedReport();
            }
            else if (target == "#tbBrokerFees") {
                UnearnedBrokerFeesReportComponent.init();
            }
        });

        UnearnedCommissionsReportComponent.getUnearnedReport();
    }
}