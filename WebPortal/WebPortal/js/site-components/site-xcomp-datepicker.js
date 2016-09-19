/*
 Bootstrap date- and timepicker.

 See https://bootstrap-datepicker.readthedocs.org/en/latest/index.html
 See http://weareoutman.github.io/clockpicker/

 Alternative: http://www.malot.fr/bootstrap-datetimepicker/demo.php

 Required markup:

   <div class="col-md-8 input-group date" id="container">                                            <-- Note 'date' and 'id'
     @Html.BootstrapSpan(Enums.SpanType.InputGroupAddon, Enums.SpanStyle.Calendar)
     @Html.DisplayFor(model => model.textfield, new { @id = "textfield", container = "#container" }) <-- Tie 'container' to 'id' above
   </div>

 Generated markup (from display/editor-templates):

   <input class="form-control" 
          data-date="2015-09-09"
          data-date-autoclose="true"
          data-date-calendar-weeks="true"
          data-date-container="#container"       <-- Required
          data-date-format="yyyy-mm-dd"
          data-date-language="sv"
          data-date-orientation="bottom left"
          data-date-start-date="2015-09-09"
          data-date-today-btn="linked"
          data-date-today-highlight="true"
          data-date-week-start="1"
          data-provide="datepicker"              <-- Required
          id="plannedopendate"
          name="plannedopendate"
          readonly="readonly"
          type="text"
          value="" />

 Initialization:

   Site.DatePicker.init();
   Site.DatePicker.hookDate($('#textfield'));

 Set date:

   Site.DatePicker.setSelectedDate($('#textfield'), '2014-12-13');
*/

var Site = Site || {};

Site.DatePicker = Site.DatePicker || {

    init: function() {
        $.fn.datepicker.dates.sv = {
            days: ['Söndag', 'Måndag', 'Tisdag', 'Onsdag', 'Torsdag', 'Fredag', 'Lördag', 'Söndag'],
            daysShort: ['Sön', 'Mån', 'Tis', 'Ons', 'Tor', 'Fre', 'Lör', 'Sön'],
            daysMin: ['Sö', 'Må', 'Ti', 'On', 'To', 'Fr', 'Lö', 'Sö'],
            months: ['Januari', 'Februari', 'Mars', 'April', 'Maj', 'Juni', 'Juli', 'Augusti', 'September', 'Oktober', 'November', 'December'],
            monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'Maj', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
            today: 'Idag',
            clear: 'Rensa'
        }
    },
    
    hookDate: function (element) {
        element.datepicker()
    },

    getCurrentDate: function () {
        var today = new Date();
        var dd    = today.getDate();
        var mm    = today.getMonth() + 1;
        var yyyy  = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        return yyyy + '-' + mm + '-' + dd;
    },

    getCurrentTime: function() {
        var today = new Date();
        var hh = today.getHours();
        var mm = today.getMinutes();
        if (hh < 10) {
            hh = '0' + hh;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        return hh + ':' + mm;
    },

    setSelectedDate: function (element, targetdate) {
        element.datepicker('update', targetdate);
    },

    setSelectedTime: function (element, targettime) {
        element.val(targettime);
    },

    hookTime: function (element) {
        element.clockpicker({
            placement: 'bottom',
            donetext: 'Klar',
            autoclose: true,
            'default': 'now'
        })
    }
}