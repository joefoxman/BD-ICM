// ReSharper disable once InconsistentNaming
module app.parts {
    "use strict";

    interface IPartScope {
        part: models.IPart;
        modelErrors: string[];
        save(): void;
        saveNew(): void;
        addPart(): void;
        cancel(): void;
        editPart(part: models.IPart);
        partTypes: models.IPartType[];
        partActionTypes: models.IPartActionType[];
        breadcrumbs: models.IBreadcrumb[];
        getParts(key: string);
        isBusy: boolean;
        addAction(): void;
        addMetadata(): void;
        deleteAction(action: models.IPartAction): void;
        deleteMetadata(metadata: models.IPartMetadata): void;
        selectPart($item: any, $model: any, $label: any, $event: any);
    }

    class PartController implements IPartScope {
        part: models.IPart;
        modelErrors: string[];
        partTypes: models.IPartType[];
        partActionTypes: models.IPartActionType[];
        breadcrumbs: models.IBreadcrumb[];
        parentPartId: number;
        instrumentId: number;
        partId: number;
        isBusy: boolean;

        static $inject = [
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

        constructor(private $state: angular.ui.IStateService,
            private $stateParams: angular.ui.IStateParamsService,
            private partService: services.IPartService,
            private partActionService: services.IPartActionService,
            private instrumentService: services.IInstrumentService,
            private partTypeService: services.IPartTypeService,
            private partActionTypeService: services.IPartActionTypeService,
            private partMetadataService: services.IPartMetadataService,
            private dialogService: services.IDialogService,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private toastr: angular.toastr.IToastrService,
            private modalService: angular.ui.bootstrap.IModalService) {

            this.modelErrors = [];
            this.parentPartId = $stateParams["parentPartId"];
            this.instrumentId = this.$stateParams["instrumentId"];
            this.partId = this.$stateParams["id"];
            this.isBusy = true;
            this.loadPartActionTypes()
                .then(this.loadPartTypes)
                .then(this.loadPart)
                .then(this.loadPartNodes)
                .then(() => {
                    this.isBusy = false;
                }).catch((reason) => {
                    this.toastr.error(reason.statusText);
                    this.isBusy = false;
                });
        }

        private loadPart = (): ng.IPromise<any> => {
            if (this.partId) {
                return this.loadPartCore(this.partId);
            } else {
                return this.partService.create().then((part: models.IPart) => {
                    this.part = angular.copy(part);
                    this.setParent(this.part, this.parentPartId, this.instrumentId);
                });
            }
        }

        private loadPartActionTypes = (): ng.IPromise<void> => {
            return this.partActionTypeService.getAll().then((types: models.IPartActionType[]) => {
                this.partActionTypes = angular.copy(types);
            });
        }

        private loadPartTypes = (): ng.IPromise<void> => {
            return this.partTypeService.getAll().then((types: models.IPartType[]) => {
                this.partTypes = angular.copy(types);
            });
        }

        private loadPartNodes = (): ng.IPromise<void> => {
            return this.partService.getNodes(this.instrumentId, this.parentPartId).then((nodes: models.IPartNode[]) => {
                this.createBreadCrumbs(nodes);
            });
        }

        private loadPartCore = (id: number): ng.IPromise<any> => {
            return this.partService.get(id).then((part: models.IPart) => {
                this.part = angular.copy(part);
                this.setParent(this.part, this.parentPartId, this.instrumentId);
            });
        }

        setParent = (obj: models.IPart, parentPartId: number, instrumentId: number) => {
            if (parentPartId) {
                obj.parentPartId = parentPartId;
            } else {
                obj.instrumentId = instrumentId;
            }
        }

        cancel = (): void => {
            this.$state.go(this.previousState().route, this.previousState().params);
        }

        deletePart = (part: models.IPart) => {
            this.dialogService.askYesNo("Delete Part", "Are you sure you want to delete part [" + part.name + "]?").then((result: services.DialogResult) => {
                if (result === services.DialogResult.Yes) {
                    this.partService.delete(part).then((deleted: boolean) => {
                        if (deleted) {
                            this.toastr.success("Part deleted.");
                            this.part.parts.splice(this.part.parts.indexOf(part),1);
                        } else {
                            this.toastr.error("Error deleting part.");
                        }
                    });
                }
            });
        }

        saveNew = (): void => {
            this.partService.save(this.part).then(
                (part: models.IPart) => {
                    this.toastr.success("Part saved.");
                    this.part = angular.copy(part);
                    this.partService.create().then((newPart: models.IPart) => {
                        this.part = angular.copy(newPart);
                        this.setParent(this.part, this.parentPartId, this.instrumentId);
                    });
                },
                (response: any) => {
                    if (response.data.modelState) {
                        this.modelErrors.length = 0;
                        _.each(response.data.modelState, (error: string) => {
                            this.modelErrors.push(error);
                        });
                        this.toastr.warning("Please check validation errors.");
                    } else {
                        this.toastr.error(response.data.message);
                    }
                });
        }

        save = (): void => {
            this.partService.save(this.part).then(
                (part: models.IPart) => {
                    this.toastr.success("Part saved.");
                    this.part = angular.copy(part);
                },
                (response: any) => {
                    if (response.data.modelState) {
                        this.modelErrors.length = 0;
                        _.each(response.data.modelState, (error: string) => {
                            this.modelErrors.push(error);
                        });
                        this.toastr.warning("Please check validation errors.");
                    } else {
                        this.toastr.error(response.data.message);
                    }
                });
        }

        addPart = () => {
            this.partService.create().then((part: models.IPart) => {
                this.part.parts.push(part);
                this.$state.go("root.instrument.part", {
                    id: null, parentPartId: this.part.id
                }, { reload: true });
            });
        }

        editPart = (part: models.IPart) => {
            this.$state.go("root.instrument.part", { id: part.id, parentPartId: this.part.id }, { reload: true });
        }

        createBreadCrumbs = (nodes: models.IPartNode[]) => {
            this.breadcrumbs = [];
            _.each(nodes, (node: models.IPartNode) => {
                var bc = new models.Breadcrumb();
                this.breadcrumbs.push(bc);
                bc.title = node.name;
                if (node.partId) {
                    bc.route = "root.instrument.part";
                    bc.params = { id: node.partId, parentPartId: node.parentPartId };
                } else {
                    bc.route = "root.instrument.edit";
                    bc.params = { instrumentId: node.instrumentId };
                }
                bc.sref = bc.route + "(" + JSON.stringify(bc.params) + ")";
            });
        }

        previousState = (): models.IBreadcrumb => {
            var lastState = this.breadcrumbs[this.breadcrumbs.length - 1];
            return lastState;
        }

        getParts = (key: string): ng.IPromise<models.IPart[]> => {
            var deferred = this.$q.defer();
            this.partService.searchNames(key).then((parts: models.IPart[]): any => {
                deferred.resolve(parts);
            });
            return deferred.promise;
        }

        addAction = () => {
            var modalInstance = this.modalService.open({
                templateUrl: "/app/partActions/partAction.view.html",
                controller: "app.partActions.PartActionController as pavm",
                keyboard: false,
                backdrop: 'static',
                resolve: {
                    action: (): ng.IPromise<models.IPartAction> => {
                        return this.partActionService.create().then((action: models.IPartAction) => {
                            return action;
                        });
                    },
                    part: (): models.IPart => {
                        return this.part;
                    }
                }
            });
            modalInstance.result.then((result: any) => {
                this.reloadActions();
            }, (result: any) => {
                if (result) {

                }

            });
        }

        reloadActions = () => {
            this.partActionService.getAll(this.part.id).then((actions: models.IPartAction[]) => {
                this.part.actions = angular.copy(actions);
            });
        }

        editAction = (action: models.IPartAction) => {
            var modalInstance = this.modalService.open({
                templateUrl: "/app/partActions/partAction.view.html",
                controller: "app.partActions.PartActionController as pavm",
                keyboard: false,
                backdrop: 'static',
                resolve: {
                    action: (): models.IPartAction => {
                        return action;
                    },
                    part: (): models.IPart => {
                        return this.part;
                    }
                }
            });
            modalInstance.result.then((result: any) => {
            }, (result: any) => {
                if (result) {

                }

            });
        }

        deleteAction = (action: models.IPartAction) => {
            this.dialogService.askYesNo("Delete Action", "Are you sure you want to delete this action?").then((result: services.DialogResult) => {
                if (result !== services.DialogResult.Yes) return;
                this.partActionService.delete(this.part.id, action).then((deleted: boolean) => {
                    if (deleted) {
                        this.toastr.success("Action deleted.");
                        this.part.actions.splice(this.part.actions.indexOf(action),1);
                    } else {
                        this.toastr.error("Error deleting action.");
                    }
                });
            });
        }

        addMetadata = () => {
            var modalInstance = this.modalService.open({
                templateUrl: "/app/partMetadata/partMetadata.view.html",
                controller: "app.partMetadata.PartMetadataController as pmvm",
                keyboard: false,
                scope: this.$scope,
                backdrop: 'static',
                resolve: {
                    metadata: (): ng.IPromise<models.IPartMetadata> => {
                        return this.partMetadataService.create().then((metadata: models.IPartMetadata) => {
                            return metadata;
                        });
                    },
                    part: (): models.IPart => {
                        return this.part;
                    }
                }
            });
            modalInstance.result.then((result: any) => {
                this.reloadMetadata();
            }, (result: any) => {
                if (result) {

                }

            });
        }

        deleteMetadata = (metadata: models.IPartMetadata) => {
            this.dialogService.askYesNo("Delete Metadata", `Are you sure you want to delete the metadata with key ${metadata.metaKey}?`).then((result: services.DialogResult) => {
                if (result !== services.DialogResult.Yes) return;
                this.partMetadataService.delete(this.part.id, metadata).then((deleted: boolean) => {
                    if (deleted) {
                        this.toastr.success("Metadata deleted.");
                        this.part.metadata.splice(this.part.metadata.indexOf(metadata), 1);
                    } else {
                        this.toastr.error("Error deleting metadata.");
                    }
                });
            });
        }

        editMetadata = (metadata: models.IPartMetadata) => {
            var modalInstance = this.modalService.open({
                templateUrl: "/app/partMetadata/partMetadata.view.html",
                controller: "app.partMetadata.PartMetadataController as pmvm",
                keyboard: false,
                backdrop: 'static',
                resolve: {
                    metadata: (): models.IPartMetadata => {
                        return metadata;
                    },
                    part: (): models.IPart => {
                        return this.part;
                    }
                }
            });
            modalInstance.result.then((result: any) => {
            }, (result: any) => {
                if (result) {

                }

            });
        }

        reloadMetadata = () => {
            this.partMetadataService.getAll(this.part.id).then((metadata: models.IPartMetadata[]) => {
                this.part.metadata = angular.copy(metadata);
            });
        }

        selectPart = ($item: any, $model: any, $label: any, $event: any) => {
            this.part.name = $item.name;
            this.part.description = $item.description;
            this.part.sapPartType = $item.sapPartType;
            this.part.sapPartNumber = $item.sapPartNumber;
            this.part.documentNumber = $item.documentNumber;
            this.part.dashNumber = $item.dashNumber;
            this.part.mfgPartNumber = $item.mfgPartNumber;
            this.part.revisionNumber = 0;
        }

    }

    angular.module("app.parts")
        .controller("app.parts.PartController", PartController);

}