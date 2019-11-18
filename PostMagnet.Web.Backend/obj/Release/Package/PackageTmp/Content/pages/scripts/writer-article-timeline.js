var initializeSummernote = function (elem, code) {
    $(elem).summernote({
        height: 200, // set editor height
        width: "90%", // set editor height
        minHeight: null, // set minimum height of editor
        maxHeight: null, // set maximum height of editor
        dialogsInBody: true
    });

    $(elem).summernote('code', code);
    $(elem).summernote('disable');
};

var displayJobExtraPayments = function (payments) {
    $("#job-payment-list-dialog").find('#payment-list').html("");

    // load all payments to view
    payments.forEach(function (payment, index) {
        var content = '';

        content += '<div class="form-group">';
        content += '<label class="control-label col-md-2">Payment #' + (++index) + '</label>';
        content += '<div class="col-md-10">';
        content += '<input type="text" class="form-control" placeholder="' + payment.Amount + '" disabled/><br>';
        content += '<textarea class="form-control" placeholder="' + payment.Note + '" rows="3" disabled></textarea>';
        content += '</div>';
        content += '</div>';

        $("#job-payment-list-dialog").find('#payment-list').append(content);
    });

    $("#job-payment-list-dialog").modal('show');
};

var getJobExtraPaymentInformation = function (jobId) {
    $.ajax({
        url: "/job/jobextrapaymentlist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                displayJobExtraPayments(data.payments);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function (req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, errorSettings);
        }
    });
};

var displayJobResources = function (resources) {
    $("#job-resource-list-dialog").find('#resource-list').html("");

    // load all resources to view
    resources.forEach(function (resource, index) {
        var content = '';

        content += '<div class="form-group">';
        content += '<label class="control-label col-md-2">Resource #' + (++index) + '</label>';
        content += '<div class="col-md-10">';
        content += '<div id="summernote-' + index + '"></div>';
        content += '</div>';
        content += '</div>';

        $("#job-resource-list-dialog").find('#resource-list').append(content);

        var currentSummernote = $("#job-resource-list-dialog").find('div[id="summernote-' + index + '"]');

        initializeSummernote(currentSummernote, resource.Content);
    });

    $("#job-resource-list-dialog").modal('show');
};

var getJobResourceInformation = function (jobId) {
    $.ajax({
        url: "/job/jobresourcelist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                displayJobResources(data.resources);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function (req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, errorSettings);
        }
    });
};

var displayArticleTimelines = function(timelines) {
    $(".timeline-addition-placeholder").html("");

    // load all the timeline
    timelines.forEach(function(timeline) {
        var content = '';

        content += '<div class="timeline-item">';
        content += '<div class="timeline-badge"><img class="timeline-badge-userpic" src="/Content/pages/img/avatars/team1.jpg"></div>';
        content += '<div class="timeline-body">';
        content += '<div class="timeline-body-arrow"> </div>';
        content += '<div class="timeline-body-head"><div class="timeline-body-head-caption"><a href="javascript:;" class="timeline-body-title font-blue-madison">' + timeline.AuthorName + '</a><span class="timeline-body-time font-grey-cascade">Submitted at ' + timeline.SubmitDate + '</span></div></div>';
        content += '<div class="timeline-body-content">';
        content += '<div id="article-content-' + timeline.Id + '"></div>';
        content += '</div>';
        content += '</div>';
        content += '</div>';

        $(".timeline-addition-placeholder").append(content);

        var currentSummernote = $(".timeline-addition-placeholder").find('div[id="article-content-' + timeline.Id + '"]');

        initializeSummernote(currentSummernote, timeline.Content);
    });
};

var getTimelineInformation = function () {
    var jobId = $(".btn-refresh-timeline").data("job-id");
    $.ajax({
        url: "/articletimeline/writerarticletimelinelist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                displayArticleTimelines(data.timelines);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function (req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, errorSettings);
        }
    });
};

var ArticleSubmissionDialogEvent = function() {

    var handleDialogEvents = function() {

        $("#writer-article-submission-dialog").on("hidden.bs.modal", function(e) {
            if (articleSubmissionStatus) {
                getTimelineInformation();
                $("#add-article").remove();
            }
        });
    }

    return {
        init: function() {
            handleDialogEvents();
        }
    };

}();

var ArticleTimeline = function () {

    return {

        //main function
        init: function() {
            ArticleTimeline.initNotific8();
            ArticleTimeline.initSelect2();
            ArticleTimeline.initButtons();
            ArticleTimeline.initSummernote();
            ArticleTimeline.initTimeline();
        },

        initNotific8: function() {
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

        initSelect2: function() {
            $.fn.select2.defaults.set("theme", "bootstrap");
            var placeholder = "Select types";

            $(".select2-multiple").select2({
                placeholder: placeholder,
                width: null
            });
        },

        initButtons: function() {
            $("#view-resources").on("click", function() {
                var jobId = $(this).data("job-id");
                getJobResourceInformation(jobId);
            });

            $("#view-payments").on("click", function() {
                var jobId = $(this).data("job-id");
                getJobExtraPaymentInformation(jobId);
            });

            $("#add-article").on("click", function() {
                articleSubmissionStatus = false;
                $("#writer-article-submission-dialog").modal('show');
            });

            $("#writer-article-submit").on("click", function() {
                $("#form-writer-article-submission-detail").submit();
            });

            $(".btn-refresh-timeline").on("click", function() {
                getTimelineInformation();
            });
        },

        initSummernote: function() {
            $("#writer-submission-content").summernote({
                height: 300, // set editor height
                width: "90%", // set editor height
                minHeight: null, // set minimum height of editor
                maxHeight: null, // set maximum height of editor
                dialogsInBody: true
            });
        },

        initTimeline: function() {
            getTimelineInformation();
        }
    };

}();

jQuery(document).ready(function () {
    ArticleTimeline.init();
    ArticleSubmissionDialogEvent.init();
});
