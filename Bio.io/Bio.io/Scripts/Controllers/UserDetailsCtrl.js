app.controller("UserDetailsCtrl", function ($scope, $http) {

    $scope.message = "ACCOUNT DETAILS HERE"

    

   

    $scope.getThisUsersId = function(){

        $http({
               method: 'GET',
               url: "GetCurrentUserId"
             }).then(function (response) {
            
                 console.log("response", response.data);
                 var this_users_id = response.data;
                 $scope.createScopedUserData(this_users_id);
                })
    }

    $scope.createScopedUserData = function (this_users_id) {


        
        

    }

    //$scope.getCurrUser = function () {
    //    $scope.currUser;

    //    $http({
    //        method: 'GET',
    //        url: "User/GetCurrentUser()"
    //    }).then(function () {

    //    })

    //}
})