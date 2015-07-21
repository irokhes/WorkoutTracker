var app;
(function() {
    'use strict';
    app = angular.module('workoutTracker', ['ngRoute', 'ui.bootstrap', 'ui.bootstrap.datepicker']);

    app.config(function($routeProvider) {
        $routeProvider.when("/exercises", {
            controller: "ExerciseController",
            templateUrl: "app/Exercises/exercise.html"
        })
        .when("/exercises/new", {
            controller: "ExerciseNewController",
            templateUrl: "app/Exercises/exerciseNew.html"
        })
        .when("/exercises/:id", {
            controller: "ExerciseDetailController",
            templateUrl: "app/Exercises/exerciseDetail.html"
            })
        .when("/workouts", {
            controller: "WorkoutController",
            templateUrl: "app/Workouts/workout.html"
        })
        .when("/workouts/edit", {
                controller: "EditWorkoutController",
                templateUrl: "app/Workouts/editWorkout.html"
            })
        .when("/workouts/edit/:id", {
            controller: "EditWorkoutController",
            templateUrl: "app/Workouts/editWorkout.html"
        })
        .otherwise({ redirectTo: '/exercises' });
    });
})();