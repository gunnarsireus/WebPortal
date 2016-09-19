/*
 DataTables support.
 
 Configures look and language
*/

var Site = Site || {};

Site.DataTables = Site.DataTables || {

    localization: function () {
        return {
            'sEmptyTable': '(Inget data)',
            'sInfo': 'Visar _START_ - _END_ av totalt _TOTAL_',
            'sInfoEmpty': 'Inga rader att visa',
            'sInfoFiltered': '(filtrerade från totalt _MAX_)',
            'sInfoPostFix': '',
            'sInfoThousands': ' ',
            'sLengthMenu': '_MENU_',
            'sLoadingRecords': 'Laddar...',
            'sProcessing': '...',
            'sSearch': 'Sök:',
            'sZeroRecords': 'Hittade inga matchande resultat',
            'oPaginate': {
                'sFirst': 'Första',
                'sLast': 'Sista',
                'sNext': 'Nästa',
                'sPrevious': 'Föregående'
            },
            'oAria': {
                'sSortAscending': ': aktivera för att sortera kolumnen i stigande ordning',
                'sSortDescending': ': aktivera för att sortera kolumnen i fallande ordning'
            }
        }
    },

    pageLengthOptions: function () {
        return [10, 20, 50];
    },

    maxPageLengthOption: function() {
        return [50];
    },

    defaultPageLength: function () {
        return 20;
    },

    maxPageLength: function () {
        return 50;
    },

    paginationType: function () {
        return 'bootstrap';
        //return 'simple';           // Prev + Next
        //return 'simple_numbers';   // Prev + Pages + Next
        //return 'full';             // First + Prev + Next + Last
        //return 'full_numbers';     // First + Prev + Pages + Next + Last
    },

    numberOfRows: function (table) {
        var rows = table.find('tbody tr');
        if (rows.length == 1 && rows[0].innerText == '(Inget data)') {
            return 0;
        }
        return rows.length;
    },

    clearContent: function (table) {
        table.dataTable().fnClearTable();
    },

    reloadContent: function (table, callbacks) {
        table.api().ajax.reload(function () {
            for (var i = 0; i < callbacks.length; i++) {
                var callback = callbacks[i];
                callback();
            }
        }, false);
    },

    addContent: function (table, content) {
        table.dataTable().fnAddData(content);
    },

    getColumnClicked: function (table, tr, td) {
        var column  = tr.children().index(td);
        var content = table.fnGetData(tr, column);
        return content != '' ? column : -1;
    },

    getColumnValue: function (table, row, col) {
        return table.fnGetData(row, col);
    },

    showColumn: function (table, col) {
        table.fnSetColumnVis(col, true);
    },

    hideColumn: function (table, col) {
        table.fnSetColumnVis(col, false);
    }
};
