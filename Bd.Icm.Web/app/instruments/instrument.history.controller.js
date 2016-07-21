// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments) {
        "use strict";
        var InstrumentHistoryController = (function () {
            function InstrumentHistoryController($rootScope, $state, $stateParams, instrumentService, instrumentCommitService, dialogService, $q, $location, $scope, toastr) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$state = $state;
                this.$stateParams = $stateParams;
                this.instrumentService = instrumentService;
                this.instrumentCommitService = instrumentCommitService;
                this.dialogService = dialogService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.toastr = toastr;
                this.canDiff = function () {
                    var selected = _.filter(_this.commits, function (item) {
                        return item.isSelected;
                    });
                    return (selected.length === 2);
                };
                this.compare = function () {
                    var selected = _.filter(_this.commits, function (item) {
                        return item.isSelected;
                    });
                    _this.$state.go("root.instrument.compare", {
                        id: _this.instrument.instrumentId,
                        fromVersion: selected[1].id,
                        toVersion: selected[0].id
                    });
                };
                this.exportToExcel = function (change) {
                    _this.instrumentService.exportToExcel(_this.instrument, change.effectiveTo).then(function () {
                        _this.toastr.info("Export complete.");
                    });
                };
                this.instrumentId = this.$stateParams["instrumentId"];
                this.commits = [];
                this.isBusy = true;
                this.instrumentService.get(this.instrumentId).then(function (instrument) {
                    _this.instrument = angular.copy(instrument);
                }).then(function () {
                    return _this.instrumentCommitService.getAll(_this.instrumentId).then(function (commits) {
                        angular.copy(commits, _this.commits);
                        _this.isBusy = false;
                    });
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            InstrumentHistoryController.$inject = [
                "$rootScope",
                "$state",
                "$stateParams",
                "app.services.InstrumentService",
                "app.services.InstrumentCommitService",
                "app.services.DialogService",
                "$q",
                "$location",
                "$scope",
                "toastr"
            ];
            return InstrumentHistoryController;
        }());
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentHistoryController", InstrumentHistoryController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.history.controller.js.map