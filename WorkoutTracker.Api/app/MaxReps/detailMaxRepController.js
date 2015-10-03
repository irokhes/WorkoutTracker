(function() {
    'use strict';
    app.controller('detailMaxRepController', ['$scope', '$modal', '$location', '$routeParams', 'maxRepService', '$filter', function ($scope, $modal, $location, $routeParams, maxRepService, $filter) {
        $scope.maxReps = [];
        $scope.isEditing = false;

        /*       
        Calendar Options
        */

        $scope.formData = {};
        $scope.date = "";
        $scope.format = 'MM/dd/yyyy';
        $scope.opened = false;

        //Datepicker
        $scope.dateOptions = {
            'format': "'yy'",
            'show-weeks': false,

        };


        init();

        function init() {
            getMaxRepsByExerciseId($routeParams.id);
        };

        $scope.newMaxRep = function () {
            $scope.isEditing = true;
        }

        $scope.edit = function (id) {
            $scope.isEditing = true;
        }

        $scope.delete = function (exercise) {
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
                maxRepService.delete(exercise.id).success(function (data) {
                    console.info('Exercise deleted');
                    var index = $scope.filteredExercises.indexOf(exercise);
                    $scope.filteredExercises.splice(index, 1);

                }).error(function (error) {
                    $scope.status = 'Error deleting exercise: ' + error.message;
                });
            }, function () {
                //modal closed
            });
        }


        function getMaxRepsByExerciseId(exerciseId) {
            maxRepService.getByExerciseId(exerciseId).success(function (maxReps) {
                $scope.maxReps = maxReps;
                })
            .error(function (error) {
                $scope.status = 'Unable to load maxReps: ' + error.message;
            });
        }
    }]);
})();