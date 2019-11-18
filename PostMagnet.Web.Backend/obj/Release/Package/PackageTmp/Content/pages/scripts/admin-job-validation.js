var FormValidation = function () {

    var handleChangeJobValidation = function() {
        var form = $("#form-job-detail");
        var error = $(".alert-danger", form);

        form.validate({
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                job_name: {
                    required: true
                },
                job_tag: {
                    required: true
                },
                job_class: {
                    required: true
                },
                job_writer_username: {
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
                    Name: $(form).find('input[name="job_name"]').val(),
                    Deadline: deadlineDate,
                    TagId: $(form).find('select[name="job_tag"]').val(),
                    ClassId: $(form).find('select[name="job_class"]').val(),
                    WriterUsername: $(form).find('input[name="job_writer_username"]').val()
                };
                var formAction = jobOpenMode === 1 ? formChangeJobAction : formAddJobAction;
                if (jobOpenMode === 1) model.Id = $(form).find('input[name="job_id"]').val();
                $.post(formAction, $.param(model), function (data) {
                    $.notific8("zindex", 11500);
                    $.notific8(data.message, data.success ? successSettings : errorSettings);
                    jobUpdateStatus = data.success;
                    $("#job-dialog").modal('hide');
                }, "json");
                return false; // submit the form
            }
        });
    }    

    return {
        //main function to initiate the module
        init: function () {
            handleChangeJobValidation();
        }
    };
}();

jQuery(document).ready(function () {
    FormValidation.init();
});