var FormValidation = function () {

    var handleCreateEmployeeValidation = function () {
        var form = $("#employee-creation-form");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                employee_username: {
                    required: true
                },
                employee_password: {
                    minlength: 8,
                    required: true
                },
                employee_confirm_password: {
                    minlength: 8,
                    required: true,
                    equalTo: "#employee-password"
                },
                employee_name: {
                    required: true
                },
                employee_role: {
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
                    Username: $(form).find('input[name="employee_username"]').val(),
                    Password: $(form).find('input[name="employee_password"]').val(),
                    ConfirmPassword: $(form).find('input[name="employee_confirm_password"]').val(),
                    Name: $(form).find('input[name="employee_name"]').val(),
                    Email: $(form).find('input[name="employee_email"]').val(),
                    Phone: $(form).find('input[name="employee_phone"]').val(),
                    IsActive: $(form).find('input[name="employee_is_active"]').is(':checked'),
                    RoleId: $(form).find('select[name="employee_role"]').val(),
                    Rate: $(form).find('input[name="employee_rate"]').val()
                };
                $.post(formCreateEmployeeAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    employeeCreationStatus = data.success;
                    $("#employee-create-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }    

    var handlePasswordStrengthChecker = function () {
        var initialized = false;
        var input = $("#employee-password");

        input.keydown(function () {
            if (initialized === false) {
                // set base options
                input.pwstrength({
                    raisePower: 1.4,
                    minChar: 8,
                    verdicts: ["Weak", "Normal", "Medium", "Strong", "Very Strong"],
                    scores: [17, 26, 40, 50, 70]
                });

                // add your own rule to calculate the password strength
                input.pwstrength("addRule", "demoRule", function (options, word, score) {
                    return word.match(/[a-z].[0-9]/) && score;
                }, 10, true);

                // set as initialized 
                initialized = true;
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handlePasswordStrengthChecker();
            handleCreateEmployeeValidation();
        }
    };
}();

jQuery(document).ready(function () {
    FormValidation.init();
});