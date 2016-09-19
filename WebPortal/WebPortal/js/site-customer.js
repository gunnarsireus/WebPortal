var itemsTable  = null;
var createForm  = null;
var createPanel = null;
var updateForm  = null;
var updatePanel = null;

var EDIT_CMD_INDEX   = 0;
var NAME_INDEX       = 1;
var DELETE_CMD_INDEX = 5;
var ID_INDEX         = 6;

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

/* Management- and Technician ID selects */

function populateManagementsSelect(data) {
    Site.Select.load($('#managementid-select'), data);
    Site.Select.load(createForm.find('#managementid'), data);
    Site.Select.load(updateForm.find('#managementid'), data);
}

function populateTechniciansSelect(data) {
    Site.Select.load($('#technicianid-select'), data);
    Site.Select.load(createForm.find('#technicianid'), data);
    Site.Select.load(updateForm.find('#technicianid'), data);
}

/* Initialization */

$(function () {
    Site.Select.init();
    createForm  = $('#create-customer-form');
    createPanel = $('#create-customer-panel');
    updateForm  = $('#update-customer-form');
    updatePanel = $('#update-customer-panel');

    siteAjaxGetSilent('/Account/GetActiveManagers',    null, populateManagementsSelect);
    siteAjaxGetSilent('/Account/GetActiveTechnicians', null, populateTechniciansSelect);

    itemsTable = $('#customer-table').dataTable({
        language: Site.DataTables.localization(),
        lengthMenu: Site.DataTables.pageLengthOptions(),
        pageLength: Site.DataTables.defaultPageLength(),
        sPaginationType: Site.DataTables.paginationType(),
        ordering: false,
        serverSide: true,
        bProcessing: true,
        bAutoWidth: false,
        ajax: {
            url: '/Customer/ListCustomers',
            type: 'POST',
            data: function (d) {
                d.managementid = $('#managementid-select').val();
                d.technicianid = $('#technicianid-select').val();
                d.active       = $('#active-select').val();
            }
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
                'width': '30%'
            },
            {
                'mDataProp': 'orgnumber',
                'searchable': true,
                'width': '13%'
            },
            {
                'mDataProp': 'address',
                'searchable': true,
                'width': '30%'
            },
            {
                'mDataProp': 'city',
                'searchable': false,
                'width': '20%'
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

    $('#customer-table tbody').on('click', 'td:not(.dataTables_empty)', function () {
        var row = $(this).parent();
        var col = Site.DataTables.getColumnClicked(itemsTable, row, $(this));
        var id  = Site.DataTables.getColumnValue(itemsTable, row, ID_INDEX);
        if (col == EDIT_CMD_INDEX) {
            siteAjaxReadToForm('/Customer/ReadCustomer', { id: id }, updateForm, openUpdatePanel);
        }
        else if (col == DELETE_CMD_INDEX) {
            var name = Site.DataTables.getColumnValue(itemsTable, row, NAME_INDEX);
            Site.Dialogs.confirm('/img/delete.png', 'Ta bort kund', 'Vill du ta bort kund ' + name + '?', 'Nej', 'Ja', function () {
                siteAjaxPost('/Customer/DeleteCustomer', { id: id }, resetPage);
            });
        }
    });

    /* Side bar */

    $('#managementid-select').change(function () {
        siteShowProgress();
        Site.DataTables.reloadContent(itemsTable, [siteHideProgress]);
    });

    $('#technicianid-select').change(function () {
        siteShowProgress();
        Site.DataTables.reloadContent(itemsTable, [siteHideProgress]);
    });

    $('#active-select').change(function () {
        siteShowProgress();
        Site.DataTables.reloadContent(itemsTable, [siteHideProgress]);
    });

    $('#create-customer-button').click(function () {
        siteScrollToTop();
        Site.Validation.clearValidationSummary(createForm);
        Site.AutoAjax.clearValues(createForm);
        openCreatePanel();
    });

    /* Form buttons */

    createForm.find('.create-customer-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(createForm);
            data && siteAjaxPost('/Customer/CreateCustomer', data, resetPage);
        }
    });

    updateForm.find('.update-customer-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(updateForm);
            data && siteAjaxPost('/Customer/UpdateCustomer', data, resetPage);
        }
    });
});
