// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments) {
        "use strict";
        var InstrumentCopyController = (function () {
            function InstrumentCopyController($rootScope, $state, $stateParams, instrumentService, instrumentTypeService, partService, dialogService, instrument, $modalInstance, $q, $location, $scope, toastr) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$state = $state;
                this.$stateParams = $stateParams;
                this.instrumentService = instrumentService;
                this.instrumentTypeService = instrumentTypeService;
                this.partService = partService;
                this.dialogService = dialogService;
                this.$modalInstance = $modalInstance;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.toastr = toastr;
                this.loadInstrumentTypes = function () {
                    return _this.instrumentTypeService.getAll().then(function (types) {
                        _this.instrumentTypes = angular.copy(types);
                    });
                };
                this.save = function (close) {
                    _this.isBusy = true;
                    _this.instrumentService.copy(_this.instrument).then(function (instrument) {
                        _this.instrument = angular.copy(instrument);
                        _this.isBusy = false;
                        _this.toastr.success("Instrument copy complete!");
                        _this.$modalInstance.close(instrument);
                    }, function (response) {
                        if (response.data.modelState) {
                            _this.modelErrors.length = 0;
                            _.each(response.data.modelState, function (error) {
                                _this.modelErrors.push(error);
                            });
                        }
                        else {
                            _this.toastr.error(response.data.message);
                        }
                        _this.isBusy = false;
                    });
                };
                this.cancel = function () {
                    _this.$modalInstance.dismiss();
                };
                this.modelErrors = [];
                this.instrumentTypes = [];
                this.instrument = angular.copy(instrument);
                this.instrument.type = "";
                this.instrument.nickName = "";
                this.instrument.serialNumber = "";
                this.isBusy = true;
                this.loadInstrumentTypes().then(function () {
                    _this.isBusy = false;
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            InstrumentCopyController.$inject = [
                "$rootScope",
                "$state",
                "$stateParams",
                "app.services.InstrumentService",
                "app.services.InstrumentTypeService",
                "app.services.PartService",
                "app.services.DialogService",
                "instrument",
                "$uibModalInstance",
                "$q",
                "$location",
                "$scope",
                "toastr"
            ];
            return InstrumentCopyController;
        }());
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentCopyController", InstrumentCopyController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.copy.controller.js.map