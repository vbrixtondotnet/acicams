var AccountService = {
    getAccountDetails: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + '/details',
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getAccount: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id,
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getAccounts: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getExpiringAccounts: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts?expiring=true",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    saveAccountDetails: function (accountModel, callback) {
        $.ajax({
            type: "POST",
            url: "/api/accounts",
            data: JSON.stringify(accountModel),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}