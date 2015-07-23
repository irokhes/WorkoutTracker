(function () {
    'use strict';
    app.controller('EditWorkoutController', ['$scope', '$location', '$routeParams', 'workoutService', function ($scope, $location, $routeParams, workoutService) {


        $scope.workout = {};
        $scope.workout.name = '';
        $scope.workout.description = '';
        $scope.typeOfWorkout = ['AMRAP', 'EMOM', 'AFAP', 'PowerLifting'];
        $scope.workout.selectedWOD = $scope.typeOfWorkout[0];

        $scope.exercises = ['Power clean', 'Thruster', 'Snatch clean'];
        $scope.isExerciseSelected = false;

        $scope.onSelect = function ($item) {
            //mark the object as selected
            $scope.isExerciseSelected = true;
        };

        $scope.onChange = function() {
            if ($scope.isExerciseSelected) {
                alert($scope.workout.newExercise.name);
                $scope.isExerciseSelected = false;
            }
        }

        $scope.save = function () {
            if ($scope.workout.id !== 'undefined' && $scope.workout.id !== 0) {
                workoutService.update($scope.workout.id, toDto())
                    .success(function (data) {
                        $location.path('/workouts');
                    }).
                    error(function (error) {
                        $scope.status = 'Unable to load exercises: ' + error.message;
                        console.error('Unable to load exercises: ' + error.message);
                    });
                
            } else {
                workoutService.save(toDto())
                    .success(function (data) {
                        $location.path('/workouts');
                    }).
                    error(function (error) {
                        $scope.status = 'Unable to load exercises: ' + error.message;
                        console.error('Unable to load exercises: ' + error.message);
                    });
            }
            
        }

        function toDto() {
            return {
                Id: $scope.workout.id,
                Name: $scope.workout.name,
                Description: $scope.workout.description,
                MuscularGroup: $scope.workout.selectedWOD,
                Date: $scope.workout.dt
            }
        }

        //Begin Calendar code

        $scope.today = function () {
            $scope.workout.dt = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.workout.dt = null;
        };

        $scope.toggleMin = function () {
            $scope.minDate = $scope.minDate ? null : new Date();
        };
        $scope.toggleMin();

        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        };

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        //End calendar code

        init();

        function init() {
            if ($routeParams.id === 'undefined') {
                return;
            }
            $scope.workout.id = $routeParams.id;
            workoutService.get($scope.workout.id).success(function (workout) {
                $scope.workout.name = workout.Name;
                $scope.workout.description = workout.Description;
                $scope.workout.dt = workout.Date;
                $scope.workout.selectedWOD = workout.WODType;
            })
            .error(function (error) {
                $scope.status = 'Unable to load exercises: ' + error.message;
                console.error('Unable to load exercises: ' + error.message);
            });

        };








    }]);
})();