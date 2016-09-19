/*
 Select with filter and AJAX backend support.

 See http://select2.github.io and http://github.com/select2/select2-bootstrap-theme

 Make sure the .js and the .css files are loaded, also the bootstrap-css-addon.

 Required markup (see Helpers/SiteSelect2Inputs):

   <select id="my-select" data-width="100%">
   </select>
 Or:
   @Html.SiteSelect2("my-select", list-of-options-or-null, true)

 Initialization:

   var ajaxurl    = '/Car/SearchCars';     // backend GET URL receives string
   var mintext    = 3;                     // min chars to trigger ajax search
   var ajaxCB     = calledBeforeAjax;      // can be null - add parameters here
   var setsetCB   = calledWhenSelection;   // can be null - called with int 'id'
   var clearsetCB = calledWhenClearing;    // can be null - called when clearing

   Site.Select2.hookSelect($('#my-select'), ajaxurl, mintext, ajaxCB, setsetCB, clearsetCB);

 To add parameters to the AJAX call:

   function calledBeforeAjax(params) {
     var customerid = customerForm.find('#clientid').val();
     return {
       customerid: Site.Validation.toInt(customerid),
       filter: params.term
     };
   }

 To enable clearing without defining a callback function:

   Site.Select2.hookSelect($('#my-select'), ajaxurl, mintext, ajaxCB, setsetCB, Site.Select2.noop);

 On the server-side, get and handle the search request like this:

   [HttpGet]
   public ActionResult SearchCars(string filter)
   {
      return "[{'id':1, 'text':'Volvo', {'id':1,'text':'BMW'}]"
   }

   [HttpGet]
   public ActionResult SearchCarsForCustomer(int customerid, string filter)
   {
      return "[{'id':1, 'text':'Volvo', {'id':1,'text':'BMW'}]"
   }
*/

var Site = Site || {};

Site.Select2 = Site.Select2 || {

    hookSelect: function (target, url, mininput, ajaxCB, setCB, clearCB) {
        var roundedCorners = target.attr('class') == 'site-select2-roundedcorners'; /* See Html Helper */
        var parent = target.closest('div');
        target.select2({
            theme: 'bootstrap',
            language: 'select2/i18n/sv',
            dropdownParent: parent,
            allowClear: (clearCB != null),
            roundedCorners: roundedCorners, /* See lines 1304-1309 in select2/select2.full.js */
            placeholder: {
                id: '',
                placeholder: 'Söktext ...'
            },
            ajax: {
                url: url,
                dataType: 'json',
                delay: 250,
                data: (ajaxCB != null) ? ajaxCB : function (params) {
                    return {
                        filter: params.term
                    };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    return {
                        results: data,
                        pagination: {
                            more: (params.page * 30) < data.length
                        }
                    };
                },
                cache: true
            },
            escapeMarkup: function (markup) { return markup; },
            minimumInputLength: mininput
        });
        if (setCB != null) {
            target.on("select2:select", function (e) {
                setCB(e.params.data.id);
            });
        }
        if (clearCB != null) {
            target.on("select2:unselect", function () {
                clearCB();
                return false;
            });
        }
    },

    setSelection: function (target, id, text) {
        var $option = $('<option selected>' + text + '</option>').val(id);
        target.html('').append($option).trigger('change');
    },

    getSelection: function (target) {
        var id   = target.val();
        var text = target.text();
        return (id == null) ? null : { id: id, text: text };
    },

    clear: function (target) {
        target.val(null).trigger('change');
    },

    disable: function (target) {
        target.prop('disabled', true);
    },

    enable: function (target) {
        target.prop('disabled', false);
    },

    noop: function () {
        // Pass this as clearsetCB to enable "clear" without capturing the event
    }
}
