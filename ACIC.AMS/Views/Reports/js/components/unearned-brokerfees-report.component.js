var UnearnedBrokerFeesReportComponent = {
    unearnedReportData: null,
    accounts: [],
    selectedReport: null,
    selectedGroupId: null,
    getUnearnedReport: function () {
        var asOf = $("#txtBrokerFeesDateAsOf").val() == '' ? new Date().toLocaleDateString() : $("#txtBrokerFeesDateAsOf").val();
        
        ReportService.getUnearnedBrokerFeesReportItems(asOf, function (data) {
            UnearnedBrokerFeesReportComponent.unearnedReportData = data;
            UnearnedBrokerFeesReportComponent.renderBrokerFeesReport(UnearnedBrokerFeesReportComponent.unearnedReportData);
            UnearnedBrokerFeesReportComponent.getUniqueAccounts();
        });
    },
    filterReport: function () {
        var accountId = $("#slcBrokerFeesAccount").val();
        var coverageType = $("#slcBrokerFeesCoverageTypes").val();
        var policyNumber = $("#txtBrokerFeesPolicyNo").val().trim();

        var filteredReportData = [];
        var filteredReportDataByAccount = [];

        var reportDataSet = UnearnedBrokerFeesReportComponent.unearnedReportData;
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

           

            filteredReportData = filteredReportDataByEndt;
        }

        UnearnedBrokerFeesReportComponent.renderBrokerFeesReport(filteredReportData);
    },
    renderBrokerFeesReport: function (reportData) {
        $("#brokerFeesUnearnedReportTable").html('');

        $("#thCommulativeBrokerFees").html('');
        $("#thUnearnedBrokerFees").html('');
        $("#thEarnedBrokerFees").html('');
        if (reportData != null && reportData.length > 0) {
           
            var totalCummulative = 0;
            var totalUnearned = 0;
            var totalEarned = 0;
            for (var i = 0; i < reportData.length; i++) {
                var report = reportData[i];
                var reportId = report.accountId + '-'  + report.coverageTypes + '-' + report.policyId;
                totalCummulative += report.brokerFees;
                totalUnearned += report.unearned;
                totalEarned += report.earned;
                var policyNumber = report.policyNumber == null ? '' : report.policyNumber;

                var reportRow = `<div class="col-md-12 table-row">
                                    <div class="col-md-1" style="width:20%; text-align:left">`+ report.legalName+`</div>
                                    <div class="col-md-1" style="width:15%; text-align:left">`+ report.coverageTypes +`</div>
                                    <div class="col-md-1" style="width:10%; text-align:left">`+ policyNumber +`</div>
                                    <div class="col-md-1" style="width:15%; text-align:center">`+ ValidationService.formatDate(report.effective) +`</div>
                                    <div class="col-md-1" style="width:14%; text-align:center">`+ ValidationService.formatMoney(report.brokerFees) +`</div>
                                    <div class="col-md-1" style="width:7%; text-align:center">`+ ValidationService.formatMoney(report.unearned) +`</div>
                                    <div class="col-md-1" style="width:7%; text-align:center">`+ ValidationService.formatMoney(report.earned) +`</div>
                                    <div class="col-md-1" style="width:12%; text-align:center;padding-top:5px !important;"><button data-id="`+ reportId + `" class="btn btn-info" style="padding:5px;font-size:12px;">Detailed</button></div>
                                </div>`;
                $("#brokerFeesUnearnedReportTable").append(reportRow);
            }

            $("#thCommulativeBrokerFees").html(ValidationService.formatMoney(totalCummulative.toFixed(2)));
            $("#thUnearnedBrokerFees").html(ValidationService.formatMoney(totalUnearned.toFixed(2)));
            $("#thEarnedBrokerFees").html(ValidationService.formatMoney(totalEarned.toFixed(2)));
        }
    },
    getUniqueAccounts: function () {
        $("#slcBrokerFeesAccount").html('<option value="">&nbsp</option>');
        var retval = [];
        var reportDataSet = UnearnedBrokerFeesReportComponent.unearnedReportData;
        if (reportDataSet != null && reportDataSet.length > 0) {

            for (var i = 0; i < reportDataSet.length; i++) {
                var reportData = reportDataSet[i];
                var account = retval.find((p) => { return p["accountId"] === reportData.accountId });
                if (!account) retval.push(reportData);
            }
        }

        UnearnedBrokerFeesReportComponent.accounts = retval;
        for (var i = 0; i < UnearnedBrokerFeesReportComponent.accounts.length; i++) {
            var account = UnearnedBrokerFeesReportComponent.accounts[i];
            $("#slcBrokerFeesAccount").append('<option value="' + account.accountId + '">' + account.legalName + '</option>');
        }
    },
    updateDownPayment: function (accountId, ern, coverageType, policyId, status) {
        ReportService.updateDownPayment(accountId, ern, coverageType, policyId, status, function () {
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
    updatePaymentStatus: function (accountId, ern, coverageType, policyId, status) {
        ReportService.updatePaymentStatus(accountId, ern, coverageType, policyId, status, function () {
            var dataId = accountId + '-' + ern + '-' + coverageType + '-' + policyId;
            var btn = $("button.update-paymentstatus[data-id='" + dataId + "']");
            if (status == 1) {
                btn.removeClass('btn-default').addClass('btn-info').html('Paid');
            }
            else {
                btn.removeClass('btn-info').addClass('btn-default').html('Not Received');
            }
        });
    },
    updateFinanceRef: function () {
        var financeRef = $("#txtFinanceReference").val();
        var accountId = EndorsementReceivedReportComponent.selectedReport[0];
        var ern = EndorsementReceivedReportComponent.selectedReport[1];
        var coverageType = EndorsementReceivedReportComponent.selectedReport[2];
        var policyId = EndorsementReceivedReportComponent.selectedReport[3];
        var dataId = accountId + '-' + ern + '-' + coverageType + '-' + policyId;
        var financeRefButton = $("button.update-finance-ref[data-id='" + dataId + "']");
        ReportService.updateFinancingReference(accountId, ern, coverageType, policyId, financeRef, function () {
            $("#txtFinanceReference").val('');
            $("#mdlFinanceReference").modal('hide');
            financeRefButton.attr('data-financeref', financeRef);
            
        });
    },
    updateEndtItems: function (reportId) {
        
        $("#mdlEndtItems").modal({ backdrop: 'static' });
        $("#endtRptGroupItems").html(``);
        $("#endtRptGroupItemsPreloader").removeClass('hide');
        $("#totalPremium").html('');
        $("#totalSurcharge").html('');
        $("#totalSurplusTaxes").html('');
        $("#totalEndtFees").html('');
        $("#totalNonTaxedFees").html('');
        $("#totalOtherFees").html('');
        $("#totalCommissions").html('');
        $("#totalNetAmount").html('');

        EndorsementReceivedReportComponent.getReceivedEndorsementsReportItems(reportId[0], reportId[1], reportId[2], reportId[3]);
    },
    getReceivedEndorsementsReportItems: function (accountId, ern, coverageType, policyId) {

        ReportService.getReceivedEndorsementsReportItems(accountId, ern, coverageType, policyId, function (data) {

            $("#lblEndtRptGroupAccount").html(data.account);
            $("#lblEndtRptGroupEndtNo").html(data.endtNo);
            $("#lblEndtRptGroupCoverageType").html(data.coverageType);
            $("#endtRptGroupItemsPreloader").addClass('hide');
            $("#endtRptGroupItems").html(``);

            var endorsementReportEndtItems = data.endorsementReportEndtItems;
            for (var i = 0; i < endorsementReportEndtItems.length; i++) {
                var endt = endorsementReportEndtItems[i];
                var commission = endt.commission == 0 ? '' : endt.commission;
                var endtFee = endt.endtFee == 0 ? '' : endt.endtFee;
                var nonTaxedRateUnit = endt.nonTaxedRateUnit == 0 ? '' : endt.nonTaxedRateUnit;
                var otherFees = endt.otherFees == 0 ? '' : endt.otherFees;
                var premium = endt.premium == 0 ? '' : endt.premium;
                var surcharge = endt.surcharge == 0 ? '' : endt.surcharge;
                var surplusTax = endt.surplusTax == 0 ? '' : endt.surplusTax;
                var totalAmount = endt.totalAmount == 0 ? '' : endt.totalAmount;

                var row = `<div class="col-md-12 table-row-popup" style="margin-top:5px;">
                                <div class="col-md-2 text-center" style="padding-left:5px;"><input type="text" data-type='decimal' onkeypress="return false;" value="`+ endt.reference+`"/></div>
                                <div class="col-md-1 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId+` data-field='Premium' data-type='decimal' value="`+ premium + `"/></div>
                                <div class="col-md-1 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId +` data-field='Surcharge' data-type='decimal' value="`+ surcharge + `"/></div>
                                <div class="col-md-1 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId +` data-field='SurplusTax' data-type='decimal' value="`+ surplusTax + `"/></div>
                                <div class="col-md-1 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId +` data-field='EndtFee' data-type='decimal' value="`+ endtFee + `"/></div>
                                <div class="col-md-2 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId +` data-field='NonTaxedRateUnit' data-type='decimal' value="`+ nonTaxedRateUnit + `"/></div>
                                <div class="col-md-1 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId +` data-field='OtherFees' data-type='decimal' value="`+ otherFees + `"/></div>
                                <div class="col-md-1 text-center"><input type="text" class="endorsement-amount" data-id=`+ endt.endtId +` data-field='Commission' data-type='decimal' value="`+ commission + `"/></div>
                                <div class="col-md-1 text-center"><input type="text" data-type='decimal' value="`+ totalAmount +`"/></div>
                                <div class="col-md-1 text-center">&nbsp;</div>
                            </div>`;
                $("#endtRptGroupItems").append(row);
            }

            $("#totalPremium").html(data.totalPremium);
            $("#totalSurcharge").html(data.totalSurcharge);
            $("#totalSurplusTaxes").html(data.totalSurplusTaxes);
            $("#totalEndtFees").html(data.totalEndtFees);
            $("#totalNonTaxedFees").html(data.totalNonTaxedFees);
            $("#totalOtherFees").html(data.totalOtherFees);
            $("#totalCommissions").html(data.totalCommissions);
            $("#totalNetAmount").html(data.totalNetAmount);
        });
    },
    updateEndorsementAmount: function (id, field, amount) {
        clearTimeout(EndorsementReceivedReportComponent.updateEndorsementAmountTimeout);
        EndorsementReceivedReportComponent.updateEndorsementAmountTimeout = setTimeout(function () {
            ReportService.updateAmount(id, field, amount, function () {
                var reportId = EndorsementReceivedReportComponent.selectedGroupId;
                EndorsementReceivedReportComponent.getReceivedReport();
                EndorsementReceivedReportComponent.getReceivedEndorsementsReportItems(reportId[0], reportId[1], reportId[2], reportId[3]);
            });
        }, 500);
    },
    init: function () {
        var today = new Date();
        var todayStr = today.toLocaleDateString().split('/');
        var dateStr = todayStr[2] + '-' + (parseInt(todayStr[1]) < 10 ? ('0' + todayStr[1]) : (todayStr[1])) + '-' + (parseInt(todayStr[0]) < 10 ? ('0' + todayStr[0]) : todayStr[0]);
        
        $("#txtBrokerFeesDateAsOf").val(dateStr);

        $("#txtBrokerFeesDateAsOf").on('change', function () {
            UnearnedBrokerFeesReportComponent.getUnearnedReport();
        });

        $("#slcBrokerFeesAccount, #slcBrokerFeesCoverageTypes").on("change", function () {
            UnearnedBrokerFeesReportComponent.filterReport();
        });

        $("#txtBrokerFeesPolicyNo").on("keyup", function () {
            UnearnedBrokerFeesReportComponent.filterReport();
        });

        $("#btnExportBrokerFeesReport").on("click", function () {
            var asOf = $("#txtBrokerFeesDateAsOf").val() == '' ? new Date().toLocaleDateString() : $("#txtBrokerFeesDateAsOf").val();
            window.open('/api/reports/brokerfees/unearned/download?asOf=' + asOf, '_blank');
        });

        UnearnedBrokerFeesReportComponent.getUnearnedReport();
    }
}