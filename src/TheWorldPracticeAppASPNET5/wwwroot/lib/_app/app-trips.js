!function(){"use strict";angular.module("app-trips",["simpleControls","ngRoute"]).config(["$routeProvider",function(r){r.when("/",{controller:"tripsController",controllerAs:"vm",templateUrl:"/views/tripsView.html"}),r.when("/editor/:tripName",{controller:"tripsEditorController",controllerAs:"vm",templateUrl:"/views/tripsEditorView.html"}),r.otherwise({redirectTo:"/"})}])}();