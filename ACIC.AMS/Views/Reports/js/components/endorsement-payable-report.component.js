var EndorsementPayableReportComponent = {
    payableReportData: null,
    accounts: [],
    selectedReport: null,
    selectedReportId: null,
    updateEndorsementAmountTimeout: null,
    selectedGroupId: null,
    getPayableReport: function () {
        ReportService.getPayableEndorsementsReportItems(function (data) {
            EndorsementPayableReportComponent.payableReportData = data;
            EndorsementPayableReportComponent.renderPayableReport(EndorsementPayableReportComponent.payableReportData);
        });
    },
    filterReceivedReports: function () {
        var accountId = $("#slcAccount").val();
        var coverageType = $("#slcCoverageTypes").val();
        var endtNo = $("#txtEndorsementNo").val();
        var paymentStatus = $("#slcStatus").val();

        var filteredReportData = [];
        var filteredReportDataByAccount = [];

        if (EndorsementReceivedReportComponent.receivedReportData != null && EndorsementReceivedReportComponent.receivedReportData.length > 0) {

            for (var i = 0; i < EndorsementReceivedReportComponent.receivedReportData.length; i++) {
                var reportData = EndorsementReceivedReportComponent.receivedReportData[i];
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
                    if (reportData.coverageTypes == coverageType) {
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
                if (endtNo != "") {
                    if (reportData.ern == endtNo) {
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
                if (paymentStatus == "") {
                    filteredReportDataByStatus.push(reportData);
                }
                else {
                    if (paymentStatus == "1") {
                        if (reportData.paymentStatus == true) 
                            filteredReportDataByStatus.push(reportData);
                    }
                    else {
                        if (reportData.paymentStatus == false)
                            filteredReportDataByStatus.push(reportData);
                    }
                }
            }

            filteredReportData = filteredReportDataByStatus;
        }
        EndorsementReceivedReportComponent.renderReceivedReport(filteredReportData);
    },
    renderPayableReport: function (reportData) {
        $("#payableReportTable").html('');
        if (reportData != null && reportData.length > 0) {
            var totalPremium = 0;
            var totalSlTaxes = 0;
            var totalFees = 0;
            var totalCommission = 0;
            var totalPayableAmount = 0;

            for (var i = 0; i < reportData.length; i++) {
                var report = reportData[i];
                var premium = report.premium >= 0 ? (report.premium == 0 ? '' : report.premium.toFixed(2)) : '(' + (report.premium * -1).toFixed(2) + ')';
                var slTaxes = report.slTaxes >= 0 ? (report.slTaxes == 0 ? '' : report.slTaxes) : '(' + (report.slTaxes * -1) + ')';
                var fees = report.fees >= 0 ? (report.fees == 0 ? '' : report.fees) : '(' + (report.fees * -1) + ')';
                var commission = report.commission >= 0 ? (report.commission == 0 ? '' : report.commission) : '(' + (report.commission * -1) + ')';
                var payableAmount = report.payableAmount >= 0 ? (report.payableAmount == 0 ? '' : report.payableAmount) : '(' + (report.payableAmount * -1) + ')';
           
                var dueDate = report.dueDate == null ? '' : ValidationService.formatDate(report.dueDate);
                var remarks = report.dueInDays == null ? 'No Due Date Assigned' : report.dueInDays < 0 ? ('Past Due for ' + (report.dueInDays * -1) + ' days') : 'Due In ' + report.dueInDays + ' days';

                var color = report.dueInDays < 0 ? 'red' : 'black';

                totalPremium += report.premium;
                totalSlTaxes += report.slTaxes;
                totalFees += report.fees;
                totalCommission += report.commission;
                totalPayableAmount += report.payableAmount;
                
                var reportId = report.accountId + '-' + report.ern + '-' + report.coverageTypes + '-' + report.policyId;

                var reportRow = `<div class="col-md-12 table-row payable">
                                    <div class="col-md-1" style="width:15%; text-align:left; font-size:13px;padding-left:5px;"><span>`+ report.legalName +`</span><br /><span>`+report.coverageTypeDesc+`</span></div>
                                    <div class="col-md-1" style="width:18%; text-align:left; font-size:13px;"><span>`+report.carrierName+`</span><br /><span>`+report.mganame+`</span></div>
                                    <div class="col-md-1" style="width:4%; text-align:center;font-size:13px;"><span>`+ report.ern + `</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:right;font-size:13px;padding-right:10px;"><span>`+ ValidationService.formatMoney(premium) +`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center;font-size:13px;"><span>`+ ValidationService.formatMoney(slTaxes)+`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center;font-size:13px;"><span>`+ ValidationService.formatMoney(fees)+`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:right;font-size:13px;padding-right:15px;"><span>`+ ValidationService.formatMoney(commission)+`</span></div>
                                    <div class="col-md-1" style="width:8%; text-align:center;font-size:13px;"><span>`+ ValidationService.formatMoney(payableAmount) + `</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center;font-size:13px;color:`+ color+`;"><span>`+ dueDate + `</span></div>
                                    <div class="col-md-1" style="width:9%; text-align:left;font-size:13px;;color:`+ color +`;"><span>`+ remarks + `</span></div>
                                    <div class="col-md-1" style="width:11%; text-align:center;padding:0;">
                                        <button data-account="`+ report.legalName+`" data-coverage-type="`+ report.coverageTypeDesc + `" data-endtno="` + report.ern +`" data-due-date="`+ dueDate+`" data-id="`+ reportId + `" class="update-duedate btn btn-info action-button-payable">Update <br/>Due Date</button>
                                        <button data-id="`+ reportId + `" class="mark-as-paid btn btn-info action-button-payable">Mark as <br/>Paid</button>
                                    </div>
                                </div>`;
                $("#payableReportTable").append(reportRow);
            }

            $("#subhTotalPremiumPayable").html(ValidationService.formatMoney(totalPremium.toFixed(2)));
            $("#subhTotalSLTaxesPayable").html(ValidationService.formatMoney(totalSlTaxes.toFixed(2)));
            $("#subhTotalFeesPayable").html(ValidationService.formatMoney(totalFees.toFixed(2)));
            $("#subhTotalCommissionPayable").html(ValidationService.formatMoney(totalCommission.toFixed(2)));
            $("#subhTotalPayableAmountPayable").html(ValidationService.formatMoney(totalPayableAmount.toFixed(2)));
        }
    },
    updateDownPayment: function (accountId, ern, coverageType, policyId, status) {
        ReportService.updateDownPayment(accountId, ern, coverageType, policyId, status, function () {
            debugger;
            var dataId = accountId + '-' + ern + '-' + coverageType + '-' + policyId;
            var btn = $("button.update-dp[data-id='" + dataId + "']");
            if (status == 1) {
                btn.removeClass('btn-default').addClass('btn-info').html('Received');
            }
            else {
                btn.removeClass('btn-info').addClass('btn-default').html('Unpaid');
            }

        });
    },
    updateDueDate: function (accountId, ern, coverageType, policyId, dueDate) {
        ReportService.updateDueDate(accountId, ern, coverageType, policyId, dueDate, function () {
            $("#mdlUpdateDueDate").modal('hide');
            EndorsementPayableReportComponent.getPayableReport();
        });
    },
    markAsPaid: function (accountId, ern, coverageType, policyId) {
        ReportService.markAsPaid(accountId, ern, coverageType, policyId, function () {
            EndorsementPayableReportComponent.getPayableReport();
        });
    },
    init: function () {
        EndorsementPayableReportComponent.getPayableReport();

        $("#btnExportPayableEndReport").on("click", function () {
            window.open('/api/reports/endorsements/payable/download', '_blank');
        });

        $("html").on("click", "button.update-duedate", function () {
            EndorsementPayableReportComponent.selectedReportId = $(this).attr('data-id');
            var dueDate = $(this).attr('data-due-date');
            var account = $(this).attr('data-account');
            var coverageType = $(this).attr('data-coverage-type');
            var ern = $(this).attr('data-endtno');

            $("#txtPayableDueDate").val(dueDate);
            $("#txtPayableEndtNumber").val(ern);
            $("#txtPayableTypeOfCoverage").val(coverageType);
            $("#txtPayableAccountName").val(account);
            $("#mdlUpdateDueDate").modal({ backdrop: 'static' });
        });

        $("html").on("click", "button.mark-as-paid", function () {
            EndorsementPayableReportComponent.selectedReportId = $(this).attr('data-id');
            var reportId = EndorsementPayableReportComponent.selectedReportId.split('-');
            EndorsementPayableReportComponent.markAsPaid(reportId[0], reportId[1], reportId[2], reportId[3]);
        });

        $("#btnSaveDueDate").click(function () {
            var reportId = EndorsementPayableReportComponent.selectedReportId.split('-');
            var dueDate = $("#txtPayableDueDate").val();
            EndorsementPayableReportComponent.updateDueDate(reportId[0], reportId[1], reportId[2], reportId[3], dueDate);
        });
    }
}