app.controller("ActiveDevicesCtrl", function ($scope, $http) {

    $scope.message = "Please Enter The ThingSpeak Channel ID Here";

    
    $scope.recentChannels = [];
    $scope.continueLog = false;
    $scope.currChannelId;
    $scope.responseCoords = [];
    $scope.coordinate_arrays = [];
    $scope.googleLats = [];
    $scope.googleLongs = [];
    $scope.datapoint_array = [];


    $scope.stopLog = function() {
        
        continueLog = false;
        console.log("Stopped logging");
    }

    $scope.startLog = function () {
        continueLog = true;
        console.log("startLog");
        //while(continueLog = true){
        //setTimeout(function () {
        $http({
            method: 'GET',
            url: "https://api.thingspeak.com/channels/"+ $scope.currChannelId +"/feed.json"
        }).then(function (response) {
            console.log(response);
            $scope.createMap();
            $scope.createCoordsArray(response.data.feeds);
            $scope.createDatapointArray(response.data.feeds);
        })
                
        //}, 20000)

            
        //}
    }



    $scope.createMap = function () {
        console.log("createMap");
        $scope.mapOptions = {
            zoom: 19,
            center: new google.maps.LatLng(36.172057, -86.746411), //Can I make a constant here?
            mapTypeId: google.maps.MapTypeId.Roadmap
        };
        $scope.map = new google.maps.Map(document.getElementById('map'), $scope.mapOptions)
        
    }




    $scope.createCoordsArray = function (responseFeed) {
        console.log("createCoordsArray");
        for (var i = 0; i < responseFeed.length; i++) {
            $scope.responseCoords.push(responseFeed[i].field1);
        }
        console.log("responseCoords",$scope.responseCoords);
        $scope.makeCleanCoordinateArrays();
    }




    $scope.makeCleanCoordinateArrays = function () {
        console.log("makeCleanCoords");
        var string_coords = $scope.responseCoords;
        for (var i = 0; i < string_coords.length; i++) {
            $scope.coordinate_arrays[i] = string_coords[i].split(",");
        }
        console.log("scopecoordsarrays",$scope.coordinate_arrays);
        $scope.parseCoordinateArrays();
    }




    $scope.parseCoordinateArrays = function () {
        console.log("parseCoords");
        var coordinate_arrays = $scope.coordinate_arrays;
        for (var i = 0; i < coordinate_arrays.length; i++) {
            $scope.googleLats[i] = parseFloat(coordinate_arrays[i][0]);
            $scope.googleLongs[i] = parseFloat(coordinate_arrays[i][1])
        }
        console.log("scopelats", $scope.googleLats);
        $scope.makeMarkers();
    }




    $scope.makeMarkers = function () {
        console.log("makeMarkers");
        var lats = $scope.googleLats;
        var longs = $scope.googleLongs;
        console.log("lats", lats)
        console.log("longs", longs)
        var markers = [];

        for (var i = 0; i < lats.length; i++) {
            markers[i] = new google.maps.Marker({
                map: $scope.map,
                position: { lat: lats[i], lng: longs[i] }
            })
        }
        console.log(markers)
        $scope.makePath(markers);
    }



    $scope.makePath = function (markers) {
        console.log("makePath");
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

    $scope.datapoint = function (channel_id, created_at, lat_pos, long_pos) {
        this.ChannelId = channel_id;
        this.Created = created_at;
        this.Lat = lat_pos;
        this.Long = long_pos;
    }



    $scope.createDatapointArray = function (responseFeed) {
        console.log("googLats", $scope.googleLats);
        console.log("googLongs", $scope.googleLongs);
        
        var datapoint = $scope.datapoint;
        var datapoint_array = $scope.datapoint_array;
        var currChannelId = $scope.currChannelId
        var lats = $scope.googleLats;
        var longs = $scope.googleLongs;

        for (var i = 0; i < responseFeed.length; i++) {
            datapoint_array[i] = JSON.stringify(new datapoint(currChannelId, responseFeed[i].created_at, lats[i], longs[i]));
        }
        console.log(datapoint_array);
        $scope.addNewDatapoints(datapoint_array);
    }
    



    $scope.addNewDatapoints = function (datapoint_array) {

        for (var i = 0; i < datapoint_array.length; i++) {
            $http.post({ url: "http://localhost:51089/Active/AddDatapoint", data: datapoint_array[i] })
        }

    }






})