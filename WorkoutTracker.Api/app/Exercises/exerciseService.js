(function () {
    'use strict';
    app.service('exerciseService', ['$http', function ($http) {
        var urlBase = 'api/exercise';

        this.get = function() {
            return $http.get(urlBase);
        };

        this.get = function(id) {
            return $http.get(urlBase + '/' + id);
        }

    }]);
})();