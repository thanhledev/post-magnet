var AdminInvoice = function () {

    return {

        //main function
        init: function () {
            AdminInvoice.initNotific8();
            AdminInvoice.initButtons();
            AdminInvoice.initSelect2();
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

        initButtons: function () {
            $("#invoice-submit").on("click", function () {
                $("#form-invoice-detail").submit();
            });
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
    AdminInvoice.init();
});
