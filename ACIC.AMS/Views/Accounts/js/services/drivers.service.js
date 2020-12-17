var DriverService = {
    getDrivers: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + "/drivers",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}