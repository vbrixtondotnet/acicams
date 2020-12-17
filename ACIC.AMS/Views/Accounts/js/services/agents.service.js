var AgentService = {
    getAgents: function (callback) {
        $.ajax({
            type: "GET",
            url: "/api/agents",
            success: function (data) {
                if (callback) callback(data);
            }
        });
    }
}