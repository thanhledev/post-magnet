var LogTableManaged = function () {

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
			"autoWidth": false,
			"ajax": {
			    "url": "/log/getlogs",
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
                    "orderable": true,
                    "searchable": true,
                    "width": "20%",
                    "data": "Created",
                    "render": function (data, type, row, meta) {
                        return data;
                    }
                },
				{
					"targets": [1],
					"orderable": false,
					"searchable": true,
					"data": "Content",
					"render": function(data, type, row, meta) {
						return data;
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
		var table = $('#log-table');
		LogTableManaged.init(table);
    });
}