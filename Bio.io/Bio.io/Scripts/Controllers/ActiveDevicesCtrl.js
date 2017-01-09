app.controller("ActiveDevicesCtrl", function ($scope, $http, $window,$timeout) {

    $scope.message = "Enter Your ThingSpeak Channel ID Here";

    
    $scope.recentChannels = [];
    $scope.continueLog = false;
    $scope.currChannelId;
    $scope.coordinate_arrays = [];
    $scope.googleLats = [];
    $scope.googleLongs = [];
    $scope.datapoint_array = [];
    $scope.check_datapoint_array = [];

    var continueLog = false;
    var counter = 0;

        console.log("getThisUser");
        $http({
            method: 'GET',
            url: 'http://localhost:51089/User/GetCurrentUser'
        }).then(function (response) {
            if (response.data == "") {
                console.log("change path");
                $window.location.assign('Account/Register');
            }

            else if (response.data != ""){

                $scope.user = response.data;
                console.log("USER USER", response.data);
            }
        })

        if ($scope.user = null) {
            $('#slide-out').addClass("hidden");
        }

    $scope.stopLog = function() {
        
        continueLog = false;
        console.log("Stopped logging");
    }

    $scope.startLog = function () {
        continueLog = true; //Might need to move this out and have if statement
        console.log("startLog");
        //var channelId = $scope.currChannelId

        //while(continueLog = true){

        //look for $timeout in angular docs
        //setTimeout(function () {
        $http({
            method: 'GET',
            url: "https://api.thingspeak.com/channels/"+ $scope.currChannelId +"/feed.json"
        }).then(function (response) {
            
            var responseFeed = response.data.feeds;
            console.log("feed",responseFeed);

            $scope.createMap(responseFeed);
            $scope.createCoordsArray(responseFeed);
            
            if (counter < 1) {
                
                console.log("first");
                $scope.createDatapointArray(responseFeed);
                counter++;
            }
            else {
                //var findNewestDatapoint = function (respFeed) {
                console.log("second");
                    return responseFeed.reduce(function (currentItem, mostRecent) {
                        
                        var current = new Date(currentItem.created_at);
                        var newest = new Date(mostRecent.created_at);
                        if (current > newest) {
                            return currentItem;
                        } else {
                            return mostRecent;
                        }
                    }, { createdDate: 'Jan 01, 1970' });

                    console.log(mostRecent);
                    $scope.createDatapointArray(mostRecent);
                //}
                //findNewestDatapoint();
            }

            $timeout(function(){

                if (!!continueLog) {
                    $scope.startLog();
                }
            },60000)
                //console.log($scope.currChannelId);
                

                
                //$scope.createMap();
                //$scope.createCoordsArray(response.data.feeds);
                //$scope.createDatapointArray(response.data.feeds);
                //$scope.getMostRecentRoute(response.data.feeds);
                //$scope.startlog  (call start log again to limit calls)
                //wait for response or else will have multiple calls waiting for responses --> crash
                //might need two different functions (one for start/one for continue)
            
            //$scope.startLog();
        })
                
        //}, 20000)

            
        //}
    }



    $scope.createMap = function () {
        $scope.mapOptions = {
            zoom: 19,
            center: new google.maps.LatLng(36.172057, -86.746411), //Can I make a constant here?
            mapTypeId: google.maps.MapTypeId.Roadmap
        };
        $scope.map = new google.maps.Map(document.getElementById('map'), $scope.mapOptions)
        
    }




    $scope.createCoordsArray = function (responseFeed) {
        $scope.responseCoords = [];

        for (var i = 0; i < responseFeed.length; i++) {
            $scope.responseCoords.push(responseFeed[i].field1);
        }
        $scope.makeCleanCoordinateArrays();
    }




    $scope.makeCleanCoordinateArrays = function () {
        var string_coords = $scope.responseCoords;
        for (var i = 0; i < string_coords.length; i++) {
            $scope.coordinate_arrays[i] = string_coords[i].split(",");
        }
        $scope.parseCoordinateArrays();
    }




    $scope.parseCoordinateArrays = function () {
        var coordinate_arrays = $scope.coordinate_arrays;
        for (var i = 0; i < coordinate_arrays.length; i++) {
            $scope.googleLats[i] = parseFloat(coordinate_arrays[i][0]);
            $scope.googleLongs[i] = parseFloat(coordinate_arrays[i][1])
        }
        console.log("scopelats", $scope.googleLats);
        $scope.makeMarkers();
    }




    $scope.makeMarkers = function () {
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

    //Call this after Ive saved the route the first time, otherwise it will grab the previous channels route
    //$scope.getMostRecentRoute = function (responseFeed) {

    //    $http({
    //        method: 'GET',
    //        url:'http://localhost:51089/Routes/GetUserRoutes',
    //    }).then(function(response){
            
    //        var response_data = response.data;
    //        var last_set = response_data.pop();
           

            
    //        $scope.createDatapointsToCheck(responseFeed,last_set);

    //    })
    //}

    //$scope.createDatapointsToCheck = function (responseFeed,last_set) {

    //    console.log("RESPONSE", responseFeed);

    //    var datapoint = $scope.datapoint;
    //    var datapoint_array_to_check = $scope.check_datapoint_array;
    //    var currChannelId = $scope.currChannelId
    //    var lats = $scope.googleLats;
    //    var longs = $scope.googleLongs;

    //    for (var i = 0; i < responseFeed.length; i++) {
    //                datapoint_array_to_check[i] = JSON.stringify(new datapoint(currChannelId, responseFeed[i].created_at, lats[i], longs[i]));
    //    }
        


    //    $scope.checkDatapoints(datapoint_array_to_check, last_set);
    //}

    //$scope.checkDatapoints = function (datapoint_array_to_check, last_set) {
        
    //    console.log("POINTS", datapoint_array_to_check);
    //    console.log("LAST", last_set);
        

    //    datapoint_array_to_check.forEach(function (point) {

    //        last_set.forEach(function (new_point) {

    //            if (new_point.Created == point.Created) {

    //                console.log("This is old", new_point);

    //            }

    //            else {

    //                //$http post call to database to post updated route
    //            }

    //        })
    //    })

        //for (var i = 0; i < datapoint_array_to_check.length; i++) {
           
        //    //if (last_set[i].includes()) {

        //    //}


        //    for (var k = 0; k < datapoint_array_to_check.length; k++) {

        //        if (datapoint_array_to_check[k].Created != last_set[i].Created) {
        //            console.log("DATA", datapoint_array_to_check[k]);
        //        }

        //    }

        //}


    //}

    //if else statement to call either createDataPointArray/createNewRoute or updateRoute

    $scope.createDatapointArray = function (responseFeed, mostRecent) {
                
        var datapoint = $scope.datapoint;
        var datapoint_array = $scope.datapoint_array;
        var currChannelId = $scope.currChannelId
        var lats = $scope.googleLats;
        var longs = $scope.googleLongs;

        for (var i = 0; i < responseFeed.length; i++) {
            datapoint_array[i] = JSON.stringify(new datapoint(currChannelId, responseFeed[i].created_at, lats[i], longs[i]));
        }
        $scope.createNewRoute(datapoint_array);
    }


    $scope.createNewRoute = function (datapoint_array) {

            $http({
                method:'POST',
                url: 'http://localhost:51089/Active/CreateNewRoute',
                data: datapoint_array
            })
    }


})