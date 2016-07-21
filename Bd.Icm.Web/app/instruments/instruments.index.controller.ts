// ReSharper disable once InconsistentNaming
module app.instruments {
    "use strict";

    interface IInstrumentIndexScope {
        instruments: models.IInstrument[];
        isBusy: boolean;
        addInstrument(): void;
        copyInstrument(instrument: models.IInstrument): void;
        exportToExcel(instrument: models.IInstrument): void;
    }

    class InstrumentIndexController implements IInstrumentIndexScope {
        instruments: models.IInstrument[];
        isBusy: boolean;

        static $inject = [
            "$state",
            "app.services.InstrumentService",
            "$q",
            "$location",
            "$scope",
            "toastr",
            "$uibModal"
        ];

        constructor(private $state: angular.ui.IStateService,
            private instrumentService: services.IInstrumentService,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private toastr: angular.toastr.IToastrService,
            private modalService: angular.ui.bootstrap.IModalService) {

            this.isBusy = true;

            instrumentService.getAll().then((instruments: models.IInstrument[]) => {
                this.instruments = angular.copy(instruments);
                this.isBusy = false;
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
                this.isBusy = false;
            });
        }

        addInstrument = () => {
            this.$state.go("root.instrument.edit", { id: null }, { reload: true });
        }

        copyInstrument = (instrument: models.IInstrument) => {
            var modalInstance = this.modalService.open({
                templateUrl: "/app/instruments/instrument.copy.view.html",
                controller: "app.instruments.InstrumentCopyController as icvm",
                keyboard: false,
                backdrop: 'static',
                resolve: {
                    instrument: (): models.IInstrument => {
                        return instrument;
                    }
                }
            });
            modalInstance.result.then((result: models.IInstrument) => {
                if (result) {
                    this.$state.go("root.instrument.edit", { instrumentId: result.instrumentId });
                }
            }, (result: any) => {
            });
        }

        exportToExcel = (instrument: models.IInstrument) => {
            this.instrumentService.exportToExcel(instrument, null).then(() => {
                this.toastr.info("Export complete.");
            });
        }

    }

    angular.module("app.instruments")
        .controller("app.instruments.InstrumentIndexController", InstrumentIndexController);

}