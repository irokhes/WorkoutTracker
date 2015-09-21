(function () {
    'use strict';
    app.controller('EditWorkoutController', ['$scope', '$location', '$routeParams', 'workoutService', 'exerciseService', 'FileReader', function ($scope, $location, $routeParams, workoutService, exerciseService, FileReader) {


        $scope.workout = {};
        $scope.newExercise = {};

        $scope.typeOfWorkout = ['AMRAP', 'EMOM', 'AFAP', 'PowerLifting'];
        $scope.workout.wodType = $scope.typeOfWorkout[0];
        $scope.deletedExercises = [];
        $scope.exercises = {};
        
        $scope.selectedExercise = '';
        $scope.isExerciseSelected = false;

        init();


        function init() {
            exerciseService.getAll().then(function (exercises) {
                $scope.exercises = exercises.data;
                $scope.workout = { minutes: 0, seconds:0 };
                if (typeof $routeParams.id !== 'undefined') {
                    workoutService.get($routeParams.id).then(function (workout) {
                        $scope.workout = workout.data;

                        $scope.workout.minutes = getMinutes($scope.workout.time);
                        $scope.workout.seconds = getSeconds($scope.workout.time);
                    });
                }

            }).catch(function (error) {
                $scope.status = 'Unable to load exercises: ' + error;
                console.error('Unable to load exercises: ' + error);
            });
        }

        function getMinutes(timespan) {
            return timespan.split(':')[1];
        }

        function getSeconds(timespan) {
            return timespan.split(':')[2];
        }

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
        $scope.addExercise = function () {
            $scope.workout.exercises.push({ exerciseId: $scope.newExercise.id, name: $scope.newExercise.name, numReps: $scope.newExercise.Reps, weightOrDistance: $scope.newExercise.weightOrDistance });
            resetNewExercise();
        };

        $scope.deleteExercise = function (exercise) {
            var index = $scope.workout.exercises.indexOf(exercise);
            $scope.workout.exercises.splice(index, 1);
        }

        $scope.deleteImage = function (image) {
            var index = $scope.workout.images.indexOf(image);
            $scope.deletedExercises.push($scope.workout.images[index]);
            $scope.workout.images.splice(index, 1);
        }

        function resetNewExercise() {
            $scope.newExercise = {};
            $scope.selectedExercise = '';
            $scope.isExerciseSelected = false;
        }

        //TODO move to directive
        //Begin Calendar code

        $scope.today = function () {
            $scope.workout.date = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.workout.date = null;
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

        //an array of files selected
        $scope.files = [];

        $scope.getFile = function () {
            if (typeof $scope.file != 'undefined') {
                FileReader.readAsDataURL($scope.file, $scope).then(function (result) {

                    console.info("File read correctly");
                    $scope.imageSrc = result;
                }, function (err) {
                    // Do stuff
                    console.info(err);
                });
            }

        }
        $scope.deleteNewImage = function () {
            $scope.file = undefined;
            $scope.imageSrc = null;
        }

        //the save method
        $scope.save = function () {
            if (typeof $scope.file != 'undefined') {
                $scope.files.push($scope.file);
            }
            var minutes = $scope.workout.minutes === undefined ? 0 : $scope.workout.minutes;
            var seconds = $scope.workout.seconds === undefined ? 0 : $scope.workout.seconds;
            $scope.workout.time = '0:' + minutes + ':' + seconds;
            $scope.workout.deletedExercises = $scope.deletedExercises;

            workoutService.save($scope.workout.id, $scope.workout, $scope.files)
            .success(function (data) {
                $location.path('/workouts');
            }).
            error(function (error) {
                $scope.status = 'Unable to load exercises: ' + error.message;
                console.error('Unable to load exercises: ' + error.message);
            });
        };
    }]);
})();