// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instruments;
    (function (instruments) {
        "use strict";
        var InstrumentController = (function () {
            function InstrumentController($rootScope, $state, $stateParams, instrumentService, instrumentTypeService, partService, dialogService, $q, $location, $scope, toastr) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.$state = $state;
                this.$stateParams = $stateParams;
                this.instrumentService = instrumentService;
                this.instrumentTypeService = instrumentTypeService;
                this.partService = partService;
                this.dialogService = dialogService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.toastr = toastr;
                this.loadInstrumentTypes = function () {
                    return _this.instrumentTypeService.getAll().then(function (types) {
                        _this.instrumentTypes = angular.copy(types);
                    });
                };
                this.loadInstrument = function () {
                    if (_this.instrumentId) {
                        return _this.instrumentService.get(_this.instrumentId).then(function (instrument) {
                            _this.instrument = angular.copy(instrument);
                        });
                    }
                    else {
                        return _this.instrumentService.create().then(function (instrument) {
                            _this.instrument = angular.copy(instrument);
                            _this.instrument.sapPartType = _this.instrumentTypes[1];
                        });
                    }
                };
                this.save = function (close) {
                    _this.instrumentService.save(_this.instrument).then(function (instrument) {
                        _this.toastr.success("Instrument saved.");
                        _this.$rootScope.$broadcast("instrumentChanged", instrument);
                        if (close) {
                            _this.$state.go("root.instruments", {});
                        }
                        else {
                            _this.instrument = angular.copy(instrument);
                        }
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
                    });
                };
                this.addPart = function () {
                    _this.partService.create().then(function (part) {
                        part.instrumentId = _this.instrument.instrumentId;
                        _this.instrument.parts.push(part);
                        _this.$state.go("root.instrument.part", { instrumentId: _this.instrument.instrumentId, id: null }, { reload: true });
                    });
                };
                this.editPart = function (part) {
                    _this.$state.go("root.instrument.part", { instrumentId: _this.instrument.instrumentId, id: part.id }, { reload: true });
                };
                this.createBreadCrumbs = function () {
                    var bc = new app.models.Breadcrumb();
                    bc.title = _this.instrument.nickName;
                    bc.route = "root.instrument";
                    bc.params = { id: _this.instrument.instrumentId };
                    bc.sref = bc.route + "(" + JSON.stringify(bc.params) + ")";
                    return [bc];
                };
                this.deletePart = function (part) {
                    _this.dialogService.askYesNo("Delete Part", "Are you sure you want to delete part [" + part.name + "]?").then(function (result) {
                        if (result === app.services.DialogResult.Yes) {
                            _this.partService.delete(part).then(function (deleted) {
                                if (deleted) {
                                    _this.toastr.success("Part deleted.");
                                    _this.instrument.parts.splice(_this.instrument.parts.indexOf(part));
                                }
                                else {
                                    _this.toastr.error("Error deleting part.");
                                }
                            });
                        }
                    });
                };
                this.modelErrors = [];
                this.instrumentTypes = [];
                this.instrumentId = this.$stateParams["instrumentId"];
                this.isBusy = true;
                this.loadInstrumentTypes().then(this.loadInstrument).then(function () {
                    _this.isBusy = false;
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            InstrumentController.$inject = [
                "$rootScope",
                "$state",
                "$stateParams",
                "app.services.InstrumentService",
                "app.services.InstrumentTypeService",
                "app.services.PartService",
                "app.services.DialogService",
                "$q",
                "$location",
                "$scope",
                "toastr"
            ];
            return InstrumentController;
        }());
        angular.module("app.instruments")
            .controller("app.instruments.InstrumentController", InstrumentController);
    })(instruments = app.instruments || (app.instruments = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.controller.js.map