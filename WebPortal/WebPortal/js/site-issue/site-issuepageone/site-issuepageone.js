var Site = Site || {};

Site.IssuePageOne = function (updateCB, exitCB) {
    this.tabsController = null;
    this.commandPanel   = null;

    var thiz     = this;
    this.issueid = 0;
    this.page    = $('#page-issue-one');

    this.show = function (issueid, status) {
        this.setIssueId(issueid);
        this.tabsController.setIssue(issueid);
        this.commandPanel.setStatus(status);
    }

    this.hide = function () {
        this.page.hide();
    }

    /* Actions */

    this.updateStatus = function (status) {
        updateCB(thiz.issueid, status);
    }

    /* Callbacks */

    this.unloadIssue = function () {
        thiz.tabsController.clearIssue();
        exitCB();
    }

    this.issueLoaded = function () {
        thiz.page.show();
    }

    /* Side bar */

    this.setIssueId = function (id) {
        this.issueid = id;
        this.page.find('#issue-page-one-number').text(id);
    }

    this.page.find('#page-description-tooltip').click(function (e) {
        var popover = new Site.Popover();
        popover.setTitle('Hjälp');
        popover.setHeader('Funktioner på sidan');
        popover.setBody(
            '<p><i>Ändra status</i><br>Klicka på en statusikon märkt <i>Till...</i></p>' +
            '<p><i>Tillbaka till alla ärenden</i><br>Klicka på tillbaka-ikonen</p>' +
            '<p><i>Ändra information</i><br>Tryck på visad <i>Spara</i> knapp</p>'
            );
        popover.show(e, $(this));
    });

    this.commandPanel = new Site.IssueCommand(this.updateStatus, this.unloadIssue);
    this.tabsController = new Site.IssueTabs(this.issueLoaded);
}
