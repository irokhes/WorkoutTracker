(function() {
    'use strict';
    app.controller('ExerciseController', ['$scope', '$location', 'exerciseService', '$filter', function ($scope, $location, exerciseService, $filter) {
        $scope.exercices = [];
        $scope.totalExercises = 0;
        $scope.filteredExercises = [];
        $scope.totalFilteredExercises = 0;
        $scope.muscularGroup = ['All','Biceps', 'Triceps', 'Back', 'Chest', 'Legs', 'Abs'];
        $scope.selectedMuscularGroup = $scope.muscularGroup[0];
        $scope.filterValue = '';

        init();

        function init() {
            createWatch();
            getExercises();
        };


        $scope.showExerciseDetails = function(id) {
            $location.path('/exercises/' + id);
        }

        $scope.newExercise = function() {
            $location.path('/exercises/new');
        }

        function getExercises() {
            exerciseService.get().success(function (exercises) {
                $scope.exercices = exercises;
                $scope.totalExercises = $scope.exercices.length;
                filterExercises('', 'All');
                })
            .error(function (error) {
                $scope.status = 'Unable to load exercises: ' + error.message;
            });
        }



        function filterExercises(filterValue, muscularGroup) {
            $scope.filteredExercises = $filter('exercisesFilter')($scope.exercices, filterValue, muscularGroup);
            $scope.totalFilteredExercises = $scope.filteredExercises.length;
        };

        function createWatch() {

            $scope.$watch("filterValue", function (filterValue) {
                filterExercises(filterValue, $scope.selectedMuscularGroup);
            });

            $scope.$watch("selectedMuscularGroup", function (selectedMuscularGroup) {
                filterExercises($scope.filterValue, selectedMuscularGroup);
            });
        }

    }]);
})();