(function() {
    'use strict';
    app.controller('ExerciseController', ['exerciseService', function (exerciseService) {
        this.exercices = [];
        this.exercises = exerciseService.get();
        this.welcome = 'hello cundo';
        }]);
})();