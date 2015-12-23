//tripsController.js
(function () {
	"use strict"

	var tripsController = function ($http) {
		var vm = this;

		vm.trips = [{
			name: "US trip",
			created:new Date()
		}, {
			name: "World trip",
			created: new Date()
		}
		];
		vm.errorMessage = "";
		vm.isBusy = true;
		$http.get("/api/trips").then(function (response) {
			//success
			vm.trips = response.data;
		}, function (error) {
			//fail
			console.log(error)
			vm.errorMessage = "";
			vm.errorMessage = "Failed to load data : " + error.statusText;
		}).finally(function () {
			vm.isBusy = false;


		})
		vm.newTrip = {};
		vm.addTrip = function () {
			
			$http.post("/api/trips", vm.newTrip).then(function (response) {
				//success

				vm.trips.push(response.data);
				vm.errorMessage = "";
				vm.newTrip = {};
				//vm.trips = response.data;
			}, function (error) {
				//fail
				console.log(error)
				vm.errorMessage = "Failed to post data : " + error.statusText;
			}).finally(function () {
				vm.isBusy = false;
			})


			//vm.trips.push(
			//	{
			//	name: vm.newTrip.name, created: new Date()
			//	})
			//vm.newTrip = {};

		}
	}
	//getting the existing module/reference
	angular.module("app-trips")
	.controller("tripsController", tripsController);
	

}

)();