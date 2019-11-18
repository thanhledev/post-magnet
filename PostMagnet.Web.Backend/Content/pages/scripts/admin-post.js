var AdminPost = function () {

    return {

        //main function
        init: function () {
            AdminPost.initNotific8();
            AdminPost.initSwitch();
            AdminPost.initDateRange();
            AdminPost.initWebsite();
            AdminPost.initSelectionChange();
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

        initDateRange: function () {
            $('#post-schedule').daterangepicker({
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
                    postScheduleDate = start.format('MMMM DD YYYY HH:mm:ss');
                    $('#post-schedule span').html(postScheduleDate);
                }
            );
            postScheduleDate = moment().format('MMMM DD YYYY HH:mm:ss');
            $('#post-schedule span').html(postScheduleDate);
        },

        initWebsite: function() {
            $.ajax({
                url: "/post/getavailablewesites",
                type: "GET"
            }).done(function (data) {
                var content = '<option value="" disabled selected>Please choose a website</option>';
                $.each(data.websites, function (i) {
                    content += '<option value="' + data.websites[i].Host + '">' + data.websites[i].Host + '</option>';
                });
                $("#post-to-website").html(content);
                $("#post-to-website").selectpicker('refresh');

            }).fail(function () {
                $.notific8("zindex", 11500);
                $.notific8("Failed!", errorSettings);
                $("#post-to-website").empty();
                $("#post-to-category").empty();
            });
        },

        initSelectionChange: function() {
            $('#post-to-website').on('change', function() {
                var selectedHost = $(this).val();
                $("#post-to-category").empty().html('<option value="" disabled selected>Loading...</option>');
                $("#post-to-category").prop("disabled", true);
                App.blockUI({
                    boxed: true,
                    message: 'Processing...'
                });
                $.ajax({
                    url: "/post/getavailablecategories",
                    type: "POST",
                    data: { host: selectedHost },
                    dataType: "json"
                }).done(function (data) {
                    var content = '<option value="" disabled selected>Please choose a category</option>';
                    $.each(data.categories, function(i) {
                        content += '<option value="' + data.categories[i].Name + '">' + data.categories[i].Name + '</option>';
                    });
                    $("#post-to-category").empty().html(content);
                }).fail(function() {
                    $.notific8("zindex", 11500);
                    $.notific8("Failed!", errorSettings);
                }).always(function() {
                    App.unblockUI();
                    $("#post-to-category").prop("disabled", false);
                    $("#post-to-category").selectpicker('refresh');
                });
            });

            $("#post-confirmation").on('change', function() {
                var confirmation = $(this).val();
                confirmation === 'Approved' ? $("#post-submission-action").removeClass('hide') : $("#post-submission-action").addClass('hide');
            });

            $("#post-submission").on('change', function() {
                var submission = $(this).val();

                if (submission === 'No') {
                    $('#post-submit-details').addClass('hide');
                    $('#post-schedule-details').addClass('hide');
                } else if (submission === 'Yes') {
                    $('#post-submit-details').removeClass('hide');
                    $('#post-schedule-details').addClass('hide');
                } else {
                    $('#post-submit-details').removeClass('hide');
                    $('#post-schedule-details').removeClass('hide');
                }
            });
        }
    };

}();

jQuery(document).ready(function () {
    AdminPost.init();
});
