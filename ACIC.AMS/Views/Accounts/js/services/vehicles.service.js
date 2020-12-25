var VehicleService = {
    getVehicles: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + "/vehicles",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getVehicleMakes: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/vehicles/makes",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getVehicleHistory: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/vehicles/" + id + "/history",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getLienHolders: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/banks/lien-holders",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    saveVehicle: function (vehicleEndorsementModel, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: "/api/accounts/" + vehicleEndorsementModel.accountId + "/vehicles",
            data: JSON.stringify(vehicleEndorsementModel),
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
    updateVehicle: function (vehicle, successCallback, errorCallback) {
        $.ajax({
            type: "PATCH",
            url: "/api/vehicles/" + vehicle.accountId,
            data: JSON.stringify(vehicle),
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