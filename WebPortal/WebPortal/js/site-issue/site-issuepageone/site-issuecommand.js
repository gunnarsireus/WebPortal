var Site = Site || {};

Site.IssueCommand = function (updateCB, exitCB) {
    var thiz     = this;
    this.command = $('#command-issue-panel');

    this.setStatus = function (status) {
        // Set visibility for current status spans
        this.command.find('[id^=command-issue-panel-curr-status-]').each(function () {
            $(this).hide();
        });
        this.command.find('#command-issue-panel-curr-status-' + status).show();

        // Set visibility for next status divs
        this.command.find('[id^=command-issue-panel-next-status-for-]').each(function () {
            $(this).hide();
        });
        this.command.find('#command-issue-panel-next-status-for-' + status).show();
    }

    this.command.find('.command-issue-panel-nextstatus').click(function () {
        var img    = $(this);
        var image  = img.attr('src')
        var status = img.attr('data-tostatus');
        var name   = img.attr('data-tostatusname');
        Site.Dialogs.confirm(image, 'Byt status', 'Vill du ändra status på ärendet till ' + name + '?', 'Nej', 'Ja', function () {
            updateCB(status);
        });
    });

    this.command.find('#command-issue-panel-exit').click(function () {
        exitCB();
    });
}
