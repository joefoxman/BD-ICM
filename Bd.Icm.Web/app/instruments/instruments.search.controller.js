// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments) {
        "use strict";
        var InstrumentSearchController = (function () {
            function InstrumentSearchController($state, instrumentService, $q, $location, $scope, modalService) {
                this.$state = $state;
                this.instrumentService = instrumentService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.modalService = modalService;
                this.search = function () {
                };
                this.isBusy = true;
            }
            InstrumentSearchController.$inject = [
                "$state",
                "app.services.InstrumentService",
                "$q",
                "$location",
                "$scope",
                "$uibModal"
            ];
            return InstrumentSearchController;
        })();
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentSearchController", InstrumentSearchController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instruments.search.controller.js.map