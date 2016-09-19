var updateForm = null;
var secureForm = null;

/* Callbacks */

function profileDeleted() {
    siteGoTo('/Login');
}

function profileUpdated() {
    Site.Dialogs.info('Kontoinformation uppdaterad');
}

function profileSecured() {
    Site.AutoAjax.clearValues(secureForm);
    Site.Dialogs.info('Lösenord uppdaterat');
}

/* Initialization */

$(function () {
    updateForm = $('#update-profile-form');
    secureForm = $('#change-password-form');

    $('#delete-profile-button').click(function () {
        Site.Dialogs.confirm('/img/delete.png', 'Ta bort konto', 'Vill du avsluta ditt konto?', 'Nej', 'Ja', function () {
            siteAjaxPost('/Profile/DeleteProfile', null, profileDeleted);
        });
    });

    Site.AutoAjax.clearValues(secureForm);
    siteAjaxReadToForm('/Profile/ReadProfile', null, updateForm);

    /* Form buttons */

    updateForm.find('.update-profile-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(updateForm);
            if (data) {
                Site.Validation.clearValidationSummary(updateForm);
                siteAjaxPost('/Profile/UpdateProfile', data, profileUpdated);
            }
        }
    });

    secureForm.find('.change-password-form-buttons').click(function () {
        var button = $(this);
        if (Site.InputPanels.isAPPLY(button)) {
            var data = Site.InputPanels.getFormData(secureForm);
            if (data) {
                Site.Validation.clearValidationSummary(secureForm);
                data && siteAjaxPost('/Profile/ChangePassword', data, profileSecured);
            }
        }
    });
});
