(function() {
    'use strict';
    app.controller('ExerciseDetailController', ['$scope', '$routeParams', 'exerciseService', function ($scope, exerciseService, $routeParams) {
        $scope.exerciseId = $routeParams.id;
        $scope.name = '';
        $scope.description = '';

        init();

        function init() {
            if ($scope.exerciseId !== 'undefined') {
                getExerciseDetails();
            }
        };

        function getExerciseDetails() {
            exerciseService.getDetails($scope.exerciseId).success(function (exercise) {
                $scope.name = exercise.Name;
                $scope.description = exercise.Description;
            })
            .error(function (error) {
                $scope.status = 'Unable to load exercise: ' + error.message;
            });
        }

    }]);
})();