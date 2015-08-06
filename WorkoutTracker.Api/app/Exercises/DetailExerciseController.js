(function() {
    'use strict';
    app.controller('DetailExerciseController', ['$scope', '$routeParams', 'exerciseService', function ($scope, $routeParams, exerciseService) {
        $scope.exercise = {};

        $scope.back = function () {
            history.back();
        }

        function init() {
            if ($routeParams.id !== 'undefined') {
                getExerciseDetails();
            }
        };

        function getExerciseDetails() {
            exerciseService.get($routeParams.id).success(function (exercise) {
                $scope.exercise = exercise;
            })
            .error(function (error) {
                $scope.status = 'Unable to load exercise: ' + error.message;
            });
        }

        init();


    }]);
})();