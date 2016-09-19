var Site = Site || {};

Site.IssueTabs = function (loadedCB) {
    var thiz         = this;
    this.page        = $('#issue-tabs-page');
    this.tabmenu     = null;
    this.problemtab  = null;
    this.feedbacktab = null;
    this.historytab  = null;

    /* Actions */

    this.setIssue = function (issueid) {
        siteAjaxGet('/Issue/ReadIssue', { id: issueid }, function (data) {
            thiz.problemtab.setData(issueid, data.problem);
            thiz.feedbacktab.setData(issueid, data.feedbacks);
            thiz.historytab.setData(issueid, data.transitions);
            loadedCB();
        });
    }

    this.clearIssue = function () {
        thiz.problemtab.clearData();
        thiz.feedbacktab.clearData();
        thiz.historytab.clearData();
    }

    /* Initialization */

    this.problemtab  = new Site.IssueTabProblem(this.setIssue);
    this.feedbacktab = new Site.IssueTabFeedback(this.setIssue);
    this.historytab  = new Site.IssueTabHistory(this.setIssue);

    this.tabmenu = new Site.TabMenu();
    this.tabmenu.init([
        [this.problemtab.getPageId(),  null],
        [this.feedbacktab.getPageId(), null],
        [this.historytab.getPageId(),  null]
    ]);
}
