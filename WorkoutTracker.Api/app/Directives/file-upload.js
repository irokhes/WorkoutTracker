app.directive('fileUpload', function () {
    return {
        link: function ($scope, el) {
            el.bind('change', function (event) {
                $scope.file = (event.srcElement || event.target).files[0];
                $scope.getFile();
            });
        }
    };
});