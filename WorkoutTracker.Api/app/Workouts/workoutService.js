(function () {
    'use strict';
    app.service('workoutService', ['$http', function ($http) {
        var urlBase = 'api/workout';

        this.get = function() {
            return $http.get(urlBase);
        };

        this.save = function(workout) {
            return $http.post(urlBase, workout);
        }

    }]);
})();