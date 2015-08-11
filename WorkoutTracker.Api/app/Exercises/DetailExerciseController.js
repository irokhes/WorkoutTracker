(function() {
    'use strict';
    app.controller('DetailExerciseController', ['$scope', '$routeParams', 'exerciseService', 'Lightbox', function ($scope, $routeParams, exerciseService, Lightbox) {
        $scope.exercise = {};

        init();

        function init() {
            if ($routeParams.id !== 'undefined') {
                getExerciseDetails();
            }
        };

        $scope.back = function() {
            history.back();
        };

        function getExerciseDetails() {
            exerciseService.get($routeParams.id).success(function (exercise) {
                $scope.exercise = exercise;
            })
            .error(function (error) {
                $scope.status = 'Unable to load exercise: ' + error.message;
            });
        }



        


    }]);
})();