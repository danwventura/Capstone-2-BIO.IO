app.controller("AllRoutesCtrl", function ($scope, $http) {

    $scope.userRoutes = [];

    $http({
           method:'GET',
           url: 'http://localhost:51089/Routes/GetUserRoutes',
    }).then(function (response) {
        console.log(response.data);
        var data = response.data;
        //var userRoutes = $scope.userRoutes;
        //for (var i = 0; i < data.length; i++) {
        //userRoutes = response.data
        //var output = new JavaScriptSerializer().Serialize(data);
        //}
    })


});