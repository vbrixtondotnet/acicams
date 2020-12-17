var CurrentAccount = {};

var AccountListMenu = {
    init: function () {
        this.getAccounts();
        this.getExpiringAccounts();
        this.initEventHandlers();
    },
    initEventHandlers: function () {
        $("html").on("click", "a.account-menu-item", function () {
            var id = $(this).attr('data-id');
            $(".account-menu-item").parent().removeClass('active');
            $(this).parent().addClass('active');
            AccountListMenu.getAccountDetails(id);

        });
        $("#btnAddAccount").on("click", function () {
            $("#mdlUpdateAccount").modal('show');
            var newAccount = AccountModel.new();
            AccountDetails.populateAccountForm(newAccount);
            AccountDetails.getAgents();

        });
    },
    getAccountDetails: function (id) {
        $("#accountDetailsPreloader").removeClass('hide');
        $("#accountDetailsRow").addClass('hide');
        AccountService.getAccountDetails(id, function (data) {
            CurrentAccount = data;
            AccountDetails.displayDetails();
        });
        Contacts.getContacts(id)
        Drivers.getDrivers(id);
    },
    getAccounts: function (id = 0) {
        AccountService.getAccounts(function (data) {
            $("#accountlistMenu-accounts").html('');
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    var userRow = `<li class="nav-item  ">
                                        <a href="javascript:" class="nav-link account-menu-item" data-id="`+ data[i].accountId + `">
                                            <strong class="title">`+ data[i].legalName + `</strong>
                                        </a>
                                    </li>`;

                    $("#accountlistMenu-accounts").append(userRow);
                }

                if (id == 0) {
                    $("a.account-menu-item").first().click();
                }
                else {
                    $(".menu-account-list.scroller").animate({
                        scrollTop: $(".nav-link.account-menu-item[data-id='" + id + "']").offset().top
                    }, 2000);
                    $("a.account-menu-item[data-id='" + id + "']").click();
                }
            }
        });
    },
    getExpiringAccounts: function () {
        AccountService.getExpiringAccounts(function (data) {
            $("#expiringAccountlistMenu-accounts").html('');
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    var userRow = `<li class="nav-item  ">
                                        <a href="javascript:" class="nav-link account-menu-item" data-id="`+ data[i].accountId + `">
                                            <strong class="title">`+ data[i].legalName + `</strong>
                                        </a>
                                    </li>`;

                    $("#expiringAccountlistMenu-accounts").append(userRow);
                }
            }
        });
    }
}