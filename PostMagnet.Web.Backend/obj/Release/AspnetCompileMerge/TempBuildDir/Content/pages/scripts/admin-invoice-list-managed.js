var InvoiceTableManaged = function () {

    var displayInvoiceInformation = function (info) {
        invoiceUpdateStatus = false;

        $("#form-invoice-detail").find('input[name="invoice_id"]').val(info.Id);
        $("#form-invoice-detail").find('input[name="invoice_created"]').val(info.Created);
        $("#form-invoice-detail").find('input[name="invoice_writer"]').val(info.Writer);
        $("#form-invoice-detail").find('input[name="invoice_total_amount"]').val(info.TotalAmount);
        $("#form-invoice-detail").find('a[id="invoice-file-link"]').attr('href', info.FileLink);
        $("#form-job-detail").find('select[name="invoice_status"]').val(info.Status);
        $("#form-job-detail").find('select[name="invoice_status"]').selectpicker('refresh');
        $("#form-job-detail").find('select[name="invoice_method"]').val(info.Method);
        $("#form-job-detail").find('select[name="invoice_method"]').selectpicker('refresh');

        $("#invoice-dialog").modal('show');
    }

    var getInvoiceInformation = function (jobId) {
        $.ajax({
            url: "/invoice/invoicedetail",
            type: "POST",
            data: { id: jobId },
            dataType: "json",
            success: function(data) {
                if (data.success) {
                    displayInvoiceInformation(data.info);
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

    var initTable = function (table, filterStatusSelect) {
        
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
			    "url": "/invoice/getinvoiceswithadministrator",
			    "type": "POST",
			    "timeout": 200000,
			    "contentType": "application/json",
			    "dataType": "json",
				"data": function ( d ) {
				    d.FilterStatusOption = filterStatusSelect.find(":selected").val();
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
                    "data": "Created",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
					"targets": [2],
					"orderable": false,
					"searchable": true,
					"data": "Writer",
					"render": function(data, type, row, meta) {
						return data;
					}
				},
				{
					"targets": [3],
					"orderable": false,
					"searchable": true,
					"data": "Method",
					"render": function(data, type, row, meta) {
					    if (data === "InternetBanking") {
					        return '<span class="label label-sm label-info">Internet Banking <i class="fa fa-bank"></i></span>';
					    } else if (data === "Cash") {
					        return '<span class="label label-sm label-info">Internet Banking <i class="fa fa-dollar"></i></span>';
					    } else {
					        return '<span class="label label-sm label-info">Internet Banking <i class="fa fa-paypal"></i></span>';
					    }
					}
				},
                {
                    "targets": [4],
                    "orderable": false,
                    "searchable": true,
                    "data": "FileLink",
                    "render": function (data, type, row, meta) {
                        return '<a href="' + data + '" class="btn btn-sm btn-default" download>Download</a>';
                    }
                },
                {
                    "targets": [5],
                    "orderable": false,
                    "searchable": true,
                    "data": "TotalAmount",
                    "render": function (data, type, row, meta) {
                        return '<span class="label label-sm label-success">' + data + ' VND</span>';
                    }
                },
                {
                    "targets": [6],
                    "orderable": false,
                    "searchable": true,
                    "data": "Status",
                    "render": function (data, type, row, meta) {
                        if (data === "Pending") {
                            return '<span class="label label-sm label-primary"> Pending </span>';
                        }
                        else if (data === "Paid") {
                            return '<span class="label label-sm label-success"> Paid </span>';
                        }
                        else {
                            return '<span class="label label-sm label-warning"> Delay </span>';
                        }
                    }
                },
                {
                    "targets": [7],
                    "orderable": false,
                    "searchable": false,
                    "data": "Editable",
                    "render": function (data, type, row, meta) {
                        if (data === true) {
                            return '<button type="button" class="btn btn-sm btn-default btn-process-invoice"><i class="fa fa-cogs"></i>Process</button>';
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

        table.on('click', 'button.btn-process-invoice', function () {
            var data = table.api().row($(this).parents('tr')).data();
            getInvoiceInformation(data["Id"]);
        });
    }
    
    return {

        //main function to initiate the module
        init: function (table, filterStatusSelect) {
            if (!jQuery().dataTable) {
                return;
            }
            initTable(table, filterStatusSelect);
        }

    };
}();

var InvoiceFilterSelect = function () {

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

var InvoiceFilterOptionsChanged = function () {
	
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

var InvoiceModalDialogEvent = function() {

    var handleDialogEvent = function (table, dialog) {
        dialog.on("hidden.bs.modal", function (e) {
            if (invoiceUpdateStatus) {
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
        var filterStatusSelect = $('#filter-invoice-status');
        var table = $('#invoice-table');
        InvoiceFilterSelect.init();
        InvoiceTableManaged.init(table, filterStatusSelect);
        InvoiceFilterOptionsChanged.init(table, filterStatusSelect);

		var invoiceDialog = $("#invoice-dialog");
		InvoiceModalDialogEvent.init(table, invoiceDialog);
    });
}