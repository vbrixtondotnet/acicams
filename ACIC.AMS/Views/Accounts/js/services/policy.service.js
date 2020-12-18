var PolicyService = {
    getAvailableCoverageTypes: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + '/coveragetypes',
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}