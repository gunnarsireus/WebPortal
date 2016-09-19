/*
 Modal Bootstrap dialogs support.
*/

var Site = Site || {};

Site.Dialogs = Site.Dialogs || {

    confirmIf: function(condition, icon, header, question, cancelText, performText, callback) {
        if (!condition) {
            callback();
        }
        else {
            Site.Dialogs.confirm(icon, header, question, cancelText, performText, callback);
        }
    },

    confirm: function (icon, header, question, cancelText, performText, callback) {
        var confirmModal =
    $('<div class="modal fade" id="confirm-modal" tabindex="-1" role="dialog" aria-hidden="true">' +
        '<div class="modal-dialog">' +
          '<div class="modal-content">' +
            '<div class="modal-header">' +
              '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
              '<h4 class="modal-title"><img src="' + icon + '">&nbsp;&nbsp;' + header + '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
              '<p>' + question + '</p>' +
            '</div>' +
            '<div class="modal-footer">' +
              '<div class="btn btn-info" data-dismiss="modal">' + cancelText + '</div>' +
              '<div id="okButton" class="btn btn-success">' + performText + '</div>' +
            '</div>' +
          '</div>' +
        '</div>' +
      '</div>');
        confirmModal.find('#okButton').click(function () {
            callback();
            confirmModal.modal('hide');
            return false;
        });
        confirmModal.modal('show');
    },

    confirmInput: function (icon, header, question, input, cancelText, performText, callback) {
        var confirmModal =
   $('<div class="modal fade" id="confirm-modal" tabindex="-1" role="dialog" aria-hidden="true">' +
       '<div class="modal-dialog">' +
         '<div class="modal-content">' +
           '<div class="modal-header">' +
             '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
             '<h4 class="modal-title"><img src="' + icon + '">&nbsp;&nbsp;' + header + '</h4>' +
           '</div>' +
           '<div class="modal-body">' +
             '<p>' + question + '</p>' +
             '<textarea class="form-control" id="input" name="input" rows="3">' + input + '</textarea>' +
           '</div>' +
           '<div class="modal-footer">' +
             '<div class="btn btn-info" data-dismiss="modal">' + cancelText + '</div>' +
             '<div id="okButton" class="btn btn-success">' + performText + '</div>' +
           '</div>' +
         '</div>' +
       '</div>' +
     '</div>');
        confirmModal.find('#okButton').click(function () {
            var input = confirmModal.find('#input').val();
            callback(input);
            confirmModal.modal('hide');
            return false;
        });
        confirmModal.modal('show');
    },

    error: function (message) {
        var errorModal =
      $('<div class="modal fade" id="error-modal" tabindex="-1" role="dialog" aria-hidden="true">' +
          '<div class="modal-dialog">' +
            '<div class="modal-content">' +
              '<div class="modal-header">' +
                '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                '<h4 class="modal-title"><img src="/img/error.png">&nbsp;&nbsp;Ett fel har inträffat</h4>' +
              '</div>' +
              '<div class="modal-body">' +
                '<p>' + message + '</p>' +
              '</div>' +
              '<div class="modal-footer">' +
                '<div id="okButton" class="btn btn-success">Stäng</div>' +
              '</div>' +
            '</div>' +
          '</div>' +
        '</div>');
        errorModal.find('#okButton').click(function () {
            errorModal.modal('hide');
            return false;
        });
        errorModal.modal('show');
    },

    info: function (message) {
        var infoModal =
      $('<div class="modal fade" id="info-modal" tabindex="-1" role="dialog" aria-hidden="true">' +
          '<div class="modal-dialog">' +
            '<div class="modal-content">' +
              '<div class="modal-header">' +
                '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                '<h4 class="modal-title"><img src="/img/info.png">&nbsp;&nbsp;Information</h4>' +
              '</div>' +
              '<div class="modal-body">' +
                '<p>' + message + '</p>' +
              '</div>' +
              '<div class="modal-footer">' +
                '<div id="okButton" class="btn btn-success">Stäng</div>' +
              '</div>' +
            '</div>' +
          '</div>' +
        '</div>');
        infoModal.find('#okButton').click(function () {
            infoModal.modal('hide');
            return false;
        });
        infoModal.modal('show');
    }
};

/* Makes dialogs moveable */
$(function () {
    $('.modal-dialog').draggable({
        handle: '.modal-header'
    });
});
