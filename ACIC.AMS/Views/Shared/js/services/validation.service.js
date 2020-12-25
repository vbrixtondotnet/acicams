ValidationService = {
    isNumberKey: function(txt, evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 46) {
            //Check if the text already contains the . character
            if (txt.value.indexOf('.') === -1) {
                return true;
            } else {
                return false;
            }
        } else {
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
    }
}

$(document).ready(function () {
    ValidationService.maskPhoneNumber();
    ValidationService.bindEmailAddressValidation();
});