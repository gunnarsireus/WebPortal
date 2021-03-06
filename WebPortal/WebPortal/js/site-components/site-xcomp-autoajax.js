﻿/*
 JSON/AJAX/Form conversion support.
 
 Elements in forms that should be mapped or cleared needs attribute data-autoajax="true"

 Use clearValues(form) to clear all input/select fields
 Use setValues(data, form) to transfer JSON values to form elements
*/

var Site = Site || {};

Site.AutoAjax = Site.AutoAjax || {

    clearValues: function (form) {
        if (!Site.AutoAjax.isForm(form)) {
            Site.AutoAjax.debug('clearValues: no such form');
            return;
        }
        else {
            var elements = $(form).find('select, input[type=text], input[type=number], input[type=hidden], input[type=password], input[type=checkbox], textarea');

            //Input checkboxes generated by bootstrap-multiselect.js do not have any data-autoajax attribute
            //They will therefore trigger a "clearValues: element not mapped".
            //Remove these elements.
            elements = elements.not('ul.multiselect-container input[type=checkbox]');

            $.each(elements, function (i, n) {
                if (!Site.AutoAjax.isMapped(n)) {
                    Site.AutoAjax.debug('clearValues: element not mapped');
                    return;
                }
                if (Site.AutoAjax.isMappedToTrue(n)) {
                    if (Site.AutoAjax.isSelect2Type(n)) {
                        $(n).val(null).trigger('change');
                    }
                    else if (Site.AutoAjax.isMultiSelectType(n)) {
                        $('option:selected', $(n)).each(function () {
                            $(this).prop('selected', false);
                        })
                        $(n).multiselect('refresh');
                    }
                    else if (Site.AutoAjax.isSelectType(n)) {
                        //If there is an option with the "selected" attribute, then set it to that one.
                        //Otherwise set it to the first option.
                        if ($(n).find('option[selected]').length > 0) {
                            $(n).find('option[selected]').prop('selected', true);
                        } else {
                            $(n).prop('selectedIndex', 0);
                        }
                    }
                    else if (Site.AutoAjax.isCheckboxType(n)) {
                        $(n).prop('checked', false);
                    }
                    else if (Site.AutoAjax.isDatePickerType(n)) {
                        $(n).datepicker('setDate', new Date());
                    }
                    else if (Site.AutoAjax.isTextAreaType(n)) {
                        $(n).val('');
                        n.style.height = 'auto';
                        n.style.width = '100%';
                    }
                    else {
                        $(n).val('');
                    }
                }
            });
        }
    },

    setValues: function (data, form) {
        if (!Site.AutoAjax.isForm(form)) {
            Site.AutoAjax.debug('setValues: no such form');
            return;
        }
        else {
            $.each(data, function (i, n) {
                var element = $(form).find('#' + i);
                if ($(element).length) {
                    if (!Site.AutoAjax.isMapped(element)) {
                        Site.AutoAjax.debug('setValues: element not mapped');
                        return;
                    }
                    if (Site.AutoAjax.isMappedToTrue(element)) {
                        if (Site.AutoAjax.isSelect2Type(element)) {
                            // For select2 we need two values, id+text. If we get a hit we need
                            // to search for corresponding json attribute with 'text' as suffix
                            var text = Site.AutoAjax.findObject(data, i + 'text');
                            if (text != null) {
                                var $option = $('<option selected>' + text + '</option>').val(n);
                                element.html('').append($option).trigger('change');
                            }
                        }
                        else if (Site.AutoAjax.isSelectType(element)) {
                            $(element).val(n).change();
                        }
                        else if (Site.AutoAjax.isCheckboxType(element)) {
                            $(element).prop('checked', (n == 1));
                        }
                        else if (Site.AutoAjax.isDatePickerType(element)) {
                            element.datepicker('update', n);
                        }
                        else if (Site.AutoAjax.isTextAreaType(element)) {
                            $(element).val(n);
                            element.get(0).style.height = 'auto';
                            element.get(0).style.width = '100%';
                        }
                        else {
                            $(element).val(n);
                        }
                    }
                }
            });
        }
    },

    isForm: function (form) {
        if ($(form).length < 1) {
            return false;
        }
        return true;
    },

    isMapped: function (element) {
        return $(element).attr('data-autoajax') != undefined;
    },

    isMappedToTrue: function (element) {
        return $(element).attr('data-autoajax') == 'true';
    },

    isTextAreaType: function (element) {
        var type = $(element)[0].nodeName;
        return $(type).is('textarea');
    },

    isSelectType: function (element) {
        var type = $(element)[0].nodeName;
        return $(type).is('select');
    },

    isMultiSelectType: function (element) {
        var type = $(element)[0].nodeName;
        if ($(type).is('select')) {
            var classes = $(element).attr('class');
            return classes != undefined && classes.indexOf('selectmultiplepicker') >= 0;
        }
        return false;
    },

    isSelect2Type: function (element) {
        var type = $(element)[0].nodeName;
        if ($(type).is('select')) {
            var classes = $(element).attr('class');
            return classes != undefined && classes.indexOf('select2') >= 0;
        }
        return false;
    },

    isCheckboxType: function (element) {
        var type = $(element)[0].type;
        return type == 'checkbox';
    },

    isDatePickerType: function (element) {
        var provider = $(element).data('provide');
        return (provider != undefined && provider == 'datepicker');
    },

    findObject: function (data, key) {
        var result = null;
        $.each(data, function (k, v) {
            if (k === key) {
                result = v;
                return false;
            }
        });
        return result;
    },

    debug: function (text) {
        alert('Site.AutoAjax - ' + text);
    }
};
