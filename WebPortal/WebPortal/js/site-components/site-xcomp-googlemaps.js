/*
 Google Maps support.

 See https://hpneo.github.io/gmaps/
 */

var Site = Site || {};

Site.GoogleMaps = function (divname, width, height, zoom) {
    this.zoom = zoom;
    google.maps.event.addDomListener(window, 'resize'); // Resizing map

    this.map = new GMaps({
        div: divname,
        lat: Site.GoogleMaps.getDefaultLatitude(),
        lng: Site.GoogleMaps.getDefaultLongitude(),
        zoom: zoom,
        width: width,
        height: height,
        zoomControl: true
    });

    this.initWithMarker = function (position, callback, icon) {
        var lat = (position == null) ? Site.GoogleMaps.getDefaultLatitude() : position.latitude;
        var lng = (position == null) ? Site.GoogleMaps.getDefaultLongitude() : position.longitude;
        var img = (icon == undefined) ? null : icon;
        this.map.removeMarkers();
        this.map.addMarker({
            lat: lat,
            lng: lng,
            icon: img,
            draggable: true,
            dragend: function (e) {
                if (callback != null) {
                    var p1 = e.latLng.lat().toFixed(7);
                    var p2 = e.latLng.lng().toFixed(7);
                    callback(p1, p2);
                }
            }
        });
        this.map.setCenter(lat, lng);
        this.map.setZoom(this.zoom);
    }

    this.moveMarkerLatitude = function (latitude) {
        var marker = this.map.markers[0];
        var position = new google.maps.LatLng(latitude, marker.position.lng());
        marker.setPosition(position);
        this.map.setCenter(marker.position.lat(), marker.position.lng());
        this.map.setZoom(this.zoom);
    }

    this.moveMarkerLongitude = function (longitude) {
        var marker = this.map.markers[0];
        var position = new google.maps.LatLng(marker.position.lat(), longitude);
        marker.setPosition(position);
        this.map.setCenter(marker.position.lat(), marker.position.lng());
        this.map.setZoom(this.zoom);
    }

    this.moveMarker = function (latitude, longitude) {
        var marker = this.map.markers[0];
        var position = new google.maps.LatLng(latitude, longitude);
        marker.setPosition(position);
        this.map.setCenter(marker.position.lat(), marker.position.lng());
        this.map.setZoom(this.zoom);
    }

    this.removeMarkers = function () {
        this.map.removeMarkers();
    }

    this.addClickableMarker = function (latitude, longitude, icon, title, content) {
        var thismap = this.map;
        var center = this.map.getCenter();
        this.map.addMarker({
            lat: latitude,
            lng: longitude,
            icon: icon,
            title: title,
            click: function () {
                window.center = thismap.getCenter();
            },
            infoWindow: {
                content: content,
                closeclick: function () {
                    thismap.setCenter({
                        lat: parseFloat(window.center.lat()),
                        lng: parseFloat(window.center.lng())
                    });
                }
            }
        });
    }

    this.setAddressMarker = function (address, callback, icon) {
        var img = (icon == undefined) ? null : icon;
        var thismap = this.map;
        var thiszoom = this.zoom;
        GMaps.geocode({
            address: address,
            callback: function (results, status) {
                if (status == 'OK') {
                    thismap.removeMarkers();
                    var position = results[0].geometry.location;
                    if (callback != null) {
                        callback(position.lat(), position.lng());
                    }
                    thismap.addMarker({
                        lat: position.lat(),
                        lng: position.lng(),
                        icon: img,
                        draggable: true,
                        dragend: function (e) {
                            if (callback != null) {
                                var p1 = e.latLng.lat().toFixed(7);
                                var p2 = e.latLng.lng().toFixed(7);
                                callback(p1, p2);
                            }
                        }
                    });
                    thismap.setCenter(position.lat(), position.lng());
                    thismap.setZoom(thiszoom);
                }
            }
        });
    }

    this.changeMarkerIcon = function (icon) {
        var marker = this.map.markers[0];
        marker.icon = icon;
        this.map.removeMarkers();
        this.map.addMarker(marker);
        this.map.setCenter(marker.position.lat(), marker.position.lng());
        this.map.setZoom(this.zoom);
    }

    this.setZoom = function (zoom) {
        this.zoom = zoom;
        this.map.setZoom(zoom);
    }

    this.showZoomed = function (zoom) {
        // Don't save zoom
        this.map.setZoom(zoom);
    }

    this.setCenter = function (latitude, longitude) {
        this.map.setCenter(latitude, longitude);
    }

    this.adjustView = function () {
        if (this.map.markers.length == 1) {
            var marker = this.map.markers[0];
            this.map.setCenter(marker.position.lat(), marker.position.lng());
            this.map.setZoom(this.zoom);
        }
        else if (this.map.markers.length > 1) {
            this.map.fitZoom();
        }
    }

    this.isValidPosition = function (position) {
        return position.latitude != 0 && position.longitude != 0;
    }
}

Site.GoogleMaps.getDefaultLatitude = function () {
    return 61.8193660;
}

Site.GoogleMaps.getDefaultLongitude = function () {
    return 16.5642930;
}
