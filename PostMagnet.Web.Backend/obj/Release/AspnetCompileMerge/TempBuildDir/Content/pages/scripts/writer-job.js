var WriterJob = function () {

    return {

        //main function
        init: function () {
            WriterJob.initNotific8();
            WriterJob.initSwitch();
            WriterJob.initSelect2();
        },

        initNotific8: function () {
            successSettings = {
                theme: "success",
                sticky: false,
                horizontalEdge: "top",
                verticalEdge: "right",
                life: 5000
            };
            warningSettings = {
                theme: "warning",
                sticky: false,
                horizontalEdge: "top",
                verticalEdge: "right",
                life: 5000
            };
            errorSettings = {
                theme: "error",
                sticky: false,
                horizontalEdge: "top",
                verticalEdge: "right",
                life: 5000
            };
        },

        initSwitch: function () {
            $(".make-switch").bootstrapSwitch();
        },

        initSelect2: function () {
            $.fn.select2.defaults.set("theme", "bootstrap");
            var placeholder = "Select types";

            $(".select2-multiple").select2({
                placeholder: placeholder,
                width: null
            });
        }
    };

}();

jQuery(document).ready(function () {
    WriterJob.init();
});
