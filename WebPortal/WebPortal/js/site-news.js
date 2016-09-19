var itemsTable  = null;
var createForm  = null;
var createPanel = null;
var updateForm  = null;
var updatePanel = null;

var EDIT_CMD_INDEX     = 0;
var HEADLINE_INDEX     = 1;
var CATEGORY_CMD_INDEX = 2;
var SPEAK_CMD_INDEX    = 4;
var DELETE_CMD_INDEX   = 6;
var ID_INDEX           = 7;
var MESSAGE_INDEX      = 8;

/* Customer ID select */

function populateCustomersSelect(data) {
    Site.Select.load($('#customerid-select'), data);
}

function getCustomerSelection() {
    return $('#customerid-select').val();
}

function setCustomer(form) {
    var customerid = getCustomerSelection();
    form.find('#customerid').val(customerid);
}

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
    createForm  = $('#create-news-form');
    createPanel = $('#create-news-panel');
    updateForm  = $('#update-news-form');
    updatePanel = $('#update-news-panel');

    siteAjaxGetSilent('/Customer/GetAllCompanyCustomers', null, populateCustomersSelect);

    Site.DatePicker.init();
    Site.DatePicker.hookDate(createForm.find('#showfromdate'));
    Site.DatePicker.hookTime(createForm.find('#showfromtime'));
    Site.DatePicker.hookDate(createForm.find('#showuntildate'));
    Site.DatePicker.hookTime(createForm.find('#showuntiltime'));
    Site.DatePicker.hookDate(updateForm.find('#showfromdate'));
    Site.DatePicker.hookTime(updateForm.find('#showfromtime'));
    Site.DatePicker.hookDate(updateForm.find('#showuntildate'));
    Site.DatePicker.hookTime(updateForm.find('#showuntiltime'));

    itemsTable = $('#news-table').dataTable({
        language: Site.DataTables.localization(),
        lengthMenu: Site.DataTables.pageLengthOptions(),
        pageLength: Site.DataTables.defaultPageLength(),
        sPaginationType: Site.DataTables.paginationType(),
        ordering: false,
        serverSide: true,
        bProcessing: true,
        bAutoWidth: false,
        ajax: {
            url: '/News/ListNews',
            type: 'POST',
            data: function (d) {
                d.customerid = getCustomerSelection();
            }
        },
        aoColumns: [
            {
                'mDataProp': 'editcmdlink',
                'searchable': false,
                'width': '4%'
            },
            {
                'mDataProp': 'headline',
                'searchable': true,
                'width': '60%'
            },
            {
                'mDataProp': 'categoryicon',
                'searchable': false,
                'width': '5%'
            },
            {
                'mDataProp': 'showfrom',
                'searchable': false,
                'width': '12%'
            },
            {
                'mDataProp': 'speakicon',
                'searchable': false,
                'width': '4%'
            },
            {
                'mDataProp': 'showuntil',
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
            },
            {
                'mDataProp': 'message',
                'searchable': false,
                'visible': false
            },
            {
                'mDataProp': 'message',
                'searchable': false,
                'visible': false
            }
        ]
    });

    $('#news-table tbody').on('click', 'td:not(.dataTables_empty)', function (e) {
        var row = $(this).parent();
        var col = Site.DataTables.getColumnClicked(itemsTable, row, $(this));
        var id  = Site.DataTables.getColumnValue(itemsTable, row, ID_INDEX);
        if (col == EDIT_CMD_INDEX) {
            siteAjaxReadToForm('/News/ReadNews', { id: id }, updateForm, openUpdatePanel);
        }
        else if (col == CATEGORY_CMD_INDEX) {
            var category = $(this).find('img').attr('alt');
            var message = Site.DataTables.getColumnValue(itemsTable, row, MESSAGE_INDEX);
            var popover = new Site.Popover();
            popover.setTitle(category);
            popover.setBody(message);
            popover.show(e, $(this));
        }
        else if (col == SPEAK_CMD_INDEX) {
            var popover = new Site.Popover();
            popover.setTitle('Nyhet');
            popover.setBody('Visas just nu!');
            popover.show(e, $(this));
        }
        else if (col == DELETE_CMD_INDEX) {
            var headline = Site.DataTables.getColumnValue(itemsTable, row, HEADLINE_INDEX);
            var warning  = (getCustomerSelection() !== '0') ?
                'Vill du ta bort nyhet "' + headline + '" ?' :
                'OBS - detta påverkar alla kunder - vill du ta bort nyhet "' + headline + '" ?';
            Site.Dialogs.confirm('/img/delete.png', 'Ta bort nyhet', warning, 'Nej', 'Ja', function () {
                siteAjaxPost('/News/DeleteNews', { id: id }, resetPage);
            });
        }
    });

    /* Side bar */

    $('#customerid-select').change(function () {
        siteShowProgress();
        closePanels();
        Site.DataTables.reloadContent(itemsTable, [siteHideProgress]);
    });

    $('#create-news-button').click(function () {
        siteScrollToTop();
        Site.Validation.clearValidationSummary(createForm);
        Site.AutoAjax.clearValues(createForm);
        setCustomer(createForm);
        openCreatePanel();
    });

    /* Form buttons */

    createForm.find('.create-news-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(createForm);
            data && siteAjaxPost('/News/CreateNews', data, resetPage);
        }
    });

    updateForm.find('.update-news-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            closePanels();
        }
        else if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(updateForm);
            data && siteAjaxPost('/News/UpdateNews', data, resetPage);
        }
    });
});
