ValidationService = {
    isNumberKey: function (txt, evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 46) {
            //Check if the text already contains the . character
            if (txt.val().indexOf('.') === -1) {
                return true;
            } else {
                return false;
            }
        }
        if (charCode == 45) {
            if (txt.val() === "") {
                return true;
            } else {
                return false;
            }
        }
        else {
            if (charCode > 31 &&
                (charCode < 48 || charCode > 57))
                return false;
        }
        return true;
    },
    isFomulaKey: function (txt, evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 46) {
            //Check if the text already contains the . character
            if (txt.value.indexOf('.') === -1) {
                return true;
            } else {
                return false;
            }
        }
        if (charCode == 61 || charCode == 43 || charCode == 45 || charCode == 42 || charCode == 47) {
            return true;
        }
        else {
            if (charCode > 31 &&
                (charCode < 48 || charCode > 57))
                return false;
        }
        return true;
    },
    maskPhoneNumber: function(){
        $("input.phone-number").mask("(999) 999-9999");
    },
    bindEmailAddressValidation: function () {
        $("input.emailaddress").bind("keyup", function () {
            var val = $(this).val();
            $(this).removeClass('validator-error');

            $(this).parents('form').find('button[type="submit"]').removeAttr("disabled");
            if (val != '') {
                var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!regex.test(val)) {
                    $(this).addClass('validator-error');
                    $(this).parents('form').find('button[type="submit"]').attr("disabled", true);
                }
            }
        });
    },
    bindDecimalValidation: function () {
        $("html").on("keypress", "input[type='text'][data-type='decimal']", function (e) {
            var txt = $(this);
            return ValidationService.isNumberKey(txt, e);
        });
    },
    bindFormulaValidation: function () {
        $("html").on("keypress", "input[type='text'][data-type='formula']", function (e) {
            var txt = $(this);
            return ValidationService.isFomulaKey(txt, e);
        });
    },
    formatDate: function (date) {
        var formattedDateStr = '';
        var formattedDate = date.replace('/', '-').replace('/', '-');
        if (formattedDate != '') {
            formattedDate = formattedDate.substr(0, 10);
            var date = new Date(formattedDate);
            var month = date.getMonth() + 1;
            var monthStr = month.toString().padStart(2, '0');
            var day = date.getDate().toString().padStart(2, '0');
            formattedDateStr = date.getFullYear() + '-' + monthStr + '-' + day;
        }

        return formattedDateStr;
    },
    formatMoney: function (x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
}

$(document).ready(function () {
    ValidationService.maskPhoneNumber();
    ValidationService.bindEmailAddressValidation();
    ValidationService.bindDecimalValidation();
    ValidationService.bindFormulaValidation();
});