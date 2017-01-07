app.controller("AccountCtrl", function ($scope, $http,$window) {

    $http({
        method: 'GET',
        url: 'http://localhost:51089/User/GetCurrentUser'
    }).then(function (response) {
        if (response.data != "") {
            console.log("change path");
            $window.location.assign('Active/Index');
        }

        
    })
})