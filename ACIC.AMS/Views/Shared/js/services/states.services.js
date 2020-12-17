var StateService = {
    getCities: function (stateId, callback) {
        $.ajax({
            type: "GET",
            url: "/api/states/" + stateId,
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}