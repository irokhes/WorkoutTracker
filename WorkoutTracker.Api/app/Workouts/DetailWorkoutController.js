﻿(function() {
    'use strict';
    app.controller('DetailWorkoutController', ['$scope', '$routeParams', 'workoutService', function ($scope, $routeParams, workoutService) {
        $scope.workout = {};

        $scope.back = function () {
            history.back();
        }

        function init() {
            if ($routeParams.id !== 'undefined') {
                getExerciseDetails();
            }
        };

        

        function getExerciseDetails() {
            if ($routeParams.id === 'undefined') {
                return;
            }
            workoutService.get($routeParams.id).then(function (workout) {
                $scope.workout = workout.data;
                $scope.workout.dt = workout.data.date;
            }).catch(function (error) {
                $scope.status = 'Unable to load exercises: ' + error;
                console.error('Unable to load exercises: ' + error);
            });
        }

        init();


    }]);
})();