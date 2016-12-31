app.controller("UserDetailsCtrl", function ($scope, $http) {

    $scope.message = "ACCOUNT DETAILS HERE"

    $scope.user;
   

    $scope.getThisUser = function () {
        console.log("getThisUser");
        $http({
               method: 'GET',
               url: 'http://localhost:51089/User/GetCurrentUser'
             }).then(function(response) {
 
                 $scope.user = response.data;
                 var Timestamp = new Date(response.data.BaseUser.Registered)
                 $scope.user.createdMonth = Timestamp.getMonth();
                 $scope.user.createdDay = Timestamp.getDate();
                 $scope.user.createdYear = Timestamp.getFullYear();
                })
    }

    

    $scope.getThisUser();
})