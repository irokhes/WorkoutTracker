var app;
(function () {
    'use strict';
    app = angular.module('workoutTracker', ['ngRoute', 'ui.bootstrap', 'ui.bootstrap.datepicker', 'bootstrapLightbox', 'filereader']);

    app.config(function (LightboxProvider) {
        LightboxProvider.getImageUrl = function (image) {
            return image.baseUrl + image.name + "/";
        };
    });

    app.config(function ($routeProvider) {
        $routeProvider
        .when("/exercises", {
            controller: "ExerciseController",
            templateUrl: "app/Exercises/exercise.html"
        })
        .when("/exercises/detail/:id", {
            controller: "DetailExerciseController",
            templateUrl: "app/Exercises/detailExercise.html"
        })
        .when("/exercises/new", {
            controller: "EditExerciseController",
            templateUrl: "app/Exercises/editExercise.html"
        })
        .when("/exercises/edit/:id", {
            controller: "EditExerciseController",
            templateUrl: "app/Exercises/editExercise.html"
        })
        .when("/workouts", {
            controller: "WorkoutController",
            templateUrl: "app/Workouts/workout.html"
        })
        .when("/workouts/detail/:id", {
            controller: "DetailWorkoutController",
            templateUrl: "app/Workouts/detailWorkout.html"
        })
        .when("/workouts/edit", {
            controller: "EditWorkoutController",
            templateUrl: "app/Workouts/editWorkout.html"
        })
        .when("/workouts/edit/:id", {
            controller: "EditWorkoutController",
            templateUrl: "app/Workouts/editWorkout.html"
        })
        .when("/maxreps", {
            controller: "MaxRepController",
            templateUrl: "app/MaxReps/maxrep.html"
            })
        .otherwise({ redirectTo: '/workouts' });
    });
})();