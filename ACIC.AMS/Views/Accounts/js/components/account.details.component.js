
var AccountDetails = {
    mailingCities: [],
    garagingCities: [],
    account: null,
    init: function () {
        this.initEventHandlers();
    },
    displayDetails: function () {
        $("#accountDetailsLegalName").html(CurrentAccount.legalName);
        $("#accountDetailsDba").html(CurrentAccount.dba);
        $("#accountDetailsStatus").html(CurrentAccount.status);
        $("#accountDetailsTaxId").html(CurrentAccount.taxId);
        $("#accountDetailsType").html(CurrentAccount.type);
        $("#accountDetailsUsdot").html(CurrentAccount.usdot);
        $("#accountDetailsOperation").html(CurrentAccount.operationType);
        $("#accountDetailsStatePermit").html(CurrentAccount.statePermit);
        $("#accountDetailsRadius").html(CurrentAccount.operationRadius);
        $("#accountDetailsMailing").html(CurrentAccount.mailingAddress);
        $("#accountDetailsSource").html(CurrentAccount.source);
        $("#accountDetailsGaraging").html(CurrentAccount.garageAddressComplete);
        $("#accountDetailsAgent").html(CurrentAccount.agent);
        $("#accountDetailsPreloader").addClass('hide');
        $("#accountDetailsRow").removeClass('hide');
    },
    populateAccountForm: function (data) {
        AccountDetails.account = data;
        BindingService.bindModelToForm("frmAccountDetails", AccountDetails.account);
        $("#slcMailingState").trigger('change');
        $("#slcGaragingState").trigger('change');

    },
    getAgents: function () {
        $("#slcAgent").html('');
        AgentService.getAgents(function (data) {
            $("#slcAgent").append('<option value=""></option>');
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var agent = data[i];
                    var agentRow = '<option value="' + agent.agentId + '">' + agent.firstName + ' ' + agent.lastName + '</option>';
                    $("#slcAgent").append(agentRow);
                    $("#slcAgent").val(CurrentAccount.agentId);
                }
            }
        });
    },
    populateMailingZip: function () {
        $("#slcMailingZip").html('');
        var cityId = $("#slcMailingCity").val();
        var city = AccountDetails.mailingCities.find((c) => { return c["id"] === parseInt(cityId) });
        if (city != null) {
            var zips = city.zips.split(' ');
            for (var i = 0; i < zips.length; i++) {
                var zip = zips[i];
                $("#slcMailingZip").append('<option value="' + zip + '">' + zip + '</option>');
            }
            $("#slcMailingZip").val(CurrentAccount.zip);
        }
    },
    populateGaragingZip: function () {
        $("#slcGaragingZip").html('');
        var cityId = $("#slcGaragingCity").val();
        var city = AccountDetails.garagingCities.find((c) => { return c["id"] === parseInt(cityId) });
        if (city != null) {
            var zips = city.zips.split(' ');
            for (var i = 0; i < zips.length; i++) {
                var zip = zips[i];
                $("#slcGaragingZip").append('<option value="' + zip + '">' + zip + '</option>');
            }
            $("#slcGaragingZip").val(CurrentAccount.garageZip);
        }
    },
    getCitiesByMailingState: function () {
        $("#slcMailingCity").html('');
        var state = $("#slcMailingState").val();
        $("#slcMailingCity").attr("disabled", true);
        $("#slcMailingZip").attr("disabled", true);
        StateService.getCities(state, function (data) {
            AccountDetails.mailingCities = data;
            if (AccountDetails.mailingCities.length > 0) {
                for (var i = 0; i < AccountDetails.mailingCities.length; i++) {
                    var city = AccountDetails.mailingCities[i];
                    var cityRow = '<option value="' + city.id + '">' + city.city + '</option>';
                    $("#slcMailingCity").append(cityRow);
                }
            }
            $("#slcMailingCity").val(CurrentAccount.city).trigger('change');
            $("#slcMailingCity").removeAttr("disabled");
            $("#slcMailingZip").removeAttr("disabled");
        });
    },
    getCitiesByGaragingState: function () {
        $("#slcGaragingCity").html('');
        var state = $("#slcGaragingState").val();
        $("#slcGaragingCity").attr("disabled", true);
        $("#slcGaragingZip").attr("disabled", true);
        StateService.getCities(state, function (data) {
            AccountDetails.garagingCities = data;
            if (AccountDetails.garagingCities.length > 0) {
                for (var i = 0; i < AccountDetails.garagingCities.length; i++) {
                    var city = AccountDetails.garagingCities[i];
                    var cityRow = '<option value="' + city.id + '">' + city.city + '</option>';
                    $("#slcGaragingCity").append(cityRow);
                }
            }
            $("#slcGaragingCity").removeAttr("disabled");
            $("#slcGaragingZip").removeAttr("disabled");
            $("#slcGaragingCity").val(CurrentAccount.garageCity).trigger('change');
        });
    },
    saveAccount: function () {
        App.blockUI({
            target: "#detailsFormContainer",
            blockerOnly: true
        });
        AccountService.saveAccountDetails(AccountDetails.account, function (data) { 
            
            App.unblockUI("#detailsFormContainer");
            $("#mdlUpdateAccount").modal('hide');

            if (AccountDetails.account.accountId == 0) {
                AccountListMenu.getAccounts(data.accountId);
            }
            else {
                CurrentAccount = data;
                AccountDetails.displayDetails();
            }

        }); 
    },
    initEventHandlers: function () {
        $("#btnUpdateAccount").click(function () {
            $("#mdlUpdateAccount").modal('show');
            AccountDetails.getAgents();

            App.blockUI({
                target: "#detailsFormContainer",
                blockerOnly: true
            });
            AccountService.getAccount(CurrentAccount.accountId, function (data) {
               
                AccountDetails.populateAccountForm(data);
                App.unblockUI("#detailsFormContainer");
            });
        });

        $("#btnAddContact").click(function () {
            $("#mdlAddContact").modal('show');
        });

        $("#btnAddPolicy").click(function () {
            $("#mdlPolicy").modal('show');
        });

        $("#btnAddDriver").click(function () {
            $("#mdlDriver").modal('show');
        });

        $("#btnAddVehicle").click(function () {
            $("#mdlVehicle").modal('show');
        });

        $("#btnAddEndorsement").click(function () {
            $("#mdlEndorsement").modal('show');
        });

        $("#slcMailingState").on("change", function () {
            AccountDetails.getCitiesByMailingState();
        });

        $("#slcGaragingState").on("change", function () {
            AccountDetails.getCitiesByGaragingState();
        });

        $("#slcMailingCity").on("change", function () {
            AccountDetails.populateMailingZip();
        });

        $("#slcGaragingCity").on("change", function () {
            AccountDetails.populateGaragingZip();
        });

        $("#frmAccountDetails").on('submit', function () {
            AccountDetails.saveAccount();
            return false;
        });

       
    }
}