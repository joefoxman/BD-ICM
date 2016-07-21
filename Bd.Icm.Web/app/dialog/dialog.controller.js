var app;
(function (app) {
    var dialog;
    (function (dialog) {
        "use strict";
        var DialogController = (function () {
            function DialogController(message, title, $modalInstance) {
                var _this = this;
                this.$modalInstance = $modalInstance;
                this.yes = function () {
                    _this.$modalInstance.close(app.services.DialogResult.Yes);
                };
                this.no = function () {
                    _this.$modalInstance.close(app.services.DialogResult.No);
                };
                this.cancel = function () {
                    _this.$modalInstance.dismiss(app.services.DialogResult.Cancel);
                };
                this.message = message;
                this.title = title;
            }
            DialogController.$inject = ['message', 'title', '$uibModalInstance'];
            return DialogController;
        }());
        angular.module("app.dialog")
            .controller("app.dialog.DialogController", DialogController);
    })(dialog = app.dialog || (app.dialog = {}));
})(app || (app = {}));
//# sourceMappingURL=dialog.controller.js.map