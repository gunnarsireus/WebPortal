Site.IssueTabFeedback = function (reloadCB) {
    var thiz     = this;
    this.table   = null;
    this.issueid = 0;
    this.pageid  = '#issue-tabfeedback-page';
    this.page    = $(this.pageid);
    this.form    = this.page.find('#issue-tabfeedback-form');
    this.creator = this.page.find('#create-issuefeedback-panel');

    this.ACCESSTYPE_INDEX = 0;

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
        thiz.creator.hide();
        Site.DataTables.clearContent(thiz.table);
    }

    /* Callbacks */

    this.feedbackCreated = function () {
        thiz.creator.hide();
        reloadCB(thiz.issueid);
    }

    /* Form buttons */

    this.form.find('.issue-tabfeedback-form-buttons').click(function (e) {
        var button = $(this);
        if (Site.InputForms.isRELOAD(button)) {
            reloadCB(thiz.issueid);
        }
        else if (Site.InputForms.isCREATE(button)) {
            thiz.creator.show(thiz.issueid);
        }
    });

    /* Initialization */

    this.table = this.page.find('#issue-tabfeedback-table').dataTable({
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
                'mDataProp': 'accessicon',
                'width': '4%'
            },
            {
                'mDataProp': 'description',
                'width': '56%'
            },
            {
                'mDataProp': 'name',
                'width': '18%'
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

    $('#issue-tabfeedback-table tbody').on('click', 'td:not(.dataTables_empty)', function (e) {
        var row = $(this).parent();
        var col = Site.DataTables.getColumnClicked(thiz.table, row, $(this));
        if (col == thiz.ACCESSTYPE_INDEX) {
            var popover = new Site.Popover();
            popover.setTitle('Synlighet');
            popover.setBody('Syns bara för avsändare och Renew');
            popover.show(e, $(this));
        }
    });

    this.creator = new Site.IssueFeedbackCreate(this.feedbackCreated);
}
