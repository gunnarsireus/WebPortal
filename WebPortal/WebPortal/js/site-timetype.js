var itemsTable  = null;
var updateForm  = null;
var updatePanel = null;

var EDIT_CMD_INDEX   = 0;
var CODE_INDEX       = 1;
var DELETE_CMD_INDEX = 5;
var ID_INDEX         = 6;

/* Helpers */

function closePanels() {
    updatePanel.hide();
}

function openUpdatePanel() {
    updatePanel.show('fast');
}

function resetPage() {
    siteShowProgress();
    Site.DataTables.reloadContent(itemsTable, [closePanels, siteHideProgress]);
}

function syncPerformed() {
    siteScrollToTop();
    Site.Dialogs.info('Tidkoder synkroniserade');
    resetPage();
}

/* Initialization */

$(function () {
    Site.Select.init();
    updateForm  = $('#update-timetype-form');
    updatePanel = $('#update-timetype-panel');

    itemsTable = $('#timetype-table').dataTable({
        language: Site.DataTables.localization(),
        lengthMenu: Site.DataTables.pageLengthOptions(),
        pageLength: Site.DataTables.defaultPageLength(),
        sPaginationType: Site.DataTables.paginationType(),
        ordering: false,
        serverSide: true,
        bProcessing: true,
        bAutoWidth: false,
        ajax: {
            url: '/TimeType/ListTimeTypes',
            type: 'POST'
        },
        aoColumns: [
            {
                'mDataProp': 'editcmdlink',
                'searchable': false,
                'width': '4%'
            },
            {
                'mDataProp': 'code',
                'searchable': false,
                'width': '18%'
            },
            {
                'mDataProp': 'active',
                'searchable': false,
                'width': '10%'
            },
            {
                'mDataProp': 'name',
                'searchable': true,
                'width': '50%'
            },
            {
                'mDataProp': 'unit',
                'searchable': false,
                'width': '15%'
            },
            {
                'mDataProp': 'deletecmdlink',
                'searchable': false,
                'width': '3%'
            },
            // Hidden columns
            {
                'mDataProp': 'id',
                'searchable': false,
                'visible': false
            }
        ]
    });

    $('#timetype-table tbody').on('click', 'td:not(.dataTables_empty)', function () {
        var row = $(this).parent();
        var col = Site.DataTables.getColumnClicked(itemsTable, row, $(this));
        var id  = Site.DataTables.getColumnValue(itemsTable, row, ID_INDEX);
        if (col == EDIT_CMD_INDEX) {
            siteAjaxReadToForm('/TimeType/ReadTimeType', { id: id }, updateForm, openUpdatePanel);
        }
        else if (col == DELETE_CMD_INDEX) {
            var code = Site.DataTables.getColumnValue(itemsTable, row, CODE_INDEX);
            Site.Dialogs.confirm('/img/delete.png', 'Ta bort tidkod', 'Vill du ta bort tidkod ' + code + '?', 'Nej', 'Ja', function () {
                siteAjaxPost('/TimeType/DeleteTimeType', { id: id }, resetPage);
            });
        }
    });

    /* Side bar */

    $('#sync-timetype-button').click(function () {
        Site.Dialogs.confirm('/img/info.png', 'Synkronisera med Visma', 'Vill du synkronisera tidkoder med Visma?', 'Nej', 'Ja', function () {
            siteAjaxPost('/TimeType/SyncTimeTypes', null, syncPerformed);
        });
    });

    /* Form buttons */

    updateForm.find('.update-timetype-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(updateForm);
            data && siteAjaxPost('/TimeType/UpdateTimeType', data, resetPage);
        }
    });
});
