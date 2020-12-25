var UserService = {
    saveUser: function (user, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: "/api/users",
            data: JSON.stringify(user),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (successCallback) successCallback(data)
            },
            error: function (err) {
                if (errorCallback) errorCallback(err.responseText)
            }
        });
    },
    getActiveUsers: function (successCallback) {
        $.ajax({
            type: "GET",
            url: "/api/users",
            success: function (data) {
                if (successCallback)
                    successCallback(data);
            }
        });
    },
    getDeletedUsers: function (successCallback) {
        $.ajax({
            type: "GET",
            url: "/api/users?deleted=true",
            success: function (data) {
                if (successCallback)
                    successCallback(data);
            }
        });
    },
    setStatus: function (id, active, successCallback) {
        $.ajax({
            type: "POST",
            url: "/api/users/" + id + "/active/" + active,
            success: function (data) {
                if (successCallback)
                    successCallback();
            }
        });
    },
    deleteUser: function (id, successCallback) {
        $.ajax({
            type: "DELETE",
            url: "/api/users/" + id,
            success: function (data) {
                if (successCallback)
                    successCallback();
            }
        });
    },
    getUserDetails: function (id, successCallback) {
        $.ajax({
            type: "GET",
            url: "/api/users/" + id,
            success: function (data) {
                if (successCallback)
                    successCallback(data);
            }
        });
    }

}