var PolicyService = {
    getAvailableCoverageTypes: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + '/coveragetypes',
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getPolicies: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + '/policy',
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getCarriers: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/carriers",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getMgas: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/mga",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getPremiumFinancers: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/banks/premium-financers",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    savePolicy: function (policyModel, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: "/api/accounts/" + policyModel.accountId + "/policy",
            data: JSON.stringify(driverEndorsementModel),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (successCallback) successCallback(data);
            },
            error: function (data) {
                if (errorCallback) errorCallback(data);
            }
        });
    },
}