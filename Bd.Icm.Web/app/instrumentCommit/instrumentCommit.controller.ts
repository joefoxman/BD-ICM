// ReSharper disable once InconsistentNaming
module app.instrumentCommit {
    "use strict";

    interface IInstrumentCommitScope {
        commit();
        instrumentCommit: models.IInstrumentCommit;
        instrument: models.IInstrument;
        cancel(): void;
        canCommit(): boolean;
        changes: models.IUncommittedChange[];
        isBusy: boolean;
        note: string;
        rowSelected(selectedItem: models.IUncommittedChange);
    }

    class InstrumentCommitController implements IInstrumentCommitScope {
        instrumentCommit: models.IInstrumentCommit;
        instrument: models.IInstrument;
        changes: models.IUncommittedChange[];
        isBusy: boolean;
        note: string;

        static $inject = [
            "app.services.InstrumentCommitService",
            "app.services.DialogService",
            "app.services.AuthorizationService",
            "$uibModalInstance",
            "toastr",
            "instrument"];

        constructor(private instrumentCommitService: services.IInstrumentCommitService,
            private dialogService: services.IDialogService,
            private authService: services.IAuthorizationService,
            private $modalInstance: angular.ui.bootstrap.IModalServiceInstance,
            private toastr: angular.toastr.IToastrService,
            instrument: models.IInstrument) {

            if (this.authService.isInRoleType([enums.RoleType.Committer]) === false) return;

            this.instrumentCommit = new models.InstrumentCommit();
            this.instrumentCommit.instrumentId = instrument.instrumentId;

            this.changes = [];
            this.instrument = instrument;
            this.isBusy = true;
            this.instrumentCommitService.getUncommittedChanges(this.instrument.instrumentId).then((changes: models.IUncommittedChange[]) => {
                this.changes = angular.copy(changes);
            }).finally(() => {
                this.isBusy = false;
            });
        }

        commit = () => {
            this.dialogService.askYesNo("Confirm Commit", "Are you sure you want to commit your changes?").then((result: services.DialogResult) => {
                if (result !== services.DialogResult.Yes) return;
                this.isBusy = true;
                var lastChange = this.getLastSelectedChange();
                this.instrumentCommitService.commitChanges(this.instrumentCommit, lastChange).then((instrument: models.IInstrument) => {
                    this.instrument = angular.copy(instrument);
                    this.isBusy = false;
                    this.toastr.success("Changes have been committed!");
                    this.$modalInstance.close(instrument);
                }, (response: any) => {
                    this.toastr.error("Error committing changes.");
                    this.isBusy = false;
                });
            });
        };

        canCommit = (): boolean => {
            return _.any(this.changes, (item: models.IUncommittedChange) => {
                return item.isSelected;
            });
        }

        private getLastSelectedChange = (): models.IUncommittedChange => {
            var lastSelectedIndex = _.findLastIndex(this.changes, (item: models.IUncommittedChange) => {
                return item.isSelected;
            });
            return lastSelectedIndex === -1 ? null : this.changes[lastSelectedIndex];
        }

        rowSelected = (selectedRow: models.IUncommittedChange) => {
            _.each(this.changes, (item: models.IUncommittedChange) => {
                item.isSelected = (item.effectiveFrom <= selectedRow.effectiveFrom);
            });
        }

        cancel = () => {
            this.$modalInstance.dismiss();
        };
    }

    angular.module("app.instrumentCommit")
        .controller("app.instrumentCommit.InstrumentCommitController", InstrumentCommitController);

}
