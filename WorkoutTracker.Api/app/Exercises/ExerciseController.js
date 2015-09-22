(function() {
    'use strict';
    app.controller('ExerciseController', ['$scope', '$modal', '$location', 'exerciseService', '$filter', function ($scope, $modal, $location, exerciseService, $filter) {
        $scope.exercices = [];
        $scope.totalExercises = 0;
        $scope.filteredExercises = [];
        $scope.totalFilteredExercises = 0;
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

        $scope.delete = function (id) {
            var modalInstance = $modal.open({
                templateUrl: '/app/ModalDialog/template.html',
                controller: 'ModalDialogCtrl',
                resolve: {
                    isOk: function () {
                        return $scope.isOk;
                    }
                }
            });

            modalInstance.result.then(function () {
                //delete action goes here
                console.info('Modal closed by user');
                exerciseService.delete(id).success(function(data) {
                    console.info('Exercise deleted');
                    

                }).error(function (error) {
                    $scope.status = 'Unable to load exercises: ' + error.message;
                });
            }, function () {
                $scope.selected = false;
                console.info('Modal closed with result: false');
                console.info('Modal dismissed at: ' + new Date());
            });
        }

        $scope.filter = function () {
            filterExercises();
        };

        function filterExercises() {
            $scope.filteredExercises = $filter('exercisesFilter')($scope.exercices, $scope.filterValue);
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