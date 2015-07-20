(function() {
    'use strict';
    app.controller('WorkoutController', ['$scope', 'workoutService', '$filter', function($scope, workoutService, $filter) {
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

        $scope.filter = function() {
            filterWorkouts();
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

        function filterWorkouts() {
            $scope.filteredWorkouts = $filter('workoutsFilter')($scope.workouts, $scope.filterValue, $scope.selectedWOD);
            $scope.totalFilteredWorkouts = $scope.filteredWorkouts.length;
        };

        
    }]);
})();