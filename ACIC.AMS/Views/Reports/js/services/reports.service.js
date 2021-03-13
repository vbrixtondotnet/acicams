var ReportService = {
    getReceivedEndorsementsReport: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/reports/endorsements/received",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getReceivedEndorsementsReportItems: function (accountId, ern, coverageType, policyId, callback) {
        $.ajax({
            type: "GET",
            url: "/api/endorsements/endorsement-report-items?accountId=" + accountId + "&ern=" + ern + "&coverageType=" + coverageType + "&policyId=" + policyId,
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getPendingEndorsementsReportItems: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/reports/endorsements/pending",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getPayableEndorsementsReportItems: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/reports/endorsements/payable",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    updateDownPayment: function (accountId, ern, coverageType, policyId, status, callback) {
        //int accountId, string ern, int coverageType, int policyId, int status
        //
        $.ajax({
            type: "PATCH",
            url: "/api/endorsements/update-downpayment",
            data: JSON.stringify({ accountId: accountId, ern: ern, coverageType: coverageType, policyId: policyId, status: status }),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                if (callback) callback();
            }
        });
    },
    updateAmount: function (enditId, field, amount, callback) {
        //
        $.ajax({
            type: "PATCH",
            url: "/api/endorsements/update-amount",
            data: JSON.stringify({ endtId: enditId, field: field, amount: amount}),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                if (callback) callback();
            }
        });
    },
    updatePaymentStatus: function (accountId, ern, coverageType, policyId, status, callback) {
        $.ajax({
            type: "PATCH",
            url: "/api/endorsements/update-payment-status",
            data: JSON.stringify({ accountId: accountId, ern: ern, coverageType: coverageType, policyId: policyId, status: status }),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                if (callback) callback();
            }
        });
    },
    updateDueDate: function (accountId, ern, coverageType, policyId, dueDate, callback) {
        $.ajax({
            type: "PATCH",
            url: "/api/endorsements/update-due-date",
            data: JSON.stringify({ accountId: accountId, ern: ern, coverageType: coverageType, policyId: policyId, dueDate: dueDate }),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                if (callback) callback();
            }
        });
    },
    markAsPaid: function (accountId, ern, coverageType, policyId, callback) {
        $.ajax({
            type: "PATCH",
            url: "/api/endorsements/mark-as-paid",
            data: JSON.stringify({ accountId: accountId, ern: ern, coverageType: coverageType, policyId: policyId }),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                if (callback) callback();
            }
        });
    },
    updateFinancingReference: function (accountId, ern, coverageType, policyId, financingReference, callback) {
        $.ajax({
            type: "PATCH",
            url: "/api/endorsements/update-finance-reference",
            data: JSON.stringify({ accountId: accountId, ern: ern, coverageType: coverageType, policyId: policyId, financingReference: financingReference }),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                if (callback) callback();
            }
        });
    }
}