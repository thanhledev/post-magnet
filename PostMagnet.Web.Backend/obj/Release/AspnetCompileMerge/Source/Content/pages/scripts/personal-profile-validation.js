var FormValidation = function () {

    $("#profile-update").on('click', function() {
        $("#profile-buttons").removeClass("hide");
        $("#form-change-profile input[type=text]").each(function() {
            $(this).removeAttr("readonly");
        });
        $(this).addClass("hide");
    });

    $("#profile-cancel").on('click', function() {
        $("#profile-buttons").addClass("hide");
        $("#form-change-profile input[type=text]").each(function () {
            $(this).attr('readonly', true);
        });
        $("#profile-update").removeClass("hide");
    });

    var handleChangeProfileValidation = function() {
        var form = $("#form-change-profile");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                employee_name: {
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
                    Name: $(form).find('input[name="employee_name"]').val(),
                    Email: $(form).find('input[name="employee_email"]').val(),
                    Phone: $(form).find('input[name="employee_phone"]').val()
                };
                $.post(formChangeProfileAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                }, "json");
                return false; // submit the form
            }
        });
    }

    var handleChangePasswordValidation = function () {

        var form = $("#form-change-password");
        var error = $(".alert-danger", form);
        var button = $("#password-submit");

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",  // validate all fields including form hidden input
            rules: {
                employee_old_password: {
                    required: true
                },
                employee_new_password: {
                    minlength: 5,
                    required: true
                },
                employee_confirm_password: {
                    minlength: 5,
                    required: true,
                    equalTo: "#employee-new-password"
                }
            },
            invalidHandler: function () { //display error alert on form submit
                error.show();
                App.scrollTo(error, -200);
            },
            errorPlacement: function (error, element) { // render error placement for each input type
                var icon = $(element).parent(".input-icon").children("i");
                icon.removeClass("fa-check").addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ "container": "body" });
            },
            highlight: function (element) { // hightlight error inputs
                $(element)
					.closest(".form-group").removeClass("has-success").addClass("has-error"); // set error class to the control group   
            },
            unhighlight: function () { // revert the change done by hightlight

            },
            success: function (label, element) {
                var icon = $(element).parent(".input-icon").children("i");
                $(element).closest(".form-group").removeClass("has-error").addClass("has-success"); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },

            submitHandler: function (form) {
                error.hide();
                var model = {
                    __RequestVerificationToken: $(form).find('input[name="__RequestVerificationToken"]').val(),
                    OldPassword: $(form).find('input[name="employee_old_password"]').val(),
                    NewPassword: $(form).find('input[name="employee_new_password"]').val(),
                    ConfirmPassword: $(form).find('input[name="employee_confirm_password"]').val()
                };
                $.post(formChangePasswordAction, $.param(model), function (data) {
                    if (data.success) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, successSettings);
                        button.attr("disabled", "disabled");
                    } else {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, errorSettings);
                    }
                }, "json");
                return false; // submit the form
            }
        });
    }

    var handlePasswordStrengthChecker = function () {
        var initialized = false;
        var input = $("#employee-new-password");

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
            handleChangeProfileValidation();
            handleChangePasswordValidation();
        }
    };
}();

jQuery(document).ready(function () {
    FormValidation.init();
});