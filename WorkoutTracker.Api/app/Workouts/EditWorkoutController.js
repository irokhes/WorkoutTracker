(function () {
    'use strict';
    app.controller('EditWorkoutController', ['$scope', '$location', '$routeParams', 'workoutService', 'exerciseService', 'FileReader', function ($scope, $location, $routeParams, workoutService, exerciseService, FileReader) {


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
        $scope.addExercise = function () {
            $scope.workout.exercises.push({ exerciseId: $scope.newExercise.id, name: $scope.newExercise.name, numReps: $scope.newExercise.Reps, weightOrDistance: $scope.newExercise.weightOrDistance });
            resetNewExercise();
        };

        $scope.deleteExercise = function (exercise) {
            var index = $scope.workout.exercises.indexOf(exercise);
            $scope.workout.exercises.splice(index, 1);
        }

        function resetNewExercise() {
            $scope.newExercise = {};
            $scope.selectedExercise = '';
            $scope.isExerciseSelected = false;
        }

        function toDto() {
            return {
                Id: $scope.workout.id,
                Name: $scope.workout.name,
                Description: $scope.workout.description,
                Exercises: $scope.workout.exercises,
                WODType: $scope.workout.selectedWOD,
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

        //an array of files selected
        $scope.files = [];

        //listen for the file selected event
        //$scope.$on("fileSelected", function (event, args) {
        //    //$scope.$apply(function () {
        //        //add the file object to the scope's files collection

        //        fileReader.readAsDataUrl(args.file, $scope).then(function (result) {
        //            $scope.files.push(args.file);
        //            console.info("File read correctly");
        //            $scope.imageSrc = result;
        //        }, function (err) {
        //            // Do stuff
        //            console.info(err);
        //        });
        //   // });
        //});

        $scope.getFile = function() {
            FileReader.readAsDataURL($scope.file, $scope).then(function (result) {
                $scope.files.push($scope.file);
                console.info("File read correctly");
                $scope.imageSrc = result;
            }, function (err) {
                // Do stuff
                console.info(err);
            });
        }

        //the save method
        $scope.save = function () {
            workoutService.save($scope.workout.id, $scope.workout, $scope.files)
            .success(function (data) {
                $location.path('/workouts');
            }).
            error(function (error) {
                $scope.status = 'Unable to load exercises: ' + error.message;
                console.error('Unable to load exercises: ' + error.message);
            });
        };

        init();


    }]);
})();