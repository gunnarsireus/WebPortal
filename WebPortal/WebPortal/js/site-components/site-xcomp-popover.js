/*
 Bootstrap popover support.
 
 $('#item').click(function(e) {
   var p = new Site.Popover();
   p.setTitle('Title1');
   p.setHeader('Header1');
   p.setBody('B1');
   p.setTimeout(1500);        // Overide default value
   p.show(e, $(this));
 });

*/

var Site = Site || {};

Site.Popover = Site.Popover || {};

Site.Popover = function () {
    this.title   = '';
    this.header  = '';
    this.body    = '';
    this.timeout = 12000;

    this.setTitle = function (t) {
        this.title = t;
    };

    this.setHeader = function (h) {
        this.header = h;
    };

    this.setBody = function (b) {
        this.body = b;
    };

    this.setTimeout = function (t) {
        this.timeout = t;
    };

    this.show = function (e, target, placement) {
        if (placement == undefined) {
            placement = 'bottom';
        }
        e.stopPropagation();
        target.popover({
            'trigger': 'manual',
            'html': true,
            'container': 'body',
            'placement': placement,
            'title': this.title + '&nbsp;&nbsp;&nbsp;<span class="site-xcomp-popover-close close" style="font-size:125%;">&times;</span>',
            'content': '<b>' + this.header + '</b><p>' + this.body
        }).on('shown.bs.popover', function (e) {
            $('.site-xcomp-popover-close').on('click', function (e) {
                target.popover('destroy');
            });
        });
        target.popover('show');
        setTimeout(function () { target.popover('destroy'); }, this.timeout);
    };
};

Site.Popover.closeAll = function () {
    $('.popover').each(function () {
        if (!$(this).parents().is('.popover.in')) {
            $(this).popover('destroy');
        }
    });
};

/* Any click outside the popover closes all other popovers */
$(function () {
    $('html').on('click', function (e) {
        $('.popover').each(function () {
            if (!$(e.target).parents().is('.popover.in')) {
                $(this).popover('destroy');
            }
        });
    });
});