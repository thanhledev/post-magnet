var FormValidation = function () {

    var handleChangeInvoiceValidation = function() {
        var form = $("#form-invoice-detail");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                invoice_id: {
                    required: true
                },
                invoice_status: {
                    required: true
                },
                invoice_method: {
                    required: true
                }
            },
            invalidHandler: function() { //display error alert on form submit
                error.show();
                App.scrollTo(error, -200);
            },
            errorPlacement: function(error, element) { // render error placement for each input type
                var icon = $(element).parent(".input-icon").children("i");
                icon.removeClass("fa-check").addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ "container": "body" });
            },
            highlight: function(element) { // hightlight error inputs
                $(element)
                    .closest(".form-group").removeClass("has-success").addClass("has-error"); // set error class to the control group   
            },
            unhighlight: function() { // revert the change done by hightlight

            },
            success: function (label, element) {
                var icon = $(element).parent(".input-icon").children("i");
                $(element).closest(".form-group").removeClass("has-error").addClass("has-success"); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },
            submitHandler: function(form) {
                error.hide();
                var model = {
                    __RequestVerificationToken: $(form).find('input[name="__RequestVerificationToken"]').val(),
                    Id: $(form).find('input[name="invoice_id"]').val(),
                    Status: $(form).find('select[name="invoice_status"]').val(),
                    Method: $(form).find('select[name="invoice_method"]').val()
                };
                $.post(formChangeInvoiceAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    invoiceUpdateStatus = data.success;
                    $("#invoice-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleChangeInvoiceValidation();
        }
    };
}();

jQuery(document).ready(function () {
    FormValidation.init();
});