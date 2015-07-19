(function() {
    'use strict';
    app.controller('WorkoutController', ['$scope', 'workoutService', '$filter', function($scope, workoutService) {
        $scope.workouts = [];
        $scope.totalWorkouts = 0;
        $scope.filteredWorkouts = [];
        $scope.totalFilteredWorkouts = 0;
        $scope.typeOfWorkout = ['All', 'AMRAP','EMOM', 'Power Lifting'];
        $scope.selectedWOD = $scope.muscularGroup[0];
        $scope.filterValue = '';

        init();

        function init() {
            createWatch();
            getWorkouts();
        };

        function getWorkouts() {
            workoutService.get().success(function (workouts) {
                $scope.workouts = workouts;
                $scope.totalWorkouts = $scope.workouts.length;
                filterWorkouts('', 'All');
            })
            .error(function (error) {
                $scope.status = 'Unable to load workouts: ' + error.message;
            });
        }

        function filterWorkouts(filterValue, muscularGroup) {
            $scope.filteredWorkouts = $filter('workoutsFilter')($scope.workouts, filterValue, muscularGroup);
            $scope.totalFilteredWorkouts = $scope.filteredWorkouts.length;
        };

        function createWatch() {

            $scope.$watch("filterValue", function (filterValue) {
                filterWorkouts(filterValue, $scope.selectedWOD);
            });

            $scope.$watch("selectedWOD", function (selectedWOD) {
                filterWorkouts($scope.filterValue, selectedWOD);
            });
        }
    }]);
})();