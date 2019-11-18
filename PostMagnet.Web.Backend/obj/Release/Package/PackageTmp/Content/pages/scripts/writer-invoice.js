var WriterInvoice = function () {

    return {

        //main function
        init: function () {
            WriterInvoice.initNotific8();
            WriterInvoice.initButtons();
            WriterInvoice.initSelect2();
            WriterInvoice.initDateRange();
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
            $("#invoice-add").on("click", function() {
                $("#invoice-dialog").modal('show');
            });

            $("#invoice-submit").on("click", function() {
                $.ajax({
                    url: formAddInvoiceAction,
                    type: "POST",
                    data: { BeginDate: startDate, EndDate: endDate, Method: $('select[name="invoice_method"]').val() },
                    dataType: "json",
                    success: function (data) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, data.success ? successSettings : errorSettings);
                        invoiceUpdateStatus = data.success;
                        $("#invoice-dialog").modal('hide');
                    },
                    error: function (req, status, error) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, errorSettings);
                    }
                });
            });
        },

        initSelect2: function () {
            $.fn.select2.defaults.set("theme", "bootstrap");
            var placeholder = "Select types";

            $(".select2-multiple").select2({
                placeholder: placeholder,
                width: null
            });
        },

        initDateRange: function () {
            $('#invoice-job-range').daterangepicker({
                    opens: (App.isRTL() ? 'left' : 'right'),
                    startDate: moment().subtract('days', 29),
                    endDate: moment(),
                    minDate: '01/01/2016',
                    maxDate: '12/31/2020',
                    dateLimit: {
                        days: 1000
                    },
                    showDropdowns: true,
                    showWeekNumbers: true,
                    timePicker: true,
                    timePickerIncrement: 1,
                    timePicker24Hour: true,
                    timePickerSeconds: true,
                    ranges: {
                        'Today': [moment(), moment()],
                        'Yesterday': [moment().subtract('days', 1), moment().subtract('days', 1)],
                        'Last 7 Days': [moment().subtract('days', 6), moment()],
                        'Last 30 Days': [moment().subtract('days', 29), moment()],
                        'This Month': [moment().startOf('month'), moment().endOf('month')],
                        'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
                    },
                    buttonClasses: ['btn'],
                    applyClass: 'green',
                    cancelClass: 'default',
                    format: 'MMMM DD YYYY HH:mm:ss',
                    separator: ' to ',
                    locale: {
                        applyLabel: 'Apply',
                        fromLabel: 'From',
                        toLabel: 'To',
                        customRangeLabel: 'Custom Range',
                        daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
                        monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                        firstDay: 1
                    }
                },
                function (start, end) {
                    startDate = start.format('MMMM DD YYYY HH:mm:ss');
                    endDate = end.format('MMMM DD YYYY HH:mm:ss');
                    $('#invoice-job-range span').html(startDate + ' - ' + endDate);
                }
            );
            startDate = moment().subtract('days', 29).format('MMMM DD YYYY HH:mm:ss');
            endDate = moment().format('MMMM DD YYYY HH:mm:ss');
            $('#invoice-job-range span').html(startDate + ' - ' + endDate);
        }
    };

}();

jQuery(document).ready(function () {
    WriterInvoice.init();
});
