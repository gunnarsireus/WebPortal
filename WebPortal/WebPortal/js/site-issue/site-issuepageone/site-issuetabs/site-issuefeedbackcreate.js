Site = Site || {};

Site.IssueFeedbackCreate = function (createdCB) {
    var thiz   = this;
    this.customerid = '0';
    this.panel = $('#create-issuefeedback-panel');
    this.form  = $('#create-issuefeedback-form');

    this.show = function (issueid) {
        Site.Validation.clearValidationSummary(this.form);
        Site.AutoAjax.clearValues(this.form);
        this.form.find('#issueid').val(issueid);
        this.panel.show('fast');
    }

    this.hide = function () {
        this.panel.hide('fast');
    }

    this.createIssueFeedback = function (data) {
        siteAjaxPost('/IssueFeedback/CreateIssueFeedback', data, function (response) {
            if (response.success) {
                createdCB();
            }
            else {
                Site.Dialogs.error(response.message);
            }
        });
    }

    /* Form buttons */

    this.form.find('.create-issuefeedback-form-buttons').click(function (e) {
        var button = $(this);
        if (Site.InputPanels.isCLOSE(button)) {
            thiz.panel.hide('fast');
        }
        else if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(thiz.form);
            data && thiz.createIssueFeedback(data);
        }
    });
}