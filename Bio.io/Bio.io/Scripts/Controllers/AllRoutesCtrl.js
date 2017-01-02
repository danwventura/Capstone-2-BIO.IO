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
        //$scope.createMapOptions(routes);
        $scope.createMapOptionsArray(routes);
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
    
    $scope.createMapOptionsArray = function (routes) {
        console.log("createMapsAllRoutes", routes);
        console.log("createMapsRoutesLength", routes.length);
        var mapOps = $scope.mapOptions;
        for (var i = 0; i < routes.length; i++) {

            mapOps[i] = {
                      zoom: 19,
                      center: new google.maps.LatLng(routes[i].Coordinates[0].Lat, routes[0].Coordinates[0].Long),
                      //center: new google.maps.LatLng(36.172057, -86.746411),
                      mapTypeId: google.maps.MapTypeId.Roadmap
                     };
           

        }

        $scope.createMaps(mapOps);
    }

    $scope.createMaps = function (mapOps) {
        
        for (var k = 0; k < mapOps.length; k++) {
            mapOps[k].mapTypeId = 'roadmap';
            console.log("typedid", mapOps[k].mapTypeId);
            $scope.map = new google.maps.Map(document.getElementById('map'), mapOps[k])
        }
        

    }

});