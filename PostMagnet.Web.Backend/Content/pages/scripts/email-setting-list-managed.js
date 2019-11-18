var toggleEmailSettingActivation = function(emailId, table) {
    $.ajax({
        url: "/emailsetting/updateemailactivation",
        type: "POST",
        data: { id: emailId },
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
            $.notific8(data.message, errorSettings);
        }
    });
}

var EmailSettingTableManaged = function () {

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
			    "url": "/emailsetting/getemailsettings",
			    "type": "POST",
			    "timeout": 20000,
			    "contentType": "application/json",
			    "dataType": "json",
				"data": function ( d ) {
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
                    "data": "Account",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
					"targets": [2],
					"orderable": false,
					"searchable": true,
					"data": "Password",
					"render": function(data, type, row, meta) {
						return data;
					}
				},
                {
                    "targets": [3],
                    "orderable": false,
                    "searchable": true,
                    "data": "IsActive",
                    "render": function (data, type, row, meta) {
                        if (data === true) {
                            return '<span class="label label-sm label-success"> Active </span>';
                        } else {
                            return '<button type="button" class="btn btn-primary btn-toggle-active">Toggle Active</button>';
                        }
                    }
                },
                {
                    "targets": [4],
                    "orderable": false,
                    "searchable": false,
                    "data": "Editable",
                    "render": function (data, type, row, meta) {
                        if (data === true) {
                            return '<button type="button" class="btn btn-default btn-edit-email-setting"><i class="fa fa-cogs"></i>Edit</button>';
                        } else {
                            return '<span class="label label-sm label-danger"> Permission denied </span>';
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

        table.on('click', 'button.btn-edit-email-setting', function () {
            var data = table.api().row($(this).parents('tr')).data();
            window.location.assign("/emailsetting/emailsettingdetail/" + data["Id"]);
        });

        table.on('click', '.btn-toggle-active', function () {
            var data = table.api().row($(this).parents('tr')).data();
            toggleEmailSettingActivation(data["Id"], table);
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

var EmailSettingButtonsClick = function() {

    var handleButtonsClick = function(addButton) {

        addButton.on("click", function(e) {
            window.location.assign("/emailsetting/addemailsetting");
        });
    }

    return {
        init: function(addButton) {
            handleButtonsClick(addButton);
        }
    };

}();

if (App.isAngularJsApp() === false) { 
    jQuery(document).ready(function() {
		var table = $('#email-setting-table');
		EmailSettingTableManaged.init(table);
		var addButton = $("#email-setting-add");
		EmailSettingButtonsClick.init(addButton);
    });
}