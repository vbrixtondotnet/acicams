var Utility = {
    getCurrentPage: function () {
        var url = window.location.href.split('?')[0].split('/');
        return url[url.length - 1];
    },
    setActivePage: function () {
        $("li.main-nav").removeClass('active');
        var currentPage = this.getCurrentPage();
        switch (currentPage.toUpperCase()) {
            case "ADMIN":
                $("li.main-nav-admin").addClass('active');
                break;
            case "ACCOUNTS":
                $("li.main-nav-accounts").addClass('active');
                break;
            case "ENDORSEMENTS":
                $("li.main-nav-reports").addClass('active');
                break;
            case "AGENTCOMMISSIONS":
                $("li.main-nav-commissions").addClass('active');
                break;
        }
    }
}

$(document).ready(function () {
    Utility.setActivePage();
    if (Authentication.getRole() != 'Admin') {
        $("li.main-nav.main-nav-admin").remove();
    }
});