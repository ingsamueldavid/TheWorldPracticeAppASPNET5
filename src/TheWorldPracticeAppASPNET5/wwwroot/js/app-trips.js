//app-trips.js

(function () {
    "use strict"

    //creating the module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when("/", {
            controller: "tripsController",
            controllerAs: "vm",
            templateUrl:"/views/tripsView.html"


        })

        $routeProvider.when("/editor/:tripName", {
            controller: "tripsEditorController",
            controllerAs: "vm",
            templateUrl: "/views/tripsEditorView.html"


        })

        $routeProvider.otherwise({
                redirectTo:"/"

        })

    })

}
)();