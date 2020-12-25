var Vehicles = {
    dTable: null,
    vehicles: null,
    vehicleMakes: null,
    vehicle: null,
    lienHolders: null,
    vehicleEndorsement: null,
    init: function () {
        this.initEventHandlers();
        this.getLienHolders();
        this.getVehicleMakes();
    },
    addVehicle: function () {
        Vehicles.populateDrivers();
        Vehicles.populateCoverageTypes();
        Vehicles.vehicleEndorsement = VehicleEndorsementModel.new();
        Vehicles.vehicleEndorsement.accountId = CurrentAccount.accountId;

        $("#mdlVehicle").modal({
            backdrop: 'static'
        });

        BindingService.bindModelToForm("frmVehicle", Vehicles.vehicleEndorsement);
    },
    calculateCoverageTotal: function (coverageTypeId, coverageType) {

        coverageTypeId = parseInt(coverageTypeId);

        var policy = Policy.availableCoverageTypes.find((c) => { return c["id"] === coverageTypeId});
        var typeId = parseInt($("#slcVehicleType").val());
        var premium = null;
        var tax = null;
        var brokerFee = null;
        if (coverageTypeId != 3) {
            switch (typeId) {
                case 1:
                case 3:
                    premium = policy.basePerUnit == null ? null : policy.basePerUnit.toFixed(2);
                    tax = (premium == null || policy.strate == null) ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
                    brokerFee = (policy.basePerUnit == null || policy.bfrate == null) ? null : policy.bfrate.toFixed(2);
                    break;
                case 2:
                    premium = policy.trailerRate == null ? null : policy.trailerRate.toFixed(2);
                    tax = policy.trailerRate == null || policy.strate == null ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
                    break;
            }
        }
        else {
            var pdValue = $("#txtPdValue").val() == '' ? 0 : parseFloat($("#txtPdValue").val());
            switch (typeId) {
                case 1:
                case 3:
                    premium = pdValue == null ? null : parseFloat(pdValue * (policy.pdrate / 100)).toFixed(2);
                    tax = (premium == null || policy.strate == null) ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
                    brokerFee = (premium == null || policy.bfrate == null) ? null : policy.bfrate.toFixed(2);
                    break;
                case 2:
                    var rate = policy.trailerRate == null ? policy.pdrate : policy.trailerRate;
                    premium = (rate == null || pdValue == 0) ? null : parseFloat(pdValue * (rate / 100)).toFixed(2);
                    tax = policy.trailerRate == null || policy.strate == null ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
                    break;
            }
        }
        

        //var premiumTax = parseFloat(coverageType.premiumTax);
        //var brokerFee = parseFloat(coverageType.brokerFee);

        coverageType.premium = premium;
        coverageType.premiumTax = tax;
        coverageType.brokerFee = brokerFee;

        coverageType.totalAmount = parseFloat(coverageType.premium == null ? 0 : coverageType.premium) + parseFloat(coverageType.premiumTax == null ? 0 : coverageType.premiumTax) + parseFloat(coverageType.brokerFee == null ? 0 : coverageType.brokerFee);

        $("input[type='text'][data-coverage-field='premium'][data-id='" + coverageTypeId + "']").val(coverageType.premium);
        $("input[type='text'][data-coverage-field='premiumTax'][data-id='" + coverageTypeId + "']").val(coverageType.premiumTax);
        $("input[type='text'][data-coverage-field='brokerFee'][data-id='" + coverageTypeId + "']").val(coverageType.brokerFee);
        $("input[type='text'][data-coverage-field='totalAmount'][data-id='" + coverageTypeId + "']").val(coverageType.totalAmount.toFixed(2));
        //coverageType.totalAmount = premium + premiumTax + brokerFee;
        //$("input[type='text'][data-coverage-field='totalAmount'][data-id='" + coverageTypeId + "']").val(coverageType.totalAmount);

        Vehicles.calculateTotalPremium();
    },
    calculateTotalPremium: function () {
        var totalPremium = 0;
        var totalPremiumTax = 0;
        var totalBrokerFee = 0;
        var totalAmount = 0;
        for (var i = 0; i < Vehicles.vehicleEndorsement.vehicleCoverages.length; i++) {
            var coverageType = Vehicles.vehicleEndorsement.vehicleCoverages[i];

            var premium = parseFloat(coverageType.premium == null ? 0 : coverageType.premium);
            var premiumTax = parseFloat(coverageType.premiumTax == null ? 0 : coverageType.premiumTax);
            var brokerFee = parseFloat(coverageType.brokerFee == null ? 0 : coverageType.brokerFee);
            var totalCoverageAmount = premium + premiumTax + brokerFee;

            totalPremium += premium;
            totalPremiumTax += premiumTax;
            totalBrokerFee += brokerFee;
            totalAmount += totalCoverageAmount;
        }

        $("input[type='text'].coverage-totalPremium").val(totalPremium.toFixed(2));
        $("input[type='text'].coverage-totalPremiumTax").val(totalPremiumTax.toFixed(2));
        $("input[type='text'].coverage-totalBrokerFee").val(totalBrokerFee.toFixed(2));
        $("input[type='text'].coverage-totalAmount").val(totalAmount.toFixed(2));
    },
    getVehicles: function (id) {
        $("#vehiclesPreloader").removeClass('hide');
        $(".vehicle-list-content").addClass('hide');
        $("#tblVehicles").html('');
        VehicleService.getVehicles(id, function (data) {
            $(".vehicle-list-content").removeClass('hide');
            $("#vehiclesPreloader").addClass('hide');
            Vehicles.renderVehiclesTable(data);
        });
    },
    getVehicleMakes: function () {
        VehicleService.getVehicleMakes(function (data) {
            Vehicles.vehicleMakes = data;
            Vehicles.populateVehicleMakes();
        });
    },
    populateDrivers: function () {
        $(".vehicle-drivers").html('');
        $(".vehicle-drivers").append('<option value=""></option>');
        for (var i = 0; i < Drivers.drivers.length; i++) {
            $(".vehicle-drivers").append('<option value="' + Drivers.drivers[i].driverId + '">' + Drivers.drivers[i].fullName + '</option>');
        }
    },
    populateLienHolders: function () {
        var data = Vehicles.lienHolders
        $(".vehicle-banks").html('');
        $(".vehicle-banks").html('<option value =""> </option >');
        for (var i = 0; i < data.length; i++) {
            $(".vehicle-banks").append('<option value="' + data[i].bankId + '">' + data[i].bankName + '</option>');
        }
    },
    populateActivePoliciesTable: function () {
        $("#tblVehicleActivePolicies").html('');
        var activePolicies = Policy.availableCoverageTypes;
        if (activePolicies.length > 0) {
            for (var i = 0; i < activePolicies.length; i++) {
                var policy = activePolicies[i];
                $("#tblVehicleActivePolicies").append(`<tr>
                                                    <td>`+ policy.policyNumber+`</td>
                                                    <td>`+ policy.name +`</td>
                                                    <td>`+ policy.company +`</td>
                                                </tr>`);
            }
        }
    },
    populateCoverageTypes: function () {

        $("#availableVehicleCoverageTypes").html('');
        var data = Policy.availableCoverageTypes;
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var coverageType = data[i];
                var coverageTypeRow = `<tr role="row">
                                                <td class='pad8'>
                                                    <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                        <input type="checkbox" class="checkboxes coverage-type" data-id="`+ coverageType.id + `">
                                                        <span></span>
                                                    </label>
                                                </td>
                                                <td colspan="2" class='pad8'>
                                                    `+ coverageType.name + `
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type" data-coverage-field="premium" disabled onkeypress="return ValidationService.isNumberKey(this, event);" data-id="`+ coverageType.id + `"/>
                                                </td>
                                                <td class="center">  
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type" data-coverage-field="premiumTax" disabled onkeypress="return ValidationService.isNumberKey(this, event);" data-id="`+ coverageType.id + `"/> 
                                                </td>
                                                <td>  
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type" data-coverage-field="brokerFee" disabled onkeypress="return ValidationService.isNumberKey(this, event);" data-id="`+ coverageType.id + `"/> 
                                                </td>
                                                <td>
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type-total totals" data-coverage-field="totalAmount" disabled data-id="`+ coverageType.id + `"/>
                                                </td>
                                            </tr>`;

                $("#availableVehicleCoverageTypes").append(coverageTypeRow);
            }
            var coverageTypeRow = `<tr role="row">
                                                <td class='pad8'></td>
                                                <td colspan="2" class='pad8'><strong>TOTAL</strong></td>
                                                <td>
                                                    <input type="text" class="form-control coverage-totalPremium totals" disabled placeholder="0.00" />
                                                </td>
                                                <td class="center">  
                                                    <input type="text" class="form-control coverage-totalPremiumTax totals" disabled placeholder="0.00"/>
                                                </td>
                                                <td>  
                                                    <input type="text" class="form-control coverage-totalBrokerFee totals" disabled placeholder="0.00"/>
                                                </td>
                                                <td>
                                                    <input type="text" class="form-control coverage-totalAmount totals" disabled placeholder="0.00"/>
                                                </td>
                                            </tr>`;

            $("#availableVehicleCoverageTypes").append(coverageTypeRow);
            $("#btnSaveNewVehicle").removeAttr("disabled");
        }
        else {
            var coverageTypeRow = `<tr class="gradeX" role="row">
                                                <td class='pad8 padb8' colspan="8">
                                                <span style="color:red;"><p class="font-red-mint"><i class="fa fa-exclamation-triangle"></i>&nbsp; The selected account does not have any active policy. Please create a new Policy for this account before you can add a new Driver. </p></span>
                                                </td>
                                            </tr>`;

            $("#availableVehicleCoverageTypes").append(coverageTypeRow);
        }
    },
    populateVehicleMakes: function () {
        $(".vehicle-makes").html('');
        $(".vehicle-makes").append('<option value=""></option>');
        for (var i = 0; i < Vehicles.vehicleMakes.length; i++) {
            $(".vehicle-makes").append('<option value="' + Vehicles.vehicleMakes[i].vmid + '">' + Vehicles.vehicleMakes[i].vehMakeName + '</option>');
        }
    },
    getLienHolders: function () {
        VehicleService.getLienHolders(function (data) {
            Vehicles.lienHolders = data;
            Vehicles.populateLienHolders();
        });
    },
    viewDetails: function (id) {
        Vehicles.vehicle = Vehicles.vehicles.find((c) => { return c["id"] === id });
        Vehicles.vehicle.company = CurrentAccount.legalName;
        BindingService.bindModelToLabels("vehicleDetailsContent", Vehicles.vehicle);

        $("#mdlVehicleDetails").modal({
            backdrop: "static"
        });

        $(".edit-fields").addClass('hide');
        $(".display-fields").removeClass('hide');
        $("#btnCancelEditVehicle").addClass('hide');
        $("#btnSaveVehicleChanges").addClass('hide');
        $("#btnDeleteVehicle").removeClass('hide');
        $("#btnEditVehicle").removeClass('hide'); 

        Vehicles.populateActivePoliciesTable();
        Vehicles.getVehicleHistory(id);
        Vehicles.populateVehicleMakes();
        Vehicles.populateDrivers();
        Vehicles.populateLienHolders();
        BindingService.bindModelToForm("frmVehicleEditForm", Vehicles.vehicle);

        $("#driverOwnerDetailsContainer").removeClass('hide');
        $("#vehicleOnLienDetailsContainer").removeClass('hide');

        if (Vehicles.vehicle.isDriverOwner)
            $("#driverOwnerDetailsContainer").addClass('hide')

        if (Vehicles.vehicle.notOnLien)
            $("#vehicleOnLienDetailsContainer").addClass('hide')
    },
    getVehicleHistory: function (id) {
        $("#tblVehicleHistory").html('');
        VehicleService.getVehicleHistory(id, function (data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    $("#tblVehicleHistory").append(`<tr>
                                                    <td>`+ data[i].dateCreatedFormatted+`</td>
                                                    <td>`+ data[i].transaction +`</td>
                                                    <td>`+ data[i].legalName +`</td>
                                                </tr>`);
                }
            }
        });
    },
    renderVehiclesTable: function (data) {
        Vehicles.vehicles = data;
        if ($.fn.dataTable.isDataTable('#dTableVehicles')) {
            Vehicles.dTable.destroy();
            $("#tblVehicles").html('');
        }

        if (Vehicles.vehicles.length > 0) {
            for (var i = 0; i < Vehicles.vehicles.length; i++) {
                var vehicle = Vehicles.vehicles[i];
                var vehicleRow = `<tr>
                                    <td> `+ vehicle.vYear + ` </td>
                                    <td> `+ vehicle.vehMakeName +` </td>
                                    <td> `+ vehicle.vin +` </td>
                                    <td> `+ vehicle.vUnit +` </td>
                                    <td> `+ vehicle.vehTypeName +` </td>
                                    <td> `+ vehicle.pdvalue +` </td>
                                    <td> `+ vehicle.driver +` </td>
                                    <td class="action"> <button class="btn btn-success btn-sm btn-vehicle-details" data-id=`+ vehicle.id +` title="View Details"><i class="fa fa-search"></i></button>   </td>
                                </tr>`;
                $("#tblVehicles").append(vehicleRow);
            }
        }

        Vehicles.dTable = $("#dTableVehicles").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false, "bInfo": false,
            "columnDefs": [{
                "targets": 6,
                "orderable": false
            }]
        })
    },
    saveVehicle: function () {
        App.blockUI({
            target: "#frmVehicle",
            blockerOnly: true
        });
        VehicleService.saveVehicle(Vehicles.vehicleEndorsement,
            function (data) {
                App.unblockUI("#frmVehicle");
                $("#mdlVehicle").modal('hide');
                Vehicles.getVehicles(CurrentAccount.accountId);
            },
            function (data) {
                App.unblockUI("#frmNewDriver");
                alert(data.responseText);
            }
        );
    },
    updateVehicle: function () {
        App.blockUI({
            target: "#frmVehicleEditForm",
            blockerOnly: true
        });
        VehicleService.updateVehicle(Vehicles.vehicle,
            function (data) {
                App.unblockUI("#frmVehicleEditForm");
                $("#mdlVehicleDetails").modal('hide');
                Vehicles.getVehicles(CurrentAccount.accountId);
            },
            function (data) {
                App.unblockUI("#frmVehicleEditForm");
                alert(data.responseText);
            }
        );
    },
    initEventHandlers: function () {
        $("#btnAddVehicle").click(function () {
            Vehicles.addVehicle();
        });

        $("#btnEditVehicle").click(function () {
            $(".edit-fields").removeClass('hide');
            $(".display-fields").addClass('hide');
            $("#btnCancelEditVehicle").removeClass('hide');
            $("#btnSaveVehicleChanges").removeClass('hide');
            $("#btnDeleteVehicle").addClass('hide');
            $("#btnEditVehicle").addClass('hide');
        });

        $("#btnCancelEditVehicle").click(function () {
            $(".edit-fields").addClass('hide');
            $(".display-fields").removeClass('hide');
            $("#btnCancelEditVehicle").addClass('hide');
            $("#btnSaveVehicleChanges").addClass('hide');
            $("#btnDeleteVehicle").removeClass('hide');
            $("#btnEditVehicle").removeClass('hide'); 
        });

        $("#slcVehicleType").bind("change", function () {
            var val = $(this).val();
            $("#frmGroupUnitNumber").addClass('hide');
            if (parseInt(val) == 1 || parseInt(val) == 3 )
                $("#frmGroupUnitNumber").removeClass('hide');

            if (Vehicles.vehicleEndorsement.vehicleCoverages.length > 0)
            for (var i = 0; i < Vehicles.vehicleEndorsement.vehicleCoverages.length; i++) {
                var coverageType = Vehicles.vehicleEndorsement.vehicleCoverages[i];
                Vehicles.calculateCoverageTotal(coverageType.coverageTypeId, coverageType);
            }
        });

        $("#txtPdValue").bind("keyup", function () {
            if (Vehicles.vehicleEndorsement.vehicleCoverages.length > 0) {
                for (var i = 0; i < Vehicles.vehicleEndorsement.vehicleCoverages.length; i++) {
                    var coverageType = Vehicles.vehicleEndorsement.vehicleCoverages[i];
                    Vehicles.calculateCoverageTotal(coverageType.coverageTypeId, coverageType);
                }
            }
        });

        $("#chkNotOnLien").bind("change", function () {
            var checked = $(this).is(':checked');
            $(".lien-field").removeClass('hide');
            if (checked) {
                $(".lien-field").addClass('hide');
            }
        });

        $("#chkDriverOwner").bind("change", function () {
            var checked = $(this).is(':checked');
            $(".driver-owner").removeClass('hide');
            if (checked) {
                $(".driver-owner").addClass('hide');
            }
        });

        $("#chkDriverOwnerEdit").bind("change", function () {
            var checked = $(this).is(':checked');
            $("#driverOwnerDetailsContainer").removeClass('hide');
            if (checked) {
                $("#driverOwnerDetailsContainer").addClass('hide');
            }
        });

        $("#chkNotOnLienEdit").bind("change", function () {
            var checked = $(this).is(':checked');
            $("#vehicleOnLienDetailsContainer").removeClass('hide');
            if (checked) {
                $("#vehicleOnLienDetailsContainer").addClass('hide');
            }
        });

        $("#frmVehicle").on("submit", function () {
            $("#vehicle-policy-validator").addClass('hide');

            if (Vehicles.vehicleEndorsement.vehicleCoverages.length > 0)
                Vehicles.saveVehicle();
            else
                $("#vehicle-policy-validator").removeClass('hide');

            return false;
        });

        $("#frmVehicleEditForm").on("submit", function () {
            Vehicles.updateVehicle();
            return false;
        });

        $("html").on("click", ".btn-vehicle-details", function () {
            var id = parseInt($(this).attr("data-id"));
            Vehicles.viewDetails(id);
        });

        $("html").on("change", "#availableVehicleCoverageTypes  input.checkboxes.coverage-type", function () {
            var checked = $(this).is(":checked");
            var coverageTypeId = $(this).attr('data-id');
            if (checked) {
                //$(this).parents('tr').find('input[type="text"].coverage-type').removeAttr("disabled");

                var vehicleCoverage = jQuery.extend({}, VehicleCoverageModel.new());
                vehicleCoverage.coverageTypeId = parseInt(coverageTypeId);
                Vehicles.vehicleEndorsement.vehicleCoverages.push(vehicleCoverage);
                Vehicles.calculateCoverageTotal(coverageTypeId, vehicleCoverage);
            }
            else {
                var vehicleCoverages = $.grep(Vehicles.vehicleEndorsement.vehicleCoverages, function (e) { return e.coverageTypeId != parseInt(coverageTypeId); });
                Vehicles.vehicleEndorsement.vehicleCoverages = vehicleCoverages;

                $(this).parents('tr').find('input[type="text"]').attr("disabled", true);
                $(this).parents('tr').find('input[type="text"]').val("");
                $(this).parents('tr').find('input[type="text"]').val("");
                Vehicles.calculateTotalPremium();
            }
        });

        $("html").on("keyup", "#availableVehicleCoverageTypes input[type='text'].coverage-type", function () {

            var coverageTypeId = parseInt($(this).attr('data-id'));
            var field = $(this).attr('data-coverage-field');
            var val = $(this).val() == '' ? 0 : $(this).val();
            var vehicleCoverage = Vehicles.vehicleEndorsement.vehicleCoverages.find((c) => { return c["coverageTypeId"] === coverageTypeId });
            vehicleCoverage[field] = val;

            Vehicles.calculateCoverageTotal(coverageTypeId, vehicleCoverage);
        });
    }
}