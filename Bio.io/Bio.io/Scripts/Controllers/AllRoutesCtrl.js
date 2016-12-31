app.controller("AllRoutesCtrl", function ($scope, $http) {

    var get_response;
    $scope.coordinates = [];
    $scope.coordinate_arrays = [];
    $scope.googleLats = [];
    $scope.googleLongs = [];
    
    
    $http({
        method: 'GET',
        url: "https://api.thingspeak.com/channels/203706/feed.json"
    }).then(function (response) {
        get_response = response.data.feeds;

        for (var i = 0; i < get_response.length; i++) {
            $scope.coordinates.push(get_response[i].field1);
        }
    });



    $scope.mapOptions = {
        zoom: 19,
        center: new google.maps.LatLng(36.172055, -86.746393), //Can I make a constant here?
        mapTypeId: google.maps.MapTypeId.Roadmap
    };




    $scope.map = new google.maps.Map(document.getElementById('map'), $scope.mapOptions)

    $scope.homeMarker = new google.maps.Marker({
        map: $scope.map,
        position: { lat: 36.172055, lng: -86.746393 },
        label: "Home",
        icon: "http://maps.google.com/mapfiles/ms/icons/green-dot.png"
    })




    $scope.vanMarker = new google.maps.Marker({
        map: $scope.map,
        position: { lat: 36.171975, lng: -86.746708},
        label: "DanVan",
        icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
    })
    


    $scope.makeCoordinateArrays = function () {
        var string_coords = $scope.coordinates;
        var coordinates;
        
        for (var i = 0; i < string_coords.length; i++) {
            $scope.coordinate_arrays[i] = string_coords[i].split(",");
            $scope.parseCoordinateArrays();
        }

       

    }

    $scope.parseCoordinateArrays = function () {
        var coordinate_arrays = $scope.coordinate_arrays;
        for (var i = 0; i < coordinate_arrays.length; i++) {
            $scope.googleLats[i] = parseFloat(coordinate_arrays[i][0]);
            $scope.googleLongs[i] = parseFloat(coordinate_arrays[i][1])
        }
        $scope.makeMarkers();
    }




    $scope.makeMarkers = function () {
        var lats = $scope.googleLats;
        var longs = $scope.googleLongs;
        var markers = [];

        for (var i = 0; i < lats.length; i++) {
            markers[i] = new google.maps.Marker({
                map: $scope.map,
                position: { lat: lats[i], lng: longs[i] }
            })
        }
        $scope.makePaths(markers);
    };



    $scope.makePath = function(markers){
        var pathCoords = [];

        for (var i = 0; i < markers.length; i++) {
            pathCoords.push(markers[i].position)
        }

        var path = new google.maps.Polyline({
            path: pathCoords,
            geodesic: true,
            strokeColor: '#FF0000',
            strokeOpacity: 1.0,
            strokeWeight: 2
        })
        path.setMap($scope.map);
    }

   

    
    


    
    


});