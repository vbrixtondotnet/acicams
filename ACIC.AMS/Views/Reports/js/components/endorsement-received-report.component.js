var EndorsementReceivedReportComponent = {
    receivedReportData: null,
    accounts: [],
    selectedReport: null,
    updateEndorsementAmountTimeout: null,
    selectedGroupId: null,
    getReceivedReport: function () {
        ReportService.getReceivedEndorsementsReport(function (data) {
            EndorsementReceivedReportComponent.receivedReportData = data;
            EndorsementReceivedReportComponent.renderReceivedReport(EndorsementReceivedReportComponent.receivedReportData);
            EndorsementReceivedReportComponent.getUniqueAccounts();
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
    renderReceivedReport: function (reportData) {
        $("#receivedReportTable").html('');
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
                var downpaymentBtnClass = report.downPayment ? 'btn-info' : 'btn-default';
                var downpaymentBtnText = report.downPayment ? 'Received' : 'Not Received';
                var paymentStatusBtnClass = report.paymentStatus ? 'btn-info' : 'btn-default';
                var paymentStatusBtnText = report.paymentStatus ? 'Paid' : 'Unpaid';
                var financeRef = report.financeRef == null ? '' : report.financeRef;

                totalPremium += report.premium;
                totalSlTaxes += report.slTaxes;
                totalFees += report.fees;
                totalCommission += report.commission;
                totalPayableAmount += report.payableAmount;
                var reference = report.financeRef == '' || report.financeRef == null ? 'Reference' : report.financeRef;
                
                var reportId = report.accountId + '-' + report.ern + '-' + report.coverageTypes + '-' + report.policyId;

                var reportRow = `<div class="col-md-12 table-row received">
                                    <div class="col-md-1" style="width:15%; text-align:left; font-size:13px;padding-left:5px;"><span>`+ report.legalName +`</span><br /><span>`+report.coverageTypeDesc+`</span></div>
                                    <div class="col-md-1" style="width:18%; text-align:left; font-size:13px;"><span>`+report.carrierName+`</span><br /><span>`+report.mganame+`</span></div>
                                    <div class="col-md-1" style="width:4%; text-align:center"><span>`+ report.ern + `</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:right"><span>`+ ValidationService.formatMoney(premium) +`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+ slTaxes+`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+ fees+`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+ commission+`</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><span>`+ payableAmount + `</span></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><button data-id="`+ reportId + `" class="update-dp btn ` + downpaymentBtnClass + `" style="padding:2px 5px 2px 5px; border:1px dashed gray;width:100px;">` + downpaymentBtnText +`</button></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><button data-id="`+ reportId + `" class="update-paymentstatus btn `+ paymentStatusBtnClass + `" style="padding:2px 5px 2px 5px; border:1px dashed gray;width:100px;">` + paymentStatusBtnText+`</button></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><button data-financeref="`+ financeRef + `" data-id="` + reportId + `" class="update-finance-ref btn btn-default" style="padding:2px 5px 2px 5px; border:1px dashed gray;width:100px;">` + reference+`</button></div>
                                    <div class="col-md-1" style="width:7%; text-align:center"><button data-id="`+ reportId + `" class="update-endt-items btn btn-info" style="padding:5px;font-size:12px;">Update Endt <br />Items</button></div>
                                </div>`;
                $("#receivedReportTable").append(reportRow);
            }

            $("#subhTotalPremium").html(totalPremium.toFixed(2));
            $("#subhTotalSLTaxes").html(totalSlTaxes.toFixed(2));
            $("#subhTotalFees").html(totalFees.toFixed(2));
            $("#subhTotalCommission").html(totalCommission.toFixed(2));
            $("#subhTotalPayableAmount").html(totalPayableAmount.toFixed(2));
        }
    },
    getUniqueAccounts: function () {
        $("#slcAccount").html('<option value="">&nbsp</option>');
        var retval = [];
        if (EndorsementReceivedReportComponent.receivedReportData != null && EndorsementReceivedReportComponent.receivedReportData.length > 0) {

            for (var i = 0; i < EndorsementReceivedReportComponent.receivedReportData.length; i++) {
                var reportData = EndorsementReceivedReportComponent.receivedReportData[i];
                var account = retval.find((p) => { return p["accountId"] === reportData.accountId });
                if (!account) retval.push(reportData);
            }
        }

        EndorsementReceivedReportComponent.accounts = retval;
        for (var i = 0; i < EndorsementReceivedReportComponent.accounts.length; i++) {
            var account = EndorsementReceivedReportComponent.accounts[i];
            $("#slcAccount").append('<option value="' + account.accountId + '">' + account.legalName + '</option>');
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
        $("#slcCoverageTypes, #slcStatus, #slcAccount").on("change", function () {
            EndorsementReceivedReportComponent.filterReceivedReports();
        });
        $("#txtEndorsementNo").on("keyup", function () {
            EndorsementReceivedReportComponent.filterReceivedReports();
        });
        $("#btnClearFilters").on("click", function () {
            $("#slcCoverageTypes").val("");
            $("#txtEndorsementNo").val("");
            $("#slcStatus").val('');
            $("#slcAccount").val("").trigger('change');
        });
        $("#btnExportRecievedEndReport").on("click", function () {
            window.open('/api/reports/endorsements/received/download', '_blank');
        });
        $("html").on("click", 'button.update-dp', function () {
            var reportId = $(this).attr('data-id').split('-');
            var status = $(this).hasClass('btn-default') ? 1 : 0;
            EndorsementReceivedReportComponent.updateDownPayment(reportId[0], reportId[1], reportId[2], reportId[3], status);
        });
        $("html").on("click",'button.update-paymentstatus', function () {
            var reportId = $(this).attr('data-id').split('-');
            var status = $(this).hasClass('btn-default') ? 1 : 0;
            EndorsementReceivedReportComponent.updatePaymentStatus(reportId[0], reportId[1], reportId[2], reportId[3], status);
        });

        $("html").on("click", 'button.update-finance-ref', function () {
            EndorsementReceivedReportComponent.selectedReport = $(this).attr('data-id').split('-');
            var financeRef = $(this).attr('data-financeref');
            $("#txtFinanceReference").val(financeRef);
            $("#mdlFinanceReference").modal('show', { backdrop: 'static' });
        });

        $("html").on("click", '#btnSaveFinancingRef', function () {
            EndorsementReceivedReportComponent.updateFinanceRef();
        });

        $("html").on("click", 'button.update-endt-items', function () {
            var dataId = $(this).attr('data-id');
            var reportId = dataId.split('-');
            $("#btnExportItems").attr('data-id', dataId);
            EndorsementReceivedReportComponent.selectedGroupId = reportId;
            EndorsementReceivedReportComponent.updateEndtItems(reportId);
        });

        $("#btnExportItems").on("click", function () {
            var reportId = $(this).attr('data-id').split('-');
            window.open('/api/reports/endorsements/endorsement-report-items/download?accountId=' + reportId[0] + '&ern=' + reportId[1] + '&coverageType=' + reportId[2] + '&policyId=' + reportId[3], '_blank');
        });

        $("html").on("keydown", "input.endorsement-amount", function () {
            clearTimeout(EndorsementReceivedReportComponent.updateEndorsementAmountTimeout);
        });

        $("html").on("keyup", "input.endorsement-amount", function () {
            var id = $(this).attr('data-id');
            var field = $(this).attr('data-field');
            var amount = $(this).val();
            EndorsementReceivedReportComponent.updateEndorsementAmount(id, field, amount);
        });

        $('a.endorsement[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("href") // activated tab
            if (target == "#tbPending") {
                EndorsementPendingReportComponent.init();
            }
            else if (target == "#tbPayable") {
                EndorsementPayableReportComponent.init();
            }
        });
    },
}