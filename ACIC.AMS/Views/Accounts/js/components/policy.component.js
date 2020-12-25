var Policy = {
    availableCoverageTypes: null,
    dTable: null,
    init: function () {
        this.initEventHandlers();
    },
    getAvailableCoverageTypes: function (id) {
        PolicyService.getAvailableCoverageTypes(id, function (data) {
            Policy.availableCoverageTypes = data;
        });
    },
    initEventHandlers: function () {
        if ($.fn.dataTable.isDataTable('#dTablePolicies')) {
            Policy.dTable.destroy();
        }

        Policy.dTable = $("#dTablePolicies").DataTable({
            "pageLength": 5,
            "searching": false,
            "bLengthChange": false, "bInfo": false,
            "columnDefs": [{
                "targets": 6,
                "orderable": false
            }]
        })
    }
}