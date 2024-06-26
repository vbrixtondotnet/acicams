﻿var DriverService = {
    getDriversByAccount: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + "/drivers",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getDrivers: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/drivers",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getDriverHistory: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/drivers/" + id + "/history",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    deleteDriver: function (id, callback) {
        $.ajax({
            type: "DELETE",
            url: "/api/drivers/" + id ,
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    saveNewDriver: function (driverEndorsementModel, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: "/api/accounts/" + driverEndorsementModel.accountId + "/drivers",
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
    updateDriver: function (driver, callback) {
        $.ajax({
            type: "PATCH",
            url: "/api/drivers",
            data: JSON.stringify(driver),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}