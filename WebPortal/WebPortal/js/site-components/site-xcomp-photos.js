/*
 Bootstrap-styled photos support.

 Required markup (see Helpers/BootstrapPhotos):

   @using (Html.BootstrapInputForm("transportorder-photos-form", null, BootstrapInputForms.RELOAD, BootstrapInputForms.NONE))
   {
      <div class="row site-itemmargin-top">
          <div class="col-md-9">
              @Html.BootstrapPhoto("/img/delete.png", "photo-delete", BootstrapPhotos.WRITEABLE)
          </div>
          <div class="col-md-3">
              <div class="row">
                  <div class="col-md-6">
                      @Html.BootstrapThumbnail("/img/camera.png", "photo-0")
                  </div>
                  <div class="col-md-6">
                      @Html.BootstrapThumbnail("/img/camera.png", "photo-1")
                  </div>
              </div>
              <div class="row">
                  <div class="col-md-6">
                      @Html.BootstrapThumbnail("/img/camera.png", "photo-2")
                  </div>
                  <div class="col-md-6">
                      @Html.BootstrapThumbnail("/img/camera.png", "photo-3")
                  </div>
              </div>
              <div class="row">
                  <div class="col-md-6">
                      @Html.BootstrapThumbnail("/img/camera.png", "photo-4")
                  </div>
                  <div class="col-md-6">
                      @Html.BootstrapThumbnail("/img/camera.png", "photo-5")
                  </div>
              </div>
          </div>
      </div>
   }

 In JS, initialize:

   photosForm = $('#transportorder-photos-form');

 Load thumbnails:

   var thumbnails  = ... array of photo objects from server
   var count       = ... max number of thumbnails (server response may contain fewer which is ok)
   var placeholder = ... image to show in empty thumbnails such as '/img/camera.png'

   Site.Photos.loadThumbnails(photosForm, thumbnails, count, placeholder);

 Catch thumbnail click:

    $('.photo-thumbnail').click(function () {
        var photoid = Site.Photos.getClickedID($(this));
        ...
    });

 Load real photo:

   Site.Photos.showPhoto(photosForm, photo);

 Catch photo deletion request:

    photosForm.find('#photo-delete').click(function () {
       var photoid = Site.Photos.getShowingID();
       ...
   });

 Delete photo:

   var photoid = Site.Photos.getShowingID();
   ... call server
   Site.Photos.hidePhoto(photosForm);
   ... load thumbnails again
*/

var Site = Site || {};

Site.Photos = Site.Photos || {

    loadThumbnails: function (form, photos, count, placeholder) {
        for (var i = 0; i < count; i++) {
            var img = form.find('#photo-' + i);
            if (i < photos.length) {
                var photo = photos[i];
                img.attr('src', 'data:image/jpg;base64,' + photo.thumbnail);
                img.attr('data-photoid', photo.id);
            }
            else {
                img.attr('src', placeholder);
                img.attr('data-photoid', '');
            }
        }
    },

    showPhoto: function (form, photo) {
        var img = form.find('#photo');
        img.attr('style', (photo.rotation == 0 || photo.rotation == 180) ? 'width:360px;height:640px' : 'width:640px;height:360px');
        img.attr('src', 'data:image/jpg;base64,' + photo.image);
        img.attr('data-photoid', photo.id);
        form.find('#caption').get(0).innerHTML = photo.caption;
        form.find('#figure').show();
    },

    hidePhoto: function (form) {
        form.find('#figure').hide();
    },

    getShowingID: function (form) {
        var img = form.find('#photo');
        var photoid = img.attr('data-photoid');
        return photoid;
    },

    getClickedID: function (img) {
        var photoid = img.attr('data-photoid');
        return photoid;
    }
}
