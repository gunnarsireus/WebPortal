/*
 Bootstrap-styled multiple select.

 Use together with helper BootstrapSelectMultiple.cs

 Initialize in JS:

   Site.SelectMultiple.init(true);

 Before opening create-form:

   Site.SelectMultiple.clear(createMessageForm.find('#foremen'));

 To set values in update form (message.foremen is an array of integers):

   Site.SelectMultiple.setSelections(updateMessageForm.find('#foremen'), message.foremen);

 See https://github.com/davidstutz/bootstrap-multiselect/
*/

var Site = Site || {};

Site.SelectMultiple = Site.SelectMultiple || {

    init: function (allselectable) {
        $('.selectmultiplepicker').multiselect({
            buttonWidth: '100%',
            numberDisplayed: 1,
            nonSelectedText: '--',
            nSelectedText: ' valda',
            allSelectedText: 'Alla valda',
            includeSelectAllOption: allselectable,
            selectAllText: '(Alla)',
            selectAllValue: '-1',
            templates: {
                button: '<button type="button" class="multiselect dropdown-toggle" data-toggle="dropdown"><span class="multiselect-selected-text pull-left"></span> <b class="caret pull-right" style="margin-top:9px;"></b></button>',
                ul: '<ul class="multiselect-container dropdown-menu"></ul>',
                filter: '<li class="multiselect-item filter"><div class="input-group"><span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span><input class="form-control multiselect-search" type="text"></div></li>',
                filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" type="button"><i class="glyphicon glyphicon-remove-circle"></i></button></span>',
                li: '<li><a tabindex="0"><label></label></a></li>',
                divider: '<li class="multiselect-item divider"></li>',
                liGroup: '<li class="multiselect-item multiselect-group"><label></label></li>'
            }
        });
    },

    clear: function (target) {
        $('option:selected', target).each(function () {
            $(this).prop('selected', false);
        })
        target.multiselect('refresh');
    },

    setSelections: function (target, selections) {
        $('option:selected', target).each(function () {
            $(this).prop('selected', false);
        })
        target.multiselect('select', selections);
        target.multiselect('refresh');
    },

    getSelections: function (target) {
        return $('option:selected', target).map(function (a, item) { return item.value; });
    },
}
