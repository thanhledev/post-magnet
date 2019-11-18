var displayApprovePostDialog = function (table, tbRow, code) {
    $('#form-post-action-detail input[name="post_code"]').val(code);
    $('#post-confirmation-action').removeClass('hide');
    $('#post-submission-action').addClass('hide');
    $('#post-submit-details').addClass('hide');
    $('#post-schedule-details').addClass('hide');
    $('#post-dialog').modal('show');
    $('#post-submit').on('click', function () {
        App.blockUI({
            boxed: true,
            message: 'Processing...',
            target: '#post-dialog .modal-content'
        });
        var model = {
            Code: $('#form-post-action-detail').find('input[name="post_code"]').val(),
            Confirmation: $('#post-confirmation').val(),
            Submission: $('#post-submission').val(),
            Host: $('#post-to-website').val(),
            Category: $('#post-to-category').val(),
            Scheduled: postScheduleDate
        };
        $.ajax({
            url: formApprovePostAction,
            type: "POST",
            data: $.param(model),
            dataType: "json"
        }).done(function (data) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, data.success ? successSettings : errorSettings);
            if (data.success) {
                table.api().row(tbRow).data(data.post).draw();
            }
            $('#post-dialog').modal('hide');
        }).fail(function () {
            $.notific8("zindex", 11500);
            $.notific8("Failed!", errorSettings);
        }).always(function () {
            App.unblockUI();
        });
    });
};

var displayDeliveryPostDialog = function (table, tbRow, code) {
    $('#form-post-action-detail input[name="post_code"]').val(code);
    $('#post-confirmation-action').addClass('hide');
    $('#post-submission-action').removeClass('hide');
    $('#post-submit-details').addClass('hide');
    $('#post-schedule-details').addClass('hide');
    $('#post-dialog').modal('show');
    $('#post-submit').on('click', function () {
        App.blockUI({
            boxed: true,
            message: 'Processing...',
            target: '#post-dialog .modal-content'
        });
        var model = {
            Code: $('#form-post-action-detail').find('input[name="post_code"]').val(),
            Submission: $('#post-submission').val(),
            Host: $('#post-to-website').val(),
            Category: $('#post-to-category').val(),
            Scheduled: postScheduleDate
        };
        $.ajax({
            url: formDeliveryPostAction,
            type: "POST",
            data: $.param(model),
            dataType: "json"
        }).done(function (data) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, data.success ? successSettings : errorSettings);
            if (data.success) {
                table.api().row(tbRow).data(data.post).draw();
            }
            $('#post-dialog').modal('hide');
        }).fail(function () {
            $.notific8("zindex", 11500);
            $.notific8("Failed!", errorSettings);
        }).always(function () {
            App.unblockUI();
        });
    });
};

var displaySchedulePostDialog = function(table, tbRow, code, host, category, scheduleDate) {
    $('#form-post-action-detail input[name="post_code"]').val(code);
    $('#post-confirmation-action').addClass('hide');
    $('#post-submission-action').removeClass('hide');
    $('#post-submit-details').removeClass('hide');
    $('#post-schedule-details').removeClass('hide');
    $('#post-submission').val('Schedule');
    $('#post-to-website').val(host);
    $('#post-to-category').val(category);
    postScheduleDate = moment(scheduleDate, "MMMM DD YYYY HH:mm:ss").format('MMMM DD YYYY HH:mm:ss');
    $("#post-schedule span").html(postScheduleDate);
    App.unblockUI();
    $('#post-dialog').modal('show');
    $('#post-submit').on('click', function () {
        App.blockUI({
            boxed: true,
            message: 'Processing...',
            target: '#post-dialog .modal-content'
        });
        var model = {
            Code: $('#form-post-action-detail').find('input[name="post_code"]').val(),
            Submission: $('#post-submission').val(),
            Host: $('#post-to-website').val(),
            Category: $('#post-to-category').val(),
            Scheduled: postScheduleDate
        };
        $.ajax({
            url: formChangeSchedulePostAction,
            type: "POST",
            data: $.param(model),
            dataType: "json"
        }).done(function (data) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, data.success ? successSettings : errorSettings);
            if (data.success) {
                table.api().row(tbRow).data(data.post).draw();
            }
            $('#post-dialog').modal('hide');
        }).fail(function () {
            $.notific8("zindex", 11500);
            $.notific8("Failed!", errorSettings);
        }).always(function () {
            App.unblockUI();
        });
    });
};

