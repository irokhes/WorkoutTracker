﻿(function () {
    'use strict';
    app.service('exerciseService', ['$http', function ($http) {
        var urlBase = 'api/exercise';

        this.getAll = function () {
            return $http.get(urlBase);
        };

        this.get = function (id) {
            return $http.get(urlBase + '/' + id);
        };

        this.save = function (exercise) {
            return $http.post(urlBase, exercise);
        }

        this.update = function (id, exercise) {
            return $http.put(urlBase + '/' + id, exercise);
        }

    }]);
})();