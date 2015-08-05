(function() {
    'use strict';
    app.controller('EditExerciseController', ['$scope', '$location', '$routeParams', 'exerciseService', function ($scope, $location, $routeParams, exerciseService) {
        $scope.exercise = {};
        $scope.muscularGroup = ['Biceps', 'Triceps', 'Back', 'Chest', 'Legs', 'Abs'];
        $scope.exercise.muscularGroup = $scope.muscularGroup[0];

        $scope.save = function () {
            var exercise = { Id: $scope.exercise.id, Name: $scope.exercise.name, Description: $scope.exercise.description, MuscularGroup: $scope.exercise.selectedMuscularGroup };
            var isExistingExercise = typeof $scope.exercise.id != 'undefined' && $scope.exercise.id != 0;

            isExistingExercise ? updateExercise(exercise) : createNewExercise(exercise);
        }

        init();

        function init() {
            if ($routeParams.id === 'undefined') {
                return;
            }
            $scope.exercise.id = $routeParams.id;
            exerciseService.get($scope.exercise.id).then(function (workout) {
                $scope.exercise = workout.data;
            }).catch(function (error) {
                $scope.status = 'Unable to load exercises: ' + error;
                console.error('Unable to load exercises: ' + error);
            });
        };

        function createNewExercise(exercise) {
            
            exerciseService.save(exercise)
                .success(function (data, status, headers, config) {
                // this callback will be called asynchronously
                    // when the response is available
                    $location.path('/exercises');
                }).
                error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
        };

        function updateExercise(exercise) {
            exerciseService.update(exercise.Id, exercise)
                .success(function (data, status, headers, config) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $location.path('/exercises');
                }).
                error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
        }

    }]);
})();