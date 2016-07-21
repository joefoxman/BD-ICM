// ReSharper disable once InconsistentNaming
module app.instruments {
    "use strict";

    interface IInstrumentHistoryScope {
        instrument: models.IInstrument;
        commits: models.IInstrumentCommit[];
        isBusy: boolean;
        exportToExcel(commit: models.IInstrumentCommit): void;
        canDiff(): boolean;
        compare(): void;
    }

    class InstrumentHistoryController implements IInstrumentHistoryScope {
        instrument: models.IInstrument;
        isBusy: boolean;
        instrumentId: number;
        commits: models.IInstrumentCommit[];

        static $inject = [
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

        constructor(private $rootScope: ng.IScope,
            private $state: angular.ui.IStateService,
            private $stateParams: angular.ui.IStateParamsService,
            private instrumentService: services.IInstrumentService,
            private instrumentCommitService: services.IInstrumentCommitService,
            private dialogService: services.IDialogService,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope,
            private toastr: angular.toastr.IToastrService) {

            this.instrumentId = this.$stateParams["instrumentId"];
            this.commits = [];

            this.isBusy = true;
            this.instrumentService.get(this.instrumentId).then((instrument: models.IInstrument) => {
                this.instrument = angular.copy(instrument);
            }).then(() => {
                return this.instrumentCommitService.getAll(this.instrumentId).then((commits: models.IInstrumentCommit[]) => {
                    angular.copy(commits, this.commits);
                    this.isBusy = false;
                });
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
                this.isBusy = false;
            });
        }

        canDiff = (): boolean => {
            var selected = _.filter(this.commits, (item: models.IInstrumentCommit) => {
                return item.isSelected;
            });
            return (selected.length === 2);
        }

        compare = (): void => {
            var selected = _.filter(this.commits, (item: models.IInstrumentCommit) => {
                return item.isSelected;
            });
            this.$state.go("root.instrument.compare",
                {
                    id: this.instrument.instrumentId,
                    fromVersion: selected[1].id,
                    toVersion: selected[0].id
                });
        }

        exportToExcel = (change: models.IInstrumentCommit) => {
            this.instrumentService.exportToExcel(this.instrument, change.effectiveTo).then(() => {
                this.toastr.info("Export complete.");
            });
        }
    }

    angular.module("app.instruments")
        .controller("app.instruments.InstrumentHistoryController", InstrumentHistoryController);

}