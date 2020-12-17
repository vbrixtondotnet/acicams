var Drivers = {
    drivers: [],
    dTable: null,
    dTable2: null,
    init: function () {
        this.initEventHandlers();
    },
    getDrivers: function (id) {
        $("#driversPreloader").removeClass('hide');
        $(".driver-list-content").addClass('hide');
        $("#tblDrivers").html('');
        DriverService.getDrivers(id, function (data) {
            $(".driver-list-content").removeClass('hide');
            $("#driversPreloader").addClass('hide');
            Drivers.renderDriversTable(data);
        });
    },
    renderDriversTable: function (driversList) {
        Drivers.drivers = driversList;
        if ($.fn.dataTable.isDataTable('#dTableDrivers')) {
            Drivers.dTable.destroy();
            $("#tblDrivers").html('');
        }

        if (Drivers.drivers.length > 0) {
            for (var i = 0; i < Drivers.drivers.length; i++) {
                var driver = Drivers.drivers[i];
                var driverRow = ` <tr>
                                <td> `+ driver.firstName + ' ' + driver.lastName + ` </td>
                                <td> `+ driver.state + ` </td>
                                <td> `+ driver.cdlnumber + `  </td>
                                <td> `+ driver.cdlyearLic + ` </td>
                                <td> `+ driver.dateHiredString + ` </td>
                                <td> `+ driver.terminatedString + `</td>
                                <td class="action text-right"><button class="btn btn-success btn-sm" title="View Details"><i class="fa fa-search"></i></button>  </td>
                            </tr>`;
                $("#tblDrivers").append(driverRow);
            }
        }

        Drivers.dTable = $("#dTableDrivers").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false,
            "columnDefs": [{
                "targets": 6,
                "orderable": false
            }]
        })
    },
    initEventHandlers: function () {
        $("#btnAddDriver").click(function () {
            $("#mdlDriver").modal('show');

        });
    }
}