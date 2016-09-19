var itemsTable  = null;
var createForm  = null;
var createPanel = null;
var updateForm  = null;
var updatePanel = null;

var EDIT_CMD_INDEX   = 0;
var NAME_INDEX       = 1;
var DELETE_CMD_INDEX = 2;
var ID_INDEX         = 3;

/* Helpers */

function closePanels() {
    createPanel.hide();
    updatePanel.hide();
}

function openCreatePanel() {
    updatePanel.hide();
    createPanel.show('fast');
}

function openUpdatePanel() {
    createPanel.hide();
    updatePanel.show('fast');
}

function resetPage() {
    siteShowProgress();
    Site.DataTables.reloadContent(itemsTable, [closePanels, siteHideProgress]);
}

/* Initialization */

$(function () {
    Site.Select.init();
    createForm  = $('#create-issueclass-form');
    createPanel = $('#create-issueclass-panel');
    updateForm  = $('#update-issueclass-form');
    updatePanel = $('#update-issueclass-panel');

    itemsTable = $('#issueclass-table').dataTable({
        language: Site.DataTables.localization(),
        lengthMenu: Site.DataTables.pageLengthOptions(),
        pageLength: Site.DataTables.defaultPageLength(),
        sPaginationType: Site.DataTables.paginationType(),
        ordering: false,
        serverSide: true,
        bProcessing: true,
        bAutoWidth: false,
        ajax: {
            url: '/IssueClass/ListIssueClasses',
            type: 'POST'
        },
        aoColumns: [
            {
                'mDataProp': 'editcmdlink',
                'searchable': false,
                'width': '4%'
            },
            {
                'mDataProp': 'name',
                'searchable': true,
                'width': '93%'
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

    $('#issueclass-table tbody').on('click', 'td:not(.dataTables_empty)', function () {
        var row = $(this).parent();
        var col = Site.DataTables.getColumnClicked(itemsTable, row, $(this));
        var id  = Site.DataTables.getColumnValue(itemsTable, row, ID_INDEX);
        if (col == EDIT_CMD_INDEX) {
            siteAjaxReadToForm('/IssueClass/ReadIssueClass', { id: id }, updateForm, openUpdatePanel);
        }
        else if (col == DELETE_CMD_INDEX) {
            var name = Site.DataTables.getColumnValue(itemsTable, row, NAME_INDEX);
            Site.Dialogs.confirm('/img/delete.png', 'Ta bort ärendeklass', 'Vill du ta bort ärendeklass ' + name + '?', 'Nej', 'Ja', function () {
                siteAjaxPost('/IssueClass/DeleteIssueClass', { id: id }, resetPage);
            });
        }
    });

    /* Side bar */

    $('#create-issueclass-button').click(function () {
        siteScrollToTop();
        Site.Validation.clearValidationSummary(createForm);
        Site.AutoAjax.clearValues(createForm);
        openCreatePanel();
    });

    /* Form buttons */

    createForm.find('.create-issueclass-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(createForm);
            data && siteAjaxPost('/IssueClass/CreateIssueClass', data, resetPage);
        }
    });

    updateForm.find('.update-issueclass-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(updateForm);
            data && siteAjaxPost('/IssueClass/UpdateIssueClass', data, resetPage);
        }
    });
});
