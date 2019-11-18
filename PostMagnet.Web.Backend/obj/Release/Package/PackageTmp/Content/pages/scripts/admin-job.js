var AdminJob = function () {

    return {

        //main function
        init: function () {
            AdminJob.initNotific8();
            AdminJob.initSwitch();
            AdminJob.initButtons();
            AdminJob.initSelect2();
            AdminJob.initDateRange();
            AdminJob.initTypeahead();
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

        initButtons: function () {
            $("#job-add").on("click", function () {
                jobOpenMode = 0;
                jobUpdateStatus = false;

                $("#form-job-detail").find("#job-status-placeholder").html('<span class="label label-warning"> Pending </span>');
                $("#form-job-detail").find("#article-status-placeholder").html('<span class="label label-success"> Unedit </span>');
                $("#form-job-detail").find("#submission-placeholder").html('<span class="label label-info"> Not yet </span>');

                $("#job-dialog").modal('show');
            });
            $("#job-submit").on("click", function () {
                $("#form-job-detail").submit();
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
            $('#job-deadline').daterangepicker({
                    opens: (App.isRTL() ? 'left' : 'right'),
                    startDate: moment(),
                    minDate: moment(),
                    singleDatePicker: true,
                    showDropdowns: true,
                    showWeekNumbers: true,
                    timePicker: true,
                    timePickerIncrement: 1,
                    timePicker24Hour: true,
                    timePickerSeconds: true,
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
                function (start) {
                    deadlineDate = start.format('MMMM DD YYYY HH:mm:ss');
                    $('#job-deadline span').html(deadlineDate);
                }
            );
            deadlineDate = moment().format('MMMM DD YYYY HH:mm:ss');
            $('#job-deadline span').html(deadlineDate);
        },

        initTypeahead: function () {
            var writerList = new Bloodhound({
                datumTokenizer: function(datum) {
                    return Bloodhound.tokenizers.whitespace(datum.username);
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                prefetch: {
                    url: '/job/getwriters',
                    filter: function(data) {
                        return $.map(data.writers, function (writer) {
                            return {
                                username: writer.username
                            }
                        });
                    }
                }
            });

            writerList.initialize();

            $('#job-writer-username').typeahead(
                {
                    hint: false,
                    highlight: false,
                    minLength: 2
                },
                {
                    name: 'writerList',
                    displayKey: 'username',
                    source: writerList.ttAdapter()
                }
            );
        }
    };

}();

jQuery(document).ready(function () {
    AdminJob.init();
});
