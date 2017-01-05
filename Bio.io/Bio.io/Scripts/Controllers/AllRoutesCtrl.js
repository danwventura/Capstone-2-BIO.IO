app.controller("AllRoutesCtrl", function ($scope, $http) {

    $scope.routes = [];
    //$scope.googleMap = new google.maps.Map(document.getElementById('map_container'), $scope.mapOptions);
    $scope.mapOptions = [];


    $http({
           method:'GET',
           url: 'http://localhost:51089/Routes/GetUserRoutes',
    }).then(function (response) {
        $scope.routes = response.data;
        console.log("ROUTES!", $scope.routes[0].Coordinates[0].Lat);
        console.log("ROUTES!", $scope.routes[0].Coordinates[0].Long);

        var routes = $scope.routes;
        var routesLength = routes.length;
        //$scope.createMapOptions(routes);
        $scope.createMapDivs(routesLength, routes);
        
       })

    //$scope.createMapOptions = function (routes) {

    //    console.log("# of routes", routes.length);
    //    var mapOptions = $scope.mapOptions;
    //    var mapOp;
    //    for (var i = 0; i < routes.length; i++) {

    //        mapOp = {
    //            zoom: 19,
    //            center: new google.maps.LatLng(routes[i].Coordinates[0].Lat, routes[0].Coordinates[0].Long), 
    //            mapTypeId: google.maps.MapTypeId.Roadmap
    //        };
    //        mapOptions.push(mapOp);
    //    }
        
    //    console.log("MAPS", mapOptions);
    //    console.log("MAPS", $scope.allMaps);
    //}
    
    $scope.createMapDivs = function (routesLength, routes) {
        console.log("length", routesLength);

        for (var j = 0; j < routesLength; j++) {
            jQuery('<div/>', {
                id: 'map' + j,
                "class": "mini_map",
            }).appendTo('#map_container');
        }

        $scope.createMapOptionsArray($scope.routes);
    }

    $scope.createMapOptionsArray = function (routes) {
        console.log("createMapsAllRoutes", routes);
        console.log("createMapsRoutesLength", routes.length);

        var routes = $scope.routes
        var mapOps = $scope.mapOptions;

        for (var i = 0; i < routes.length; i++) {

            console.log("routesI", routes[i]);
            console.log("lat", routes[i].Coordinates[0].Lat)
            console.log("long", routes[i].Coordinates[0].Long);

            mapOps[i] = {
                      zoom: 19,
                      center: new google.maps.LatLng(routes[i].Coordinates[0].Lat, routes[i].Coordinates[0].Long),
                      //center: new google.maps.LatLng(36.172057, -86.746411),
                      mapTypeId: google.maps.MapTypeId.Roadmap
                     };
        }
        $scope.createMaps(mapOps,routes);
    }


    
    $scope.createMaps = function (mapOps,routes) {
        
        for (var k = 0; k < mapOps.length; k++) {
            mapOps[k].mapTypeId = 'roadmap';
            console.log("typedid", mapOps[k].mapTypeId);
            var key = 'map' + k;
            $scope[key] = new google.maps.Map(document.getElementById('map' + k), mapOps[k])

            console.log("mapK",key);
        }
        
        $scope.createCoordsArrayLengths(routes);
    }



    $scope.createCoordsArrayLengths = function (routes) {
        console.log(routes);
        console.log(routes[0].Coordinates[0].Lat);

        var coord_length_array = [];
        //var markers = [];
        for (var i = 0; i < routes.length; i++) {
            coord_length_array[i] = routes[i].Coordinates.length;
        }
        $scope.createMapMarkerArrays(coord_length_array, routes);
    }



    $scope.createMapMarkerArrays = function(coord_length_array, routes){
        for (var i = 0; i < routes.length; i++) {
            var key = "markers" + i;
            $scope[key]= [];
        }
        $scope.createMapMarkers(coord_length_array, routes);
    }




    $scope.createMapMarkers = function (coord_length_array, routes) {
        console.log(coord_length_array);
        var markers = [];


        for (var i = 0; i < routes.length; i++) {
            console.log(coord_length_array[i]);
            for (var k = 0; k < coord_length_array[i]; k++) {
                var key = 'map' + i;
                var key2 = 'markers' + i;
                $scope[key2][k] = new google.maps.Marker({
                    map: $scope[key],
                    position: { lat: routes[i].Coordinates[k].Lat, lng: routes[i].Coordinates[k].Long }
                })
            }
        }
        $scope.createPathPositionsArrays(coord_length_array);
    }



    $scope.createPathPositionsArrays = function (coord_length_array) {

        var routes = $scope.routes;

        for (var i = 0; i < routes.length; i++) {
            var key3 = "pathPositions" + i;
            $scope[key3] = [];
        }

        $scope.fillPathPositionsArrays(coord_length_array);
    }



    $scope.fillPathPositionsArrays = function (coord_length_array) {

        var routes_length = $scope.routes.length
        console.log(coord_length_array[0]);
        console.log(coord_length_array[1]);

        for (var i = 0; i < routes_length; i++) {
            var key = "map" + i;
            var key2 = 'markers' + i;
            var key3 = 'pathPositions' + i;
            for (var k = 0; k < coord_length_array[i]; k++) {


                $scope[key3][k] = $scope[key2][k].position;
            }

        }

        $scope.makeAllPaths(coord_length_array);
    }


    $scope.makeAllPaths = function (coord_length_array) {

        var routes = $scope.routes;


        for (var i = 0; i < routes.length; i++) {

            var key = "map" + i;
            var key2 = "pathPositions" + i;
            var key3 = "path" + i;
            
            console.log("asldk;jfad;sflkj", $scope[key2]);
            for (var k = 0; k < coord_length_array[i]; k++) {


                console.log(key3);
                  var key3 = new google.maps.Polyline({
                    path: $scope[key2],
                    geodesic: true,
                    strokeColor: '4286f4',
                    strokeOpacity: 1.0,
                    strokeWeight: 2
                })
                    
            
            }
            key3.setMap($scope[key]);
        } 
    }
          
});