var EndorsementComponent = {
    endorsements: null,
    dTable: null,
    onSearchEndorsementTimeout: null,
    init: function () {
        this.initEventHandlers();
    },
    getEndorsements: function (id, type, searchText = '') {
        $("#endorsementsPreloader").removeClass('hide');
        $(".endorsement-list-content").addClass('hide');
        $("#tbodyEndorsementList").html('');
        EndorsementService.getEndorsements(id, type, searchText,  function (data) {
            $(".endorsement-list-content").removeClass('hide');
            $("#endorsementsPreloader").addClass('hide');

            EndorsementComponent.endorsements = data;
            EndorsementComponent.renderEndorsementList();
        });
    },
    addEndorsement: function () {
        $("#mdlEndorsementForm").modal({
            backdrop: 'static'
        });
    },
    renderEndorsementList: function () {
        $("#tbodyEndorsementList").html('');
        /*if ($.fn.dataTable.isDataTable('#dTableEndorsements')) {
            EndorsementComponent.dTable.destroy();
            $("#tbodyEndorsementList").html('');
        }*/

        if (EndorsementComponent.endorsements.length > 0) {
            for (var i = 0; i < EndorsementComponent.endorsements.length; i++) {
                var endorsement = EndorsementComponent.endorsements[i];
                var effective = ValidationService.formatDate(endorsement.effective);
                var year = endorsement.year == null ? '' : endorsement.year;
                var make = endorsement.make == null ? '' : endorsement.make;
                var vin = endorsement.vin == null ? '' : endorsement.vin;
                var pdvalue = endorsement.pdvalue == null ? '' : endorsement.pdvalue;
                var surcharge = endorsement.surcharge == null ? '' : endorsement.surcharge;
                var al = endorsement.al == null ? '' : endorsement.al;
                var mtc = endorsement.mtc == null ? '' : endorsement.mtc;
                var apd = endorsement.apd == null ? '' : endorsement.apd;
                var brokerFees = endorsement.brokerFees == null ? '' : endorsement.brokerFees;
                var endtFees = endorsement.endtFees == null ? '' : endorsement.endtFees;
                var totalAmount = endorsement.totalAmount == null ? '' : endorsement.totalAmount;
                var variance = endorsement.variance == null ? '' : endorsement.variance;
                var row = `<tr>
                            <td> `+ effective + `</td>
                            <td> `+ endorsement.description + `  </td>
                            <td> `+ endorsement.action +` </td>
                            <td> `+ year+` </td>
                            <td> `+ make +`  </td>
                            <td> `+ vin +` </td>
                            <td> `+ pdvalue +` </td>
                            <td> `+ surcharge +` </td>
                            <td> `+ al + ` </td>
                            <td> `+ mtc + ` </td>
                            <td> `+ apd + ` </td>
                            <td> `+ brokerFees + ` </td>
                            <td> `+ endtFees + ` </td>
                            <td> `+ totalAmount + ` </td>
                            <td> `+ endorsement.status + ` </td>
                            <td> `+ variance +` </td>
                            <td class="action"> <button class="btn btn-success btn-sm btn-endorsement-details" title="View Details"><i class="fa fa-search"></i></button>  </td>
                        </tr>`;
                $("#tbodyEndorsementList").append(row);
            }
        }

        /*EndorsementComponent.dTable = $("#dTableEndorsements").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false, "bInfo": false,
            "columnDefs": [{
                "targets": 8,
                "orderable": false
            }]
        })*/
    },
    onSearchEndorsement: function () {
        clearTimeout(PolicyComponent.onSearchEndorsementTimeout);
        PolicyComponent.onSearchEndorsementTimeout = setTimeout(function () {

            var type = $("#slcEndorsementTypeFilter").val();
            var search = $("#txtSearchEndorsement").val();
            EndorsementComponent.getEndorsements(CurrentAccount.accountId, type, search);
        }, 1500);
    },
    onEndorsementTypeChange: function () {
        var endorsementType = $("#slcEndorsementType").val();
        var action = $("#slcEndorsementAction").val();

        $("#frmNewEndorsement").find('.form-group.description').addClass('hide');
        $("#frmNewEndorsement").find('.form-body.vin').addClass('hide');
        $("#slcAvailableVehicles").html('');
        switch (endorsementType) {
            case "Tractor":
            case "Trailer":
            case "Truck":
                $("#frmNewEndorsement").find('.form-group.description').removeClass('hide');
                $("#frmNewEndorsement").find('.form-body.vin').removeClass('hide');
                $("#slcAvailableVehicles").append("<option value=''>&nbsp;</option>");
                EndorsementService.getAvailableVehicles(CurrentAccount.accountId, endorsementType, action, function (data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            var vehicle = data[i];
                            /*id: 234
legalName: "DBA Transport Inc"
make: "Freightliner"
pdvalue: 30000
vYear: "2012"
vin: "1FUJGMBG4CDBK2919"*/

                            var vehicleOption = "<option value=" + vehicle.id + ">" + vehicle.vin + " | " + vehicle.vYear + " | " + vehicle.make + " | " + vehicle.legalName +  "</option>";
                            $("#slcAvailableVehicles").append(vehicleOption);
                        }
                    }
                });
                break;

        }
    },
    initEventHandlers: function () {
        $("#slcEndorsementTypeFilter").on("change", function () {
            var type = $(this).val();
            EndorsementComponent.getEndorsements(CurrentAccount.accountId, type);
        });

        $("#btnAddEndorsement").on("click", function () {
            EndorsementComponent.addEndorsement();
        });

        $("#txtSearchEndorsement").on("keyup", function () {
            EndorsementComponent.onSearchEndorsement();
        });

        $("#txtSearchEndorsement").on("keydown", function () {
            clearTimeout(PolicyComponent.onSearchEndorsementTimeout);
        });

        $("#btnExportEndorsements").on("click", function () {
            window.open('/api/accounts/' + CurrentAccount.accountId + '/endorsements/download', '_blank');
        });

        $("#slcEndorsementType").on("change", function () {
            EndorsementComponent.onEndorsementTypeChange();
        });
    }
}