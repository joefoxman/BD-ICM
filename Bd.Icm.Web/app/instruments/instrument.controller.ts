// ReSharper disable once InconsistentNaming
module app.instruments {
    "use strict";

    interface IInstrumentScope {
        instrument: models.IInstrument;
        modelErrors: string[];
        save(close: boolean): void;
        addPart(): void;
        deletePart(part: models.IPart);
        editPart(part: models.IPart);
        instrumentTypes: models.IInstrumentType[];
        isBusy: boolean;
}

    class InstrumentController implements IInstrumentScope {
        instrument: models.IInstrument;
        modelErrors: string[];
        instrumentTypes: models.IInstrumentType[];
        isBusy: boolean;
        instrumentId: number;

        static $inject = [
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

        constructor(private $rootScope: ng.IScope,
            private $state: angular.ui.IStateService,
            private $stateParams: angular.ui.IStateParamsService,
            private instrumentService: services.IInstrumentService,
            private instrumentTypeService: services.IInstrumentTypeService,
            private partService: services.IPartService,
            private dialogService: services.IDialogService,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private toastr: angular.toastr.IToastrService) {

            this.modelErrors = [];
            this.instrumentTypes = [];
            this.instrumentId = this.$stateParams["instrumentId"];

            this.isBusy = true;
            this.loadInstrumentTypes().then(this.loadInstrument).then(() => {
                this.isBusy = false;
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
                this.isBusy = false;
            });
        }

        private loadInstrumentTypes = (): ng.IPromise<void> => {
            return this.instrumentTypeService.getAll().then((types: models.IInstrumentType[]) => {
                this.instrumentTypes = angular.copy(types);
            });
        }

        private loadInstrument = (): ng.IPromise<void> => {
            if (this.instrumentId) {
                return this.instrumentService.get(this.instrumentId).then((instrument: models.IInstrument) => {
                    this.instrument = angular.copy(instrument);
                });
            } else {
                return this.instrumentService.create().then((instrument: models.IInstrument) => {
                    this.instrument = angular.copy(instrument);
                    this.instrument.sapPartType = this.instrumentTypes[1];
                });
            }
        }

        save = (close: boolean): void => {
            this.instrumentService.save(this.instrument).then(
                (instrument: models.IInstrument) => {
                    this.toastr.success("Instrument saved.");
                    this.$rootScope.$broadcast("instrumentChanged", instrument);
                    if (close) {
                        this.$state.go("root.instruments", {});
                    } else {
                        this.instrument = angular.copy(instrument);
                    }
                },
                (response: any) => {
                    if (response.data.modelState) {
                        this.modelErrors.length = 0;
                        _.each(response.data.modelState, (error: string) => {
                            this.modelErrors.push(error);
                        });
                    } else {
                        this.toastr.error(response.data.message);
                    }
                });
        }

        addPart = () => {
            this.partService.create().then((part: models.IPart) => {
                part.instrumentId = this.instrument.instrumentId;
                this.instrument.parts.push(part);
                this.$state.go("root.instrument.part", { instrumentId: this.instrument.instrumentId, id: null }, { reload: true });
            });
        }

        editPart = (part: models.IPart) => {
            this.$state.go("root.instrument.part", { instrumentId: this.instrument.instrumentId, id: part.id }, { reload: true });
        }

        createBreadCrumbs = (): models.IBreadcrumb[] => {
            var bc = new models.Breadcrumb();
            bc.title = this.instrument.nickName;
            bc.route = "root.instrument";
            bc.params = { id: this.instrument.instrumentId };
            bc.sref = bc.route + "(" + JSON.stringify(bc.params) + ")";
            return [bc];
        }

        deletePart = (part: models.IPart) => {
            this.dialogService.askYesNo("Delete Part", "Are you sure you want to delete part [" + part.name + "]?").then((result: services.DialogResult) => {
                if (result === services.DialogResult.Yes) {
                    this.partService.delete(part).then((deleted: boolean) => {
                        if (deleted) {
                            this.toastr.success("Part deleted.");
                            this.instrument.parts.splice(this.instrument.parts.indexOf(part));
                        } else {
                            this.toastr.error("Error deleting part.");
                        }
                    });
                }
            });
        }
    }

    angular.module("app.instruments")
        .controller("app.instruments.InstrumentController", InstrumentController);

}