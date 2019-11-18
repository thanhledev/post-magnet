var changeEmployeeAccessibility = function(table, username) {
    $.ajax({
        url: "/employee/quickupdateaccessibility",
        type: "POST",
        data: { Username: username },
        dataType: "json",
        success: function (data) {
            $.notific8("zindex", 11500);
            $.notific8(data.message, data.success ? successSettings : errorSettings);
            
            if (data.success) {
                table.fnClearTable();
                table.api().ajax.reload();
            }
        },
        error: function (req, status, error) {
            $.notific8("zindex", 11500);
            $.notific8(error, errorSettings);
        }
    });
};

var EmployeeTableManaged = function () {

    var initTable = function (table, filterSelect) {
        
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
			    "url": "/employee/getemployees",
			    "type": "POST",
			    "timeout": 20000,
			    "contentType": "application/json",
			    "dataType": "json",
				"data": function ( d ) {
				    d.FilterActiveSelectOption = filterSelect.find(":selected").val();
				    return JSON.stringify(d);
				}
			},
			"autoWidth": false,
            "columnDefs": [
                {
                    "targets": [0],
                    'width': "15%",
                    "orderable": true,
                    "searchable": true,
                    "data": "Username",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
				    "targets": [1],
				    'width': "10%",
					"orderable": true,
					"searchable": true,
					"data": "Name",
					"render": function(data, type, row, meta) {
						return data;
					}
				},
				{
				    "targets": [2],
				    'width': "20%",
					"orderable": true,
					"searchable": true,
					"data": "Email",
					"render": function(data, type, row, meta) {
					    return data;
					}
				},
				{
				    "targets": [3],
				    'width': "10%",
					"orderable": true,
					"searchable": true,
					"data": "Phone",
					"render": function(data, type, row, meta) {
						return data;
					}
				},
                {
                    "targets": [4],
                    'width': "5%",
                    "orderable": false,
                    "searchable": true,
                    "data": "Role",
                    "render": function (data, type, row, meta) {
                        if(data === "Administrator")
                            return '<span class="label label-sm label-danger"> ' + data + ' </span>';
                        else if(data === "Editor")
                            return '<span class="label label-sm label-warning"> ' + data + ' </span>';
                        else
                            return '<span class="label label-sm label-info"> ' + data + ' </span>';
                    }
                },
                {
                    "targets": [5],
                    'width': "5%",
                    "orderable": false,
                    "searchable": true,
                    "data": "IsActive",
                    "render": function (data, type, row, meta) {
                        if (data === true)
                            return '<span class="label label-sm label-success"> Actived </span>';                        
                        else
                            return '<span class="label label-sm label-danger"> Blocked </span>';
                    }
                },
				{
					"targets": [6],
					"orderable": false,
					"searchable": true,
					"data": null,
					"render": function(data, type, row, meta) {
					    var content = '';

					    if (row.ViewEmployeeProfile) {
					        content += '<button type="button" class="btn btn-sm btn-default btn-view-profile"><i class="fa fa-search"></i>View Profile</button>';
					    }

					    if (row.UpdateAccessibility) {
					        content += row.IsActive ? '<a href="javascript:;" class="btn btn-sm red btn-change-accessiblity"> Lock<i class="fa fa-lock"></i></a>' : '<a href="javascript:;" class="btn btn-sm red btn-change-accessiblity"> Unlock<i class="fa fa-unlock"></i></a>';
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

        table.on('click', '.btn-view-profile', function () {
            var data = table.api().row($(this).parents('tr')).data();
            window.location.assign("/employee/viewprofile/" + data["Username"]);
        });

        table.on('click', '.btn-change-accessiblity', function () {
            var data = table.api().row($(this).parents('tr')).data();
            changeEmployeeAccessibility(table, data["Username"]);
        });
    }

    return {

        //main function to initiate the module
        init: function (table, filterSelect) {
            if (!jQuery().dataTable) {
                return;
            }
            initTable(table, filterSelect);
        }
    };
}();

var EmployeeFilterSelect = function () {

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

var EmployeeFilterOptionsChanged = function () {
	
	var handleOptionsChanged = function(table, select) {
		select.on('change', function(e) {
			table.fnClearTable();
			table.api().ajax.reload(function () {
			});
		});
	}
	
	return {
        //main function to initiate the module
        init: function (table, select) {
            handleOptionsChanged(table, select);
        }
    };
}();

var EmployeeModalDialogEvent = function () {

    var handleDialogEvent = function (table, dialog) {
        dialog.on("hidden.bs.modal", function (e) {
            if (employeeCreationStatus) {
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
		var filterSelect = $('#filter-select');
		EmployeeFilterSelect.init();
		var table = $('#employee-table');
		EmployeeTableManaged.init(table, filterSelect);
		EmployeeFilterOptionsChanged.init(table, filterSelect);

		var dialog = $("#employee-create-dialog");
        EmployeeModalDialogEvent.init(table, dialog);
    });
}