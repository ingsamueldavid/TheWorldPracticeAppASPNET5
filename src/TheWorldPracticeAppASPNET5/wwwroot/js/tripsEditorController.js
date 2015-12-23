(function () {
    "use strict";

    function tripsEditorController($routeParams,$http) {
        var vm = this;

        vm.tripName = $routeParams.tripName
        var apiurl = "/api/trips/" + vm.tripName + "/stops";
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};
        $http.get(apiurl).then(function (response) {
            //success
            vm.stops = response.data
            _showMap(vm.stops);
        }, function (error) {

            //fail
            vm.errorMessage = "Failed to Load Stops"
        }).finally(function () {
            vm.isBusy = false;
        });

        vm.addStop = function () {

            vm.isBusy = true;
            $http.post(apiurl, vm.newStop).then(function (response) {
                //success
                vm.stops.push(response.data);
                _showMap(vm.stops);
                vm.newStop = {};
            }, function (error) {

                //fail
                vm.errorMessage = "Failed to Add new Stop"
            }).finally(function () {
                vm.isBusy = false;
            });

        }





    }
    //helper functions
    function _showMap(stops) {
        if (stops && stops.length > 0) {
            //show
            var mapStops = _.map(stops,function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info:item.name
                }
            })
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom:3

            })

        }

    }

    angular.module("app-trips")
        .controller("tripsEditorController", tripsEditorController)


})();