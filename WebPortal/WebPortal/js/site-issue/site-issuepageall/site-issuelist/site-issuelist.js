var Site = Site || {};

Site.IssueList = function (createCB, openCB, statusCB) {
    var thiz   = this;
    this.table = null;
    this.view  = $('#issue-list-view');

    this.customer = 0;
    this.areatype = 0;
    this.status   = 0;
    this.prio     = 0;

    this.EDIT_CMD_INDEX    = 0;
    this.ID_INDEX          = 1;
    this.STATUSICON_INDEX  = 2;
    this.DESCRIPTION_INDEX = 5;
    this.RESPONSIBLE_INDEX = 7;
    this.STATUS_CMD_INDEX  = 9;
    this.AREATYPE_INDEX    = 10;
    this.STARTDATE_INDEX   = 11;
    this.ENDDATE_INDEX     = 12;
    this.STATUS_INDEX      = 13;

    this.show = function () {
        this.view.show();
    }

    this.hide = function () {
        this.view.hide();
    }

    this.describe = function (target, e) {
        var popover = new Site.Popover();
        popover.setTitle('Hjälp');
        popover.setHeader('Funktioner i listan');
        popover.setBody(
            '<p><i>Skapa ärende</i><br>Klicka på knappen <i>Skapa</i></p>' +
            '<p><i>Se full ärendeinformation</i><br>Klicka på pennan</p>' +
            '<p><i>Se kort ärendeinformation</i><br>Klicka på cirkeln</p>'
            );
        popover.show(e, target);
    }

    this.reload = function (customer, areatype, status, prio) {
        thiz.customer = customer;
        thiz.areatype = areatype;
        thiz.status   = status;
        thiz.prio     = prio;
        if (thiz.table != null) {
            siteShowProgress();
            Site.DataTables.reloadContent(thiz.table, [siteHideProgress]);
        }
        else {
            thiz.table = $('#issuelist-table').dataTable({
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
                        d.customer = thiz.customer;
                        d.areatype = thiz.areatype;
                        d.status   = thiz.status;
                        d.prio     = thiz.prio;
                    }
                },
                aoColumns: [
                    {
                        'mDataProp': 'editcmdlink',
                        'searchable': false,
                        'width': '4%'
                    },
                    {
                        'mDataProp': 'id',
                        'searchable': true,
                        'width': '7%'
                    },
                    {
                        'mDataProp': 'statusicon',
                        'searchable': false,
                        'width': '3%'
                    },
                    {
                        'mDataProp': 'priority',
                        'searchable': false,
                        'width': '8%'
                    },
                    {
                        'mDataProp': 'header',
                        'searchable': true,
                        'width': '26%'
                    },
                    {
                        'mDataProp': 'descriptionicon',
                        'searchable': false,
                        'width': '3%'
                    },
                    {
                        'mDataProp': 'address',
                        'searchable': true,
                        'width': '26%'
                    },
                    {
                        'mDataProp': 'responsibleicon',
                        'searchable': false,
                        'width': '3%'
                    },
                    {
                        'mDataProp': 'assigned',
                        'searchable': false,
                        'width': '20%'
                    },
                    {
                        'mDataProp': 'statuscmdlink',
                        'searchable': false,
                        'width': '3%'
                    },
                    // Hidden
                    {
                        'mDataProp': 'areatype',
                        'visible': false
                    },
                    {
                        'mDataProp': 'startdate',
                        'visible': false
                    },
                    {
                        'mDataProp': 'enddate',
                        'visible': false
                    },
                    {
                        'mDataProp': 'status',
                        'visible': false
                    }
                ]
            });
        }
    }

    $('#issuelist-table tbody').on('click', 'td:not(.dataTables_empty)', function (e) {
        var row = $(this).parent();
        var col = Site.DataTables.getColumnClicked(thiz.table, row, $(this));
        var id  = Site.DataTables.getColumnValue(thiz.table, row, thiz.ID_INDEX);
        if (col == thiz.EDIT_CMD_INDEX) {
            var status = Site.DataTables.getColumnValue(thiz.table, row, thiz.STATUS_INDEX);
            openCB(id, status);
        }
        else if (col == thiz.STATUSICON_INDEX) {
            var status    = $(this).find('img').attr('alt');
            var areatype  = Site.DataTables.getColumnValue(thiz.table, row, thiz.AREATYPE_INDEX);
            var startdate = Site.DataTables.getColumnValue(thiz.table, row, thiz.STARTDATE_INDEX);
            var enddate   = Site.DataTables.getColumnValue(thiz.table, row, thiz.ENDDATE_INDEX);
            var popover   = new Site.Popover();
            popover.setTitle('Status: <b>' + status + '</b>');
            popover.setHeader(areatype);
            popover.setBody('<p>Start: ' + startdate + '<br/>Slut: ' + enddate + '</p>');
            popover.show(e, $(this));
        }
        else if (col == thiz.DESCRIPTION_INDEX) {
            var description = $(this).find('img').attr('alt');
            var popover = new Site.Popover();
            popover.setTitle('Beskrivning');
            popover.setBody(description);
            popover.show(e, $(this));
        }
        else if (col == thiz.RESPONSIBLE_INDEX) {
            var popover = new Site.Popover();
            popover.setTitle('Utförare');
            popover.setBody('Kund är ansvarig för att lösa ärendet');
            popover.show(e, $(this));
        }
        else if (col == thiz.STATUS_CMD_INDEX) {
            var img        = $(this).find('img');
            var image      = img.attr('src')
            var altsplit   = img.attr('alt').split("/");
            var statuscode = altsplit[0];
            var statusname = altsplit[1];
            Site.Dialogs.confirm(image, 'Byt status', 'Vill du ändra status på ärende ' + id + ' till ' + statusname + '?', 'Nej', 'Ja', function () {
                statusCB(id, statuscode);
            });
        }
    });
}
