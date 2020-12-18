var BindingService = {
    bindModelToForm: function (formId, model) {
        var form = $("form#" + formId);
        //text inputs
        var textFields = form.find('input[type="text"]');
        $.each(textFields, function (ind, input) {
            var dataModel = $(input).attr("data-model");
            if(dataModel){
                $(input).val(model[dataModel]);
                $(input).on("keyup", function () {
                    model[dataModel] = $(this).val();
                });
            }
        });

        var selectFields = form.find('select');
        $.each(selectFields, function (ind, select) {
            var dataModel = $(select).attr("data-model");
            if (dataModel) {
                $(select).val(model[dataModel]);
                $(select).on("change", function () {
                    model[dataModel] = $(this).val();
                });
            }
        })

        var textAreas = form.find('textarea');
        $.each(textAreas, function (ind, textArea) {
            var dataModel = $(textArea).attr("data-model");
            if (dataModel) {
                $(textArea).val(model[dataModel]);
                $(textArea).on("keyup", function () {
                    model[dataModel] = $(this).val();
                });
            }
        })

        var checkboxes = form.find('input[type="checkbox"]');
        $.each(checkboxes, function (ind, checkbox) {
            var dataModel = $(checkbox).attr("data-model");
            if (dataModel) {
                $(checkbox).attr("checked",model[dataModel]);
                $(checkbox).on("change", function () {
                    var checked = $(checkbox).is(':checked');
                    model[dataModel] = checked;
                });
            }
        })

        var dateFields = form.find('input[type="date"]');
        $.each(dateFields, function (ind, input) {
            var dataModel = $(input).attr("data-model");
            if (dataModel) {
                var formattedDate = model[dataModel].replace('/', '-').replace('/','-');
                $(input).val(formattedDate);
                $(input).on("change", function () {
                    model[dataModel] = $(this).val();
                });
            }
        });

    },
    bindModelToLabels: function (containerId, model) {
        $("#" + containerId).find('.binding-service-field').each(function (ind, label) {
            var dataModel = $(label).attr("data-model");
            if (dataModel)
                $(label).html(model[dataModel]);
        });
    }
}