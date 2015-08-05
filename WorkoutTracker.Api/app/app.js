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
            controller: "EditExerciseController",
            templateUrl: "app/Exercises/editExercise.html"
        })
        .when("/exercises/:id", {
            controller: "DetailExerciseController",
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