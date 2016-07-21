// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments) {
        "use strict";
        var InstrumentCompareController = (function () {
            function InstrumentCompareController($rootScope, $state, $stateParams, instrumentService, dialogService, $q, $location, $scope, toastr) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$state = $state;
                this.$stateParams = $stateParams;
                this.instrumentService = instrumentService;
                this.dialogService = dialogService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.toastr = toastr;
                this.exportToExcel = function (change) {
                    _this.instrumentService.exportToExcel(_this.instrument, change.effectiveTo).then(function () {
                        _this.toastr.info("Export complete.");
                    });
                };
                this.instrumentId = this.$stateParams["instrumentId"];
                this.fromVersion = this.$stateParams["fromVersion"];
                this.toVersion = this.$stateParams["toVersion"];
                this.isBusy = true;
                this.instrumentService.get(this.instrumentId).then(function (instrument) {
                    _this.instrument = angular.copy(instrument);
                }, function (response) {
                    _this.isBusy = false;
                    _this.toastr.error(response.data.message);
                }).then(function () {
                    return _this.instrumentService.getDiff(_this.instrumentId, _this.fromVersion, _this.toVersion).then(function (changes) {
                        _this.changes = angular.copy(changes);
                        _this.isBusy = false;
                    });
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            InstrumentCompareController.$inject = [
                "$rootScope",
                "$state",
                "$stateParams",
                "app.services.InstrumentService",
                "app.services.DialogService",
                "$q",
                "$location",
                "$scope",
                "toastr"
            ];
            return InstrumentCompareController;
        }());
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentCompareController", InstrumentCompareController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.compare.controller.js.map