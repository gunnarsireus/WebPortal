Site = Site || {};

Site.IssueCreate = function (createdCB) {
    var thiz        = this;
    this.customerid = '0';
    this.panel      = $('#create-issue-panel');
    this.form       = $('#create-issue-form');

    Site.DatePicker.hookDate(this.form.find('#startdatestring'));
    Site.DatePicker.hookTime(this.form.find('#starttimestring'));
    Site.DatePicker.hookDate(this.form.find('#enddatestring'));
    Site.DatePicker.hookTime(this.form.find('#endtimestring'));

    this.show = function (customerid, startdate, starttime, enddate, endtime, assignedid) {
        Site.Validation.clearValidationSummary(this.form);
        Site.AutoAjax.clearValues(this.form);
        Site.DatePicker.setSelectedDate(this.form.find('#startdatestring'), startdate);
        Site.DatePicker.setSelectedTime(this.form.find('#starttimestring'), starttime);
        Site.DatePicker.setSelectedDate(this.form.find('#enddatestring'), enddate);
        Site.DatePicker.setSelectedTime(this.form.find('#endtimestring'), endtime);
        this.customerid = customerid;
        this.form.find('#customerid').val(customerid);
        if (assignedid == '0') {
            siteAjaxGetSilent('/Account/GetDefaultAssignee', { customerid: customerid }, function (data) {
                Site.Select.setSelection(thiz.form.find('#assignedid'), data.value);
            });
        }
        else {
            Site.Select.setSelection(this.form.find('#assignedid'), assignedid);
        }
        this.panel.show('fast');
    }

    this.hide = function () {
        this.panel.hide('fast');
    }

    this.createIssue = function (data) {
        siteAjaxPost('/Issue/CreateIssue', data, function (response) {
            if (response.success) {
                createdCB();
            }
            else {
                Site.Dialogs.error(response.message);
            }
        });
    }

    /* Select2 */

    this.setupSearchResidentsParams = function(params) {
        return {
            customerid: thiz.customerid,
            filter: params.term
        };
    }

    this.populateIssueFromResident = function (id) {
        siteAjaxGetSilent('/Resident/ReadResident', { id: id }, function (data) {
            Site.AutoAjax.setValues(data, thiz.form);
        });
    }

    /* Form buttons */

    this.form.find('.create-issue-form-buttons').click(function (e) {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            thiz.panel.hide('fast');
        }
        else if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(thiz.form);
            data && thiz.createIssue(data);
        }
    });

    siteAjaxGetSilent('/Account/GetActiveTechnicians', null, function (data) {
        Site.Select.load(thiz.form.find('#assignedid'), data);
    });

    siteAjaxGetSilent('/IssueClass/GetIssueClasses', null, function (data) {
        Site.Select.load(thiz.form.find('#issueclassid'), data);
    });

    Site.Select2.hookSelect(this.form.find('#residentid'), '/Resident/SearchResidents', 2, this.setupSearchResidentsParams, this.populateIssueFromResident, null);
}