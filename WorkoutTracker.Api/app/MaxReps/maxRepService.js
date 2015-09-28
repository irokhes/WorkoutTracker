(function () {
    'use strict';
    app.service('maxRepService', ['$http', function ($http) {
        var urlBase = 'api/maxRep';

        this.getAll = function () {
            return $http.get(urlBase);
        };

        this.getByExerciseId = function (id) {
            return $http.get(urlBase + '/exercise/' + id);
        };

        this.save = function(exercise) {
            return $http.post(urlBase, exercise);
        };

        this.update = function(id, exercise) {
            return $http.put(urlBase + '/' + id, exercise);
        };

        this.delete = function(id) {
            return $http.delete(urlBase + '/' + id);
        };

    }]);
})();