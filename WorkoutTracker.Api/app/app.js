var app;
(function() {
    'use strict';
     app = angular.module('workoutTracker', ['ngRoute', 'ui.bootstrap']);

    //app.config(function($routeProvider) {
    //    $routeProvider.when("/exercises", {
    //        controller: "ExercisesController",
    //        templateUrl: "app/views/exercises.html"
    //    });
    //    $routeProvider.otherwise({ redirectTo: '/exercises' });
    //});
})();