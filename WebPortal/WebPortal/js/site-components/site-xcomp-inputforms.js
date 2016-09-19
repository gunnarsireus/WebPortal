/*
 Button support for input forms.
 Use together with HTML Helper SiteInputForms.
*/

var Site = Site || {};

Site.InputForms = Site.InputForms || {

    getFormData: function (form) {
        if (form.valid()) {
            var data = form.serialize();
            return data;
        }
        return null;
    },

    isNONE: function (button) {
        return Site.InputForms.getAction(button) == 0x00;
    },
    enableNONE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x00);
    },

    isOPEN: function (button) {
        return Site.InputForms.getAction(button) == 0x01;
    },
    enableOPEN: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x01);
    },

    isCLOSE: function (button) {
        return Site.InputForms.getAction(button) == 0x02;
    },
    enableCLOSE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x02);
    },

    isCREATE: function (button) {
        return Site.InputForms.getAction(button) == 0x04;
    },
    enableCREATE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x04);
    },

    isAPPLY: function (button) {
        return Site.InputForms.getAction(button) == 0x08;
    },
    enableAPPLY: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x08);
    },

    isSEARCH: function (button) {
        return Site.InputForms.getAction(button) == 0x10;
    },
    enableSEARCH: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x10);
    },

    isRELOAD: function (button) {
        return Site.InputForms.getAction(button) == 0x20;
    },
    enableRELOAD: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x20);
    },

    /* Private helpers */

    getAction: function (button) {
        var action = button.attr('data-action');
        return Site.Validation.toInt(action);
    },

    setEnabled: function (form, enabled, action) {
        var button = form.find('[data-action="' + action + '"]');
        button.attr('disabled', !enabled);
    }
};
