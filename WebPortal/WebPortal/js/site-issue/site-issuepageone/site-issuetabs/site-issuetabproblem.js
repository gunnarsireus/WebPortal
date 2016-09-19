Site.IssueTabProblem = function (reloadCB) {
    var thiz      = this;
    this.issueid  = 0;
    this.pageid   = '#issue-tabproblem-page';
    this.page     = $(this.pageid);
    this.form     = this.page.find('#issue-tabproblem-form');
    this.origdata = null;

    Site.DatePicker.hookDate(this.form.find('#startdatestring'));
    Site.DatePicker.hookTime(this.form.find('#starttimestring'));
    Site.DatePicker.hookDate(this.form.find('#enddatestring'));
    Site.DatePicker.hookTime(this.form.find('#endtimestring'));

    this.getPageId = function () {
        return this.pageid;
    }

    this.setData = function (issueid, data) {
        this.issueid = issueid;
        Site.Validation.clearValidationSummary(this.form);
        Site.AutoAjax.setValues(data, this.form);
        this.origdata = Site.InputForms.getFormData(this.form);
        Site.InputForms.enableAPPLY(this.form, false);
    }

    this.clearData = function () {
        Site.AutoAjax.clearValues(this.form);
    }

    this.updateIssue = function (data) {
        siteAjaxPost('/Issue/UpdateIssue', data, function (response) {
            if (response.success) {
                Site.InputForms.enableAPPLY(thiz.form, false);
                reloadCB(thiz.issueid);
            }
            else {
                Site.Dialogs.error(response.message);
            }
        });
    }

    /* Form buttons */

    this.form.find('.issue-tabproblem-form-buttons').click(function (e) {
        var button = $(this);
        if (Site.InputForms.isRELOAD(button)) {
            reloadCB(thiz.issueid);
        }
        else if (Site.InputForms.isAPPLY(button)) {
            var data = Site.InputForms.getFormData(thiz.form);
            data && thiz.updateIssue(data);
        }
    });

    this.form.find(':input').change(function () {
        var currdata = Site.InputForms.getFormData(thiz.form);
        Site.InputForms.enableAPPLY(thiz.form, currdata != thiz.origdata);
    });

    siteAjaxGetSilent('/Account/GetActiveTechnicians', null, function (data) {
        Site.Select.load(thiz.form.find('#assignedid'), data);
    });

    siteAjaxGetSilent('/IssueClass/GetIssueClasses', null, function (data) {
        Site.Select.load(thiz.form.find('#issueclassid'), data);
    });
}
