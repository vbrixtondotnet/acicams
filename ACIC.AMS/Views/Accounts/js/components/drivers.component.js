var DriversComponent = {
    drivers: [],
    driver: null,
    dTable: null,
    availableCoverageTypes: null,
    init: function () {
        this.initEventHandlers();
    },
    onAddDriver: function () {
        $("#availableCoverageTypes").html('');
        $("#btnSaveNewDriver").attr("disabled", true);
        DriversComponent.driver = DriverEndorsementModel.new();
        DriversComponent.driver.accountId = CurrentAccount.accountId;
        DriversComponent.availableCoverageTypes = PolicyComponent.availableCoverageTypes;
        var data = DriversComponent.availableCoverageTypes;
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

                $("#availableCoverageTypes").append(coverageTypeRow);
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

            $("#availableCoverageTypes").append(coverageTypeRow);
            $("#btnSaveNewDriver").removeAttr("disabled");
        }
        else {
            var coverageTypeRow = `<tr class="gradeX" role="row">
                                                <td class='pad8 padb8' colspan="8">
                                                <span style="color:red;"><p class="font-red-mint"><i class="fa fa-exclamation-triangle"></i>&nbsp; The selected account does not have any active policy. Please create a new Policy for this account before you can add a new Driver. </p></span>
                                                </td>
                                            </tr>`;

            $("#availableCoverageTypes").append(coverageTypeRow);
        }

        BindingService.bindModelToForm("frmNewDriver", DriversComponent.driver);
    },
    saveNewDriver: function () {
        App.blockUI({
            target: "#frmNewDriver",
            blockerOnly: true
        });

        DriversComponent.driver.terminated = DriversComponent.driver.terminatedDateText;
        DriversComponent.driver.dateHired = DriversComponent.driver.dateHiredText;
        DriverService.saveNewDriver(DriversComponent.driver,
            function (data) {
                App.unblockUI("#frmNewDriver");
                $("#mdlDriver").modal('hide');
                DriversComponent.getDrivers(CurrentAccount.accountId);
            },
            function (data) {
                App.unblockUI("#frmNewDriver");
                alert(data.responseText);
            }
        );
    },
    calculateCoverageTotal: function (coverageTypeId, coverageType) {
        var premium = parseFloat(coverageType.premium);
        var premiumTax = parseFloat(coverageType.premiumTax);
        var brokerFee = parseFloat(coverageType.brokerFee);

        coverageType.totalAmount = premium + premiumTax + brokerFee;
        $("input[type='text'][data-coverage-field='totalAmount'][data-id='" + coverageTypeId + "']").val(coverageType.totalAmount);

        DriversComponent.calculateTotalPremium();
    },
    calculateTotalPremium: function () {
        var totalPremium = 0;
        var totalPremiumTax = 0;
        var totalBrokerFee = 0;
        var totalAmount = 0;
        for (var i = 0; i < DriversComponent.driver.driverCoverages.length; i++) {
            var coverageType = DriversComponent.driver.driverCoverages[i];
            var premium = parseFloat(coverageType.premium);
            var premiumTax = parseFloat(coverageType.premiumTax);
            var brokerFee = parseFloat(coverageType.brokerFee);
            var totalCoverageAmount = premium + premiumTax + brokerFee;

            totalPremium += premium;
            totalPremiumTax += premiumTax;
            totalBrokerFee += brokerFee;
            totalAmount += totalCoverageAmount;
        }

        $("input[type='text'].coverage-totalPremium").val(totalPremium);
        $("input[type='text'].coverage-totalPremiumTax").val(totalPremiumTax);
        $("input[type='text'].coverage-totalBrokerFee").val(totalBrokerFee);
        $("input[type='text'].coverage-totalAmount").val(totalAmount);
    },
    getDrivers: function (id) {
        $("#driversPreloader").removeClass('hide');
        $(".driver-list-content").addClass('hide');
        $("#tblDrivers").html('');
        DriverService.getDriversByAccount(id, function (data) {
            $(".driver-list-content").removeClass('hide');
            $("#driversPreloader").addClass('hide');
            DriversComponent.renderDriversTable(data);
        });
    },
    showDriverDetails: function (id) {
        var driver = $.extend({}, DriversComponent.drivers.find((d) => { return d["driverId"] === id }));
        DriversComponent.driver = driver;
        driver.company = CurrentAccount.legalName;
        $("#mdlDriverDetails").modal('show');
        BindingService.bindModelToLabels("driverDetailsContent", driver);

        $("#labelOwner").addClass('hide');
        if (driver.ownerOperator) $("#labelOwner").removeClass('hide');

        $("#tblDriverHistory").html('');

        BindingService.bindModelToForm("frmDriverEditForm", driver);

        DriverService.getDriverHistory(id, function (data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var history = data[i];
                    var historyRow = ` <tr>
                                        <td>`+ history.dateCreatedFormatted + `</td>
                                        <td>`+ history.transaction + `</td>
                                        <td>`+ history.legalName + `</td>
                                    </tr>`;
                    $("#tblDriverHistory").append(historyRow);
                }
            }
        });

        $("#driverDetailsContent").find('.edit-fields').addClass('hide');
        $("#driverDetailsContent").find('.display-fields').removeClass('hide');
        $("#btnSaveDriverChanges").addClass('hide');
        $("#btnCancelEditDriver").addClass('hide');
        $("#btnEditDriver").removeClass('hide');
        $("#btnDeleteDriver").removeClass('hide');

    },
    confirmDeleteDriver: function () {
        $("#deleteUserName").html(DriversComponent.driver.fullName);
        $("#mdlDeleteDriverConfirmation").modal('show');
    },
    deleteDriver: function () {
        $("#btnProceedDeleteDriver").attr("disabled", true);
        DriverService.deleteDriver(DriversComponent.driver.driverId, function (data) {
            $("#btnProceedDeleteDriver").removeAttr("disabled");
            $("#mdlDeleteDriverConfirmation").modal('hide');
            $("#mdlDriverDetails").modal('hide');
            DriversComponent.getDrivers(CurrentAccount.accountId);
        });
    },
    updateDriver: function () {
        $("#btnSaveDriverChanges").attr("disabled", true);

        DriversComponent.driver.terminated = DriversComponent.driver.terminatedDateText;
        DriversComponent.driver.dateHired = DriversComponent.driver.dateHiredText;

        DriverService.updateDriver(DriversComponent.driver, function (data) {
            $("#btnSaveDriverChanges").removeAttr("disabled");
            $("#mdlDriverDetails").modal('hide');
            DriversComponent.getDrivers(CurrentAccount.accountId);
        });
    },
    renderDriversTable: function (driversList) {
        DriversComponent.drivers = driversList;
        if ($.fn.dataTable.isDataTable('#dTableDrivers')) {
            DriversComponent.dTable.destroy();
            $("#tblDrivers").html('');
        }
        if (DriversComponent.drivers.length > 0) {
            for (var i = 0; i < DriversComponent.drivers.length; i++) {
                var driver = DriversComponent.drivers[i];
                var cdlyearLic = driver.cdlyearLic == null ? '' : driver.cdlyearLic;
                var driverRow = ` <tr>
                                <td> `+ driver.fullName + ` </td>
                                <td> `+ driver.state + ` </td>
                                <td> `+ driver.cdlnumber + `  </td>
                                <td> `+ cdlyearLic + ` </td>
                                <td> `+ driver.dateHiredText + ` </td>
                                <td> `+ driver.terminatedDateText + `</td>
                                <td class="action text-right"><button class="btn btn-success btn-sm btn-driver-details" data-id=`+ driver.driverId +` title="View Details"><i class="fa fa-search"></i></button>  </td>
                            </tr>`;
                $("#tblDrivers").append(driverRow);
            }
        }

        DriversComponent.dTable = $("#dTableDrivers").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false, "bInfo": false,
            "columnDefs": [{
                "targets": 6,
                "orderable": false
            }]
        })
    },
    initEventHandlers: function () {
        $("#btnAddDriver").click(function () {
            $("#mdlDriver").modal('show');
            DriversComponent.onAddDriver();
        });

        $("html").on("change", "#availableCoverageTypes  input.checkboxes.coverage-type", function () {
            var checked = $(this).is(":checked");
            var coverageTypeId = $(this).attr('data-id');
            if (checked) {
                $(this).parents('tr').find('input[type="text"].coverage-type').removeAttr("disabled");
            
                var driverCoverage = jQuery.extend({}, DriverCoverageModel.new());
                driverCoverage.coverageTypeId = parseInt(coverageTypeId);
                DriversComponent.driver.driverCoverages.push(driverCoverage);
            }
            else {
                var driverCoverages = $.grep(DriversComponent.driver.driverCoverages, function (e) { return e.coverageTypeId != parseInt(coverageTypeId);});
                DriversComponent.driver.driverCoverages = driverCoverages;

                $(this).parents('tr').find('input[type="text"]').attr("disabled", true);
                $(this).parents('tr').find('input[type="text"]').val("");
                $(this).parents('tr').find('input[type="text"]').val("");
                DriversComponent.calculateTotalPremium();
            }
        });

        $("html").on("keyup", "#availableCoverageTypes input[type='text'].coverage-type", function () {
            var coverageTypeId = parseInt($(this).attr('data-id'));
            var field = $(this).attr('data-coverage-field');
            var val = $(this).val() == '' ? 0 : $(this).val();
            var driverCoverage = DriversComponent.driver.driverCoverages.find((c) => { return c["coverageTypeId"] === coverageTypeId });
            driverCoverage[field] = val;

            DriversComponent.calculateCoverageTotal(coverageTypeId, driverCoverage);
        });

        $("html").on("click", ".btn-driver-details", function () {
            var driverId = parseInt($(this).attr('data-id'));
            DriversComponent.showDriverDetails(driverId);
        });

        $("#btnDeleteDriver").click(function () {
            DriversComponent.confirmDeleteDriver();
        });

        $("#btnProceedDeleteDriver").click(function () {
            DriversComponent.deleteDriver();
            return false;
        });

        $("#btnEditDriver").click(function () {
            $("#driverDetailsContent").find('.edit-fields').removeClass('hide');
            $("#driverDetailsContent").find('.display-fields').addClass('hide');
            $("#btnSaveDriverChanges").removeClass('hide');
            $("#btnCancelEditDriver").removeClass('hide');
            $("#btnDeleteDriver").addClass('hide');
            $("#btnEditDriver").addClass('hide');
        });

        $("#btnCancelEditDriver").click(function () {
            $("#driverDetailsContent").find('.edit-fields').addClass('hide');
            $("#driverDetailsContent").find('.display-fields').removeClass('hide');
            $("#btnSaveDriverChanges").addClass('hide');
            $("#btnCancelEditDriver").addClass('hide');
            $("#btnDeleteDriver").removeClass('hide');
            $("#btnEditDriver").removeClass('hide');
        });

        $("#frmDriverEditForm").on("submit", function () {
            DriversComponent.updateDriver();
            return false;
        });

        $("#frmNewDriver").on("submit", function () {

            $("#policy-validator").addClass('hide');

            if (DriversComponent.driver.driverCoverages.length > 0)
                DriversComponent.saveNewDriver();
            else
                $("#policy-validator").removeClass('hide');

            return false;
        })
    }
}