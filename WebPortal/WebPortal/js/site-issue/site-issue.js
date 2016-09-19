var pageAll = null;
var pageOne = null;

function exitAll(issueid, status) {
    pageAll.hide();
    pageOne.show(issueid, status);
}

function exitOne() {
    pageOne.hide();
    pageAll.show();
}

/* Actions */

function updateStatus(id, status) {
    siteAjaxPost('/Issue/UpdateIssueStatus', { id: id, status: status }, function (response) {
        if (response.success) {
            pageAll.show();
            pageAll.reloadIssues();
        }
        else {
            Site.Dialogs.error(response.message);
        }
    });
}

/* Initialization */

$(function () {
    Site.Select.init();
    Site.DatePicker.init();

    pageAll = new Site.IssuePageAll(updateStatus, exitAll);
    pageOne = new Site.IssuePageOne(updateStatus, exitOne);
});