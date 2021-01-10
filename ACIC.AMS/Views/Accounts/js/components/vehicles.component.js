var VehiclesComponent = {
    dTable: null,
    vehicles: null,
    vehicleMakes: null,
    vehicle: null,
    lienHolders: null,
    vehicleEndorsement: null,
    drivers: null,
    init: function () {
        this.initEventHandlers();
        this.getLienHolders();
        this.getVehicleMakes();
        this.getDrivers();
    },
    addVehicle: function () {
        VehiclesComponent.populateDrivers();
        VehiclesComponent.populateCoverageTypes();
        VehiclesComponent.vehicleEndorsement = VehicleEndorsementModel.new();
        VehiclesComponent.vehicleEndorsement.accountId = CurrentAccount.accountId;

        $("#mdlVehicle").modal({
            backdrop: 'static'
        });

        BindingService.bindModelToForm("frmVehicle", VehiclesComponent.vehicleEndorsement);
    },
    calculateCoverageTotal: function (coverageTypeId, coverageType) {
        debugger;
        coverageTypeId = parseInt(coverageTypeId);

        var policy = PolicyComponent.availableCoverageTypes.find((c) => { return c["id"] === coverageTypeId});
        var typeId = parseInt($("#slcVehicleType").val());
        var premium = null;
        var tax = null;
        var brokerFee = null;
        var makeId = parseInt($("#sclVehicleMakes").val());

        // Auto Liability and Motor Truck Cargo
        if (coverageTypeId != 3) {
            switch (typeId) {
                // Tractor and Truck
                case 1:
                case 3:
                    premium = policy.basePerUnit == null ? null : policy.basePerUnit.toFixed(2);
                    tax = (premium == null || policy.strate == null) ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
                    // calculate broker fee even basePerUnit is null or 0
                    brokerFee = (policy.bfrate == null) ? null : policy.bfrate.toFixed(2);
                    break;
                // Trailer
                case 2:
                    premium = policy.trailerRate == null ? null : policy.trailerRate.toFixed(2);
                    tax = premium == null || policy.strate == null ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2); 
                    break;
            }
        }
        // Physical Damage
        else {
            var pdValue = $("#txtPdValue").val() == '' ? 0 : parseFloat($("#txtPdValue").val());
            switch (typeId) {
               // Tractor and Truck
                case 1:
                case 3:
                    premium = pdValue == null ? null : parseFloat(pdValue * (policy.pdrate / 100)).toFixed(2);
                    tax = (premium == null || policy.strate == null) ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
                    brokerFee = (premium == null || policy.bfrate == null) ? null : policy.bfrate.toFixed(2);
                    break;
                // Trailer
                case 2:
                    var rate = policy.trailerRate == null ? policy.pdrate : policy.trailerRate;
                    var nonOwnedRate = makeId != 14 ? rate : policy.pdNonOwnedTrailerRate; //  check pdNonOwnedTrailerRate 
                    // new scenario: 22 
                    premium = (nonOwnedRate == null || pdValue == 0) ? null : parseFloat(pdValue * (nonOwnedRate / 100)).toFixed(2);
                    tax = premium == null || policy.strate == null ? null : parseFloat(premium * (policy.strate / 100)).toFixed(2);
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

        VehiclesComponent.calculateTotalPremium();
    },
    calculateTotalPremium: function () {
        var totalPremium = 0;
        var totalPremiumTax = 0;
        var totalBrokerFee = 0;
        var totalAmount = 0;
        for (var i = 0; i < VehiclesComponent.vehicleEndorsement.vehicleCoverages.length; i++) {
            var coverageType = VehiclesComponent.vehicleEndorsement.vehicleCoverages[i];

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
            VehiclesComponent.renderVehiclesTable(data);
        });
    },
    getVehicleMakes: function () {
        VehicleService.getVehicleMakes(function (data) {
            VehiclesComponent.vehicleMakes = data;
            VehiclesComponent.populateVehicleMakes();
        });
    },
    getDrivers: function () {
        DriverService.getDrivers(function (data) {
            VehiclesComponent.drivers = data;
            VehiclesComponent.populateDrivers();
        });
    },
    populateDrivers: function () {
        $(".vehicle-drivers").html('');
        $(".vehicle-drivers").append('<option value=""></option>');
        for (var i = 0; i < VehiclesComponent.drivers.length; i++) {
            $(".vehicle-drivers").append('<option value="' + VehiclesComponent.drivers[i].driverId + '">' + VehiclesComponent.drivers[i].fullName + '</option>');
        }
    },
    populateLienHolders: function () {
        var data = VehiclesComponent.lienHolders
        $(".vehicle-banks").html('');
        $(".vehicle-banks").html('<option value =""> </option >');
        for (var i = 0; i < data.length; i++) {
            $(".vehicle-banks").append('<option value="' + data[i].bankId + '">' + data[i].bankName + '</option>');
        }
    },
    populateActivePoliciesTable: function () {
        $("#tblVehicleActivePolicies").html('');
        var activePolicies = PolicyComponent.availableCoverageTypes;
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
        var data = PolicyComponent.availableCoverageTypes;
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
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type" data-coverage-field="premium" disabled data-type="decimal" data-id="`+ coverageType.id + `"/>
                                                </td>
                                                <td class="center">  
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type" data-coverage-field="premiumTax" disabled data-type="decimal" data-id="`+ coverageType.id + `"/> 
                                                </td>
                                                <td>  
                                                    <input type="text" placeholder="0.00" class="form-control coverage-type" data-coverage-field="brokerFee" disabled data-type="decimal" data-id="`+ coverageType.id + `"/> 
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
        for (var i = 0; i < VehiclesComponent.vehicleMakes.length; i++) {
            $(".vehicle-makes").append('<option value="' + VehiclesComponent.vehicleMakes[i].vmid + '">' + VehiclesComponent.vehicleMakes[i].vehMakeName + '</option>');
        }
    },
    getLienHolders: function () {
        VehicleService.getLienHolders(function (data) {
            VehiclesComponent.lienHolders = data;
            VehiclesComponent.populateLienHolders();
        });
    },
    viewDetails: function (id) {
        VehiclesComponent.vehicle = $.extend({}, VehiclesComponent.VehiclesComponent.find((c) => { return c["id"] === id }));
        VehiclesComponent.vehicle.company = CurrentAccount.legalName;
        BindingService.bindModelToLabels("vehicleDetailsContent", VehiclesComponent.vehicle);

        $("#mdlVehicleDetails").modal({
            backdrop: "static"
        });

        $(".edit-fields").addClass('hide');
        $(".display-fields").removeClass('hide');
        $("#btnCancelEditVehicle").addClass('hide');
        $("#btnSaveVehicleChanges").addClass('hide');
        $("#btnDeleteVehicle").removeClass('hide');
        $("#btnEditVehicle").removeClass('hide'); 

        VehiclesComponent.populateActivePoliciesTable();
        VehiclesComponent.getVehicleHistory(id);
        VehiclesComponent.populateVehicleMakes();
        VehiclesComponent.populateDrivers();
        VehiclesComponent.populateLienHolders();
        BindingService.bindModelToForm("frmVehicleEditForm", VehiclesComponent.vehicle);

        $("#driverOwnerDetailsContainer").removeClass('hide');
        $("#vehicleOnLienDetailsContainer").removeClass('hide');

        if (VehiclesComponent.vehicle.isDriverOwner)
            $("#driverOwnerDetailsContainer").addClass('hide')

        if (VehiclesComponent.vehicle.notOnLien)
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
        VehiclesComponent.vehicles = data;
        if ($.fn.dataTable.isDataTable('#dTableVehicles')) {
            VehiclesComponent.dTable.destroy();
            $("#tblVehicles").html('');
        }

        if (VehiclesComponent.vehicles.length > 0) {
            for (var i = 0; i < VehiclesComponent.vehicles.length; i++) {
                var vehicle = VehiclesComponent.vehicles[i];
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

        VehiclesComponent.dTable = $("#dTableVehicles").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false, "bInfo": false,
            "columnDefs": [{
                "targets": 7,
                "orderable": false
            }]
        })
    },
    saveVehicle: function () {
        App.blockUI({
            target: "#frmVehicle",
            blockerOnly: true
        });
        VehicleService.saveVehicle(VehiclesComponent.vehicleEndorsement,
            function (data) {
                App.unblockUI("#frmVehicle");
                $("#mdlVehicle").modal('hide');
                VehiclesComponent.getVehicles(CurrentAccount.accountId);
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
        VehicleService.updateVehicle(VehiclesComponent.vehicle,
            function (data) {
                App.unblockUI("#frmVehicleEditForm");
                $("#mdlVehicleDetails").modal('hide');
                VehiclesComponent.getVehicles(CurrentAccount.accountId);
            },
            function (data) {
                App.unblockUI("#frmVehicleEditForm");
                alert(data.responseText);
            }
        );
    },
    deleteVehicle: function () {
        $("#btnProceedDeleteVehicle").attr("disabled", true);
        VehicleService.deleteVehicle(VehiclesComponent.vehicle.id, function (data) {
            $("#btnProceedDeleteVehicle").removeAttr("disabled");
            $("#mdlDeleteVehicleConfirmation").modal('hide');
            $("#mdlVehicleDetails").modal('hide');
            VehiclesComponent.getVehicles(CurrentAccount.accountId);
        });
    },
    initEventHandlers: function () {
        $("#btnAddVehicle").click(function () {
            VehiclesComponent.addVehicle();
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

        $("#btnDeleteVehicle").click(function () {
            $("#mdlDeleteVehicleConfirmation").modal({
                backdrop: 'static'
            });
        });

        $("#btnProceedDeleteVehicle").click(function () {
            VehiclesComponent.deleteVehicle();
            return false;
        });

        $("#slcVehicleType").bind("change", function () {
            var val = $(this).val();
            $("#frmGroupUnitNumber").addClass('hide');
            if (parseInt(val) == 1 || parseInt(val) == 3 )
                $("#frmGroupUnitNumber").removeClass('hide');

            if (VehiclesComponent.vehicleEndorsement.vehicleCoverages.length > 0)
            for (var i = 0; i < VehiclesComponent.vehicleEndorsement.vehicleCoverages.length; i++) {
                var coverageType = VehiclesComponent.vehicleEndorsement.vehicleCoverages[i];
                VehiclesComponent.calculateCoverageTotal(coverageType.coverageTypeId, coverageType);
            }
        });

        $("#sclVehicleMakes").bind("change", function () {
            if (VehiclesComponent.vehicleEndorsement.vehicleCoverages.length > 0) {
                for (var i = 0; i < VehiclesComponent.vehicleEndorsement.vehicleCoverages.length; i++) {
                    var coverageType = VehiclesComponent.vehicleEndorsement.vehicleCoverages[i];
                    VehiclesComponent.calculateCoverageTotal(coverageType.coverageTypeId, coverageType);
                }
            }
        });

        $("#txtPdValue").bind("keyup", function () {
            if (VehiclesComponent.vehicleEndorsement.vehicleCoverages.length > 0) {
                for (var i = 0; i < VehiclesComponent.vehicleEndorsement.vehicleCoverages.length; i++) {
                    var coverageType = VehiclesComponent.vehicleEndorsement.vehicleCoverages[i];
                    VehiclesComponent.calculateCoverageTotal(coverageType.coverageTypeId, coverageType);
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

            if (VehiclesComponent.vehicleEndorsement.vehicleCoverages.length > 0)
                VehiclesComponent.saveVehicle();
            else
                $("#vehicle-policy-validator").removeClass('hide');

            return false;
        });

        $("#frmVehicleEditForm").on("submit", function () {
            VehiclesComponent.updateVehicle();
            return false;
        });

        $("html").on("click", ".btn-vehicle-details", function () {
            var id = parseInt($(this).attr("data-id"));
            VehiclesComponent.viewDetails(id);
        });

        $("html").on("change", "#availableVehicleCoverageTypes  input.checkboxes.coverage-type", function () {
            var checked = $(this).is(":checked");
            var coverageTypeId = $(this).attr('data-id');
            if (checked) {
                //$(this).parents('tr').find('input[type="text"].coverage-type').removeAttr("disabled");

                var vehicleCoverage = jQuery.extend({}, VehicleCoverageModel.new());
                vehicleCoverage.coverageTypeId = parseInt(coverageTypeId);
                VehiclesComponent.vehicleEndorsement.vehicleCoverages.push(vehicleCoverage);
                VehiclesComponent.calculateCoverageTotal(coverageTypeId, vehicleCoverage);
            }
            else {
                var vehicleCoverages = $.grep(VehiclesComponent.vehicleEndorsement.vehicleCoverages, function (e) { return e.coverageTypeId != parseInt(coverageTypeId); });
                VehiclesComponent.vehicleEndorsement.vehicleCoverages = vehicleCoverages;

                $(this).parents('tr').find('input[type="text"]').attr("disabled", true);
                $(this).parents('tr').find('input[type="text"]').val("");
                $(this).parents('tr').find('input[type="text"]').val("");
                VehiclesComponent.calculateTotalPremium();
            }
        });

        $("html").on("keyup", "#availableVehicleCoverageTypes input[type='text'].coverage-type", function () {

            var coverageTypeId = parseInt($(this).attr('data-id'));
            var field = $(this).attr('data-coverage-field');
            var val = $(this).val() == '' ? 0 : $(this).val();
            var vehicleCoverage = VehiclesComponent.vehicleEndorsement.vehicleCoverages.find((c) => { return c["coverageTypeId"] === coverageTypeId });
            vehicleCoverage[field] = val;

            VehiclesComponent.calculateCoverageTotal(coverageTypeId, vehicleCoverage);
        });
    }
}