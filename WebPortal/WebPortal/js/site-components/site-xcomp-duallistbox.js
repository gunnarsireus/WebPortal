/*
 Bootstrap dual listbox.

 See http://www.virtuosoft.eu/code/bootstrap-duallistbox/

 Required markup:

   <select id="my-select" multiple="multiple" size="20">
   </select>

 Initialization:

   Site.DualListbox.init();
   Site.DualListbox.hookList($('#my-select'));
*/

var Site = Site || {};

Site.DualListbox = Site.DualListbox || {

    init: function () {
    },

    hookList: function (element) {
        element.bootstrapDualListbox({
            nonSelectedListLabel: 'Ej valda',
            selectedListLabel: 'Valda',
            moveAllLabel: 'Välj alla',
            removeAllLabel: 'Ta bort alla',
            showFilterInputs: false,
            infoText: false,
            selectorMinimalHeight: 300
        });
    },

    load: function (target, data) {
        target.empty();
        select = target.get(0);
        for (var i = 0; i < data.length; i++) {
            select.options[i] = new Option(data[i].Text, data[i].Value, false, data[i].Selected);
        }
        target.bootstrapDualListbox('refresh');
    },

    selections: function (source) {
        select = source.get(0);
        result = [];
        for (var i = 0; i < select.length; i++) {
            if (select.options[i].selected) {
                result.push(select.options[i].value);
            }
        }
        return result;
    }
}