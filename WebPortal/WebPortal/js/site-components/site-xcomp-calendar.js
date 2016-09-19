/*
 FullCalendar

 See http://www.http://fullcalendar.io/

 Required markup:

   <div id="calendar">
   </div>

 Initialization:

   function slotLeftClick(start, end, jsevent, uitarget) { ... }
   function itemLeftClick(item, jsevent, uitarget) { ... }
   function itemRightClick(item, jsevent, uitarget) { ... }
   function itemMove(item, jsevent, uitarget, undoCB) { ... }
   function itemResize(item, jsevent, uitarget, undoCB) { ... }

   calendar1 = new Site.Calendar($('#calendar'), slotLeftClick, itemLeftClick, itemRightClick, itemMove, itemResize);
   calendar1 = new Site.Calendar($('#calendar'), slotLeftClick, itemLeftClick, null, itemMove, null);
*/

var Site = Site || {};

Site.Calendar = function (element, slotLeftClick, itemLeftClick, itemRightClick, itemMove, itemResize) {
    this.calendar  = element;
    this.prevclick = 0;

    var thiz = this;

    /* Capture right-click, http://stackoverflow.com/questions/17566412/on-right-click-trigger-left-click-at-same-place-where-mouse-exists */

    element.on('contextmenu', function (e) {
        e.preventDefault();
    });

    element.mousedown(function (e) {
        if (e.button === 2) {
            if ($(e.target).parents('.fc-event').length > 0) {
                return;
            }
            var newEvent = $.extend($.Event('mousedown'), {
                which: 3,
                clientX: e.clientX,
                clientY: e.clientY,
                pageX: e.pageX,
                pageY: e.pageY,
                screenX: e.screenX,
                screenY: e.screenY
            });
            $(e.target).trigger(newEvent);
        }
    });

    element.mouseup(function (e) {
        if (e.button === 2) {
            if (!$(e.target).parents('.fc-event').length > 0) {
                return;
            }
            var newEvent = $.extend($.Event('click'), {
                which: 3,
                clientX: e.clientX,
                clientY: e.clientY,
                pageX: e.pageX,
                pageY: e.pageY,
                screenX: e.screenX,
                screenY: e.screenY
            });
            $(e.target).trigger(newEvent);
        }
    });

    element.fullCalendar({
        lang: 'sv',
        header: {
            left:   'prev,next today',
            center: 'title',
            right:  'month,agendaWeek,agendaDay'
        },
        selectable: true,
        allDaySlot: false,
        weekNumbers: true,
        defaultDate: Date.now(),
        timezone: 'local',
        editable: true,
        eventLimit: true,
        select: function (start, end, jsEvent, view) {
            if (slotLeftClick && jsEvent.shiftKey) {
                slotLeftClick(start, end, jsEvent, $(this));
            }
        },
        eventClick: function (item, jsEvent, view) {
            if (jsEvent.which === 1 && itemLeftClick != null) {
                itemLeftClick(item, jsEvent, $(this));
            }
            else if (jsEvent.which === 3 && itemRightClick != null) {
                itemRightClick(item, jsEvent, $(this));
            }
        },
        dayClick: function (start, jsEvent, view) {
            var end = moment(start);
            end.add(1, 'hours');
            if (slotLeftClick && (jsEvent.timeStamp - thiz.prevclick) < 500) {
                slotLeftClick(start, end, jsEvent, $(this));
            }
            thiz.prevclick = jsEvent.timeStamp;
        },
        eventDrop: function (item, delta, revertFunc, jsEvent) {
            if (itemMove != null) {
                itemMove(item, jsEvent, $(this), revertFunc);
            }
        },
        eventResize: function (item, delta, revertFunc, jsEvent, ui, view) {
            if (itemResize != null) {
                itemResize(item, jsEvent, $(this), revertFunc);
            }
        }
    });

    this.reload = function (items) {
        this.calendar.fullCalendar('removeEvents');
        this.calendar.fullCalendar('addEventSource', items);
    }

    this.showsMonth = function () {
        var view = this.calendar.fullCalendar('getView');
        return view.intervalUnit == 'month';
    }

    this.showsWeek = function () {
        var view = this.calendar.fullCalendar('getView');
        return view.intervalUnit == 'week';
    }

    this.showsDay = function () {
        var view = this.calendar.fullCalendar('getView');
        return view.intervalUnit == 'day';
    }

    this.getCurrentDate = function () {
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
    }

    this.getCurrentTime = function() {
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
    }
}
