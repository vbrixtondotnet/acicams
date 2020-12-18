var DriverService = {
    getDrivers: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + "/drivers",
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
    saveNewDriver: function (driverEndorsementModel, callback) {
        $.ajax({
            type: "POST",
            url: "/api/accounts/" + driverEndorsementModel.accountId + "/drivers",
            data: JSON.stringify(driverEndorsementModel),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}