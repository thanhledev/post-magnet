var checkWebsiteConnectivity = function (table, id) {
    $.ajax({
        url: "/website/checkwebsiteconnectivity",
        type: "POST",
        data: { id: id },
        dataType: "json"        
    }).done(function (data) {
        $.notific8("zindex", 11500);
        $.notific8(data.message, data.success ? successSettings : errorSettings);
        table.fnClearTable();
        table.api().ajax.reload();
    }).fail(function () {
        $.notific8("zindex", 11500);
        $.notific8("Failed!", errorSettings);
    }).always(function () {
        App.unblockUI();
    });
};

var WebsiteTableManaged = function () {

    var displayWebsiteInformation = function (info) {
        openMode = 2;
        actionStatus = false;

        $("#website-form").find('input[name="website_id"]').val(info.Id);
        $("#website-form").find('input[name="website_host"]').val(info.Host);
        $("#website-form").find('input[name="website_username"]').val(info.Username);
        $("#website-form").find('input[name="website_password"]').val(info.Password);
        $("#website-form").find('input[name="website_confirm_password"]').val(info.Password);
        $("#website-form").find('input[name="website_tested"]').closest("div.form-group").removeClass("hide");
        $("#website-form").find('input[name="website_tested"]').val(info.Tested);
        $("#website-form").find('textarea[name="website_note"]').closest("div.form-group").removeClass("hide");
        $("#website-form").find('input[name="website_note"]').val(info.Note);

        $("#website-dialog").modal('show');
    }

    var getWebsiteInformation = function (id) {
        $.ajax({
            url: "/website/websitedetail",
            type: "POST",
            data: { id: id },
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    displayWebsiteInformation(data.info);
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
    }

    var initTable = function (table) {
        
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

            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.
			"bSort": false,
			"bProcessing": true,
			"ajax": {
			    "url": "/website/getwebsites",
			    "type": "POST",
			    "timeout": 20000,
			    "contentType": "application/json",
			    "dataType": "json",
				"data": function ( d ) {
				    return JSON.stringify(d);
				}
			},
			"autoWidth": false,
            "columnDefs": [
                {
                    "targets": [0],
                    'width': "5%",
                    "orderable": false,
                    "searchable": false,
                    "data": "Id",
                    "render": function (data, type, row, meta) {
                        return '<input type="checkbox" class="checkboxes" value="' + data + '">';
                    }
                },
				{
				    "targets": [1],
				    'width': "15%",
					"orderable": true,
					"searchable": true,
					"data": "Host",
					"render": function(data, type, row, meta) {
						return data;
					}
				},
				{
				    "targets": [2],
				    'width': "10%",
					"orderable": true,
					"searchable": true,
					"data": "Username",
					"render": function(data, type, row, meta) {
					    return data;
					}
				},
				{
				    "targets": [3],
				    'width': "15%",
					"orderable": false,
					"searchable": true,
					"data": "Tested",
					"render": function(data, type, row, meta) {
					    return '<span class="label label-sm label-warning"> ' + data + ' </span>';
					}
				},
                {
                    "targets": [4],
                    'width': "30%",
                    "orderable": false,
                    "searchable": true,
                    "data": "Note",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
				    "targets": [5],
				    'width': "25%",
					"orderable": false,
					"searchable": true,
					"data": null,
					"render": function(data, type, row, meta) {
					    var content = '';

					    if (row.Testable) {
					        content += '<a href="javascript:;" class="btn btn-sm blue btn-test-connectivity"> Check Connectivity <i class="fa fa-cog"></i></a>';
					    }

					    if (row.Editable) {
					        content += '<a href="javascript:;" class="btn btn-sm red btn-update"> Update <i class="fa fa-edit"></i></a>';
					    }

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

        table.on('click', '.btn-test-connectivity', function () {
            App.blockUI({
                boxed: true,
                message: 'Processing...'
            });

            var data = table.api().row($(this).parents('tr')).data();
            checkWebsiteConnectivity(table, data["Id"]);
        });

        table.on('click', '.btn-update', function () {
            var data = table.api().row($(this).parents('tr')).data();
            getWebsiteInformation(data["Id"]);
        });
    }

    return {

        //main function to initiate the module
        init: function (table) {
            if (!jQuery().dataTable) {
                return;
            }
            initTable(table);
        }
    };
}();

var WebsiteModalDialogEvent = function () {

    var handleDialogEvent = function (table, dialog) {
        dialog.on("hidden.bs.modal", function (e) {
            $("#website-form").find('input, textarea').each(function () {
                $(this).val("");
            });

            if (actionStatus) {
                table.fnClearTable();
                table.api().ajax.reload();
            }
        });
    }

    return {
        //main function to initiate the module
        init: function (table, dialog) {
            handleDialogEvent(table, dialog);
        }
    };

}();

if (App.isAngularJsApp() === false) { 
    jQuery(document).ready(function() {
		var table = $('#website-table');
		WebsiteTableManaged.init(table);

		var dialog = $("#website-dialog");
		WebsiteModalDialogEvent.init(table, dialog);
    });
}