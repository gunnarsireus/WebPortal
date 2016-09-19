/*
 Bootstrap-styled select.

 See http://silviomoreto.github.io/bootstrap-select/

 To avoid flickering at page load, insert 

   select {
     visibility: hidden;
     display: none;
   }

 See http://stackoverflow.com/questions/23251488/bootstrap-select-plugin-how-to-avoid-flickering
*/

var Site = Site || {};

Site.Select = Site.Select || {

    init: function () {
        $('.selectpicker').selectpicker();
    },

    refresh: function () {
        $('.selectpicker').selectpicker('refresh');
    },

    setSelection: function (target, value) {
        target.children("option").each(function () {
            this.selected = (this.value == value);
        });
        target.selectpicker('refresh');
    },

    clear: function (target) {
        target.empty();
        target.selectpicker('refresh');
    },

    load: function (target, data, selectindex) {
        target.empty();
        select = target.get(0);
        for (var i = 0; i < data.length; i++) {
            select.options[i] = new Option(data[i].text, data[i].value);
            if (i == selectindex || data[i].selected) {
                select.options[i].selected = true;
            }
        }
        target.selectpicker('refresh');
    },

    disable: function (target) {
        target.prop('disabled', true);
    },

    enable: function (target) {
        target.prop('disabled', false);
    },

    clone: function (source, target) {
        target.empty();
        target.html(source.html());
        target.selectpicker('refresh');
    },

    setReadOnly: function (target, state) {
        // Select boxes can't be set 'readonly'. However, they can be
        // set to 'disabled' but then their no longer part of a form.
        target.children("option").each(function () {
            if (this.selected != true) {
                $(this).prop('disabled', state);
            }
        });
    }
}
