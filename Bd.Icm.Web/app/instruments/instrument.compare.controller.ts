// ReSharper disable once InconsistentNaming
module app.instruments {
    "use strict";

    interface IInstrumentCompareScope {
        instrument: models.IInstrument;
        changes: models.IInstrumentDiff;
        isBusy: boolean;
        exportToExcel(change: models.IPartChange): void;
    }

    class InstrumentCompareController implements IInstrumentCompareScope {
        instrument: models.IInstrument;
        isBusy: boolean;
        instrumentId: number;
        changes: models.IInstrumentDiff;
        fromVersion: number;
        toVersion: number;

        static $inject = [
            "$rootScope",
            "$state",
            "$stateParams",
            "app.services.InstrumentService",
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
            private dialogService: services.IDialogService,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private toastr: angular.toastr.IToastrService) {

            this.instrumentId = this.$stateParams["instrumentId"];
            this.fromVersion = this.$stateParams["fromVersion"];
            this.toVersion = this.$stateParams["toVersion"];

            this.isBusy = true;
            this.instrumentService.get(this.instrumentId).then((instrument: models.IInstrument) => {
                this.instrument = angular.copy(instrument);
            }, (response) => {
                this.isBusy = false;
                this.toastr.error(response.data.message);
            }).then(() => {
                return this.instrumentService.getDiff(this.instrumentId, this.fromVersion, this.toVersion).then((changes: models.IInstrumentDiff) => {
                    this.changes = angular.copy(changes);
                    this.isBusy = false;
                });
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
                this.isBusy = false;
            });
        }

        exportToExcel = (change: models.IPartChange) => {
            this.instrumentService.exportToExcel(this.instrument, change.effectiveTo).then(() => {
                this.toastr.info("Export complete.");
            });
        }
    }

    angular.module("app.instruments")
        .controller("app.instruments.InstrumentCompareController", InstrumentCompareController);

}