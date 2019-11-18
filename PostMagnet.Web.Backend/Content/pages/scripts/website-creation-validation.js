var FormValidation = function () {

    var handleWebsiteValidation = function () {
        var form = $("#website-form");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                website_host: {
                    required: true
                },
                website_username: {
                    required: true
                },
                website_password: {
                    minlength: 8,
                    required: true
                },
                website_confirm_password: {
                    minlength: 8,
                    required: true,
                    equalTo: "#website-password"
                },
                website_timezone: {
                    required: true
                },
                website_seo_plugin_type: {
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
                    Host: $(form).find('input[name="website_host"]').val(),
                    Username: $(form).find('input[name="website_username"]').val(),
                    Password: $(form).find('input[name="website_password"]').val(),
                    TimeZone: $(form).find('select[name="website_timezone"]').val(),
                    SeoPluginType: $(form).find('select[name="website_seo_plugin_type"]').val(),
                    ConfirmPassword: $(form).find('input[name="website_confirm_password"]').val(),
                    RequireTesting: $(form).find('input[name="website_check_connectivity"]').is(':checked')
                };
                var formAction = openMode === 1 ? formCreateWebsiteAction : formUpdateWebsiteAction;
                if (openMode === 2) model.Id = $(form).find('input[name="website_id"]').val();
                $.post(formAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    actionStatus = data.success;
                    $("#website-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }    

    var handlePasswordStrengthChecker = function () {
        var initialized = false;
        var input = $("#website-password");

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
            handleWebsiteValidation();
        }
    };
}();

jQuery(document).ready(function () {
    FormValidation.init();
});