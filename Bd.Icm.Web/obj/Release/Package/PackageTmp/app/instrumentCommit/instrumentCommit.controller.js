// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var instrumentCommit;
    (function (instrumentCommit) {
        "use strict";
        var InstrumentCommitController = (function () {
            function InstrumentCommitController(instrumentCommitService, dialogService, authService, $modalInstance, toastr, instrument) {
                var _this = this;
                this.instrumentCommitService = instrumentCommitService;
                this.dialogService = dialogService;
                this.authService = authService;
                this.$modalInstance = $modalInstance;
                this.toastr = toastr;
                this.commit = function () {
                    _this.dialogService.askYesNo("Confirm Commit", "Are you sure you want to commit your changes?").then(function (result) {
                        if (result !== app.services.DialogResult.Yes)
                            return;
                        _this.isBusy = true;
                        var lastChange = _this.getLastSelectedChange();
                        _this.instrumentCommitService.commitChanges(_this.instrumentCommit, lastChange).then(function (instrument) {
                            _this.instrument = angular.copy(instrument);
                            _this.isBusy = false;
                            _this.toastr.success("Changes have been committed!");
                            _this.$modalInstance.close(instrument);
                        }, function (response) {
                            _this.toastr.error("Error committing changes.");
                            _this.isBusy = false;
                        });
                    });
                };
                this.canCommit = function () {
                    return _.any(_this.changes, function (item) {
                        return item.isSelected;
                    });
                };
                this.getLastSelectedChange = function () {
                    var lastSelectedIndex = _.findLastIndex(_this.changes, function (item) {
                        return item.isSelected;
                    });
                    return lastSelectedIndex === -1 ? null : _this.changes[lastSelectedIndex];
                };
                this.rowSelected = function (selectedRow) {
                    _.each(_this.changes, function (item) {
                        item.isSelected = (item.effectiveFrom <= selectedRow.effectiveFrom);
                    });
                };
                this.cancel = function () {
                    _this.$modalInstance.dismiss();
                };
                if (this.authService.isInRoleType([app.enums.RoleType.Committer]) === false)
                    return;
                this.instrumentCommit = new app.models.InstrumentCommit();
                this.instrumentCommit.instrumentId = instrument.instrumentId;
                this.changes = [];
                this.instrument = instrument;
                this.isBusy = true;
                this.instrumentCommitService.getUncommittedChanges(this.instrument.instrumentId).then(function (changes) {
                    _this.changes = angular.copy(changes);
                }).finally(function () {
                    _this.isBusy = false;
                });
            }
            InstrumentCommitController.$inject = [
                "app.services.InstrumentCommitService",
                "app.services.DialogService",
                "app.services.AuthorizationService",
                "$uibModalInstance",
                "toastr",
                "instrument"];
            return InstrumentCommitController;
        }());
        angular.module("app.instrumentCommit")
            .controller("app.instrumentCommit.InstrumentCommitController", InstrumentCommitController);
    })(instrumentCommit = app.instrumentCommit || (app.instrumentCommit = {}));
})(app || (app = {}));
//# sourceMappingURL=instrumentCommit.controller.js.map