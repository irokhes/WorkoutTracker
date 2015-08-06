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
            getExercises();
        };


        $scope.showExerciseDetails = function(id) {
            $location.path('/exercises/detail/' + id);
        }

        $scope.newExercise = function() {
            $location.path('/exercises/new');
        }

        $scope.edit = function (id) {
            $location.path('/exercises/edit/' + id);
        }

        $scope.delete = function () {

        }

        $scope.filter = function () {
            filterExercises();
        };

        function filterExercises() {
            $scope.filteredExercises = $filter('exercisesFilter')($scope.exercices, $scope.filterValue, $scope.selectedMuscularGroup);
            $scope.totalFilteredExercises = $scope.filteredExercises.length;
        }

        function getExercises() {
            exerciseService.getAll().success(function (exercises) {
                $scope.exercices = exercises;
                $scope.totalExercises = $scope.exercices.length;
                filterExercises();
                })
            .error(function (error) {
                $scope.status = 'Unable to load exercises: ' + error.message;
            });
        }



        

 
    }]);
})();