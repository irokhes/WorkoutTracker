(function() {
    'use strict';
    app.controller('NavigationController',['$location', function($location) {
        this.isActive = function(path) {
            return $location.path().substr(0, path.length) == path;
        };
    }]);
})();