(function() {
    'use strict';
    app.controller('ExerciseController', ['$scope','exerciseService', function ($scope, exerciseService) {
        $scope.exercices = [];
        $scope.totalExercises = 0;

        init();

        function init() {
            exerciseService.get().success(function(exercises) {
                $scope.exercices = exercises;
                $scope.totalExercises = $scope.exercices.length;
            })
            .error(function (error) {
                $scope.status = 'Unable to load exercises: ' + error.message;
            });
        };



        }]);
})();