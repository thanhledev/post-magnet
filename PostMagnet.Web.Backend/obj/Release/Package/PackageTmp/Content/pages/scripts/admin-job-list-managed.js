var initializeSummernote = function(elem, code) {
    $(elem).summernote({
        height: 200, // set editor height
        width: "90%", // set editor height
        minHeight: null, // set minimum height of editor
        maxHeight: null, // set maximum height of editor
        dialogsInBody: true
    });

    $(elem).summernote('code', code);
};

var displayJobExtraPayments = function (jobId, payments, openMode) {
    if (openMode) $("#job-payment-list-dialog").find('#extra-payment-add').data('payment-job-id', jobId);
    $("#job-payment-list-dialog").find('#payment-list').html("");

    // load all payments to view
    payments.forEach(function(payment, index) {
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

    if (openMode) $("#job-payment-list-dialog").modal('show');
};

var getJobExtraPaymentInformation = function(jobId, openMode) {
    $.ajax({
        url: "/job/jobextrapaymentlist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function(data) {
            if (data.success) {
                displayJobExtraPayments(jobId, data.payments, openMode);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function(req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, errorSettings);
        }
    });
};

var displayJobResources = function (jobId, resources, openMode) {
    if (openMode) $("#job-resource-list-dialog").find('#resource-add').data('resource-job-id', jobId);
    $("#job-resource-list-dialog").find('#resource-list').html("");

    // load all resources to view
    resources.forEach(function(resource, index) {
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

    if (openMode) $("#job-resource-list-dialog").modal('show');
};

var getJobResourceInformation = function (jobId, openMode) {
    $.ajax({
        url: "/job/jobresourcelist",
        type: "POST",
        data: { id: jobId },
        dataType: "json",
        success: function(data) {
            if (data.success) {
                displayJobResources(jobId, data.resources, openMode);
            } else {
                $.notific8("zindex", 11500);
                $.notific8(data.message, errorSettings);
            }
        },
        error: function(req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, errorSettings);
        }
    });
};

var JobTableManaged = function () {

    var displayJobInformation = function (info) {
        jobOpenMode = 1;
        jobUpdateStatus = false;

        $("#form-job-detail").find('input[name="job_id"]').val(info.Id);
        $("#form-job-detail").find('input[name="job_name"]').val(info.Name);
        datelineDate = moment(info.Deadline, "MMMM DD YYYY HH:mm:ss").format('MMMM DD YYYY HH:mm:ss');
        $("#job-deadline span").html(datelineDate);
        $("#form-job-detail").find('select[name="job_tag"]').val(info.TagId);
        $("#form-job-detail").find('select[name="job_tag"]').selectpicker('refresh');
        $("#form-job-detail").find('select[name="job_class"]').val(info.ClassId);
        $("#form-job-detail").find('select[name="job_class"]').selectpicker('refresh');
        $("#form-job-detail").find('input[name="job_writer_username"]').val(info.Writer);

        switch(info.Status) {
            case "Pending":
                $("#form-job-detail").find("#job-status-placeholder").html('<span class="label label-warning"> Pending </span>');
                break;
            case "Paid":
                $("#form-job-detail").find("#job-status-placeholder").html('<span class="label label-success"> Paid </span>');
                break;
            case "Fault":
                $("#form-job-detail").find("#job-status-placeholder").html('<span class="label label-danger"> Fault </span>');
                break;
            case "Done":
                $("#form-job-detail").find("#job-status-placeholder").html('<span class="label label-info"> Done </span>');
                break;
        };

        switch(info.ArticleStatus) {
            case "Unedit":
                $("#form-job-detail").find("#article-status-placeholder").html('<span class="label label-success"> Unedit </span>');
                break;
            case "Edit":
                $("#form-job-detail").find("#article-status-placeholder").html('<span class="label label-warning"> Edit </span>');
                break;
            case "Rewrite":
                $("#form-job-detail").find("#article-status-placeholder").html('<span class="label label-danger"> Rewrite </span>');
                break;
        };

        switch(info.SubmissionStatus) {
            case "Notyet":
                $("#form-job-detail").find("#submission-placeholder").html('<span class="label label-warning"> Not yet </span>');
                break;
            case "OnTime":
                $("#form-job-detail").find("#submission-placeholder").html('<span class="label label-success"> On-time </span>');
                break;
            case "Late":
                $("#form-job-detail").find("#submission-placeholder").html('<span class="label label-danger"> Late Submission </span>');
                break;
        }

        $("#job-dialog").modal('show');
    }

    var getJobInformation = function (jobId) {
        $.ajax({
            url: "/job/jobdetail",
            type: "POST",
            data: { id: jobId },
            dataType: "json",
            success: function(data) {
                if (data.success) {
                    displayJobInformation(data.info);
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
    }

    var initTable = function (table, filterStatusSelect, filterTagSelect, filterClassSelect) {
        
        // begin first table
        table.dataTable({

            // Internationalisation. For more info refer to http://datatables.net/manual/i18n
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "emptyTable": "No data available in table",
                "info": "Showing _START_ to _END_ of _TOTAL_ records",
                "infoEmpty": "No records found",
                "infoFiltered": "(filtered1 from _MAX_ total records)",
                "lengthMenu": "Show _MENU_",
                "search": "Search:",
                "zeroRecords": "No matching records found",
                "paginate": {
                    "previous":"Prev",
                    "next": "Next",
                    "last": "Last",
                    "first": "First"
                }
            },

            // Or you can use remote translation file
            //"language": {
            //   url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
            //},

            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "bStateSave": true, // save datatable state(pagination, sort, etc) in cookie.
			"bSort": false,
			"bProcessing": true,
			"ajax": {
			    "url": "/job/getjobswithadministrator",
			    "type": "POST",
			    "timeout": 200000,
			    "contentType": "application/json",
			    "dataType": "json",
				"data": function ( d ) {
				    d.FilterJobStatusOption = filterStatusSelect.find(":selected").val();
				    d.FilterJobTagOption = filterTagSelect.find(":selected").val();
				    d.FilterJobClassOption = filterClassSelect.find(":selected").val();
				    return JSON.stringify(d);
				}
			},
            "columnDefs": [ 
				{
					"targets": [0],
					"orderable": false,
					"searchable": false,
					"data": "Id",
					"render": function(data, type, row, meta) {
						return '<input type="checkbox" class="checkboxes" value="' + data + '">';
					}
				},
                {
                    "targets": [1],
                    "orderable": false,
                    "searchable": true,
                    "data": "Name",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
					"targets": [2],
					"orderable": false,
					"searchable": true,
					"data": null,
					"render": function(data, type, row, meta) {
					    var content = '';
					    content += '<span class="label label-sm label-info">Created: ' + row.Created + '</span><br><br>';
					    content += '<span class="label label-sm label-warning">Deadline: ' + row.Deadline + '</span><br><br>';
					    content += '<span class="label label-sm label-danger">Submit: ' + row.Submitted + '</span>';
					    return content;
					}
				},
                {
                    "targets": [3],
                    "orderable": false,
                    "searchable": true,
                    "data": null,
                    "render": function (data, type, row, meta) {
                        var content = '';
                        content += '<span class="label label-sm label-info">Editor: ' + row.Editor + '</span><br><br>';
                        content += '<span class="label label-sm label-warning">Writer: ' + row.Writer + '</span><br><br>';
                        return content;
                    }
                },
                {
                    "targets": [4],
                    "orderable": false,
                    "searchable": false,
                    "data": null,
                    "render": function (data, type, row, meta) {
                        return '<span class="label label-sm label-primary" style="margin-right:10px;"> Tag: ' + row.Tag + ' </span><br><br>' + '<span class="label label-sm label-primary"> Class:' + row.Class + ' </span>';
                    }
                },
                {
                    "targets": [5],
                    "orderable": false,
                    "searchable": false,
                    "data": "ResourcesCount",
                    "render": function (data, type, row, meta) {
                        return data === 0 ? '<button type="button" class="btn btn-sm btn-primary">Empty Resource!</button>' : '<button type="button" class="btn btn-sm btn-warning btn-view-job-resource">View Resources</button>';
                    }
                },                
                {
                    "targets": [6],
                    "orderable": false,
                    "searchable": false,
                    "data": "PaymentsCount",
                    "render": function (data, type, row, meta) {
                        return data === 0 ? '<button type="button" class="btn btn-sm btn-primary">Empty Payment!</button>' : '<button type="button" class="btn btn-sm btn-warning btn-view-job-payment">View Payments</button>';
                    }
                },
				{
					"targets": [7],
					"orderable": false,
					"searchable": false,
					"data": null,
					"render": function (data, type, row, meta) {
					    var content = '';

					    switch(row.Status) {
					        case "Pending":
					            content += '<span class="label label-sm label-warning"> Status: Pending </span><br><br>';
					            break;
					        case "Paid":
					            content += '<span class="label label-sm label-success"> Status: Paid </span><br><br>';
					            break;
					        case "Fault":
					            content += '<span class="label label-sm label-danger"> Status: Fault </span><br><br>';
					            break;
					        case "Done":
					            content += '<span class="label label-sm label-info"> Status: Done </span><br><br>';
                                break;
					    };

					    switch (row.SubmissionStatus) {
					        case "Notyet":
					            content += '<span class="label label-sm label-warning"> Submission: Not yet </span><br><br>';
					            break;
					        case "OnTime":
					            content += '<span class="label label-sm label-success"> Submission: On time </span><br><br>';
					            break;
					        case "Late":
					            content += '<span class="label label-sm label-danger"> Submission: Late </span><br><br>';
					            break;
					    };

					    switch(row.ArticleStatus) {
					        case "Unedit":
					            content += '<span class="label label-sm label-success"> Article: Unedit </span>';
					            break;
					        case "Edit":
					            content += '<span class="label label-sm label-warning"> Article: Edit </span>';
					            break;
					        case "Rewrite":
					            content += '<span class="label label-sm label-danger"> Article: Rewrite </span>';
					            break;
					    }

					    return content;
					}
				},
                {
                    "targets": [8],
                    "orderable": false,
                    "searchable": false,
                    "data": "",
                    "render": function (data, type, row, meta) {
                        return '<button type="button" class="btn btn-sm btn-info btn-view-job-timeline">View Timeline</button>';
                    }
                },
                {
                    "targets": [9],
                    "orderable": false,
                    "searchable": false,
                    "data": "Editable",
                    "render": function (data, type, row, meta) {
                        if (data === true) {
                            return '<button type="button" class="btn btn-sm btn-default btn-edit-job"><i class="fa fa-cogs"></i>Edit</button>';
                        } else {
                            return '<span class="label label-sm label-danger"> Denied </span>';
                        }
                    }
                }
			],

            "lengthMenu": [
                [5, 15, 20, 50, 100, 200, 500, -1],
                [5, 15, 20, 50, 100, 200, 500, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 50,            
            "pagingType": "bootstrap_full_number",
			"fnDrawCallback": function() {
				App.initUniform();
			}
        });

        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).prop("checked", true);
                    $(this).parents('tr').addClass("active");
                } else {
                    $(this).prop("checked", false);
                    $(this).parents('tr').removeClass("active");
                }
            });
            jQuery.uniform.update(set);
        });

        table.on('change', 'tbody tr .checkboxes', function () {
            $(this).parents('tr').toggleClass("active");
        });

        table.on('click', 'button.btn-edit-job', function () {
            var data = table.api().row($(this).parents('tr')).data();
            getJobInformation(data["Id"]);
        });

        table.on('click', '.btn-view-job-payment', function() {
            var data = table.api().row($(this).parents('tr')).data();
            getJobExtraPaymentInformation(data["Id"], true);
        });

        table.on('click', '.btn-view-job-resource', function () {
            var data = table.api().row($(this).parents('tr')).data();
            getJobResourceInformation(data["Id"], true);
        });

        table.on('click', '.btn-view-job-timeline', function() {
            var data = table.api().row($(this).parents('tr')).data();
            window.location.assign("/articletimeline/editorarticleindex/" + data["Id"]);
        });
    }
    
    return {

        //main function to initiate the module
        init: function (table, filterStatusSelect, filterTagSelect, filterClassSelect) {
            if (!jQuery().dataTable) {
                return;
            }
            initTable(table, filterStatusSelect, filterTagSelect, filterClassSelect);
        }

    };
}();

var JobFilterSelect = function () {

    var handleSelect = function() {
        $('.bs-select').selectpicker({
            iconBase: 'fa',
            tickIcon: 'fa-check'
        });
    }

    return {
        //main function to initiate the module
        init: function () {      
            handleSelect();
        }
    };

}();

var JobFilterOptionsChanged = function () {
	
	var handleOptionsChanged = function(table, select) {
		select.on('change', function(e) {
			table.fnClearTable();
			table.api().ajax.reload(function () {});
		});
	}
	
	return {
        //main function to initiate the module
        init: function (table, select) {
            handleOptionsChanged(table, select);
        }
    };
}();

var JobModalDialogEvent = function() {

    var handleDialogEvent = function (table, dialog) {
        dialog.on("hidden.bs.modal", function (e) {
            if (jobUpdateStatus) {
                table.fnClearTable();
                table.api().ajax.reload();
            }
        });
    }

    return {
        //main function to initiate the module
        init: function(table, dialog) {
            handleDialogEvent(table, dialog);
        }
    };

}();

if (App.isAngularJsApp() === false) { 
    jQuery(document).ready(function() {
        var filterStatusSelect = $('#filter-job-status');
        var filterTagSelect = $('#filter-job-tag');
        var filterClassSelect = $('#filter-job-class');
        var table = $('#job-table');
        JobFilterSelect.init();
        JobTableManaged.init(table, filterStatusSelect, filterTagSelect, filterClassSelect);
        JobFilterOptionsChanged.init(table, filterStatusSelect);
        JobFilterOptionsChanged.init(table, filterTagSelect);
        JobFilterOptionsChanged.init(table, filterClassSelect);

		var jobDialog = $("#job-dialog");
		JobModalDialogEvent.init(table, jobDialog);
    });
}