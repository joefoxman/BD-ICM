// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments) {
        "use strict";
        var InstrumentLayoutController = (function () {
            function InstrumentLayoutController($rootScope, $scope, $state, $stateParams, instrumentService, $interval, toastr, modalService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$scope = $scope;
                this.$state = $state;
                this.$stateParams = $stateParams;
                this.instrumentService = instrumentService;
                this.$interval = $interval;
                this.toastr = toastr;
                this.modalService = modalService;
                this.loadInstrument = function () {
                    _this.instrumentId = _this.$stateParams["instrumentId"];
                    if (_this.instrumentId) {
                        return _this.instrumentService.get(_this.instrumentId).then(function (instrument) {
                            _this.instrument = angular.copy(instrument);
                        });
                    }
                    else {
                        return _this.instrumentService.create().then(function (instrument) {
                            _this.instrument = angular.copy(instrument);
                        });
                    }
                };
                this.checkForChanges = function () {
                    _this.instrumentService.checkForChanges(_this.instrument.instrumentId)
                        .then(function (changeUsers) {
                        _this.changeUsers = angular.copy(changeUsers);
                        _this.changeUsersFormatted = "";
                        _.each(_this.changeUsers, function (cu) {
                            if (_this.changeUsersFormatted.length > 0)
                                _this.changeUsersFormatted += ", ";
                            var fullName = cu.firstName || "";
                            if (fullName.length > 0)
                                fullName += " ";
                            fullName += cu.lastName;
                            _this.changeUsersFormatted += fullName;
                        });
                    });
                };
                this.commit = function () {
                    var modalInstance = _this.modalService.open({
                        templateUrl: "/app/instrumentCommit/instrumentCommit.view.html?" + Math.random().toString(36).slice(2),
                        controller: "app.instrumentCommit.InstrumentCommitController as icvm",
                        keyboard: false,
                        backdrop: 'static',
                        size: 'lg',
                        resolve: {
                            instrument: function () {
                                return _this.instrument;
                            }
                        }
                    });
                    modalInstance.result.then(function (result) {
                        if (result) {
                            _this.instrument = result;
                            _this.$rootScope.$broadcast("instrumentChanged", _this.instrument);
                            _this.$state.go("root.instrument.edit", { instrumentId: result.instrumentId });
                        }
                    }, function (result) {
                    });
                };
                this.searchParts = function (searchKey) {
                    return _this.instrumentService.searchParts(_this.instrumentId, searchKey).then(function (results) {
                        return results;
                    });
                };
                this.editPart = function ($item, $model, $label, $event) {
                    _this.$state.go("root.instrument.part", { instrumentId: _this.instrument.instrumentId, id: $item.partId, parentPartId: $item.parentPartId }, { reload: true });
                };
                this.exportToExcel = function () {
                    _this.instrumentService.exportToExcel(_this.instrument, null).then(function () {
                        _this.toastr.info("Export complete.");
                    });
                };
                this.changeUsers = [];
                this.changeUsersFormatted = "";
                this.$rootScope.$on("instrumentChanged", function (evt, instrument) {
                    _this.instrument = angular.copy(instrument);
                });
                this.isBusy = true;
                this.loadInstrument().then(function () {
                    _this.checkForChanges();
                    var stop = _this.$interval(function () {
                        _this.checkForChanges();
                    }, 5000);
                    _this.isBusy = false;
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            InstrumentLayoutController.$inject = [
                "$rootScope",
                "$scope",
                "$state",
                "$stateParams",
                "app.services.InstrumentService",
                "$interval",
                "toastr",
                "$uibModal"
            ];
            return InstrumentLayoutController;
        }());
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentLayoutController", InstrumentLayoutController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.layout.controller.js.map