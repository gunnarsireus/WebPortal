var createForm  = null;
var createPanel = null;
var secureForm  = null;
var securePanel = null;

/* Helpers */

function hideMessage(form) {
    var messageField = form.find('#custom-message');
    messageField.html('');
    messageField.hide();
}

function showMessage(form, message) {
    var messageField = form.find('#custom-message');
    messageField.html(message);
    messageField.show();
}

/* Ajax callbacks */

function createAccountPassed() {
    var email = createForm.find('#email').val();
    secureForm.find('#email').val(email);
    createPanel.hide();
    securePanel.show();
}

function createAccountFailed(response) {
    showMessage(createForm, response.message);
}

function resendPinPassed() {
    showMessage(secureForm, 'Ny PIN-kod för verifiering skickad');
}

function resendPinFailed(response) {
    showMessage(secureForm, response.message);
}

function secureAccountPassed(response) {
    Site.LocalStorage.clearAll();
    siteGoTo(response.nextURL);
}

function secureAccountFailed(response) {
    showMessage(secureForm, response.message);
}

/* Initialization */

$(function () {
    createForm  = $('#create-account-form');
    createPanel = $('#create-account-panel');
    secureForm  = $('#secure-account-form');
    securePanel = $('#secure-account-panel');

    Site.Select2.hookSelect(createForm.find('#customerid'), '/Customer/SearchCustomers', 2, null, null, null);

    createForm.find('#go-back-link').click(function () {
        siteGoTo('/Login');
    });

    createForm.find('#register-button').click(function () {
        var data = Site.InputForms.getFormData(createForm);
        if (data) {
            hideMessage(createForm);
            siteAjaxPost('/Register/CreateAccount', data, createAccountPassed, createAccountFailed);
        }
    });

    createForm.on('keydown', function (e) {
        if (e.which == 13) {
            var data = Site.InputForms.getFormData(createForm);
            if (data) {
                hideMessage(createForm);
                siteAjaxPost('/Register/CreateAccount', data, createAccountPassed, createAccountFailed);
            }
        }
    });

    secureForm.find('#resend-pin-button').click(function () {
        var email = secureForm.find('#email').val();
        if (email !== '') {
            siteAjaxPost('/Account/ResendPIN', { email: email }, resendPinPassed, resendPinFailed);
        }
    });

    secureForm.find('#set-password-button').click(function () {
        var data = Site.InputForms.getFormData(secureForm);
        if (data) {
            hideMessage(secureForm);
            siteAjaxPost('/Account/SecureAccount', data, secureAccountPassed, secureAccountFailed);
        }
    });

    secureForm.on('keydown', function (e) {
        if (e.which == 13) {
            var data = Site.InputForms.getFormData(secureForm);
            if (data) {
                hideMessage(secureForm);
                siteAjaxPost('/Account/SecureAccount', data, secureAccountPassed, secureAccountFailed);
            }
        }
    });

    createPanel.show();
    securePanel.hide();
});

//"/Register/CreateAccount", 