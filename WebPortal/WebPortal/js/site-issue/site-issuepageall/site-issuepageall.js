var Site = Site || {};

Site.IssuePageAll = function (updateCB, exitCB) {
    this.viewFilter   = null;
    this.viewCalendar = null;
    this.viewSchedule = null;
    this.viewList     = null;
    this.createPanel  = null;

    var thiz  = this;
    this.page = $('#page-issue-all');

    this.show = function () {
        this.page.show();
    }

    this.hide = function () {
        this.page.hide();
    }

    /* Actions */

    this.createIssue = function (startdate, starttime, enddate, endtime, assignedid) {
        if (thiz.viewFilter.noCustomerSelected()) {
            Site.Dialogs.info('Välj kund för att skapa ärende.');
        }
        else {
            siteScrollToTop();
            var customerid = thiz.viewFilter.getCustomerSelection();
            thiz.createPanel.show(customerid, startdate, starttime, enddate, endtime, assignedid);
        }
    }

    this.openIssue = function (id, status) {
        exitCB(id, status);
    }

    this.updateStatus = function (id, status) {
        thiz.createPanel.hide();
        updateCB(id, status);
    }

    /* Callbacks */

    this.toggleViews = function() {
        thiz.viewCalendar.hide();
        thiz.viewSchedule.hide();
        thiz.viewList.hide();
        if (thiz.viewFilter.viewCalendar()) {
            thiz.viewCurrent = thiz.viewCalendar;
        }
        else if (thiz.viewFilter.viewSchedule()) {
            thiz.viewCurrent = thiz.viewSchedule;
        }
        else if (thiz.viewFilter.viewList()) {
            thiz.viewCurrent = thiz.viewList;
        }
        thiz.viewCurrent.show();
        thiz.reloadIssues();
    }

    this.reloadIssues = function() {
        if (thiz.viewFilter.noCustomerSelected()) {
            thiz.createPanel.hide();
        }
        var customer = thiz.viewFilter.getCustomerSelection();
        var areatype = thiz.viewFilter.getAreaTypeSelection();
        var status   = thiz.viewFilter.getStatusSelection();
        var priority = thiz.viewFilter.getPrioritySelection();
        thiz.viewCurrent.reload(customer, areatype, status, priority);
    }

    this.issueCreated = function () {
        thiz.createPanel.hide();
        thiz.reloadIssues();
    }

    /* Side bar */

    this.page.find('#page-description-tooltip').click(function (e) {
        thiz.viewCurrent.describe($(this), e);
    });

    this.viewFilter   = new Site.IssueFilter  (this.createIssue, this.toggleViews, this.reloadIssues)
    this.viewCalendar = new Site.IssueCalendar(this.createIssue, this.openIssue,   this.updateStatus);
    this.viewSchedule = new Site.IssueSchedule(this.createIssue, this.openIssue,   this.updateStatus);
    this.viewList     = new Site.IssueList    (this.createIssue, this.openIssue,   this.updateStatus);
    this.createPanel  = new Site.IssueCreate  (this.issueCreated);
    this.viewCurrent  = null;

    this.viewFilter.setCurrentView(this.viewFilter.CALENDAR_VIEW);
}
