var PolicyComponent = {
    availableCoverageTypes: null,
    dTable: null,
    policies: null,
    policy: null,
    carriers: null,
    mgas: null,
    premiumFinancers: null,
    rateCalculator: null,
    init: function () {
        this.initEventHandlers();
        this.getCarriers();
        this.getMgas();
        this.getPremiumFinancers();
    },
    getAvailableCoverageTypes: function (id) {
        PolicyService.getAvailableCoverageTypes(id, function (data) {
            PolicyComponent.availableCoverageTypes = data;
        });
    },
    getCarriers: function () {
        $("#slcCarriers").html('');
        $("#slcCarriers").append("<option value=''></option>");
        PolicyService.getCarriers(function (data) {
            if (data.length > 0) {
                PolicyComponent.carriers = data;
                for (var i = 0; i < data.length; i++) {
                    var carrier = data[i];
                    $("#slcCarriers").append("<option value='" + carrier.carrierId + "'>" + carrier.carrierName + "</option>");
                }

                $("#slcCarriers").select2({
                    allowClear: true,
                    placeholder: "Select Carrier",
                    width: null,
                    dropdownParent: $("#mdlPolicy")
                });
            }
        });
    },
    getMgas: function () {
        $("#slcMgas").html('');
        $("#slcMgas").append("<option value=''></option>");
        PolicyService.getMgas(function (data) {
            if (data.length > 0) {
                PolicyComponent.mgas = data;
                for (var i = 0; i < data.length; i++) {
                    var mga = data[i];
                    $("#slcMgas").append("<option value='" + mga.mgaid + "'>" + mga.mganame + "</option>");
                }

                $("#slcMgas").select2({
                    allowClear: true,
                    placeholder: "Select MGA",
                    width: null,
                    dropdownParent: $("#mdlPolicy")
                });
            }
        });
    },
    getPremiumFinancers: function () {
        $("#slcPolicyPremiumFinancers").html('');
        $("#slcPolicyPremiumFinancers").append("<option value=''></option>");
        PolicyService.getPremiumFinancers(function (data) {
            if (data.length > 0) {
                PolicyComponent.premiumFinancers = data;
                for (var i = 0; i < data.length; i++) {
                    var financer = data[i];
                    $("#slcPolicyPremiumFinancers").append("<option value='" + financer.bankId + "'>" + financer.bankName + "</option>");
                }

                $("#slcPolicyPremiumFinancers").select2({
                    allowClear: true,
                    placeholder: "Select Financer",
                    width: null,
                    dropdownParent: $("#mdlPolicy")
                });
            }
        });
    },
    getPolicies: function (id) {
        $("#policiesPreloader").removeClass('hide');
        $(".policy-list-content").addClass('hide');

        PolicyService.getPolicies(id, function (data) {
            PolicyComponent.policies = data;
            PolicyComponent.renderPolicyTable();

            $("#policiesPreloader").addClass('hide');
            $(".policy-list-content").removeClass('hide');
        });
    },
    onCoverageTypeChange: function () {
        var selectedCoverageType = $("#slcPolicyCoverageType").val();

        $(".coverage-rates").addClass('hide');
        $(".coverage-rates").each(function (ind, obj) {
            var coverageTypes = $(obj).attr("data-coverage-types").split(',');
            if (coverageTypes.length > 0) {
                var included = coverageTypes.find((c) => { return c === selectedCoverageType }) != null;

                if (included) $(obj).removeClass('hide');
            }
        });
    },
    addPolicy: function () {
        PolicyComponent.policy = $.extend({}, PolicyModel.new());

        $("#slcPolicyAccountName").html("<option>" + CurrentAccount.legalName + "</option>");
        $("#mdlPolicy").modal({
            backdrop: 'static'
        });

        BindingService.bindModelToForm("frmPolicy", PolicyComponent.policy);
        $("#slcPolicyCoverageType").val('').trigger('change');
    },
    renderPolicyTable: function () {
        if ($.fn.dataTable.isDataTable('#dTablePolicies')) {
            PolicyComponent.dTable.destroy();
            $("#tblPolicyList").html('');
        }

        if (PolicyComponent.policies.length > 0) {
            for (var i = 0; i < PolicyComponent.policies.length; i++) {
                var policy = PolicyComponent.policies[i];
                var row = `<tr><td> `+policy.coverageType+` </td>
                            <td> `+ policy.effectiveString +` </td>
                            <td> `+ policy.expirationString +`  </td>
                            <td> `+ policy.carrierName +` </td>
                            <td> `+ policy.mganame +` </td>
                            <td> `+ policy.policyNumber +` </td>
                            <td> `+ policy.premium +` </td>
                            <td> `+ policy.totalPremium +` </td>
                            <td class="action"> <button class="btn btn-success btn-sm" title="View Details" data-id=`+policy.policyId+`><i class="fa fa-search"></i></button>  </td></tr>`;
                $("#tblPolicyList").append(row);
            }
        }

        PolicyComponent.dTable = $("#dTablePolicies").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false, "bInfo": false,
            "columnDefs": [{
                "targets": 8,
                "orderable": false
            }]
        })
    },
    onEffectivityDateChange: function () {
        var effectivityDateString = $("#txtPolicyEffectiveDate").val();
        var effectivityDate = new Date(effectivityDateString);

        var year = effectivityDate.getFullYear();
        var month = effectivityDate.getMonth();
        var day = effectivityDate.getDate();
        var expiryDate = new Date(year + 1, month, day);
        var expiryDateString = ValidationService.formatDate(expiryDate);
        $("#txtPolicyExpirationDate").val(expiryDateString);

    },
    openRateCalculator: function (rate) {
        $("#mdlPolicyRateCalculator").modal({
            backdrop: "static"
        });

        $(".rate-calc-field").addClass('hide');
        $(".rate-calc-field").find('input[type="text"]').val('');

        switch (rate) {
            case 'ALRATE':
            case 'MTCRATE':
                $("#lblRateCalculatorPremium").html("Total Premium");
                $("#lblRateCalculatorUnits").html("Number of Units");
                $("#lblRateCalculatorRate").html("Base per Unit");
                break;
            case 'ALTRAILERRATE':
                $("#lblRateCalculatorPremium").html("Total Trailer Premium");
                $("#lblRateCalculatorUnits").html("Number of Trailers");
                $("#lblRateCalculatorRate").html("Base per Trailer");
                break;
            case 'NTF':
                $("#lblRateCalculatorPremium").html("Non Taxable Fees");
                $("#lblRateCalculatorUnits").html("Number of Units");
                $("#lblRateCalculatorRate").html("Non Taxable Rate / Unit");
                break;
            case 'BFPERUNIT':
                $("#lblRateCalculatorPremium").html("Total Broker Fees");
                $("#lblRateCalculatorUnits").html("Number of Units");
                $("#lblRateCalculatorRate").html("Base per Unit");
                break;
            case 'MTCTRAILERRATE':
                $("#lblRateCalculatorPremium").html("Total Trailer Premium");
                $("#lblRateCalculatorUnits").html("Number of Trailers");
                $("#lblRateCalculatorRate").html("Base per Trailer");
                break;
            case 'PDRATE':
                $("#lblRateCalculatorPremium").html("Total Premium");
                $("#lblRateCalculatorUnits").html("Total Insurable Value");
                $("#lblRateCalculatorRate").html("PD Rate (%)");
                break;
            case "PDTRAILERRATE":
                $("#lblRateCalculatorPremium").html("Total Trailer Premium");
                $("#lblRateCalculatorUnits").html("Trailers TIV");
                $("#lblRateCalculatorRate").html("Trailer PD Rate (%)");
                break;
            case "NOTRAILERRATE":
                $("#lblRateCalculatorPremium").html("Non Owned Premium");
                $("#lblRateCalculatorUnits").html("Non Owned TIV");
                $("#lblRateCalculatorRate").html("NO PD Rate (%)");
                break;
            case "INTERCHANGERATE":
                $("#lblRateCalculatorPremium").html("TI Premium");
                $("#lblRateCalculatorUnits").html("Interchange TIV");
                $("#lblRateCalculatorRate").html("TI PD Rate(%)");
                break;
        }

        $("#txtRateCalculatorPremium").val('');
        $("#txtRateCalculatorRate").val('');
        $("#txtRateCalculatorUnits").val('');
        PolicyComponent.rateCalculator = rate;
    },
    calculateRate: function () {
        var rate = parseFloat($("#txtRateCalculatorRate").val());
        switch (PolicyComponent.rateCalculator) {
            case 'ALTRAILERRATE':
            case 'MTCTRAILERRATE':
            case 'PDTRAILERRATE':
                PolicyComponent.policy.trailerRate = rate;
                break;
            case 'ALRATE':
            case 'MTCRATE':
                PolicyComponent.policy.bfrate = rate;
                break;
            case 'NTF':
                PolicyComponent.policy.nonTaxedRateUnit = rate;
                break;
            case 'BFPERUNIT':
                PolicyComponent.policy.basePerUnit = rate;
                break;
            case 'PDRATE':
                PolicyComponent.policy.pdrate = rate;
                break;
            case 'NOTRAILERRATE':
                PolicyComponent.policy.pdNonOwnedTrailerRate = rate;
                break;
            case 'INTERCHANGERATE':
                PolicyComponent.policy.trailerInterchangeRate = rate;
                break;
        }
        BindingService.bindModelToForm("frmPolicy", PolicyComponent.policy);
        $("#mdlPolicyRateCalculator").modal('hide');
    },
    computeRate: function () {

        var premium = $("#txtRateCalculatorPremium").val();
        var units = $("#txtRateCalculatorUnits").val();
        var rate = 0;

        if (premium == '' || units == '') return;

        premium = parseFloat(premium);
        units = parseFloat(units);

        if (PolicyComponent.rateCalculator == 'PDRATE' || PolicyComponent.rateCalculator == 'NOTRAILERRATE' || PolicyComponent.rateCalculator == 'PDTRAILERRATE' || PolicyComponent.rateCalculator == 'INTERCHANGERATE') {
            rate = (premium / parseFloat(units) * (100)).toFixed(2);
        }
        else {
            rate = (premium / parseFloat(units)).toFixed(2);
        }

        $("#txtRateCalculatorRate").val(rate);
    },
    initEventHandlers: function () {
        $("#btnAddPolicy").click(function () {
            PolicyComponent.addPolicy();
        });

        $("#slcPolicyCoverageType").bind("change", function () {
            PolicyComponent.onCoverageTypeChange();
        });

        $("#txtPolicyEffectiveDate").bind("change", function () {
            PolicyComponent.onEffectivityDateChange();
        });

        $("#btnRateCalculatorConfirm").click(function () {
            PolicyComponent.calculateRate();
        });

        $("#txtRateCalculatorPremium, #txtRateCalculatorUnits").on("keyup", function () {
            PolicyComponent.computeRate();
        });
    }
}