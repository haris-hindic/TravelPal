(function ($) {
    "use strict";

    $(document).ready(function () {
        qodefInitGeoLocationRangeSlider.init();
    });

    $(window).on('load', function () {
        qodefInitMultipleListingMap.init();
        qodefInitMobileMap.init();
    });

    var qodefGoogleMap = {
        initMap: function ($mapHolder) {
    
            if (typeof google !== 'object') {
                return;
            }

            var mapParams = qodefGoogleMap.initMapParams($mapHolder);

            var settings = {
                styles: qodefMapsVariables.global.mapStyle,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                zoom: parseInt(qodefMapsVariables.global.mapZoom, 10),
                scrollwheel: qodefMapsVariables.global.mapScrollable,
                draggable: qodefMapsVariables.global.mapDraggable,
                streetViewControl: qodefMapsVariables.global.streetViewControl,
                zoomControl: qodefMapsVariables.global.zoomControl,
                mapTypeControl: qodefMapsVariables.global.mapTypeControl,
                fullscreenControl: qodefMapsVariables.global.fullscreenControl
            };

            var map = new google.maps.Map(document.getElementById(mapParams.holderId), settings);
            var geocoder = new google.maps.Geocoder();

            mapParams.addressesLatLng = [];
            for (var index = 0; index < mapParams.addresses.length; ++index) {
                qodefGoogleMap.initGoogleAddress(index, mapParams, map, geocoder);
            }

            if (!isNaN(mapParams.mapHeight)) {
                var height = mapParams.mapHeight + 'px',
                    holderElement = document.getElementById(mapParams.holderId);
                holderElement.style.height = height;
            }
        },

        initMapParams: function ($map) {
            var options = {};

            var pin;
            if (typeof $map.data('pin') !== 'undefined' && $map.data('pin') !== false) {
                pin = $map.data('pin');
            }
            options.pin = pin;

            var mapHeight;
            if (typeof $map.data('height') !== 'undefined' && $map.data('height') !== false) {
                mapHeight = $map.data('height');
            }
            options.mapHeight = mapHeight;

            var uniqueId;
            if (typeof $map.data('unique-id') !== 'undefined' && $map.data('unique-id') !== false) {
                uniqueId = $map.data('unique-id');
            }
            options.uniqueId = uniqueId;
            options.holderId = "qodef-map-id--" + uniqueId;

            var addresses;
            if (typeof $map.data('addresses') !== 'undefined' && $map.data('addresses') !== false) {
                addresses = $map.data('addresses');
            }
            options.addresses = addresses;

            return options;
        },
        initGoogleAddress: function (index, mapParams, $map, geocoder) {
            var address = mapParams.addresses[index];

            if (address === '') {
                return;
            }

            var contentString = '<div id="content"><div id="siteNotice"></div><div id="bodyContent"><p>' + address + '</p></div></div>';
            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });

            geocoder.geocode({'address': address}, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    var marker = new google.maps.Marker({
                        map: $map,
                        position: results[0].geometry.location,
                        icon: mapParams.pin,
                        title: address.store_title
                    });
                    google.maps.event.addListener(marker, 'click', function () {
                        infowindow.open($map, marker);
                    });

                    google.maps.event.addDomListener(window, 'resize', function () {
                        qodefGoogleMap.initMapCenter($map, mapParams.addressesLatLng);
                    });

                    var latLng = {};
                    latLng.lat = results[0].geometry.location.lat();
                    latLng.lng = results[0].geometry.location.lng();
                    mapParams.addressesLatLng[index] = latLng;

                    if (mapParams.addresses.length === mapParams.addressesLatLng.length) {
                        setTimeout(function () {
                            qodefGoogleMap.initMapCenter($map, mapParams.addressesLatLng);
                        }, 500);
                    } else {
                        $map.setCenter(results[0].geometry.location);
                    }
                }
            });
        },

        initMapCenter: function ($map, addressesLatLng) {
            var lat_min = 99999;
            var lat_max = 1;
            var lng_min = 99999;
            var lng_max = 1;

            for (var index = 0; index < addressesLatLng.length; ++index) {
                lat_min = Math.abs(addressesLatLng[index].lat) < Math.abs(lat_min) ? addressesLatLng[index].lat : lat_min;
                lat_max = Math.abs(addressesLatLng[index].lat) > Math.abs(lat_max) ? addressesLatLng[index].lat : lat_max;

                lng_min = Math.abs(addressesLatLng[index].lng) < Math.abs(lng_min) ? addressesLatLng[index].lng : lng_min;
                lng_max = Math.abs(addressesLatLng[index].lng) > Math.abs(lng_max) ? addressesLatLng[index].lng : lng_max;
            }

            $map.setCenter(new google.maps.LatLng(
                ((lat_max + lat_min) / 2.0),
                ((lng_max + lng_min) / 2.0)
            ));
        }
    };

    window.qodefGoogleMap = qodefGoogleMap;

    var qodefInitGeoLocationRangeSlider = {
        init: function () {
            var $holder = $('.qodef-map-list-holder');
            if ($holder.length) {
                var $geoLocationRadius = $holder.find('.qodef-places-geo-radius');

                if ($geoLocationRadius.length) {
                    var $slider = document.getElementById('qodef-range-slider-id');

                    qodefInitGeoLocationRangeSlider.createSlider($geoLocationRadius, $slider);
                }
            }
        },
        reset: function () {
            var $holder = $('.qodef-map-list-holder');

            if ($holder.length) {
                var $geoLocationRadius = $holder.find('.qodef-places-geo-radius');

                if ($geoLocationRadius.length && $geoLocationRadius.is(':visible')) {
                    var $slider = document.getElementById('qodef-range-slider-id');

                    qodefInitGeoLocationRangeSlider.sliderVisibility($geoLocationRadius, '', false);
                    qodefInitGeoLocationRangeSlider.resetRadius($slider);
                }
            }
        },
        showRangeSlider: function (latlng, visibility) {
            var $holder = $('.qodef-map-list-holder');
            if ($holder.length) {
                var $geoLocationRadius = $holder.find('.qodef-places-geo-radius');

                if ($geoLocationRadius.length) {
                    qodefInitGeoLocationRangeSlider.sliderVisibility($geoLocationRadius, latlng, visibility);
                }
            }
        },
        createSlider: function ($geoLocationRadius, $slider) {
            noUiSlider.create($slider, {
                connect: [true, false],
                start: 0,
                step: 1,
                tooltips: true,
                format: {
                    from: function (value) {
                        return parseInt(value);
                    },
                    to: function (value) {
                        return parseInt(value);
                    }
                },
                range: {
                    min: 0,
                    max: 100
                }
            });

            qodefInitGeoLocationRangeSlider.updateMapRadius($geoLocationRadius, $slider);
        },

        updateMapRadius: function ($geoLocationRadius, $slider) {
            var sliderEventCount = 0;

            $slider.noUiSlider.on('set', function (values) {
                var geoLocation = $geoLocationRadius.data('geo-location');

                if (typeof geoLocation === 'object') {
                    window.qodefGoogleMultipleMap.setGeoLocationRadius($geoLocation, values, sliderEventCount > 0);
                    sliderEventCount++;
                }
            });
        },
        resetRadius: function ($slider) {
            $slider.noUiSlider.reset();
        },
        sliderVisibility: function ($geoLocationRadius, latlng, visibility) {
            $geoLocationRadius.data('geo-location', latlng);

            if (visibility) {
                $geoLocationRadius.show();
            } else {
                $geoLocationRadius.hide();
            }
        },
        disableItemsOutOfRange: function ($itemsInArea) {
            var $holder = $('.qodef-map-list-holder');

            if ($holder.length && typeof $itemsInArea === 'object') {
                var $items = $holder.find('.qodef-grid-inner article');

                if ($items.length && $itemsInArea.length > 0) {
                    if (!$holder.children('.qodef-out-of-range-holder').length) {
                        $holder.append('<div class="qodef-out-of-range-holder"></div>');
                    }

                    var $outOfRangeHolder = $holder.children('.qodef-out-of-range-holder'),
                        $outOfRangeItems = $outOfRangeHolder.children('article'),
                        $inRangeHolder = $holder.find('.qodef-grid-inner');

                    $items.each(function () {
                        var $thisItem = $(this),
                            itemID = $thisItem.data('id');

                        if (itemID !== undefined && itemID !== false) {
                            var itemInRange = false;

                            $.each($itemsInArea, function (i, id) {
                                if (parseInt(itemID, 10) === id) {
                                    itemInRange = true;
                                    return true;
                                }
                            });

                            if (!itemInRange) {
                                $thisItem.appendTo($outOfRangeHolder);

                                if ($holder.hasClass('qodef-layout--masonry')) {
                                    $inRangeHolder.isotope('layout');
                                }
                            }
                        }
                    });

                    if ($outOfRangeItems.length) {
                        $outOfRangeItems.each(function () {
                            var $thisOutItem = $(this),
                                outItemID = $thisOutItem.data('id'),
                                itemInRange = false;

                            $.each($itemsInArea, function (i, id) {
                                if (parseInt(outItemID, 10) === id) {
                                    itemInRange = true;
                                    return true;
                                }
                            });

                            if (itemInRange) {
                                $thisOutItem.appendTo($inRangeHolder);

                                if ($holder.hasClass('qodef-layout--masonry')) {
                                    $inRangeHolder.isotope('layout');
                                }
                            }
                        });
                    }
                }
            }
        }
    }

    window.qodefInitGeoLocationRangeSlider = qodefInitGeoLocationRangeSlider;

    var qodefGoogleMultipleMap = {
        //Object variables
        mapHolder: {},
        map: {},
        markers: {},
        radius: {},
        circle: {},

        /**
         * Returns map with multiple addresses
         *
         * @param options
         */
        getDirectoryItemsAddresses: function (options) {
            var defaults = {
                geolocation: false,
                mapHolder: 'qodef-multiple-map-holder',
                addresses: qodefMapsVariables.multiple.addresses,
                draggable: qodefMapsVariables.global.draggable,
                mapTypeControl: qodefMapsVariables.global.mapTypeControl,
                scrollwheel: qodefMapsVariables.global.scrollable,
                streetViewControl: qodefMapsVariables.global.streetViewControl,
                zoomControl: qodefMapsVariables.global.zoomControl,
                zoom: 16,
                styles: qodefMapsVariables.global.mapStyle,
                radius: 50, //radius for marker visibility, in km
                hasFilter: false
            };

            var settings = $.extend({}, defaults, options);

            //Get map holder
            var $mapHolder = document.getElementById(settings.mapHolder);

            //Initialize map
            var map = new google.maps.Map($mapHolder, {
                zoom: settings.zoom,
                draggable: settings.draggable,
                mapTypeControl: settings.mapTypeControl,
                scrollwheel: settings.scrollwheel,
                streetViewControl: settings.streetViewControl,
                zoomControl: settings.zoomControl
            });

            //Save variables for later usage
            this.mapHolder = settings.mapHolder;
            this.map = map;
            this.radius = settings.radius;

            //Set map style
            map.setOptions({
                styles: settings.styles
            });

            //If geolocation enabled set map center to user location
            if (navigator.geolocation && settings.geolocation) {
                this.centerOnCurrentLocation();
            }

            //Filter addresses, remove items without latitude and longitude
            var addresses = [];

            if (typeof settings.addresses !== 'undefined') {
                var addressesLength = settings.addresses.length;

                if (settings.addresses.length !== null) {
                    for (var i = 0; i < addressesLength; i++) {
                        var location = settings.addresses[i].location;

                        if (typeof location !== 'undefined' && location !== null) {

                            if (location.latitude !== '' && location.longitude !== '') {
                                addresses.push(settings.addresses[i]);
                            }
                        }
                    }
                }
            }

            //Center map and set borders of map
            this.setMapBounds(addresses);

            //Add markers to the map
            this.addMultipleMarkers(addresses);
        },

        /**
         * Add multiple markers to map
         */
        addMultipleMarkers: function (markersData) {
            var map = this.map;
            var markers = [];

            //Loop through markers
            var len = markersData.length;

            for (var i = 0; i < len; i++) {
                var latLng = {
                    lat: parseFloat(markersData[i].location.latitude),
                    lng: parseFloat(markersData[i].location.longitude)
                };

                //Custom html markers
                //Insert marker data into info window template
                var templateData = {
                    title: markersData[i].title,
                    itemId: markersData[i].itemId,
                    address: markersData[i].location.address,
                    featuredImage: markersData[i].featuredImage,
                    itemUrl: markersData[i].itemUrl,
                    latLng: latLng
                };

                var customMarker = new window.qodefCustomMarker({
                    position: latLng,
                    map: map,
                    templateData: templateData,
                    markerPin: markersData[i].markerPin
                });

                markers.push(customMarker);
            }

            this.markers = markers;

            //Init map clusters ( Grouping map markers at small zoom values )
            this.initMapClusters();

            //Init marker info
            this.initMarkerInfo();
        },

        /**
         * Set map bounds for Map with multiple markers
         *
         * @param addressesArray
         */
        setMapBounds: function (addressesArray) {
            var bounds = new google.maps.LatLngBounds();

            for (var i = 0; i < addressesArray.length; i++) {
                bounds.extend(new google.maps.LatLng(parseFloat(addressesArray[i].location.latitude), parseFloat(addressesArray[i].location.longitude)));
            }

            this.map.fitBounds(bounds);
        },

        /**
         * Init map clusters for grouping markers on small zoom values
         */
        initMapClusters: function () {

            //Activate clustering on multiple markers
            var markerClusteringOptions = {
                minimumClusterSize: 2,
                maxZoom: 12,
                styles: [{
                    width: 50,
                    height: 60,
                    url: '',
                    textSize: 12
                }]
            };

            var markerClusterer = new MarkerClusterer(this.map, this.markers, markerClusteringOptions);
        },

        initMarkerInfo: function () {
            var map = this.map;

            $(document).off('click', '.qodef-map-marker').on('click', '.qodef-map-marker', function () {
                var self = $(this),
                    $markerHolders = $('.qodef-map-marker-holder'),
                    $infoWindows = $('.qodef-info-window'),
                    $markerHolder = self.parent('.qodef-map-marker-holder'),
                    markerlatlngData = $markerHolder.data('latlng'),
                    $infoWindow = self.siblings('.qodef-info-window');

                if ($markerHolder.hasClass('qodef-active qodef-map-active')) {
                    $markerHolder.removeClass('qodef-active qodef-map-active');
                    $infoWindow.fadeOut(0);
                } else {
                    $markerHolders.removeClass('qodef-active qodef-map-active');
                    $infoWindows.fadeOut(0);
                    $markerHolder.addClass('qodef-active qodef-map-active');
                    $infoWindow.fadeIn(300);

                    if (markerlatlngData.length && markerlatlngData !== undefined) {
                        var latlngStr = markerlatlngData.replace('(', '').replace(')', '').split(',', 2);
                        var lat = parseFloat(latlngStr[0]);
                        var lng = parseFloat(latlngStr[1]);

                        map.panTo(new google.maps.LatLng(lat, lng));
                    }
                }
            });
        },

        /**
         * If geolocation enabled center map on users current position
         */
        centerOnCurrentLocation: function (setInputAddressValue, placesInput, geoLocationLinkIcon, listHolder) {
            var map = this.map;

            // Try HTML5 geolocation.
            if (navigator.geolocation) {
                if (setInputAddressValue) {
                    geoLocationLinkIcon.addClass('fa-spinner fa-spin');
                }

                navigator.geolocation.getCurrentPosition(
                    function (position) {
                        var lat = position.coords.latitude,
                            lng = position.coords.longitude,
                            latlng = {
                                lat: lat,
                                lng: lng
                            };

                        if (setInputAddressValue) {
                            var geocoder = new google.maps.Geocoder(),
                                cityName = '',
                                cityWithCountryName = '';

                            geocoder.geocode({'latLng': new google.maps.LatLng(lat, lng)}, function (results, status) {
                                if (status === google.maps.GeocoderStatus.OK && typeof results === 'object') {
                                    var resultsObject = results;

                                    for (var $i = 0; $i <= resultsObject.length; $i++) {
                                        var result = resultsObject[$i];

                                        if (typeof result === 'object' && result.types[0] === 'locality') {
                                            var currentAddress = result.address_components;

                                            cityName = currentAddress[0].long_name;

                                            for (var $j = 0; $j <= currentAddress.length; $j++) {
                                                if (typeof currentAddress[$j] === 'object' && currentAddress[$j].types[0] === 'country') {
                                                    cityWithCountryName = cityName + ',' + currentAddress[$j].long_name;
                                                }
                                            }
                                        }
                                    }

                                    if (typeof cityName === 'string') {
                                        geoLocationLinkIcon.removeClass('fa-spinner fa-spin');

                                        if (typeof cityWithCountryName === 'string') {
                                            placesInput.val(cityWithCountryName);
                                        } else {
                                            placesInput.val(cityName);
                                        }

                                        // ReInit listing list and map
                                        if (listHolder) {
                                            var locationObject = [];

                                            locationObject.push(cityName);
                                            locationObject.push(latlng);
                                            locationObject.push(true);

                                            window.qodefInitGeoLocationRangeSlider.showRangeSlider(latlng, true);
                                        }
                                    }
                                }
                            });
                        } else {
                            map.setCenter(latlng);
                        }
                    }
                );
            }
        },

        /**
         * Center map on forward location position
         */
        centerOnForwardLocation: function (forwardLocation, markerEnabled, addressName) {
            var map = this.map;

            if (typeof forwardLocation === 'object') {

                if (markerEnabled) {
                    var customMarker = new CustomMarker({
                        map: map,
                        position: forwardLocation,
                        templateData: {
                            title: 'Your location is here',
                            itemId: 'qodef-geo-location-marker',
                            address: addressName,
                            featuredImage: '',
                            itemUrl: ''
                        }
                    });

                    this.initMarkerInfo();
                }

                map.setZoom(12);
                map.setCenter(forwardLocation);
            }
        },

        /**
         * Center map on forward address name location
         */
        centerOnForwardAddressLocation: function (addressName) {

            if (typeof addressName === 'string' && typeof google === 'object') {
                var geocoder = new google.maps.Geocoder();

                geocoder.geocode({'address': addressName}, function (results, status) {
                    if (status === google.maps.GeocoderStatus.OK && typeof results[0] === 'object') {
                        this.centerOnForwardLocation(results[0].geometry.location);
                    }
                });
            }
        },

        /**
         * Set radius for current geo location location
         */
        setGeoLocationRadius: function (forwardLocation, radius, isActive) {
            var map = this.map,
                circle = this.circle,
                markers = this.markers;

            if (typeof forwardLocation === 'object' && typeof google === 'object') {

                if (isActive) {
                    circle.setMap(null);
                }

                this.circle = new google.maps.Circle({
                    map: map,
                    center: forwardLocation,
                    radius: parseInt(radius, 10) * 1000, // 1000 change meters to kilometers
                    strokeWeight: 0,
                    fillColor: '#fc475f',
                    fillOpacity: 0.15
                });

                var currentCircle = this.circle;

                var itemsInArea = [];
                $.each(markers, function (i, marker) {
                    if (currentCircle.getBounds().contains(marker.latlng)) {
                        itemsInArea.push(marker.templateData.itemId);
                    }
                });

                window.qodefInitGeoLocationRangeSlider.disableItemsOutOfRange(itemsInArea);
            }
        },

        /**
         * Create autocomplete places for forward input field
         */
        createAutocompletePlaces: function (placeInputID, listHolder) {

            if (typeof google === 'object' && typeof google.maps.places === 'object') {
                var autocompleteConfig = {
                    types: ['(cities)']
                };

                var autocomplete = new google.maps.places.Autocomplete(placeInputID, autocompleteConfig);

                autocomplete.addListener('place_changed', function () {
                    // Enable reset icon in field
                    $(placeInputID).next().show();

                    if (listHolder) {
                        var place = autocomplete.getPlace();

                        if (!place.geometry) {
                            window.alert("No details available for input: '" + place.name + "'");
                            return;
                        }

                        var locationObject = [];

                        locationObject.push(place.address_components[0].long_name);
                        locationObject.push(place.geometry.location);
                        locationObject.push(false);

                        // ReInit listing list and map
                        window.qodefInitGeoLocationRangeSlider.reset();
                    }
                });
            }
        }
    }

    window.qodefGoogleMultipleMap = qodefGoogleMultipleMap;

    var qodefInitMultipleListingMap = {
        init: function () {
            var $mapHolder = $('#qodef-multiple-map-holder');

            if ($mapHolder.length) {
                var addresses = $mapHolder.data('addresses');

                qodefMapsVariables.multiple = addresses;

                window.qodefGoogleMultipleMap.getDirectoryItemsAddresses({
                    mapHolder: 'qodef-multiple-map-holder',
                    hasFilter: true
                });
            }
        }
    }

    window.qodefInitMultipleListingMap = qodefInitMultipleListingMap;

    var qodefInitMobileMap = {
        init: function () {
            var $mapOpener = $('.qodef-view-larger-map a'),
                $mapOpenerIcon = $mapOpener.children('i'),
                $mapHolder = $('.qodef-map-holder');

            if ($mapOpener.length) {
                $mapOpener.on('click', function (e) {
                    e.preventDefault();

                    if ($mapHolder.hasClass('qodef-fullscreen-map')) {
                        $mapHolder.removeClass('qodef-fullscreen-map');
                        $mapOpenerIcon.removeClass('icon-basic-magnifier-minus');
                        $mapOpenerIcon.addClass('icon-basic-magnifier-plus');
                    } else {
                        $mapHolder.addClass('qodef-fullscreen-map');
                        $mapOpenerIcon.removeClass('icon-basic-magnifier-plus');
                        $mapOpenerIcon.addClass('icon-basic-magnifier-minus');
                    }

                    window.qodefGoogleMultipleMap.getDirectoryItemsAddresses();
                });
            }
        }
    }

    function qodefBindListTitlesAndMap() {
        var $itemsList = $('.qodef-map-list-holder');

        if ($itemsList.length) {
            $itemsList.each(function () {
                var $listItems = $(this).find('article'),
                    $map = $(this).find('.qodef-map-list-map-part');

                if ($map.length) {
                    $listItems.each(function () {
                        //Init hover
                        var $listItem = $(this);

                        if (!$listItem.hasClass('qodef-init')) {
                            $listItem.mouseenter(function () {
                                var itemId = $listItem.data('id'),
                                    $inactiveMarkersHolder = $('.qodef-map-marker-holder:not(.qodef-map-active)'),
                                    $clusterMarkersHolder = $('.qodef-cluster-marker');

                                if ($inactiveMarkersHolder.length) {
                                    $inactiveMarkersHolder.removeClass('qodef-active');
                                    $('#' + itemId + '.qodef-map-marker-holder').addClass('qodef-active');
                                }

                                if ($clusterMarkersHolder.length) {
                                    $clusterMarkersHolder.each(function () {
                                        var $thisClusterMarker = $(this),
                                            clusterMarkersItemIds = $thisClusterMarker.data('item-ids');

                                        if (clusterMarkersItemIds !== undefined && clusterMarkersItemIds.includes(itemId.toString())) {
                                            $thisClusterMarker.addClass('qodef-active');
                                        }
                                    });
                                }
                            }).mouseleave(function () {
                                var $markersHolder = $('.qodef-map-marker-holder'),
                                    $clusterMarkersHolder = $('.qodef-cluster-marker');

                                if ($markersHolder.length) {
                                    $markersHolder.each(function () {
                                        var $thisMapHolder = $(this);

                                        if (!$thisMapHolder.hasClass('qodef-map-active')) {
                                            $thisMapHolder.removeClass('qodef-active');
                                        }
                                    });
                                }

                                if ($clusterMarkersHolder.length) {
                                    $clusterMarkersHolder.removeClass('qodef-active');
                                }
                            });

                            $listItem.addClass('qodef-init');
                        }
                    });
                }
            });
        }
    }

    function qodefReinitMultipleGoogleMaps(addresses, action) {
        if (action === 'append') {
            var mapObjs = qodefMapsVariables.multiple.addresses.concat(addresses);
            qodefMapsVariables.multiple.addresses = mapObjs;

            window.qodefGoogleMultipleMap.getDirectoryItemsAddresses({
                addresses: mapObjs
            });
        } else if (action === 'replace') {
            qodefMapsVariables.multiple.addresses = addresses;
            window.qodefGoogleMultipleMap.getDirectoryItemsAddresses({
                addresses: addresses
            });
        }

        qodefBindListTitlesAndMap();
    }

    window.qodefReinitMultipleGoogleMaps = qodefReinitMultipleGoogleMaps;

})(jQuery);