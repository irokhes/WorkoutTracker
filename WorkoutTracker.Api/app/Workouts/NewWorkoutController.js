(function() {
    'use strict';
    app.controller('NewWorkoutController', ['$scope', '$location', 'exerciseService', function ($scope, $location, exerciseService) {
        $scope.name = '';
        $scope.description = '';
        $scope.muscularGroup = ['Biceps', 'Triceps', 'Back', 'Chest', 'Legs', 'Abs'];
        $scope.selectedMuscularGroup = $scope.muscularGroup[0];

        $scope.save = function () {
            exerciseService.save({ Name: $scope.name, Description: $scope.description, MuscularGroup: $scope.selectedMuscularGroup })
                .success(function (data, status, headers, config) {
                // this callback will be called asynchronously
                    // when the response is available
                    $location.path('/exercises');
                }).
                error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
        }

        init();

        function init() {
           
        };

        

    }]);
})();