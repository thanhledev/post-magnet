var Profile = function () {

    return {

        //main function
        init: function () {

            Profile.initNotific8();
            Profile.initButtons();
        },

        initNotific8: function () {
            successSettings = {
                theme: "success",
                sticky: true,
                horizontalEdge: "top",
                verticalEdge: "right"
            };
            warningSettings = {
                theme: "warning",
                sticky: true,
                horizontalEdge: "top",
                verticalEdge: "right"
            };
            errorSettings = {
                theme: "error",
                sticky: true,
                horizontalEdge: "top",
                verticalEdge: "right"
            };
        },

        initButtons: function() {
            $(".btn-update-rate-cost").on("click", function() {
                var btn = this;

                // hide button
                $(btn).addClass("hide");

                // enable input
                $(btn).closest(".form-group").find(".class-rate-cost").prop("readonly", false);

                // display other buttons
                $(btn).siblings(".btn-sumit-rate-cost").removeClass("hide");
                $(btn).siblings(".btn-cancel-rate-cost").removeClass("hide");
            });

            $(".btn-cancel-rate-cost").on("click", function() {
                var btn = this;

                // hide buttons
                $(btn).addClass("hide");
                $(btn).siblings(".btn-sumit-rate-cost").addClass("hide");

                // disable input
                $(btn).closest(".form-group").find(".class-rate-cost").prop("readonly", true);

                // display another button
                $(btn).siblings(".btn-update-rate-cost").removeClass("hide");
            });

            $(".btn-sumit-rate-cost").on("click", function() {
                $.ajax({
                    url: formUpdateRateAction,
                    type: "POST",
                    data: { Username: $("#employee-username").val(), Rate: $(this).closest(".form-group").find(".class-rate-cost").val() },
                    dataType: "json",
                    success: function (data) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, data.success ? successSettings : errorSettings);
                    },
                    error: function (req, status, error) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, errorSettings);
                    }
                });
            });
        }
    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function () {
        Profile.init();
    });
}