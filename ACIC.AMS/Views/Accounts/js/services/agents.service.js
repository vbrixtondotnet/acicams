var AgentService = {
    getAgents: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/agents",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    },
    saveAgent: function (agent, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: "/api/agents",
            data: JSON.stringify(agent),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                if (successCallback) successCallback(data);
            },
            error: function (data) {
                if (errorCallback) errorCallback(data);
            }
        });
    }
}