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

var displayJobExtraPayments = function (jobId, payments, openMode) {
    if (openMode) $("#job-payment-list-dialog").find('#extra-payment-add').data('payment-job-id', jobId);
    $("#job-payment-list-dialog").find('#payment-list').html("");

    // load all payments to view
    payments.forEach(function (payment, index) {
        var content = '';

        content += '<div class="form-group">';
        content += '<label class="control-label col-md-2">Payment #' + (++index) + '</label>';
        content += '<div class="col-md-10">';
        content += '<input type="text" class="form-control" placeholder="' + payment.Amount + '" disabled/><br>';
        content += '<textarea class="form-control" placeholder="' + payment.Note + '" rows="3" disabled></textarea><br>';
        if (jobAdditionEditableStatus) {
            content += '<a href="javascript:;" class="btn btn-sm blue btn-edit-payment-detail" style="margin-right:10px" data-payment-id="' + payment.Id + '" data-payment-job-id="' + jobId + '"> Edit<i class="fa fa-edit"></i></a>';
            content += '<a href="javascript:;" class="btn btn-sm red btn-remove-payment-detail" data-payment-id="' + payment.Id + '" data-payment-job-id="' + jobId + '"> Remove<i class="fa fa-trash"></i></a>';
        }
        content += '</div>';
        content += '</div>';

        $("#job-payment-list-dialog").find('#payment-list').append(content);
    });

    if (openMode) $("#job-payment-list-dialog").modal('show');
};

var getJobExtraPaymentInformation = function (jobId, openMode) {
    $.ajax({
        url: "/job/jobextrapaymentlist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                displayJobExtraPayments(jobId, data.payments, openMode);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function (req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(error, errorSettings);
        }
    });
};

var displayJobResources = function (jobId, resources, openMode) {
    if (openMode) $("#job-resource-list-dialog").find('#resource-add').data('resource-job-id', jobId);
    $("#job-resource-list-dialog").find('#resource-list').html("");

    // load all resources to view
    resources.forEach(function (resource, index) {
        var content = '';

        content += '<div class="form-group">';
        content += '<label class="control-label col-md-2">Resource #' + (++index) + '</label>';
        content += '<div class="col-md-10">';
        content += '<div id="summernote-' + index + '"></div><br>';
        if (jobAdditionEditableStatus) {
            content += '<a href="javascript:;" class="btn btn-sm blue btn-edit-resource-detail" style="margin-right:10px" data-resource-id="' + resource.Id + '" data-resource-job-id="' + jobId + '"> Edit<i class="fa fa-edit"></i></a>';
            content += '<a href="javascript:;" class="btn btn-sm red btn-remove-resource-detail" data-resource-id="' + resource.Id + '" data-resource-job-id="' + jobId + '"> Remove<i class="fa fa-trash"></i></a>';
        }
        content += '</div>';
        content += '</div>';

        $("#job-resource-list-dialog").find('#resource-list').append(content);

        var currentSummernote = $("#job-resource-list-dialog").find('div[id="summernote-' + index + '"]');

        initializeSummernote(currentSummernote, resource.Content);
    });

    if (openMode) $("#job-resource-list-dialog").modal('show');
};

var getJobResourceInformation = function (jobId, openMode) {
    $.ajax({
        url: "/job/jobresourcelist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                displayJobResources(jobId, data.resources, openMode);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function (req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(error, errorSettings);
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
        url: "/articletimeline/editorarticletimelinelist",
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
            $.notific8(error, errorSettings);
        }
    });
};

