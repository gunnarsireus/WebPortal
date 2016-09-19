var Site = Site || {};

Site.IssueSchedule = function (createCB, openCB, statusCB) {
    var thiz     = this;
    this.visible = false;
    this.view    = $('#issue-schedule-view');

    this.show = function () {
        this.view.show();
        this.visible = true;
    }

    this.hide = function () {
        this.view.hide();
        this.visible = false;
    }

    this.describe = function (target, e) {
        var popover = new Site.Popover();
        popover.setTitle('Hjälp');
        popover.setHeader('Funktioner i schemat');
        popover.setBody(
            '<p><i>Skapa halvtimmes ärende</i><br>Shift + klicka på ledig tid i schema</p>' +
            '<p><i>Skapa heltimmes ärende</i><br>Dubbelklicka på ledig tid i schema</p>' +
            '<p><i>Skapa valfri ärendeperiod</i><br>Shift + markera ledig tid i schema</p>' +
            '<p><i>Skapa ärende utan period</i><br>Klicka på knappen <i>Skapa</i></p>' +
            '<p><i>Se full ärendeinformation</i><br>Klicka på ärende i schema</p>' +
            '<p><i>Se kort ärendeinformation</i><br>Högerklicka på ärende i schema</p>' +
            '<p><i>Flytta ärende</i><br>Dra och släpp ärende i schema</p>' +
            '<p><i>Ändra sluttid</i><br>Dra ärendets underkant i schema</p>'
            );
        popover.show(e, target);
    }

    this.reload = function (customer, areatype, status, prio) {
        data = {
            customer: customer,
            areatype: areatype,
            status  : status,
            prio    : prio
        };
        siteAjaxGet('/IssueSchedule/ListIssues', data, function (uims) {
            var resources = [];
            var events    = [];
            for (var i = 0; i < uims.resources.length; i++) {
                var uim = uims.resources[i];
                var res = {
                    id   : uim.id,
                    title: uim.name
                };
                resources.push(res);
            }
            for (var i = 0; i < uims.events.length; i++) {
                var uim = uims.events[i];
                var event = {
                    id         : uim.id,
                    resourceId : uim.assignedid,
                    status     : uim.status,
                    priority   : uim.priority,
                    title      : uim.header,
                    description: uim.description,
                    address    : uim.address,
                    assigned   : uim.assigned,
                    responsible: uim.responsible,
                    areatype   : uim.areatype,
                    start      : uim.startdate,
                    end        : uim.enddate,
                    color      : uim.backcolor,
                    textColor  : uim.textcolor,
                    changeable : uim.changeable,
                    statusname : uim.statusname,
                    statuslink : uim.statuslink
                };
                events.push(event);
            }
            thiz.schedule.reload(resources, events);
        });
    }

    this.rescheduleIssue = function (id, startdate, starttime, enddate, endtime, assignedid, undofunction) {
        data = {
            id        : id,
            assignedid: assignedid,
            startdate : startdate,
            starttime : starttime,
            enddate   : enddate,
            endtime   : endtime
        };
        siteAjaxPost('/IssueCalendar/RescheduleIssue', data, function (response) {
            if (!response.success) {
                undofunction();
                Site.Dialogs.error(response.message);
            }
        });
    }

    this.scheduleCreateItem = function(startmoment, endmoment, e, target, resourceid) {
        var limit = new Date(Date.now());
        if (thiz.schedule.showsMonth()) {
            limit = limit.setHours(0, 0, 0, 0);
        }
        if (startmoment.valueOf() >= limit) {
            var startdate = startmoment.format('YYYY-MM-DD');
            var starttime = startmoment.format('HH:mm');
            var enddate   = endmoment.format('YYYY-MM-DD');
            var endtime   = endmoment.format('HH:mm');
            if (thiz.schedule.showsMonth()) {
                if (thiz.schedule.getCurrentDate() == startdate) {
                    var now = new Date(Date.now());
                    var s = now.getHours() + 1;
                    var e = s + 1;
                    starttime = (s < 10) ? '0' + s + ':00' : s + ':00';
                    endtime = (e < 10) ? '0' + e + ':00' : e + ':00';
                }
                else {
                    starttime = '08:00';
                    endtime = '09:00';
                }
            }
            createCB(startdate, starttime, enddate, endtime, resourceid);
        }
        else {
            Site.Dialogs.error('Tiden du valde har redan passerat. Ärende kan ej skapas.');
        }
    }

    this.scheduleUpdateItem = function(item, e, target) {
        var issueid = item.id;
        var status  = item.status;
        if (item.changeable) {
            openCB(issueid, status);
        }
        else {
            Site.Dialogs.error('Du har inte behörighet att öppna detta ärende');
        }
    }

    this.schedulePopupItem = function(item, e, target) {
        var p = new Site.Popover();
        p.setTitle(item.id + ' (' + item.statusname + ')');
        p.setHeader(item.title);
        p.setBody(
            '<table border="0">' +
            '<tr><td style="padding-right:20px;">Prio</td><td>' + item.priority + '</td></tr>' +
            '<tr><td style="padding-right:20px;">Utförare</td><td>' + item.responsible + '</td></tr>' +
            '<tr><td style="padding-right:20px; padding-bottom:10px;">Tilldelad</td><td style="padding-bottom:10px;">' + item.assigned + '</td></tr>' +
            '<tr><td style="padding-right:20px;">Adress</td><td>' + item.address + '</td></tr>' +
            '<tr><td style="padding-right:20px; padding-bottom:10px;">Utrymme</td><td style="padding-bottom:10px;">' + item.areatype + '</td></tr>' +
            '<tr><td style="padding-right:20px;">Från</td><td>' + item.start.format('YYYY-MM-DD HH:mm') + '</td></tr>' +
            '<tr><td style="padding-right:20px;">Till</td><td>' + item.end.format('YYYY-MM-DD HH:mm') + '</td></tr>' +
            '</table>' +
            '<p style="margin-top:15px;">' + item.description + '</p>' +
            '<p><center>' + item.statuslink + '</center></p>');
        p.show(e, target, 'left');
    }

    this.scheduleMoveItem = function(item, e, target, undofunction) {
        if (item.changeable) {
            var moment = item.start;
            var startdate = moment.format('YYYY-MM-DD');
            var starttime = moment.format('HH:mm');
            moment = item.end;
            var enddate = moment.format('YYYY-MM-DD');
            var endtime = moment.format('HH:mm');
            thiz.rescheduleIssue(item.id, startdate, starttime, enddate, endtime, item.resourceId, undofunction);
        }
        else {
            undofunction();
        }
    }

    this.scheduleResizeItem = function(item, e, target, undofunction) {
        if (item.changeable) {
            var moment = item.start;
            var startdate = moment.format('YYYY-MM-DD');
            var starttime = moment.format('HH:mm');
            moment = item.end;
            var enddate = moment.format('YYYY-MM-DD');
            var endtime = moment.format('HH:mm');
            thiz.rescheduleIssue(item.id, startdate, starttime, enddate, endtime, item.resourceId, undofunction);
        }
        else {
            undofunction();
        }
    }

    $(document).on('click', '.popover-content .site-clickable', function () {
        if (thiz.visible) {
            Site.Popover.closeAll();
            var img        = $(this).closest('img');
            var image      = img.attr('src')
            var idattr     = img.attr('id').split('-');
            var issueid    = idattr[1];
            var statusattr = img.attr('alt').split('/');
            var statuscode = statusattr[0];
            var statusname = statusattr[1];
            Site.Dialogs.confirm(image, 'Byt status', 'Vill du ändra status på ärende ' + issueid + ' till ' + statusname + '?', 'Nej', 'Ja', function () {
                statusCB(issueid, statuscode);
            });
        }
    });

    this.schedule = new Site.Scheduler($('#issueschedule-div'), this.scheduleCreateItem, this.scheduleUpdateItem, this.schedulePopupItem, this.scheduleMoveItem, this.scheduleResizeItem);
}
