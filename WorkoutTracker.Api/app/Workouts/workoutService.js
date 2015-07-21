(function () {
    'use strict';
    app.service('workoutService', ['$http', function ($http) {
        var urlBase = 'api/workout';

        this.getAll = function() {
            return $http.get(urlBase);
        };

        this.get = function (id) {
            return $http.get(urlBase + '/'+ id);
        };

        this.save = function (workout) {
            return $http.post(urlBase, workout);
        }

        this.update = function(id, workout) {
            return $http.put(urlBase + '/'+ id, workout);
        }


    }]);
})();