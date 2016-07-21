// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments_1) {
        "use strict";
        var InstrumentIndexController = (function () {
            function InstrumentIndexController($state, instrumentService, $q, $location, $scope, toastr, modalService) {
                var _this = this;
                this.$state = $state;
                this.instrumentService = instrumentService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.toastr = toastr;
                this.modalService = modalService;
                this.addInstrument = function () {
                    _this.$state.go("root.instrument.edit", { id: null }, { reload: true });
                };
                this.copyInstrument = function (instrument) {
                    var modalInstance = _this.modalService.open({
                        templateUrl: "/app/instruments/instrument.copy.view.html",
                        controller: "app.instruments.InstrumentCopyController as icvm",
                        keyboard: false,
                        backdrop: 'static',
                        resolve: {
                            instrument: function () {
                                return instrument;
                            }
                        }
                    });
                    modalInstance.result.then(function (result) {
                        if (result) {
                            _this.$state.go("root.instrument.edit", { instrumentId: result.instrumentId });
                        }
                    }, function (result) {
                    });
                };
                this.exportToExcel = function (instrument) {
                    _this.instrumentService.exportToExcel(instrument, null).then(function () {
                        _this.toastr.info("Export complete.");
                    });
                };
                this.isBusy = true;
                instrumentService.getAll().then(function (instruments) {
                    _this.instruments = angular.copy(instruments);
                    _this.isBusy = false;
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            InstrumentIndexController.$inject = [
                "$state",
                "app.services.InstrumentService",
                "$q",
                "$location",
                "$scope",
                "toastr",
                "$uibModal"
            ];
            return InstrumentIndexController;
        }());
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentIndexController", InstrumentIndexController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instruments.index.controller.js.map