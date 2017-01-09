app.controller("UserDetailsCtrl", function ($scope, $http) {

    

    $scope.user;
   

    $scope.getThisUser = function () {
        
        $http({
               method: 'GET',
               url: 'http://localhost:51089/User/GetCurrentUser'
             }).then(function(response) {
 
                 $scope.user = response.data;
                 console.log("getThisUser", $scope.user);
                 var Timestamp = new Date(response.data.BaseUser.Registered)
                 $scope.user.createdMonth = Timestamp.getMonth();
                 $scope.user.createdDay = Timestamp.getDate();
                 $scope.user.createdYear = Timestamp.getFullYear();
                })
    }

    

    $scope.getThisUser();
})