(function () {
    'use strict';
    app.controller('EditWorkoutController', ['$scope', '$location', '$routeParams', 'workoutService', 'exerciseService', function ($scope, $location, $routeParams, workoutService, exerciseService) {


        $scope.workout = {};
        $scope.newExercise = {};

        $scope.typeOfWorkout = ['AMRAP', 'EMOM', 'AFAP', 'PowerLifting'];
        $scope.workout.wodType = $scope.typeOfWorkout[0];

        $scope.exercises = {};
        $scope.selectedExercise = '';
        $scope.isExerciseSelected = false;

        function init() {
            if ($routeParams.id === 'undefined') {
                return;
            }
            $scope.workout.id = $routeParams.id;
            workoutService.get($scope.workout.id).then(function (workout) {
                $scope.workout = workout.data;
                $scope.workout.dt = workout.data.date;
                return exerciseService.get();
            }).then(function (exercises) {
                $scope.exercises = exercises.data;
            }).catch(function (error) {
                $scope.status = 'Unable to load exercises: ' + error;
                console.error('Unable to load exercises: ' + error);
            });

        };

        $scope.onSelect = function ($item) {
            $scope.isExerciseSelected = true;
            console.info('exercise selected: ' + $item);
            $scope.newExercise = $item;
        };

        $scope.onChange = function () {
            if ($scope.isExerciseSelected) {
                $scope.isExerciseSelected = false;
            }
        }
        $scope.addExercise = function() {
            $scope.workout.exercises.push({ name: $scope.newExercise.name, numReps: $scope.newExercise.Reps, weightOrDistance: $scope.newExercise.weightOrDistance });
            resetNewExercise();
        };

        function resetNewExercise() {
            $scope.newExercise = {};
            $scope.selectedExercise = '';
            $scope.isExerciseSelected = false;
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

        //TODO move to directive
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

        
    }]);
})();