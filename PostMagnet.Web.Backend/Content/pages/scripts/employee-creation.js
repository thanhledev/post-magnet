var Creation = function () {

    return {

        //main function
        init: function () {
            Creation.initNotific8();
            Creation.initSwitch();
            Creation.initButtons();
            Creation.initRateDisplay();
        },

        initSwitch: function () {
            $(".make-switch").bootstrapSwitch();
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
            $("#employee-create").on("click", function() {
                $("#employee-create-dialog").modal('show');
            });

            $("#employee-submit").on("click", function() {
                $("#employee-creation-form").submit();
            });
        },

        initRateDisplay: function() {
            $("#employee-role").on("change", function(e) {
                var selectedValue = this.value;

                if (selectedValue === '3') {
                    $("#employee-rate").closest('div.form-group').removeClass("hide");
                } else {
                    $("#employee-rate").closest('div.form-group').addClass("hide");
                    $("#employee-rate").val("0");
                }
            });
        }
    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function () {
        Creation.init();
    });
}