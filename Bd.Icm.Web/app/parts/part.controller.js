// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var parts;
    (function (parts_1) {
        "use strict";
        var PartController = (function () {
            function PartController($state, $stateParams, partService, partActionService, instrumentService, partTypeService, partActionTypeService, partMetadataService, dialogService, $q, $location, $scope, toastr, modalService) {
                var _this = this;
                this.$state = $state;
                this.$stateParams = $stateParams;
                this.partService = partService;
                this.partActionService = partActionService;
                this.instrumentService = instrumentService;
                this.partTypeService = partTypeService;
                this.partActionTypeService = partActionTypeService;
                this.partMetadataService = partMetadataService;
                this.dialogService = dialogService;
                this.$q = $q;
                this.$location = $location;
                this.$scope = $scope;
                this.toastr = toastr;
                this.modalService = modalService;
                this.loadPart = function () {
                    if (_this.partId) {
                        return _this.loadPartCore(_this.partId);
                    }
                    else {
                        return _this.partService.create().then(function (part) {
                            _this.part = angular.copy(part);
                            _this.setParent(_this.part, _this.parentPartId, _this.instrumentId);
                        });
                    }
                };
                this.loadPartActionTypes = function () {
                    return _this.partActionTypeService.getAll().then(function (types) {
                        _this.partActionTypes = angular.copy(types);
                    });
                };
                this.loadPartTypes = function () {
                    return _this.partTypeService.getAll().then(function (types) {
                        _this.partTypes = angular.copy(types);
                    });
                };
                this.loadPartNodes = function () {
                    return _this.partService.getNodes(_this.instrumentId, _this.parentPartId).then(function (nodes) {
                        _this.createBreadCrumbs(nodes);
                    });
                };
                this.loadPartCore = function (id) {
                    return _this.partService.get(id).then(function (part) {
                        _this.part = angular.copy(part);
                        _this.setParent(_this.part, _this.parentPartId, _this.instrumentId);
                    });
                };
                this.setParent = function (obj, parentPartId, instrumentId) {
                    if (parentPartId) {
                        obj.parentPartId = parentPartId;
                    }
                    else {
                        obj.instrumentId = instrumentId;
                    }
                };
                this.cancel = function () {
                    _this.$state.go(_this.previousState().route, _this.previousState().params);
                };
                this.deletePart = function (part) {
                    _this.dialogService.askYesNo("Delete Part", "Are you sure you want to delete part [" + part.name + "]?").then(function (result) {
                        if (result === app.services.DialogResult.Yes) {
                            _this.partService.delete(part).then(function (deleted) {
                                if (deleted) {
                                    _this.toastr.success("Part deleted.");
                                    _this.part.parts.splice(_this.part.parts.indexOf(part), 1);
                                }
                                else {
                                    _this.toastr.error("Error deleting part.");
                                }
                            });
                        }
                    });
                };
                this.saveNew = function () {
                    _this.partService.save(_this.part).then(function (part) {
                        _this.toastr.success("Part saved.");
                        _this.part = angular.copy(part);
                        _this.partService.create().then(function (newPart) {
                            _this.part = angular.copy(newPart);
                            _this.setParent(_this.part, _this.parentPartId, _this.instrumentId);
                        });
                    }, function (response) {
                        if (response.data.modelState) {
                            _this.modelErrors.length = 0;
                            _.each(response.data.modelState, function (error) {
                                _this.modelErrors.push(error);
                            });
                            _this.toastr.warning("Please check validation errors.");
                        }
                        else {
                            _this.toastr.error(response.data.message);
                        }
                    });
                };
                this.save = function () {
                    _this.partService.save(_this.part).then(function (part) {
                        _this.toastr.success("Part saved.");
                        _this.part = angular.copy(part);
                    }, function (response) {
                        if (response.data.modelState) {
                            _this.modelErrors.length = 0;
                            _.each(response.data.modelState, function (error) {
                                _this.modelErrors.push(error);
                            });
                            _this.toastr.warning("Please check validation errors.");
                        }
                        else {
                            _this.toastr.error(response.data.message);
                        }
                    });
                };
                this.addPart = function () {
                    _this.partService.create().then(function (part) {
                        _this.part.parts.push(part);
                        _this.$state.go("root.instrument.part", {
                            id: null, parentPartId: _this.part.id
                        }, { reload: true });
                    });
                };
                this.editPart = function (part) {
                    _this.$state.go("root.instrument.part", { id: part.id, parentPartId: _this.part.id }, { reload: true });
                };
                this.createBreadCrumbs = function (nodes) {
                    _this.breadcrumbs = [];
                    _.each(nodes, function (node) {
                        var bc = new app.models.Breadcrumb();
                        _this.breadcrumbs.push(bc);
                        bc.title = node.name;
                        if (node.partId) {
                            bc.route = "root.instrument.part";
                            bc.params = { id: node.partId, parentPartId: node.parentPartId };
                        }
                        else {
                            bc.route = "root.instrument.edit";
                            bc.params = { instrumentId: node.instrumentId };
                        }
                        bc.sref = bc.route + "(" + JSON.stringify(bc.params) + ")";
                    });
                };
                this.previousState = function () {
                    var lastState = _this.breadcrumbs[_this.breadcrumbs.length - 1];
                    return lastState;
                };
                this.getParts = function (key) {
                    var deferred = _this.$q.defer();
                    _this.partService.searchNames(key).then(function (parts) {
                        deferred.resolve(parts);
                    });
                    return deferred.promise;
                };
                this.addAction = function () {
                    var modalInstance = _this.modalService.open({
                        templateUrl: "/app/partActions/partAction.view.html",
                        controller: "app.partActions.PartActionController as pavm",
                        keyboard: false,
                        backdrop: 'static',
                        resolve: {
                            action: function () {
                                return _this.partActionService.create().then(function (action) {
                                    return action;
                                });
                            },
                            part: function () {
                                return _this.part;
                            }
                        }
                    });
                    modalInstance.result.then(function (result) {
                        _this.reloadActions();
                    }, function (result) {
                        if (result) {
                        }
                    });
                };
                this.reloadActions = function () {
                    _this.partActionService.getAll(_this.part.id).then(function (actions) {
                        _this.part.actions = angular.copy(actions);
                    });
                };
                this.editAction = function (action) {
                    var modalInstance = _this.modalService.open({
                        templateUrl: "/app/partActions/partAction.view.html",
                        controller: "app.partActions.PartActionController as pavm",
                        keyboard: false,
                        backdrop: 'static',
                        resolve: {
                            action: function () {
                                return action;
                            },
                            part: function () {
                                return _this.part;
                            }
                        }
                    });
                    modalInstance.result.then(function (result) {
                    }, function (result) {
                        if (result) {
                        }
                    });
                };
                this.deleteAction = function (action) {
                    _this.dialogService.askYesNo("Delete Action", "Are you sure you want to delete this action?").then(function (result) {
                        if (result !== app.services.DialogResult.Yes)
                            return;
                        _this.partActionService.delete(_this.part.id, action).then(function (deleted) {
                            if (deleted) {
                                _this.toastr.success("Action deleted.");
                                _this.part.actions.splice(_this.part.actions.indexOf(action), 1);
                            }
                            else {
                                _this.toastr.error("Error deleting action.");
                            }
                        });
                    });
                };
                this.addMetadata = function () {
                    var modalInstance = _this.modalService.open({
                        templateUrl: "/app/partMetadata/partMetadata.view.html",
                        controller: "app.partMetadata.PartMetadataController as pmvm",
                        keyboard: false,
                        scope: _this.$scope,
                        backdrop: 'static',
                        resolve: {
                            metadata: function () {
                                return _this.partMetadataService.create().then(function (metadata) {
                                    return metadata;
                                });
                            },
                            part: function () {
                                return _this.part;
                            }
                        }
                    });
                    modalInstance.result.then(function (result) {
                        _this.reloadMetadata();
                    }, function (result) {
                        if (result) {
                        }
                    });
                };
                this.deleteMetadata = function (metadata) {
                    _this.dialogService.askYesNo("Delete Metadata", "Are you sure you want to delete the metadata with key " + metadata.metaKey + "?").then(function (result) {
                        if (result !== app.services.DialogResult.Yes)
                            return;
                        _this.partMetadataService.delete(_this.part.id, metadata).then(function (deleted) {
                            if (deleted) {
                                _this.toastr.success("Metadata deleted.");
                                _this.part.metadata.splice(_this.part.metadata.indexOf(metadata), 1);
                            }
                            else {
                                _this.toastr.error("Error deleting metadata.");
                            }
                        });
                    });
                };
                this.editMetadata = function (metadata) {
                    var modalInstance = _this.modalService.open({
                        templateUrl: "/app/partMetadata/partMetadata.view.html",
                        controller: "app.partMetadata.PartMetadataController as pmvm",
                        keyboard: false,
                        backdrop: 'static',
                        resolve: {
                            metadata: function () {
                                return metadata;
                            },
                            part: function () {
                                return _this.part;
                            }
                        }
                    });
                    modalInstance.result.then(function (result) {
                    }, function (result) {
                        if (result) {
                        }
                    });
                };
                this.reloadMetadata = function () {
                    _this.partMetadataService.getAll(_this.part.id).then(function (metadata) {
                        _this.part.metadata = angular.copy(metadata);
                    });
                };
                this.selectPart = function ($item, $model, $label, $event) {
                    _this.part.name = $item.name;
                    _this.part.description = $item.description;
                    _this.part.sapPartType = $item.sapPartType;
                    _this.part.sapPartNumber = $item.sapPartNumber;
                    _this.part.documentNumber = $item.documentNumber;
                    _this.part.dashNumber = $item.dashNumber;
                    _this.part.mfgPartNumber = $item.mfgPartNumber;
                    _this.part.revisionNumber = 0;
                };
                this.modelErrors = [];
                this.parentPartId = $stateParams["parentPartId"];
                this.instrumentId = this.$stateParams["instrumentId"];
                this.partId = this.$stateParams["id"];
                this.isBusy = true;
                this.loadPartActionTypes()
                    .then(this.loadPartTypes)
                    .then(this.loadPart)
                    .then(this.loadPartNodes)
                    .then(function () {
                    _this.isBusy = false;
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            PartController.$inject = [
                "$state",
                "$stateParams",
                "app.services.PartService",
                "app.services.PartActionService",
                "app.services.InstrumentService",
                "app.services.PartTypeService",
                "app.services.PartActionTypeService",
                "app.services.PartMetadataService",
                "app.services.DialogService",
                "$q",
                "$location",
                "$scope",
                "toastr",
                "$uibModal"
            ];
            return PartController;
        }());
        angular.module("app.parts")
            .controller("app.parts.PartController", PartController);
    })(parts = app.parts || (app.parts = {}));
})(app || (app = {}));
//# sourceMappingURL=part.controller.js.map