// ReSharper disable once InconsistentNaming
module app.instruments {
    "use strict";

    interface IInstrumentCopyScope {
        instrument: models.IInstrument;
        modelErrors: string[];
        save(close: boolean): void;
        cancel(): void;
        instrumentTypes: models.IInstrumentType[];
        isBusy: boolean;
}

    class InstrumentCopyController implements IInstrumentCopyScope {
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
            "instrument",
            "$uibModalInstance",
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
            instrument: models.IInstrument,
            private $modalInstance: angular.ui.bootstrap.IModalServiceInstance,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private toastr: angular.toastr.IToastrService) {

            this.modelErrors = [];
            this.instrumentTypes = [];
            this.instrument = angular.copy(instrument);
            this.instrument.type = "";
            this.instrument.nickName = "";
            this.instrument.serialNumber = "";

            this.isBusy = true;
            this.loadInstrumentTypes().then(() => {
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

        save = (close: boolean): void => {
            this.isBusy = true;
            this.instrumentService.copy(this.instrument).then((instrument: models.IInstrument) => {
                this.instrument = angular.copy(instrument);
                this.isBusy = false;
                this.toastr.success("Instrument copy complete!");
                this.$modalInstance.close(instrument);
            }, (response: any) => {
                if (response.data.modelState) {
                    this.modelErrors.length = 0;
                    _.each(response.data.modelState, (error: string) => {
                        this.modelErrors.push(error);
                    });
                } else {
                    this.toastr.error(response.data.message);
                }
                this.isBusy = false;
            });
        }

        cancel = () => {
            this.$modalInstance.dismiss();
        };

    }

    angular.module("app.instruments")
        .controller("app.instruments.InstrumentCopyController", InstrumentCopyController);

}