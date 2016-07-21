// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var layout;
    (function (layout) {
        "use strict";
        var LayoutController = (function () {
            function LayoutController($state, instrumentService, $q, $location, $scope) {
                this.$state = $state;
                this.instrumentService = instrumentService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
            }
            LayoutController.$inject = [
                "$state",
                "$q",
                "$location",
                "$scope"
            ];
            return LayoutController;
        }());
        angular.module("app.layout")
            .controller("app.layout.LayoutController", LayoutController);
    })(layout = app.layout || (app.layout = {}));
})(app || (app = {}));
//# sourceMappingURL=layout.controller.js.map