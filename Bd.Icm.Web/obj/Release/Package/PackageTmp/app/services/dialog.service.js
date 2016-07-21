var app;
(function (app) {
    var services;
    (function (services) {
        (function (DialogResult) {
            DialogResult[DialogResult["Yes"] = 0] = "Yes";
            DialogResult[DialogResult["No"] = 1] = "No";
            DialogResult[DialogResult["Cancel"] = 2] = "Cancel";
            DialogResult[DialogResult["Ok"] = 3] = "Ok";
        })(services.DialogResult || (services.DialogResult = {}));
        var DialogResult = services.DialogResult;
        var DialogService = (function () {
            function DialogService($q, $modal) {
                var _this = this;
                this.$q = $q;
                this.$modal = $modal;
                this.askYesNo = function (title, message) {
                    var deferred = _this.$q.defer();
                    var modalInstance = _this.$modal.open({
                        templateUrl: '/app/dialog/yesNoCancel.view.html',
                        controller: 'app.dialog.DialogController as vm',
                        resolve: {
                            message: function () { return message; },
                            title: function () { return title; }
                        }
                    });
                    modalInstance.result.then(function (response) {
                        deferred.resolve(response);
                    });
                    return deferred.promise;
                };
            }
            DialogService.$inject = ["$q", "$uibModal"];
            return DialogService;
        }());
        factory.$inject = ["$q", "$uibModal"];
        function factory($q, $modal) {
            return new DialogService($q, $modal);
        }
        angular.module("app.services")
            .factory("app.services.DialogService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=dialog.service.js.map