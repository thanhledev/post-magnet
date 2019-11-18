var MessageTableManaged = function () {

    var markReadMessage = function (table, tbRow, code) {
        $.ajax({
            url: "/employee/markmessageread",
            type: "POST",
            data: { code: code },
            dataType: "json",
            success: function (data) {
                $.notific8("zindex", 11500);
                $.notific8(data.message, data.success ? successSettings : errorSettings);

                if (data.success) {
                    table.api().row(tbRow).data(data.info).draw();
                }
            },
            error: function (req, status, error) {
                $.notific8("zindex", 11500);
                $.notific8(error, errorSettings);
                return false;
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
			    "url": "/employee/getmessages",
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
                    "data": "Code",
                    "render": function (data, type, row, meta) {
                        return '<input type="checkbox" class="checkboxes" value="' + data + '">';
                    }
                },
                {
                    "targets": [1],
                    'width': "10%",
                    "orderable": true,
                    "searchable": true,
                    "data": "Sender",
                    "render": function (data, type, row, meta) {
                        return '<span class="label label-sm label-info"> ' + data + ' </span>';
                    }
                },
				{
				    "targets": [2],
				    'width': "15%",
					"orderable": true,
					"searchable": true,
					"data": "Sent",
					"render": function(data, type, row, meta) {
					    return '<span class="label label-sm label-success"> ' + data + ' </span>';
					}
				},
				{
				    "targets": [3],
					"orderable": true,
					"searchable": true,
					"data": "Content",
					"render": function(data, type, row, meta) {
					    return data;
					}
				},
                {
                    "targets": [4],
                    'width': "15%",
                    "orderable": true,
                    "searchable": true,
                    "data": "IsRead",
                    "render": function (data, type, row, meta) {
                        if (data) {
                            return '<span class="label label-sm label-success"> Read </span>';
                        } else {
                            return '<span class="label label-sm label-danger"> Unread </span>';
                        }
                    }
                },
				{
				    "targets": [5],
				    'width': "10%",
					"orderable": false,
					"searchable": true,
					"data": null,
					"render": function(data, type, row, meta) {
					    return row.IsRead ? '' : '<a href="javascript:;" class="btn btn-sm red btn-mark"> Mark As Read <i class="fa fa-bookmark"></i></a>';
					}
				}
			],

            "lengthMenu": [
                [50, 150, 200, 500, 1000, 2000, 5000, -1],
                [50, 150, 200, 500, 1000, 2000, 5000, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 200,
            "pagingType": "bootstrap_full_number",
			"fnDrawCallback": function() {
				//App.initUniform();
			}
        });

        table.on('click', '.btn-mark', function () {
            var row = table.api().row($(this).parents('tr'));
            var data = row.data();
            markReadMessage(table, row, data["Code"]);
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

if (App.isAngularJsApp() === false) { 
    jQuery(document).ready(function() {
        var table = $('#message-table');
        MessageTableManaged.init(table);
    });
}