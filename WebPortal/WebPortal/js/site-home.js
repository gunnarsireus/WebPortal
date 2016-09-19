var issueListTable = null;

var STATUS_IMG_INDEX = 0;
var NAME_INDEX       = 1;
var ASSIGNEDTO_INDEX = 2;
var ID_INDEX         = 3;

$(function () {
    Site.Select.init();

    issueListTable = $('#issuelist-table').dataTable({
        language: Site.DataTables.localization(),
        lengthMenu: Site.DataTables.pageLengthOptions(),
        pageLength: Site.DataTables.defaultPageLength(),
        sPaginationType: Site.DataTables.paginationType(),
        ordering: false,
        serverSide: true,
        bProcessing: true,
        bAutoWidth: false,
        ajax: {
            url: '/IssueList/ListIssues',
            type: 'POST',
            data: function (d) {
                d.areatype=0,
                d.status=0,
                d.prio=0
            }
        },
        aoColumns: [
            {
                'mDataProp': 'StatusAsImage',
                'searchable': false,
                'width': '4%'
            },
            {
                'mDataProp': 'name',
                'searchable': true,
                'width': '20%'
            },
            {
                'mDataProp': 'assignedidtext',
                'searchable': true,
                'width': '20%'
            },
            {
                'mDataProp': 'id',
                'visible': false,
                'searchable': false
            }
        ]
    });

    $('#issuelist-table tbody').on('click', 'td:not(.dataTables_empty)', function (e) {
        var row = $(this).parent();
        var column = Site.DataTables.getColumnClicked(issueListTable, row, $(this));
        var id = Site.DataTables.getColumnValue(issueListTable, row, ID_INDEX);
        if (column == STATUS_IMG_INDEX) {
            var status = $(this).find('img').attr('alt');
            var popover = new Site.Popover();
            popover.setTitle('Status: <b>' + status + '</b>');
            popover.show(e, $(this));
        } 
    });

    //populateTable();
});
