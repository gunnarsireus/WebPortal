/*
 Data-validaion support.
 
 Use isXXX() to validate every keystroke for an input field. For example:

   @Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue, new {
     @class = "form-control",
     id = ViewData["id"],
     type = "number",
     onkeypress = "return Site.Validation.isNumber(event)"
   })

 Use clearValidationSummary() to reset possible jQuery validation errors
 in a form before showing it to the user.
*/

var Site = Site || {};

Site.Validation = Site.Validation || {

    isNumber: function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    },

    isDecimal: function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 44) {
            return false;
        }
        return true;
    },

    isHourMinute: function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 58) {
            return false;
        }
        return true;
    },

    isOrgNumber: function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45) {
            return false;
        }
        return true;
    },

    isPhoneNumber: function (evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45) {
            return false;
        }
        return true;
    },

    isZeroText: function (text) {
        return (text == null || text == '' || text == '0' || text == '0.0');
    },

    toFloat: function (text) {
        if (text == undefined || text == null || text == '') {
            return 0.0;
        }
        if (text.indexOf(',') >= 0) {
            text = text.replace(',', '.');
        }
        var result = parseFloat(text);
        if (isNaN(result)) {
            return 0.0;
        }
        return result;
    },

    toInt: function (text) {
        if (text == undefined || text == null || text == '') {
            return 0;
        }
        var result = parseInt(text);
        if (isNaN(result)) {
            return 0;
        }
        return result;

    },

    isSecurePassword: function (event) {
        var $field = $(event.srcElement);
        var text = $field.val();
        var $formGroup = $field.closest('.form-group');
        if (!$formGroup.hasClass('custom-error-validation')) {
            $formGroup.addClass('custom-error-validation');
        }

        var pointsThreshold = 8;
        var pwlThreshold    = 6;
        var bonusLimit      = 3;
        var ppDeduction     = 1;
        var ppDistinct      = 1;
        var ppSymbol        = 2;
        var ppUpper         = 1;

        $formGroup.removeClass('has-error');
        $formGroup.removeClass('has-warning');
        $formGroup.removeClass('has-success');

        if (text.length < pwlThreshold) {
            $formGroup.addClass('has-error');
            return true;
        }

        var upperBonus  = 0;
        var symbolBonus = 0;
        var points      = 0;

        var unique = text.split('').filter(function (item, i, ar) { return ar.indexOf(item) === i; }).join('');
        points += ppDistinct * unique.length;

        for (var i = 0, len = text.length; i < len; i++) {
            var c = text[i];
            if (c.toLowerCase() != c.toUpperCase()) { // letter
                if(c.toUpperCase() == c && upperBonus < bonusLimit) { // upper case
                    points += ppUpper;
                    upperBonus += ppUpper;
                }
            }
            else if($.isNumeric(c)) { // integer
            }
            else if (symbolBonus < bonusLimit) { // symbol
                points += ppSymbol;
                symbolBonus += ppSymbol;
            }
        }

        if ($.isNumeric(text[0]) || (text[0] == text[0].toUpperCase())) {
            points -= ppDeduction;
        }
        if (text.endsWith("!")) {
            points -= ppDeduction;
        }
        else if (text.endsWith(".")) {
            points -= ppDeduction;
        }
        else if (text.endsWith("2016")) {
            points -= ppDeduction;
        }

        if (points < pointsThreshold) {
            $formGroup.addClass('has-error');
        }
        else if (points < (pointsThreshold * 1.25)) {
            $formGroup.addClass('has-warning');
        }
        else {
            $formGroup.addClass('has-success');
        }
        return true;
    },

    clearValidationSummary: function (form) {
        // Reset jQuery Validate's internals
        form.validate().resetForm();

        // Reset validation summary, if it exists
        form.find('[data-valmsg-summary=true]')
            .removeClass('validation-summary-errors')
            .addClass('validation-summary-valid')
            .find('ul').empty();

        // Reset form-groups
        form.find('.has-error')  .removeClass('has-error');
        form.find('.has-warning').removeClass('has-warning');
        form.find('.has-success').removeClass('has-success');

        // Reset field level, if it exists
        form.find('[data-valmsg-replace]')
            .removeClass('field-validation-error')
            .addClass('field-validation-valid')
            .empty();
    }
};

String.prototype.endsWith = function (suffix) {
    return this.indexOf(suffix, this.length - suffix.length) !== -1;
};