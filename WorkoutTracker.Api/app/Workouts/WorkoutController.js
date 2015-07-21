(function() {
    'use strict';
    app.controller('WorkoutController', ['$scope','$location', 'workoutService', '$filter', function($scope, $location, workoutService, $filter) {
        $scope.workouts = [];
        $scope.totalWorkouts = 0;
        $scope.filteredWorkouts = [];
        $scope.totalFilteredWorkouts = 0;
        $scope.typeOfWorkout = ['All', 'AMRAP','EMOM', 'AFAP', 'PowerLifting'];
        $scope.selectedWOD = $scope.typeOfWorkout[0];
        $scope.filterValue = '';

        init();

        function init() {
            getWorkouts();
        };

        $scope.newWorkout = function () {
            $location.path('/workouts/edit/');
        }

        $scope.edit = function (id) {
            $location.path('/workouts/edit/' + id);
        }

        $scope.delete = function () {
            
        }

        $scope.filter = function() {
            filterWorkouts();
        };

        function getWorkouts() {
            workoutService.getAll().success(function (workouts) {
                $scope.workouts = workouts;
                $scope.totalWorkouts = $scope.workouts.length;
                filterWorkouts('', 'All');
            })
            .error(function (error) {
                $scope.status = 'Unable to load workouts: ' + error.message;
            });
        }

        function filterWorkouts() {
            $scope.filteredWorkouts = $filter('workoutsFilter')($scope.workouts, $scope.filterValue, $scope.selectedWOD);
            $scope.totalFilteredWorkouts = $scope.filteredWorkouts.length;
        };

        
    }]);
})();