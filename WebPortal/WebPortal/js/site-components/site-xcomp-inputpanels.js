/*
 Button support for input panels.
 Use together with HTML Helper SiteInputPanels.
*/

var Site = Site || {};

Site.InputPanels = Site.InputPanels || {

    getFormData: function (form) {
        if (form.valid()) {
            var data = form.serialize();
            return data;
        }
        return null;
    },

    isNONE: function (button) {
        return Site.InputPanels.getAction(button) == 0x0000;
    },
    enableNONE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0000);
    },

    isOPEN: function (button) {
        return Site.InputPanels.getAction(button) == 0x0001;
    },
    enableOPEN: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0001);
    },

    isCLOSE: function (button) {
        return Site.InputPanels.getAction(button) == 0x0002;
    },
    enableCLOSE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0002);
    },

    isCREATE: function (button) {
        return Site.InputPanels.getAction(button) == 0x0004;
    },
    enableCREATE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0004);
    },

    isAPPLY: function (button) {
        return Site.InputPanels.getAction(button) == 0x0008;
    },
    enableAPPLY: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0008);
    },

    isSEARCH: function (button) {
        return Site.InputPanels.getAction(button) == 0x0010;
    },
    enableSEARCH: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0010);
    },

    isCLONE: function (button) {
        return Site.InputPanels.getAction(button) == 0x0020;
    },
    enableCLONE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0020);
    },

    isDELETE: function (button) {
        return Site.InputPanels.getAction(button) == 0x0040;
    },
    enableDELETE: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0040);
    },

    isFETCH: function (button) {
        return Site.InputPanels.getAction(button) == 0x0080;
    },
    enableFETCH: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0080);
    },

    isNOW: function (button) {
        return Site.InputPanels.getAction(button) == 0x0100;
    },
    enableNOW: function (form, enabled) {
        Site.InputForms.setEnabled(form, enabled, 0x0100);
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
