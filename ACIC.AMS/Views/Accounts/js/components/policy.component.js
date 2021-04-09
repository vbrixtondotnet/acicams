
var PolicyComponent = {
    availableCoverageTypes: null,
    dTable: null,
    policies: null,
    policy: null,
    carriers: null,
    mgas: null,
    premiumFinancers: null,
    rateCalculator: null,
    agentCommissions: null,
    selectedPolicyId: null,
    editMode: false,
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
                    placeholder: "Select Company",
                    width: null,
                    dropdownParent: $("#mdlPolicy")
                });
            }
        });
    },
    getPolicies: function (id, callback) {
        $("#policiesPreloader").removeClass('hide');
        $(".policy-list-content").addClass('hide');

        PolicyService.getPolicies(id, function (data) {
            PolicyComponent.policies = data;
            PolicyComponent.renderPolicyTable();

            $("#policiesPreloader").addClass('hide');
            $(".policy-list-content").removeClass('hide');

            if (callback) callback();
        });
    },
    onCoverageTypeChange: function (checkGross) {
        var selectedCoverageType = $("#slcPolicyCoverageType").val();
        $(".coverage-rates").addClass('hide');

        $(".coverage-rates").each(function (ind, obj) {
            var coverageTypes = $(obj).attr("data-coverage-types").split(',');
            if (coverageTypes.length > 0) {
                var included = coverageTypes.find((c) => { return c === selectedCoverageType }) != null;
                if (included) $(obj).removeClass('hide');
            }
        });
        PolicyComponent.isGross();

    },
    addPolicy: function () {
        PolicyComponent.policy = $.extend({}, PolicyModel.new());
        $("#slcPolicyAccountName").html("<option value='" + CurrentAccount.accountId + "' selected>" + CurrentAccount.legalName + "</option>");
        $("#mdlPolicy").modal({
            backdrop: 'static'
        });
        PolicyComponent.policy.accountId = CurrentAccount.accountId;
        BindingService.bindModelToForm("frmPolicy", PolicyComponent.policy);
        $("#slcPolicyCoverageType").val('').trigger('change');
    },
    editPolicy: function () {

        $("#mdlPolicyDetails").modal('hide');
       
        $("#slcPolicyAccountName").html("<option value='" + CurrentAccount.accountId + "' selected>" + CurrentAccount.legalName + "</option>");
        $("#mdlPolicy").modal({
            backdrop: 'static'
        });
        PolicyComponent.policy.accountId = CurrentAccount.accountId;
        BindingService.bindModelToForm("frmPolicy", PolicyComponent.policy);
        PolicyComponent.editMode = true;
    },
    closePolicyForm: function () {
        if (PolicyComponent.editMode) {
            PolicyComponent.viewPolicy(PolicyComponent.selectedPolicyId);
        }
        $("#mdlPolicy").modal('hide');
        PolicyComponent.editMode = false;
    },
    viewPolicy: function (policyId) {
        PolicyComponent.policy = $.extend({}, PolicyModel.new());
        var policy = $.extend(PolicyComponent.policy, PolicyComponent.policies.find((p) => { return p["policyId"] === parseInt(policyId) }));
        PolicyComponent.policy = policy;
        var inceptionStage = PolicyComponent.policy.inceptionStage;
        if (inceptionStage) {
            $("#btnPolicyDetailsInceptionStage").html("Servicing Stage");
        }
        else {
            $("#btnPolicyDetailsInceptionStage").html("Inception Stage");
        }

        $("#tbPolicyDetailsEndorsements").html('');
        $("#tbodyPolicyDetailsActiveVehicles").html('');
        $("#tbodyPolicyDetailsActiveVehiclesCounter").html('[0]');

        $("#mdlPolicyDetails").modal({
            backdrop: 'static'
        });

        PolicyComponent.getEndorsementStats();
        PolicyComponent.getActiveVehicles();
        PolicyComponent.getAgentCommissions();
        BindingService.bindModelToLabels("policyDetailsContent", PolicyComponent.policy);
        $("#txtPolicyDetailsNotes").val(PolicyComponent.policy.notes);
        PolicyComponent.displayCurrentDate();
    },
    renderPolicyTable: function () {
        if ($.fn.dataTable.isDataTable('#dTablePolicies')) {
            PolicyComponent.dTable.destroy();
            $("#tblPolicyList").html('');
        }

        if (PolicyComponent.policies.length > 0) {
            for (var i = 0; i < PolicyComponent.policies.length; i++) {
                var policy = PolicyComponent.policies[i];
                var carrier = policy.carrierName == null ? '' : policy.carrierName;
                var mganame = policy.mganame == null ? '' : policy.mganame;
                var policyNumber = policy.policyNumber == null ? '' : policy.policyNumber;
                var premium = policy.premium == null ? '' : policy.premium;
                var totalPremium = policy.totalPremium == 0 ? '' : policy.totalPremium;
                var row = `<tr><td> `+policy.coverageType+` </td>
                            <td> `+ policy.effectiveDateText +` </td>
                            <td> `+ policy.expirationDateText +`  </td>
                            <td> `+ carrier +` </td>
                            <td> `+ mganame +` </td>
                            <td> `+ policyNumber +` </td>
                            <td> `+ premium +` </td>
                            <td> `+ totalPremium +` </td>
                            <td class="action"> <button class="btn btn-success btn-sm btn-view-policy" title="View Details" data-id=`+policy.policyId+`><i class="fa fa-search"></i></button>  </td></tr>`;
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
        var expiryDate = new Date(year + 1, month, day + 1);
        var expiryDateString = ValidationService.formatDate(expiryDate.toISOString());   
        $("#txtPolicyExpirationDate").val(expiryDateString).trigger('change');

    },
    openRateCalculator: function (rate) {
        $("#mdlPolicyRateCalculator").modal({
            backdrop: "static"
        });

        $(".rate-calc-field").addClass('hide');
        $(".rate-calc-field").find('input[type="text"]').val('');
        $("#txtRateCalculatorPremium").removeAttr('data-formula');
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
    assignRate: function () {
        var rate = parseFloat($("#txtRateCalculatorRate").val());
        switch (PolicyComponent.rateCalculator) {
            case 'ALTRAILERRATE':
            case 'MTCTRAILERRATE':
            case 'PDTRAILERRATE':
                PolicyComponent.policy.trailerRate = rate;
                break;
            case 'ALRATE':
            case 'MTCRATE':
                PolicyComponent.policy.basePerUnit = rate;
                break;
            case 'NTF':
                PolicyComponent.policy.nonTaxedRateUnit = rate;
                break;
            case 'BFPERUNIT':
                PolicyComponent.policy.bfrate = rate;
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
    calculateRate: function () {

        var premium = $("#txtRateCalculatorPremium").val();
        var units = $("#txtRateCalculatorUnits").val();
        var rate = 0;

        if (premium == '' || units == '') return;

        premium = parseFloat(premium);
        units = parseFloat(units);

        // changed the number of decimal points to 6

        if (PolicyComponent.rateCalculator == 'PDRATE' || PolicyComponent.rateCalculator == 'NOTRAILERRATE' || PolicyComponent.rateCalculator == 'PDTRAILERRATE' || PolicyComponent.rateCalculator == 'INTERCHANGERATE') {
            rate = (premium / parseFloat(units) * (100)).toFixed(6);
        }
        else {
            rate = (premium / parseFloat(units)).toFixed(6);
        }

        $("#txtRateCalculatorRate").val(rate);
    },
    onGrossReceiptChange: function () {
        PolicyComponent.onCoverageTypeChange(false);
        PolicyComponent.isGross();
    },
    isGross: function () {
        var isGross = $("#chkGrossReceipt").is(":checked");
        var selectedCoverageType = $("#slcPolicyCoverageType").val();
        $(".coverage-premium").addClass('hide');
        if (isGross) {
            $(".coverage-premium").removeClass('hide');
            $(".coverage-rates").addClass('hide');
            switch (selectedCoverageType) {
                case '1':
                    $("#lblPremiumLabel").html('AL Gross Premium');
                    break;
                case '2':
                    $("#lblPremiumLabel").html('MTC Gross Premium');
                    break;
                case '3':
                    $("#lblPremiumLabel").html('PD Gross Premium');
                    break;
            }

            
            PolicyComponent.toggleGrossRate();
        }

        switch (selectedCoverageType) {
            case '4':
                $(".coverage-premium").removeClass('hide');
                $("#lblPremiumLabel").html('GL Premium');
                break;
            case '5':
                $(".coverage-premium").removeClass('hide');
                $("#lblPremiumLabel").html('EL Premium');
                break;
            case '6':
                $(".coverage-premium").removeClass('hide');
                $("#lblPremiumLabel").html('Premium Amount');
                break;
        }
        //BindingService.bindModelToForm("frmPolicy", PolicyComponent.policy);
    },
    toggleGrossRate: function (coverageType) {

        var selectedCoverageType = $("#slcPolicyCoverageType").val();
        $(".gross-rate").addClass('hide');
        $(".gross-rate").each(function (ind, obj) {
            var coverageTypes = $(obj).attr("data-coverage-types").split(',');
            if (coverageTypes.length > 0) {
                var included = coverageTypes.find((c) => { return c === selectedCoverageType }) != null;
                if (included) $(obj).removeClass('hide');
            }
        });
    },
    computeSurplusTax: function () {
        debugger;
        var surplusTax = null;
        var premium = $("input[type='text'][data-model='premium']").val();
        var surcharge = $("input[type='text'][data-model='surcharge']").val();
        var surplusTaxRate = $("input[type='text'][data-model='surplusTaxRate']").val();
        var mgaFees = $("input[type='text'][data-model='mgafees']").val();
        var policyFees = $("input[type='text'][data-model='policyFees']").val();

        var isMgaTaxed = $("#chkTaxedMgaFees").is(":checked");
        var isPolicyFeesTaxed = $("#chkTaxedPolicyFees").is(":checked");

        surplusTaxRate = surplusTaxRate == '' ? 0 : parseFloat(surplusTaxRate);
        surcharge = surcharge == '' ? 0 : parseFloat(surcharge);
        surplusTaxRate = parseFloat(surplusTaxRate);
        var premiumVal = premium == '' ? 0 : parseFloat(premium);

        surplusTax = (parseFloat(premiumVal) + parseFloat(surcharge)) * (surplusTaxRate / 100);
        if (mgaFees != '') {
            var isMgaTaxed = $("#chkTaxedMgaFees").is(":checked");
            var mgaFeesRaw = (parseFloat(mgaFees)) * (surplusTaxRate / 100);
            surplusTax = surplusTax + mgaFeesRaw;
            if (!isMgaTaxed) {
                surplusTax = surplusTax - mgaFeesRaw;
            }
        }
        if (policyFees != '') {
            var policyFeesRaw = (parseFloat(policyFees)) * (surplusTaxRate / 100);
            surplusTax = surplusTax + policyFeesRaw;
            if (!isPolicyFeesTaxed) {
                surplusTax = surplusTax - policyFeesRaw;
            }
        }

        surplusTax = surplusTax == 0 ? '' : surplusTax = surplusTax.toFixed(2); 

        //surplusTax = surplusTax.toFixed(2);

        $("input[type='text'][data-model='surplusTax']").val(surplusTax).trigger('change');
        PolicyComponent.policy.surplusTax = parseFloat(surplusTax);
        PolicyComponent.computeTotal();
        PolicyComponent.computeCommission();
    },
    computeTotal: function () {
        var isGross = $("#chkGrossReceipt").is(":checked");
        var surcharge = $("input[type='text'][data-model='surcharge']").val();
        var surplusTax = $("input[type='text'][data-model='surplusTax']").val();
        var policyFees = $("input[type='text'][data-model='policyFees']").val();
        var mgafees = $("input[type='text'][data-model='mgafees']").val();
        var otherFees = $("input[type='text'][data-model='otherFees']").val();
        var premium = 0;
        var brokerFees = 0;

        surcharge = surcharge == '' ? 0 : parseFloat(surcharge);
        surplusTax = surplusTax == '' ? 0 : parseFloat(surplusTax);
        policyFees = policyFees == '' ? 0 : parseFloat(policyFees);
        mgafees = mgafees == '' ? 0 : parseFloat(mgafees);
        otherFees = otherFees == '' ? 0 : parseFloat(otherFees);

        if (isGross) {
            premium = $("input[type='text'][data-model='premium']").val();
            premium = premium == '' ? 0 : parseFloat(premium);
        }

        var coverageType = PolicyComponent.policy.coverageTypeId;

        if (coverageType == "4" || coverageType == "5" || coverageType == "6") {
            brokerFees = $("input[type='text'][data-model='brokerFees']").val();
            brokerFees = brokerFees == '' ? 0 : parseFloat(brokerFees);

        }

        $("#txtTaxesTotal").val((surcharge + surplusTax + policyFees + mgafees + otherFees + premium + brokerFees).toFixed(2));

    },
    computeCommission: function () {
        var isGross = $("#chkGrossReceipt").is(":checked");
        if (isGross) {
            var premium = $("input[type='text'][data-model='premium']").val();
            var commRate = $("input[type='text'][data-model='commRate']").val();

            if (premium != '' && commRate != '') {
                premium = parseFloat(premium);
                commRate = parseFloat(commRate);
                $("input[type='text'][data-model='commission']").val((premium * (commRate / 100)).toFixed(2));
            }
            else {
                $("input[type='text'][data-model='commission']").val('');
            }

            $("input[type='text'][data-model='commission']").trigger('change');
        }
    },
    savePolicy: function () {
        var isGross = $("#chkGrossReceipt").is(":checked");
        if (isGross) {
            var bfRate = $("input[type='text'][data-model='basePerUnit']").val();
            var strate = $("input[type='text'][data-model='surplusTaxRate']").val();

            bfRate = bfRate == '' ? null : parseFloat(bfRate);
            strate = strate == '' ? null : parseFloat(strate);

            PolicyComponent.policy.bfRate = bfRate;
            PolicyComponent.policy.strate = strate;
        }

        App.blockUI({
            target: "#frmPolicy",
            blockerOnly: true
        });

        PolicyComponent.policy.effective = $("#txtPolicyEffectiveDate").val();
        PolicyComponent.policy.expiration = $("#txtPolicyExpirationDate").val();
        PolicyComponent.policy.bindDate = $("#txtPolicyBindDate").val();

        PolicyService.savePolicy(PolicyComponent.policy,
           function (data) {
               $("#mdlPolicy").modal('hide');
               PolicyComponent.getPolicies(CurrentAccount.accountId, function () {
                   PolicyComponent.closePolicyForm(); });
               App.unblockUI("#frmPolicy");

        }, function (data) {

        });
    },
    getEndorsementStats: function () {
        PolicyService.getEndorsementStats(CurrentAccount.accountId, PolicyComponent.policy.policyId, function (data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length - 1; i++) {
                    var policyEndorsement = data[i];
                    var initial = policyEndorsement.initial == 0 ? '' : ValidationService.formatMoney(policyEndorsement.initial);
                    var endorsements = policyEndorsement.endorsements == 0 ? '' : ValidationService.formatMoney(policyEndorsement.endorsements);
                    var current = policyEndorsement.current == 0 ? '' : ValidationService.formatMoney(policyEndorsement.current);
                    var row = `<tr>
                                    <td>`+ policyEndorsement.unit+`</td>
                                    <td style="text-align:center;">`+ initial +`</td>
                                    <td style="text-align:center;">`+ endorsements +`</td>
                                    <td style="text-align:center;">`+ current +`</td>
                                </tr>`;

                    $("#tbPolicyDetailsEndorsements").append(row);
                }
                var policyEndorsement = data[data.length - 1];
                var initial = policyEndorsement.initial == 0 ? '' : ValidationService.formatMoney(policyEndorsement.initial);
                var endorsements = policyEndorsement.endorsements == 0 ? '' : ValidationService.formatMoney(policyEndorsement.endorsements);
                var current = policyEndorsement.current == 0 ? '' : ValidationService.formatMoney(policyEndorsement.current);
                var row = `<tr>
                                    <td><strong style="font-size:16px;">`+ policyEndorsement.unit + `</strong></td>
                                    <td style="text-align:center;"><strong style="font-size:16px;">`+ initial + `</strong></td>
                                    <td style="text-align:center;"><strong style="font-size:16px;">`+ endorsements + `</strong></td>
                                    <td style="text-align:center;"><strong style="font-size:16px;">`+ current + `</strong></td>
                                </tr>`;

                $("#tbPolicyDetailsEndorsements").append(row);
            }
          
        });
    },
    getActiveVehicles: function () {
        PolicyService.getActiveVehicles(PolicyComponent.policy.policyId, function (data) {

            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var vehicle = data[i];
                    var year = vehicle.year == null ? '' : vehicle.year;
                    var unit = vehicle.unit == null ? '' : vehicle.unit;
                    var row = `<tr>
                                    <td>`+ year+`</td>
                                    <td>`+ vehicle.make +`</td>
                                    <td>`+ vehicle.vin +`</td>
                                    <td>`+ unit +`</td>
                                    <td>`+ vehicle.type + `</td>
                                    <td>`+ ValidationService.formatMoney(vehicle.pdValue) +`</td>
                                    <td>`+ vehicle.driver +`</td>
                                </tr>`;


                    $("#tbodyPolicyDetailsActiveVehicles").append(row);
                }

                $("#tbodyPolicyDetailsActiveVehiclesCounter").html('[' + data.length+']');
            }

        });
    },
    getAgentCommissions: function () {
        PolicyService.getPolicyAgentCommissions(PolicyComponent.policy.policyId, function (data) {
            PolicyComponent.agentCommissions = data;
            BindingService.bindModelToLabels("policyDetailsAgentCommissions", PolicyComponent.agentCommissions);
        });
    },
    displayCurrentDate: function () {
        const date = new Date();  // 2009-11-10
        const month = date.toLocaleString('default', { month: 'long' });
        var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
        var dayName = days[date.getDay()];

        var dateString = month + ' ' + date.getDate() + ',' + date.getFullYear() + ', ' + dayName;
        $("#lblPolicyDetailsCurrentDate").html(dateString);
    },
    setInceptionStage: function () {
        var policyId = PolicyComponent.policy.policyId;
        var inceptionStage = !PolicyComponent.policy.inceptionStage;

        PolicyService.setInceptionStage(policyId, inceptionStage, function (data) {
            PolicyComponent.policy.inceptionStage = inceptionStage;
            var policy = PolicyComponent.policies.find((p) => { return p["policyId"] === parseInt(policyId) });
            policy.inceptionStage = inceptionStage;

            if (inceptionStage) {
                $("#btnPolicyDetailsInceptionStage").html("Servicing Stage");
                $("#btnAddDriver").hide();
                $("#btnAddVehicle").hide();
            }
            else {
                $("#btnPolicyDetailsInceptionStage").html("Inception Stage");
                $("#btnAddDriver").show();
                $("#btnAddVehicle").show();
            }
            
        });
    },
    initEventHandlers: function () {
        $("#btnAddPolicy").click(function () {
            PolicyComponent.addPolicy();
        });

        $("html").on("click", '.btn-view-policy', function () {
            var policyId = $(this).attr('data-id');
            PolicyComponent.selectedPolicyId = policyId;
            PolicyComponent.viewPolicy(policyId);
        });

        $("#btnClosePolicyForm").click(function () {
            PolicyComponent.closePolicyForm();
        });

        $("#slcPolicyCoverageType").bind("change", function () {
            PolicyComponent.onCoverageTypeChange(true);
        });

        $("#txtPolicyEffectiveDate").bind("change", function () {
            PolicyComponent.onEffectivityDateChange();
        });

        $("#btnRateCalculatorConfirm").click(function () {
            debugger;
            PolicyComponent.assignRate();
        });

        $("#txtRateCalculatorUnits").on("keyup", function () {
            PolicyComponent.calculateRate();
        });

        $("#txtRateCalculatorUnits").on("focus", function () {
            PolicyComponent.calculateRate();
        });

        $("#chkGrossReceipt").on("change", function () {
            PolicyComponent.onGrossReceiptChange();
        });

        $("input[type='text'][data-model='premium'], input[type='text'][data-model='surplusTaxRate'], input[type='text'][data-model='surcharge'], input[type='text'][data-model='policyFees'], input[type='text'][data-model='mgafees']").bind("keyup", function () {
            PolicyComponent.computeSurplusTax();
        });

        $("input[type='text'][data-model='commRate']").bind("keyup", function () {
            PolicyComponent.computeCommission();
        });

        $("#chkTaxedPolicyFees").on("change", function () {
            PolicyComponent.computeSurplusTax();
        });

        $("#chkTaxedMgaFees").on("change", function () {
            PolicyComponent.computeSurplusTax();
        });

        $("input[type='text'][data-model='otherFees']").on("keyup", function () {
            PolicyComponent.computeTotal();
        });

        $("#frmPolicy").on("submit", function () {
            
            PolicyComponent.savePolicy();
            return false;
        })

        $("#btnPolicyDetailsInceptionStage").on("click", function () {
            PolicyComponent.setInceptionStage();
        });

        $("#btnEditPolicy").on("click", function () {
            PolicyComponent.editPolicy();
        });

        BindingService.bindFormulaInput();
    }
}