var ArticleSubmissionDialogEvent = function() {

    var handleDialogEvents = function() {
        $("#job-resource-detail-dialog").on("hidden.bs.modal", function (e) {
            if (jobResourceUpdateStatus) {
                var jobId = $("#resource-add").data('resource-job-id');
                getJobResourceInformation(jobId, false);
            }
        });
        $("#job-payment-detail-dialog").on("hidden.bs.modal", function () {
            if (jobExtraPaymentUpdateStatus) {
                var jobId = $("#extra-payment-add").data('payment-job-id');
                getJobExtraPaymentInformation(jobId, false);
            }
        });
        $("#editor-article-submission-dialog").on("hidden.bs.modal", function() {
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
                getJobResourceInformation(jobId, true);
            });

            $("#resource-add").on("click", function (e) {
                jobResourceOpenMode = 0;
                jobResourceUpdateStatus = false;

                $("#form-job-resource-detail").find('input[name="resource_id"]').val('');
                $("#form-job-resource-detail").find('input[name="resource_detail_job_id"]').val($(this).data("resource-job-id"));
                $("#resource-content").summernote('code', '');

                $("#job-resource-detail-dialog").modal('show');
            });

            $("#job-resource-list-dialog").on("click", ".btn-edit-resource-detail", function (e) {
                $.ajax({
                    url: "/job/getjobresource",
                    type: "POST",
                    data: { Id: $(this).data("resource-id"), JobId: $(this).data("resource-job-id") },
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            jobResourceOpenMode = 1;
                            jobResourceUpdateStatus = false;

                            $("#form-job-resource-detail").find('input[name="resource_id"]').val(data.resource.Id);
                            $("#form-job-resource-detail").find('input[name="resource_detail_job_id"]').val(data.resource.JobId);
                            $("#resource-content").summernote('code', data.resource.Content);

                            $("#job-resource-detail-dialog").modal('show');
                        } else {
                            $.notific8("zindex", 11500);
                            $.notific8(data.message, errorSettings);
                        }
                    },
                    error: function (req, status, error) {
                        $.notific8("zindex", 11500);
                        $.notific8(error, errorSettings);
                    }
                });
            });

            $("#job-resource-list-dialog").on("click", ".btn-remove-resource-detail", function (e) {
                var btn = $(this);

                bootbox.confirm("Are you sure?", function (result) {
                    if (result) {
                        $.ajax({
                            url: "/job/removejobresource",
                            type: "POST",
                            data: { Id: $(btn).data("resource-id"), JobId: $(btn).data("resource-job-id") },
                            dataType: "json",
                            success: function (data) {
                                $.notific8("zindex", 11500);
                                $.notific8(data.message, data.success ? successSettings : errorSettings);
                                if (data.success) {
                                    getJobResourceInformation($(btn).data("resource-job-id"), false);
                                }
                            },
                            error: function (req, status, error) {
                                $.notific8("zindex", 11500);
                                $.notific8(error, errorSettings);
                            }
                        });
                    }
                });
            });

            $("#resource-submit").on("click", function () {
                $("#form-job-resource-detail").submit();
            });

            $("#view-payments").on("click", function() {
                var jobId = $(this).data("job-id");
                getJobExtraPaymentInformation(jobId, true);
            });

            $("#extra-payment-add").on("click", function (e) {
                jobExtraPaymentOpenMode = 0;
                jobExtraPaymentUpdateStatus = false;

                $("#form-job-payment-detail").find('input[name="payment_id"]').val('');
                $("#form-job-payment-detail").find('input[name="payment_detail_job_id"]').val($(this).data("payment-job-id"));
                $("#form-job-payment-detail").find('input[name="extra_payment_amount"]').val('0');
                $("#form-job-payment-detail").find('textarea[name="extra_payment_note"]').val('');

                $("#job-payment-detail-dialog").modal('show');
            });

            $("#job-payment-list-dialog").on("click", ".btn-edit-payment-detail", function (e) {
                $.ajax({
                    url: "/job/getjobextrapayment",
                    type: "POST",
                    data: { Id: $(this).data("payment-id"), JobId: $(this).data("payment-job-id") },
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            jobExtraPaymentOpenMode = 1;
                            jobExtraPaymentUpdateStatus = false;

                            $("#form-job-payment-detail").find('input[name="payment_id"]').val(data.payment.Id);
                            $("#form-job-payment-detail").find('input[name="payment_detail_job_id"]').val(data.payment.JobId);
                            $("#form-job-payment-detail").find('input[name="extra_payment_amount"]').val(data.payment.Amount);
                            $("#form-job-payment-detail").find('textarea[name="extra_payment_note"]').val(data.payment.Note);

                            $("#job-payment-detail-dialog").modal('show');
                        } else {
                            $.notific8("zindex", 11500);
                            $.notific8(data.message, errorSettings);
                        }
                    },
                    error: function (req, status, error) {
                        $.notific8("zindex", 11500);
                        $.notific8(error, errorSettings);
                    }
                });
            });

            $("#job-payment-list-dialog").on("click", ".btn-remove-payment-detail", function (e) {
                var btn = $(this);

                bootbox.confirm("Are you sure?", function (result) {
                    if (result) {
                        $.ajax({
                            url: "/job/removejobextrapayment",
                            type: "POST",
                            data: { Id: $(btn).data("payment-id"), JobId: $(btn).data("payment-job-id") },
                            dataType: "json",
                            success: function (data) {
                                $.notific8("zindex", 11500);
                                $.notific8(data.message, data.success ? successSettings : errorSettings);
                                if (data.success) {
                                    getJobExtraPaymentInformation($(btn).data("payment-job-id"), false);
                                }
                            },
                            error: function (req, status, error) {
                                $.notific8("zindex", 11500);
                                $.notific8(error, errorSettings);
                            }
                        });
                    }
                });
            });

            $("#extra-payment-submit").on("click", function () {
                $("#form-job-payment-detail").submit();
            });

            $("#add-article").on("click", function() {
                articleSubmissionStatus = false;
                $("#editor-article-submission-dialog").modal('show');
            });

            $("#editor-article-submit").on("click", function() {
                $("#form-editor-article-submission-detail").submit();
            });

            $(".btn-refresh-timeline").on("click", function() {
                getTimelineInformation();
            });

            $("#mark-article-done").on("click", function() {
                $.ajax({
                    url: "/job/updatejobstatus",
                    type: "POST",
                    data: { Id: $(this).data("job-id"), Acceptance: true },
                    dataType: "json",
                    success: function (data) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, data.success ? successSettings : errorSettings);

                        if (data.success) {
                            $("#mark-article-done").remove();
                            $("#mark-article-fault").remove();
                        }
                    },
                    error: function (req, status, error) {
                        $.notific8("zindex", 11500);
                        $.notific8(error, errorSettings);
                    }
                });
            });

            $("#mark-article-fault").on("click", function () {
                $.ajax({
                    url: "/job/updatejobstatus",
                    type: "POST",
                    data: { Id: $(this).data("job-id"), Acceptance: false },
                    dataType: "json",
                    success: function (data) {
                        $.notific8("zindex", 11500);
                        $.notific8(data.message, data.success ? successSettings : errorSettings);

                        if (data.success) {
                            $("#mark-article-done").remove();
                            $("#mark-article-fault").remove();
                        }
                    },
                    error: function (req, status, error) {
                        $.notific8("zindex", 11500);
                        $.notific8(error, errorSettings);
                    }
                });
            });
        },

        initSummernote: function() {
            $("#editor-submission-content").summernote({
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
