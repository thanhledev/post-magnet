var Creation = function () {

    return {

        //main function
        init: function () {
            Creation.initNotific8();
            Creation.initSwitch();
            Creation.initButtons();
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
            $("#website-create").on("click", function () {
                openMode = 1;
                $("#website-form").find('input[name="website_tested"]').closest("div.form-group").addClass("hide");
                $("#website-form").find('textarea[name="website_note"]').closest("div.form-group").addClass("hide");
                $("#website-dialog").modal('show');
            });

            $("#website-submit").on("click", function() {
                $("#website-form").submit();
            });
        }
    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function () {
        Creation.init();
    });
}