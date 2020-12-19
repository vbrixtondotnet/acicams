var Authentication = {
    checkToken: function () {
        var token = localStorage.getItem('accessToken');
        if (token == null)
            location.href = "/Login";
        else {
            var role = this.getRole();
            var page = Utility.getCurrentPage();

            if (page == "Admin" && role != "Admin") location.href = "/Accounts"; 
        }
    },
    checkUserLogIn: function () {
        var token = localStorage.getItem('accessToken');
        if (token != null)
            location.href = "/Accounts";
    },
    logOut: function () {
        localStorage.clear();
        location.href = "/Login";
    },
    userInfo: function () {
        return JSON.parse(localStorage.getItem('userInfo'));
    },
    getRole: function () {
        var userInfo = this.userInfo();
        return userInfo.role;
    },
    signIn: function (username, password, callback) {
        var data = {
            userName: username,
            password: password
        };

        $.ajax({
            type: "POST",
            url: "/api/authentication",
            data: JSON.stringify(data),
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                localStorage.setItem('accessToken', data.token);
                localStorage.setItem('userInfo', JSON.stringify(data.user));
                location.href = "/Accounts";
            },
            error: function (errMsg) {
                debugger;
                if (callback) callback();
            }
        });
    }
}
