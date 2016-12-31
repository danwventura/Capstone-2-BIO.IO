app.controller("UserDetailsCtrl", function ($scope, $http) {

    $scope.message = "ACCOUNT DETAILS HERE"

    $scope.user;
   

    $scope.getThisUser = function () {
        console.log("getThisUser");
        $http({
               method: 'GET',
               url: 'http://localhost:51089/User/GetCurrentUser'
             }).then(function (response) {
            
                 console.log("response", response.data);
                 
                 $scope.user = response.data;
                 //$scope.createScopedUserData(this_user);
                })
    }

    //$scope.createScopedUserData = function (this_users_id) {

    //    console.log("ID", this_users_id);

    //    $http({
    //        method: 'GET',
    //        url: "GetUserByID",
            
    //    }).then(function (response) {
    //        console.log(response);
    //    })

        //$http.get("GetUserByID", { params: { this_users_id } })
        //    .then(function (response) {
        //        console.log(response);
        //     });
        
        

    //}

    //$scope.getCurrUser = function () {
    //    $scope.currUser;

    //    $http({
    //        method: 'GET',
    //        url: "User/GetCurrentUser()"
    //    }).then(function () {

    //    })

    //}
})