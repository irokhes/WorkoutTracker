var app;
(function() {
    'use strict';
    app = angular.module('workoutTracker', ['ngRoute', 'ui.bootstrap']);

    app.config(function($routeProvider) {
        $routeProvider.when("/exercises", {
            controller: "ExerciseController",
            templateUrl: "app/Exercises/exercise.html"
        })
        .when("/workouts", {
            controller: "WorkoutController",
            templateUrl: "app/Workouts/workout.html"
        })
        .otherwise({ redirectTo: '/exercises' });
    });
})();