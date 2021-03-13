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

        var passwordFields = form.find('input[type="password"]');
        $.each(passwordFields, function (ind, input) {
            var dataModel = $(input).attr("data-model");
            if (dataModel) {
                $(input).val(model[dataModel]);
                $(input).on("keyup", function () {
                    model[dataModel] = $(this).val();
                });
            }
        });

        var emailFields = form.find('input[type="email"]');
        $.each(emailFields, function (ind, input) {
            var dataModel = $(input).attr("data-model");
            if (dataModel) {
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
                $(select).val(model[dataModel]).trigger('change');
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
                if (model[dataModel] != null) {
                    var formattedDateStr = '';
                    var formattedDate = model[dataModel].replace('/', '-').replace('/', '-');
                    if (formattedDate != '') {
                        formattedDate = formattedDate.substr(0, 10);
                        var date = new Date(formattedDate);
                        var month = date.getMonth() + 1;
                        var monthStr = month.toString().padStart(2, '0');
                        var day = date.getDate().toString().padStart(2, '0');
                        formattedDateStr = date.getFullYear() + '-' + monthStr + '-' + day;
                    }

                    $(input).val(formattedDateStr);
                }
               
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
    },
    bindFormulaInput: function () {
       
        $("input[type='text'][data-type='formula']").bind('focusout', function () {
            var formula = $(this).val();
            $(this).attr('data-formula', formula);

            if (formula.indexOf('=') == 0)
                $(this).val(math.eval(formula.replace('=', '')));
        });

        $("input[type='text'][data-type='formula']").bind('focus', function () {
            var formula = $(this).attr('data-formula');
            if (formula) $(this).val($(this).attr('data-formula'));
        });
    }
}