var getPostScheduleInformation = function (table, tbRow, code) {
    App.blockUI({
        boxed: true,
        message: 'Processing...'
    });
    $.ajax({
        url: "/post/getpostscheduleinformation",
        type: "POST",
        data: { code: code },
        dataType: "json",
        success: function (data) {
            var post = data.post;
            if (data.success) {
                // get categories
                $.ajax({
                    url: "/post/getavailablecategories",
                    type: "POST",
                    data: { host: post.Host },
                    dataType: "json"
                }).done(function (data) {
                    var content = '<option value="" disabled selected>Please choose a category</option>';
                    $.each(data.categories, function(i) {
                        content += '<option value="' + data.categories[i].Name + '">' + data.categories[i].Name + '</option>';
                    });
                    $("#post-to-category").empty().append(content);
                    $("#post-to-category").selectpicker('refresh');
                    displaySchedulePostDialog(table, tbRow, code, post.Host, post.Category, post.Scheduled);
                }).fail(function() {
                    $.notific8("zindex", 11500);
                    $.notific8("Failed!", errorSettings);
                });
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

var displayPostExtraPayments = function (postCode, payments) {
    $("#post-payment-list-dialog").find('#payment-list').html("");
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
        $("#post-payment-list-dialog").find('#payment-list').append(content);
    });
};

var getPostExtraPaymentInformation = function (postCode) {
    $.ajax({
        url: "/post/postextrapaymentlist",
        type: "POST",
        data: { code: postCode },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                displayPostExtraPayments(postCode, data.payments);
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

var PostTableManaged = function () {

    var initTable = function (table, filterStatusSelect, filterQualitySelect) {
        
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
			    "url": "/post/administratorgetallposts",
			    "type": "POST",
			    "timeout": 200000,
			    "contentType": "application/json",
			    "dataType": "json",
				"data": function ( d ) {
				    d.FilterPostStatusOption = filterStatusSelect.find(":selected").val();
				    d.FilterPostQualityOption = filterQualitySelect.find(":selected").val();
				    return JSON.stringify(d);
				}
			},
			"autoWidth": false,
            "columnDefs": [ 
				{
				    "targets": [0],
                    "width": "2%",
					"orderable": false,
					"searchable": false,
					"data": "Code",
					"render": function(data, type, row, meta) {
						return '<input type="checkbox" class="checkboxes" value="' + data + '">';
					}
				},
                {
                    "targets": [1],
                    "width": "40%",
                    "orderable": false,
                    "searchable": true,
                    "data": "Title",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
				    "targets": [2],
				    "width": "15%",
					"orderable": false,
					"searchable": true,
					"data": "Author",
					"render": function (data, type, row, meta) {
					    return data;
					}
				},
                {
                    "targets": [3],
                    "width": "15%",
                    "orderable": false,
                    "searchable": true,
                    "data": null,
                    "render": function (data, type, row, meta) {
                        var content = '';

                        content += '<span class="label label-sm label-info"> Created: ' + row.Created + ' </span>';
                        
                        if (row.Submitted !== "") {
                            content += '<br><br><span class="label label-sm label-primary"> Submit: ' + row.Submitted + ' </span>';
                        }
                        if (row.Approved !== "") {
                            content += '<br><br><span class="label label-sm label-success"> Approved: ' + row.Approved + ' </span>';
                        }
                        if (row.Posted !== "") {
                            content += '<br><br><span class="label label-sm label-default"> Posted: ' + row.Posted + ' </span>';
                        } else {
                            if (row.Scheduled) {
                                content += '<br><br><span class="label label-sm label-warning"> Scheduled: ' + row.Scheduled + ' </span>';
                            }
                        }

                        return content;
                    }
                },
                {
                    "targets": [4],
                    "width": "10%",
                    "orderable": false,
                    "searchable": false,
                    "data": null,
                    "render": function (data, type, row, meta) {
                        var content = '';

                        if (row.UniquePercentage > 75 && row.UniquePercentage <= 100) {
                            content += '<span class="label label-sm label-success"> Quality: Good </span><br><br>';
                        } else if (row.UniquePercentage > 50 && row.UniquePercentage <= 75) {
                            content += '<span class="label label-sm label-warning"> Quality: Normal </span><br><br>';
                        } else {
                            content += '<span class="label label-sm label-danger"> Quality: Bad </span><br><br>';
                        }

                        if (row.Status === "Draft") {
                            content += '<span class="label label-sm label-default"> Status: Draft </span>';
                        } else if (row.Status === "Submitted") {
                            content += '<span class="label label-sm label-warning"> Status: Submitted </span>';
                        } else if (row.Status === "Approved") {
                            content += '<span class="label label-sm label-success"> Status: Approved </span>';
                        } else if (row.Status === "Denied") {
                            content += '<span class="label label-sm label-danger"> Status: Denied </span>';
                        } else if (row.Status === "Scheduled") {
                            content += '<span class="label label-sm label-info"> Status: Scheduled </span>';
                        } else {
                            content += '<span class="label label-sm label-success"> Status: Posted </span>';
                        }

                        return content;
                    }
                },
                {
                    "targets": [5],
                    "width": "18%",
                    "orderable": false,
                    "searchable": false,
                    "data": null,
                    "render": function (data, type, row, meta) {
                        var content = '';

                        content += '<div class="btn-group">';
                        content += '<button class="btn btn-xs green dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false"> Actions <i class="fa fa-angle-down"></i></button>';
                        content += '<ul class="dropdown-menu" role="menu">';

                        if (row.Viewable) {
                            content += '<li><a href="/post/view/' + row.Code + '" target="_blank"><i class="icon-magnifier"></i> View Post </a></li>';
                        }

                        if (row.ViewExtraPayment) {
                            content += '<li><a href="javascript:;" class="btn-view-post-payment"><i class="icon-tag"></i> View Payments ' + (row.ExtraPaymentCount >= 0 ? '<span class="badge badge-warning">' + row.ExtraPaymentCount + '</span>' : '<span class="badge badge-default">0</span>') + '</a></li>';
                        }

                        if (row.Approvable || row.Deliverable || row.ChangeSchedulable) {
                            content += '<li class="divider"> </li>';
                        }

                        if (row.Approvable) {
                            content += '<li><a href="javascript:;" class="btn-post-approve"><i class="icon-question"></i> Approve/Deny </a></li>';
                        }

                        if (row.Deliverable) {
                            content += '<li><a href="javascript:;" class="btn-post-delivery"><i class="icon-share"></i> Delivery </a></li>';
                        }

                        if (row.ChangeSchedulable) {
                            content += '<li><a href="javascript:;" class="btn-change-post-schedule"><i class="icon-calendar"></i> Change Schedule </a></li>';
                        }

                        content += '</ul>';
                        content += '</div>';

                        return content;
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
				//App.initUniform();
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

        table.on('click', 'a.btn-view-post-payment', function () {
            // view post's extra payments
            var data = table.api().row($(this).parents('tr')).data();
            getPostExtraPaymentInformation(data["Code"]);
        });

        table.on('click', 'a.btn-post-approve', function () {
            // open post approval dialog
            var row = table.api().row($(this).parents('tr'));
            var data = row.data();
            displayApprovePostDialog(table, row, data["Code"]);
        });

        table.on('click', 'a.btn-post-delivery', function () {
            // open post delivery dialog
            var row = table.api().row($(this).parents('tr'));
            var data = row.data();
            displayDeliveryPostDialog(table, row, data["Code"]);
        });

        table.on('click', 'a.btn-change-post-schedule', function () {
            // open change post schedule dialog
            var row = table.api().row($(this).parents('tr'));
            var data = row.data();
            getPostScheduleInformation(table, row, data["Code"]);
        });
    }
    
    return {

        //main function to initiate the module
        init: function (table, filterStatusSelect, filterQualitySelect) {
            if (!jQuery().dataTable) {
                return;
            }
            initTable(table, filterStatusSelect, filterQualitySelect);
        }

    };
}();

var PostFilterSelect = function () {

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

var PostFilterOptionsChanged = function () {
	
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

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function() {
        var filterStatusSelect = $('#filter-post-status');
        var filterQualitySelect = $('#filter-post-quality');
        var table = $('#post-table');
        PostFilterSelect.init();
        PostTableManaged.init(table, filterStatusSelect, filterQualitySelect);
        PostFilterOptionsChanged.init(table, filterStatusSelect);
        PostFilterOptionsChanged.init(table, filterQualitySelect);
    });
}