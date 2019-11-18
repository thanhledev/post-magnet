var Login = function () {

    var handleLogin = function () {

        $(".login-form").validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                Username: {
                    required: true
                },
                Password: {
                    required: true
                }
            },

            messages: {
                Username: {
                    required: "Username is required."
                },
                Password: {
                    required: "Password is required."
                }
            },

            invalidHandler: function () { //display error alert on form submit   
                $(".alert-danger", $(".login-form")).show();
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest(".form-group").addClass("has-error"); // set error class to the control group
            },

            success: function (label) {
                label.closest(".form-group").removeClass("has-error");
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest(".input-icon"));
            },

            submitHandler: function (form) {
                $.post(formAction, $(form).serialize(), function (data) {
                    if (data.success) {
                        window.location.assign(data.redirectUrl);
                    } else {
                        $(".alert-danger", $(".login-form")).show();
                        $("#btnLogin").html("Login");
                        $("#btnLogin").removeAttr("disabled");
                    }
                }, "json");
                return false;
            }
        });

        $(".login-form input").keypress(function (e) {
            if (e.which === 13) {
                if ($(".login-form").validate().form()) {
                    $("#btnLogin").prop("disabled", "disabled");
                    $("#btnLogin").html("Please wait...");
                    $(".login-form").submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleLogin();
        }
    };
}();

jQuery(document).ready(function () {
    Login.init();
});