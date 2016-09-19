var itemsTable  = null;
var createForm  = null;
var createPanel = null;
var updateForm  = null;
var updatePanel = null;
var secureForm  = null;
var securePanel = null;

var EDIT_CMD_INDEX   = 0;
var USERNAME_INDEX   = 3;
var SECURE_CMD_INDEX = 4;
var DELETE_CMD_INDEX = 7;
var ID_INDEX         = 8;

/* Helpers */

function closePanels() {
    createPanel.hide();
    updatePanel.hide();
    securePanel.hide();
}

function openCreatePanel() {
    updatePanel.hide();
    securePanel.hide();
    createPanel.show('fast');
}

function openUpdatePanel() {
    createPanel.hide();
    securePanel.hide();
    updatePanel.show('fast');
}

function openSecurePanel() {
    createPanel.hide();
    updatePanel.hide();
    securePanel.show('fast');
}

function resetPage() {
    siteShowProgress();
    Site.DataTables.reloadContent(itemsTable, [closePanels, siteHideProgress]);
}

/* Actions */

function prepareSecure(id, email) {
    siteScrollToTop();
    Site.Validation.clearValidationSummary(secureForm);
    secureForm.find('#id').val(id);
    secureForm.find('#email').val(email);
    secureForm.find('#password').val('');
    openSecurePanel();
}

/* Initialization */

$(function () {
    Site.Select.init();
    createForm  = $('#create-employee-form');
    createPanel = $('#create-employee-panel');
    updateForm  = $('#update-employee-form');
    updatePanel = $('#update-employee-panel');
    secureForm  = $('#secure-employee-form');
    securePanel = $('#secure-employee-panel');

    itemsTable = $('#employee-table').dataTable({
        language:  Site.DataTables.localization(),
        lengthMenu: Site.DataTables.pageLengthOptions(),
        pageLength: Site.DataTables.defaultPageLength(),
        sPaginationType: Site.DataTables.paginationType(),
        ordering: false,
        serverSide: true,
        bProcessing: true,
        bAutoWidth: false,
        ajax: {
            url: '/Employee/ListEmployees',
            type: 'POST',
            data: function (d) {
                d.authz  = $('#authz-select').val();
                d.active = $('#active-select').val();
            }
        },
        aoColumns: [
            {
                'mDataProp': 'editcmdlink',
                'searchable': false,
                'width': '4%'
            },
            {
                'mDataProp': 'firstname',
                'searchable': true,
                'width': '18%'
            },
            {
                'mDataProp': 'lastname',
                'searchable': true,
                'width': '18%'
            },
            {
                'mDataProp': 'email',
                'searchable': true,
                'width': '30%'
            },
            {
                'mDataProp': 'securecmdlink',
                'searchable': false,
                'width': '3%'
            },
            {
                'mDataProp': 'authz',
                'searchable': false,
                'width': '12%'
            },
            {
                'mDataProp': 'lastlogin',
                'searchable': false,
                'width': '12%'
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

    $('#employee-table tbody').on('click', 'td:not(.dataTables_empty)', function () {
        var row   = $(this).parent();
        var col   = Site.DataTables.getColumnClicked(itemsTable, row, $(this));
        var id    = Site.DataTables.getColumnValue(itemsTable, row, ID_INDEX);
        var email = Site.DataTables.getColumnValue(itemsTable, row, USERNAME_INDEX);
        if (col == EDIT_CMD_INDEX) {
            siteAjaxReadToForm('/Employee/ReadEmployee', { id: id }, updateForm, openUpdatePanel);
        }
        else if (col == SECURE_CMD_INDEX) {
            prepareSecure(id, email);
        }
        else if (col == DELETE_CMD_INDEX) {
            Site.Dialogs.confirm('/img/delete.png', 'Ta bort konto', 'Vill du ta bort konto ' + email + '?', 'Nej', 'Ja', function() {
                siteAjaxPost('/Employee/DeleteEmployee', { id: id }, resetPage);
            });
        }
    });

    /* Side bar */
    
    $('#authz-select').change(function () {
        siteShowProgress();
        Site.DataTables.reloadContent(itemsTable, [siteHideProgress]);
    });

    $('#active-select').change(function () {
        siteShowProgress();
        Site.DataTables.reloadContent(itemsTable, [siteHideProgress]);
    });

    $('#create-employee-button').click(function () {
        siteScrollToTop();
        Site.Validation.clearValidationSummary(createForm);
        Site.AutoAjax.clearValues(createForm);
        openCreatePanel();
    });

    /* Form buttons */

    createForm.find('.create-employee-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(createForm);
            data && siteAjaxPost('/Employee/CreateEmployee', data, resetPage);
        }
    });

    updateForm.find('.update-employee-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(updateForm);
            data && siteAjaxPost('/Employee/UpdateEmployee', data, resetPage);
        }
    });

    secureForm.find('.secure-employee-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(secureForm);
            data && siteAjaxPost('/Employee/ChangePassword', data, closePanels);
        }
    });
});
