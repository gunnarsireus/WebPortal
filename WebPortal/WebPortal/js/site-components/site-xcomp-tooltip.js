/*
 Bootstrap tooltip support. Triggered by right mouse-click.
 
 Site.Tooltip.set($('#item'), 'Header', 'This is the tooltip text');
 
*/

var Site = Site || {};

Site.Tooltip = Site.Tooltip || {

    set: function (element, title, tooltip) {
        element.on('click', function (e) {
            element.hover(function () {
                $(this).css('cursor', 'help');
            });
            element.popover({
                'trigger': 'manual',
                'html': true,
                'container': 'body',
                'placement': 'bottom',
                'title': title,
                'content': tooltip
            });
            element.popover('show');
            setTimeout(function () { element.popover('hide') }, 2000);
        });
    }

};
