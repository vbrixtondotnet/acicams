var EndorsementService = {
    getEndorsements: function (id, type, search = '', callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + '/endorsements/' + type + '?searchText=' + search,
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getAvailableVehicles: function (id, type, action, callback) {
        //accounts/{id}/endorsements/{type}/available-vehicles/{action}
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + "/endorsements/" + type + "/available-vehicles/" + action,
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}