var FormValidation = function () {

    var handleEditorSubmitArticleValidation = function () {
        var form = $("#form-editor-article-submission-detail");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                editor_submission_detail_job_id: {
                    required: true
                },
                editor_article_status: {
                    required: true
                }
            },
            invalidHandler: function() { //display error alert on form submit
                error.show();
                App.scrollTo(error, -200);
            },
            errorPlacement: function(error, element) { // render error placement for each input type
                var icon = $(element).parent(".input-icon").children("i");
                icon.removeClass("fa-check").addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ "container": "body" });
            },
            highlight: function(element) { // hightlight error inputs
                $(element)
                    .closest(".form-group").removeClass("has-success").addClass("has-error"); // set error class to the control group   
            },
            unhighlight: function() { // revert the change done by hightlight

            },
            success: function (label, element) {
                var icon = $(element).parent(".input-icon").children("i");
                $(element).closest(".form-group").removeClass("has-error").addClass("has-success"); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },
            submitHandler: function(form) {
                error.hide();
                var model = {
                    __RequestVerificationToken: $(form).find('input[name="__RequestVerificationToken"]').val(),
                    JobId: $(form).find('input[name="edtitor_submission_detail_job_id"]').val(),
                    Content: $(form).find('#editor-submission-content').summernote('code'),
                    ArticleStatus: $(form).find('select[name="editor_article_status"]').val()
                };
                $.post(formSubmitArticleAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    articleSubmissionStatus = data.success;
                    $("#editor-article-submission-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }

    var handleChangeJobExtraPaymentValidation = function () {
        var form = $("#form-job-payment-detail");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                extra_payment_amount: {
                    required: true
                },
                extra_payment_note: {
                    required: true
                }
            },
            invalidHandler: function () { //display error alert on form submit
                error.show();
                App.scrollTo(error, -200);
            },
            errorPlacement: function (error, element) { // render error placement for each input type
                var icon = $(element).parent(".input-icon").children("i");
                icon.removeClass("fa-check").addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ "container": "body" });
            },
            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest(".form-group").removeClass("has-success").addClass("has-error"); // set error class to the control group   
            },
            unhighlight: function () { // revert the change done by hightlight

            },
            success: function (label, element) {
                var icon = $(element).parent(".input-icon").children("i");
                $(element).closest(".form-group").removeClass("has-error").addClass("has-success"); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },
            submitHandler: function (form) {
                error.hide();
                var model = {
                    __RequestVerificationToken: $(form).find('input[name="__RequestVerificationToken"]').val(),
                    Amount: $(form).find('input[name="extra_payment_amount"]').val(),
                    Note: $(form).find('textarea[name="extra_payment_note"]').val(),
                    JobId: $(form).find('input[name="payment_detail_job_id"]').val()
                };
                var formAction = jobExtraPaymentOpenMode === 1 ? formChangeJobExtraPaymentAction : formAddJobExtraPaymentAction;
                if (jobExtraPaymentOpenMode === 1) model.Id = $(form).find('input[name="payment_id"]').val();
                $.post(formAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    jobExtraPaymentUpdateStatus = data.success;
                    $("#job-payment-detail-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }

    var handleChangeJobResourceValidation = function () {
        var form = $("#form-job-resource-detail");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
            },
            invalidHandler: function () { //display error alert on form submit
                error.show();
                App.scrollTo(error, -200);
            },
            errorPlacement: function (error, element) { // render error placement for each input type
                var icon = $(element).parent(".input-icon").children("i");
                icon.removeClass("fa-check").addClass("fa-warning");
                icon.attr("data-original-title", error.text()).tooltip({ "container": "body" });
            },
            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest(".form-group").removeClass("has-success").addClass("has-error"); // set error class to the control group   
            },
            unhighlight: function () { // revert the change done by hightlight

            },
            success: function (label, element) {
                var icon = $(element).parent(".input-icon").children("i");
                $(element).closest(".form-group").removeClass("has-error").addClass("has-success"); // set success class to the control group
                icon.removeClass("fa-warning").addClass("fa-check");
            },
            submitHandler: function (form) {
                error.hide();
                var model = {
                    __RequestVerificationToken: $(form).find('input[name="__RequestVerificationToken"]').val(),
                    Content: $(form).find('#resource-content').summernote('code'),
                    JobId: $(form).find('input[name="resource_detail_job_id"]').val()
                };
                var formAction = jobResourceOpenMode === 1 ? formChangeJobResourceAction : formAddJobResourceAction;
                if (jobResourceOpenMode === 1) model.Id = $(form).find('input[name="resource_id"]').val();
                $.post(formAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    jobResourceUpdateStatus = data.success;
                    $("#job-resource-detail-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {
            handleEditorSubmitArticleValidation();
            handleChangeJobExtraPaymentValidation();
            handleChangeJobResourceValidation();
        }
    };
}();

jQuery(document).ready(function () {
    FormValidation.init();
});