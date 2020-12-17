var ContactService = {
    getContacts: function (id, callback) {
        $.ajax({
            type: "GET",
            url: "/api/accounts/" + id + "/contacts",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    getTitles: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/contacts/titles",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    saveContact: function (contact, callback) {
        $.ajax({
            type: "POST",
            url: "/api/contacts",
            data: JSON.stringify(contact),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}