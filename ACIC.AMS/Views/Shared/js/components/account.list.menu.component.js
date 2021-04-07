var CurrentAccount = {};

var AccountListMenu = {
    init: function () {
        this.getAccounts();
        this.getExpiringAccounts();
        this.initEventHandlers();
    },
    initEventHandlers: function () {
        $("html").on("click", "a.account-menu-item", function () {
            if (Utility.getCurrentPage() == "Accounts") {
                var id = $(this).attr('data-id');
                $(".account-menu-item").parent().removeClass('active');
                $(this).parent().addClass('active');
                AccountListMenu.getAccountDetails(id);
            }
            else {
                location.href = "/Accounts";
            }

        });
        $("#btnAddAccount").on("click", function () {
            $("#mdlUpdateAccount").modal('show');
            $("#accountDetailsFormTitle").html("Add Account");
            var newAccount = AccountModel.new();
            AccountDetails.populateAccountForm(newAccount);

            $("#slcType").val = 1;
        });
        $("#txtAccountListFilter").on("keyup", function () {
            AccountListMenu.filterList();
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
        DriversComponent.getDrivers(id);
        VehiclesComponent.getVehicles(id);
        PolicyComponent.getAvailableCoverageTypes(id);
        PolicyComponent.getPolicies(id);
        EndorsementComponent.getEndorsements(id, 'all');
    },
    getAccounts: function (id = 0) {
        AccountService.getAccounts(function (data) {
            $("#accountlistMenu-accounts").html('');
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    var userRow = `<li class="nav-item account">
                                        <a href="javascript:" class="nav-link account-menu-item" data-id="`+ data[i].accountId + `">
                                            <strong class="title">`+ data[i].legalName + `</strong>
                                        </a>
                                    </li>`;

                    $("#accountlistMenu-accounts").append(userRow);
                }

                if (Utility.getCurrentPage() == "Accounts") {
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
    },
    filterList: function () {
        var searchval = $("#txtAccountListFilter").val();
        $("li.nav-item.account").each(function () {
            if (searchval == '') {
                $(this).removeClass('hide');
            }
            else {
                $(this).addClass('hide');
                var item = $(this).find('strong').html().toLowerCase();
                if (item.indexOf(searchval.toLowerCase()) >= 0)
                    $(this).removeClass('hide');
            }

        });
    }
}