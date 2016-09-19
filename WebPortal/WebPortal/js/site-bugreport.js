var createForm = null;

/* Helpers */

function resetPage(response) {
    Site.AutoAjax.clearValues(createForm);
    Site.Dialogs.info(response.data1 + '<br/>' + response.data2);
}

/* Initialization */

$(function () {
    createForm = $('#create-bugreport-form');

    /* Form buttons */

    createForm.find('.create-bugreport-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isCREATE(button)) {
            var data = Site.InputPanels.getFormData(createForm);
            data && siteAjaxPost('/BugReport/CreateBugReport', data, resetPage);
        }
    });
});
