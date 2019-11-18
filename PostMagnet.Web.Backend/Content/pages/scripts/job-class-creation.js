var JobClass = function () {

    return {

        //main function
        init: function () {
            JobClass.initNotific8();
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
        }
    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function () {
        JobClass.init();
    });
}