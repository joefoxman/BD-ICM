// ReSharper disable once InconsistentNaming
module app.instruments {
    "use strict";

    interface IInstrumentLayoutScope {
        instrument: models.IInstrument;
        changeUsers: models.IChangeUser[];
        changeUsersFormatted: string;
        commit(): void;
        isBusy: boolean;
        searchKey: string;
        searchParts(key: string): ng.IPromise<models.IPartSearchResult[]>;
        editPart($item: any, $model: any, $label: any, $event: any);
        exportToExcel();
}

    class InstrumentLayoutController implements IInstrumentLayoutScope {
        instrument: models.IInstrument;
        isBusy: boolean;
        instrumentId: number;
        searchKey: string;
        changeUsers: models.IChangeUser[];
        changeUsersFormatted: string;

        static $inject = [
            "$rootScope",
            "$scope",
            "$state",
            "$stateParams",
            "app.services.InstrumentService",
            "$interval",
            "toastr",
            "$uibModal"
        ];

        constructor(
            private $rootScope: ng.IScope,
            private $scope: ng.IScope,
            private $state: angular.ui.IStateService,
            private $stateParams: angular.ui.IStateParamsService,
            private instrumentService: services.IInstrumentService,
            private $interval: angular.IIntervalService,
            private toastr: angular.toastr.IToastrService,
            private modalService: angular.ui.bootstrap.IModalService) {

            this.changeUsers = [];
            this.changeUsersFormatted = "";

            this.$rootScope.$on("instrumentChanged", (evt: ng.IAngularEvent, instrument: models.IInstrument) => {
                this.instrument = angular.copy(instrument);
            });
            this.isBusy = true;
            this.loadInstrument().then(() => {
                this.checkForChanges();
                var stop = this.$interval(() => {
                    this.checkForChanges();
                }, 5000);
                this.isBusy = false;
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
                this.isBusy = false;
            });
        }

        private loadInstrument = (): ng.IPromise<void> => {
            this.instrumentId = this.$stateParams["instrumentId"];
            if (this.instrumentId) {
                return this.instrumentService.get(this.instrumentId).then((instrument: models.IInstrument) => {
                    this.instrument = angular.copy(instrument);
                });
            } else {
                return this.instrumentService.create().then((instrument: models.IInstrument) => {
                    this.instrument = angular.copy(instrument);
                });
            }
        }

        checkForChanges = () => {
            this.instrumentService.checkForChanges(this.instrument.instrumentId)
                .then((changeUsers: models.IChangeUser[]) => {
                    this.changeUsers = angular.copy(changeUsers);
                    this.changeUsersFormatted = "";
                    _.each(this.changeUsers, (cu: models.IChangeUser) => {
                        if (this.changeUsersFormatted.length > 0)
                            this.changeUsersFormatted += ", ";
                        var fullName = cu.firstName || "";
                        if (fullName.length > 0)
                            fullName += " ";
                        fullName += cu.lastName;
                        this.changeUsersFormatted += fullName;
                    });
                });
        }

        commit = () => {
            var modalInstance = this.modalService.open({
                templateUrl: "/app/instrumentCommit/instrumentCommit.view.html?" + Math.random().toString(36).slice(2),
                controller: "app.instrumentCommit.InstrumentCommitController as icvm",
                keyboard: false,
                backdrop: 'static',
                size: 'lg',
                resolve: {
                    instrument: (): models.IInstrument => {
                        return this.instrument;
                    }
                }
            });
            modalInstance.result.then((result: models.IInstrument) => {
                if (result) {
                    this.instrument = result;
                    this.$rootScope.$broadcast("instrumentChanged", this.instrument);
                    this.$state.go("root.instrument.edit", { instrumentId: result.instrumentId });
                }
            }, (result: any) => {
            });
        }

        searchParts = (searchKey: string): ng.IPromise<models.IPartSearchResult[]> => {
            return this.instrumentService.searchParts(this.instrumentId, searchKey).then((results: models.IPartSearchResult[]) => {
                return results;
            });
        }

        editPart = ($item: any, $model: any, $label: any, $event: any) => {
            this.$state.go("root.instrument.part", { instrumentId: this.instrument.instrumentId, id: $item.partId, parentPartId: $item.parentPartId }, { reload: true });
        }

        exportToExcel = () => {
            this.instrumentService.exportToExcel(this.instrument, null).then(() => {
                this.toastr.info("Export complete.");
            });
        }
    }

    angular.module("app.instruments")
        .controller("app.instruments.InstrumentLayoutController", InstrumentLayoutController);

}