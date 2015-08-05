(function() {
    'use strict';
    app.controller('EditExerciseController', ['$scope', '$location', 'exerciseService', function ($scope, $location, exerciseService) {
        $scope.name = '';
        $scope.description = '';
        $scope.muscularGroup = ['Biceps', 'Triceps', 'Back', 'Chest', 'Legs', 'Abs'];
        $scope.selectedMuscularGroup = $scope.muscularGroup[0];

        $scope.save = function () {
            var exercise = { Name: $scope.name, Description: $scope.description, MuscularGroup: $scope.selectedMuscularGroup };
            var isExistingExercise = typeof exercise.id != 'undefined' && exercise.id != 0;

            isExistingExercise ? updateExercise(exercise) : createNewExercise(exercise);
        }

        init();

        function init() {
           
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
            exerciseService.update(exercise)
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