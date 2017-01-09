app.controller("AllRoutesCtrl", function ($scope, $http) {


    //Setting Up Scoped Arrays 
    $scope.routes = [];
    $scope.mapOptions = [];

    //HTTP GET Call that gets all user's routes/datapoints
    $http({
           method:'GET',
           url: 'http://localhost:51089/Routes/GetUserRoutes',
    }).then(function (response) {
        $scope.routes = response.data;
        var routes = $scope.routes;
        console.log(routes);
        var routesLength = routes.length;
        $scope.createMapDivs(routesLength, routes);
        
       })

   

    
    $scope.createMapDivs = function (routesLength, routes) {
        for (var j = 0; j < routesLength; j++) {
            jQuery('<div/>', {
                id: 'map' + j,
                "class": "mini_map",
            }).appendTo('#map_container');
        }
        $scope.createMapOptionsArray($scope.routes);
    }




    $scope.createMapOptionsArray = function (routes) {
        var routes = $scope.routes
        var mapOps = $scope.mapOptions;
        for (var i = 0; i < routes.length; i++) {
            mapOps[i] = {
                      zoom: 19,
                      center: new google.maps.LatLng(routes[i].Coordinates[0].Lat, routes[i].Coordinates[0].Long),
                      mapTypeId: google.maps.MapTypeId.Roadmap
                     };
        }
        $scope.createMaps(mapOps,routes);
    }



    
    $scope.createMaps = function (mapOps,routes) {
        for (var k = 0; k < mapOps.length; k++) {
            mapOps[k].mapTypeId = 'roadmap';
            var key = 'map' + k;
            $scope[key] = new google.maps.Map(document.getElementById('map' + k), mapOps[k])
        }

        
        $scope.createCoordsArrayLengths(routes);
    }




    $scope.createCoordsArrayLengths = function (routes) {
        var coord_length_array = [];
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
        var markers = [];
        for (var i = 0; i < routes.length; i++) {
            for (var k = 0; k < coord_length_array[i]; k++) {
                var key = 'map' + i;
                var key2 = 'markers' + i;
                $scope[key2][k] = new google.maps.Marker({
                    map: $scope[key],
                    position: { lat: routes[i].Coordinates[k].Lat, lng: routes[i].Coordinates[k].Long },
                    visible: false
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
            for (var k = 0; k < coord_length_array[i]; k++) {
                  var key3 = new google.maps.Polyline({
                    path: $scope[key2],
                    geodesic: true,
                    strokeColor: '#4286f4',
                    strokeOpacity: 1.0,
                    strokeWeight: 2
                })
            }
            key3.setMap($scope[key]);
        } 
    }
          
});