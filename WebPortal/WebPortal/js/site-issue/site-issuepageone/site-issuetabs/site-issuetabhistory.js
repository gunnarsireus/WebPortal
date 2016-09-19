Site.IssueTabHistory = function (reloadCB) {
    var thiz     = this;
    this.table   = null;
    this.issueid = 0;
    this.pageid  = '#issue-tabhistory-page';
    this.page    = $(this.pageid);
    this.form    = this.page.find('#issue-tabhistory-form');

    this.getPageId = function () {
        return this.pageid;
    }

    this.setData = function (issueid, data) {
        this.issueid = issueid;
        Site.DataTables.clearContent(thiz.table);
        if (data.length > 0) {
            Site.DataTables.addContent(thiz.table, data);
        }
    }

    this.clearData = function () {
        Site.DataTables.clearContent(thiz.table);
    }

    /* Form buttons */

    this.form.find('.issue-tabhistory-form-buttons').click(function (e) {
        var button = $(this);
        if (Site.InputForms.isRELOAD(button)) {
            reloadCB(thiz.issueid);
        }
    });

    /* Initialization */

    this.table = this.page.find('#issue-tabhistory-table').dataTable({
        language: Site.DataTables.localization(),
        lengthMenu: Site.DataTables.maxPageLengthOption(),
        pageLength: Site.DataTables.maxPageLength(),
        bPaginate: false,
        bFilter: false,
        bInfo: false,
        ordering: false,
        bProcessing: true,
        bAutoWidth: false,
        aoColumns: [
            {
                'mDataProp': 'fromstatus',
                'width': '20%'
            },
            {
                'mDataProp': 'tostatus',
                'width': '20%'
            },
            {
                'mDataProp': 'statusicon',
                'width': '8%'
            },
            {
                'mDataProp': 'name',
                'width': '30%'
            },
            {
                'mDataProp': 'date',
                'width': '12%'
            },
            {
                'mDataProp': 'time',
                'width': '10%'
            }
        ]
    });
}
