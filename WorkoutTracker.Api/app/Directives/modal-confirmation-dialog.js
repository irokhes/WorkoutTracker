﻿app.directive('modalConfirmationDialog', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        transclude: true,
        scope: {
            ngModel: '='
        },
        template: '<div class="modal-header">\
        <h3 class="modal-title">{{title}}</h3>\
        </div>\
        <div class="modal-body">\
            <ul>\
                <li ng-repeat="item in items">\
                    <a ng-click="selected.item = item">{{ item }}</a>\
                </li>\
            </ul>\
            Selected: <b>{{ selected.item }}</b>\
        </div>\
        <div class="modal-footer">\
            <button class="btn btn-primary" ng-click="ok()">OK</button>\
            <button class="btn btn-warning" ng-click="cancel()">Cancel</button>\
        </div>',
        link: function (scope, el, attrs, transcludeFn) {
            scope.modalId = attrs.modalId;
            scope.title = attrs.modalTitle;
        }
    };
});