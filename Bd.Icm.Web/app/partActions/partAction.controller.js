// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var partActions;
    (function (partActions) {
        "use strict";
        var PartActionController = (function () {
            function PartActionController(authorizationService, $modalInstance, toastr, action, part, partActionService, partActionTypeService) {
                var _this = this;
                this.authorizationService = authorizationService;
                this.$modalInstance = $modalInstance;
                this.toastr = toastr;
                this.partActionService = partActionService;
                this.partActionTypeService = partActionTypeService;
                this.openActionDate = function () {
                    _this.actionDateOpened = true;
                };
                this.save = function () {
                    _this.partActionService.save(_this.part.id, _this.partAction).then(function (action) {
                        _this.partAction = action;
                        _this.$modalInstance.close();
                    }, function (response) {
                        if (response.data.modelState) {
                            _this.modelErrors.length = 0;
                            _.each(response.data.modelState, function (error) {
                                _this.modelErrors.push(error);
                            });
                        }
                        else {
                            _this.toastr.error(response.data.message);
                        }
                    });
                };
                this.cancel = function () {
                    _this.$modalInstance.dismiss();
                };
                this.isReadOnly = function () {
                    return _this.authorizationService.isInRole(["ReadOnly"]);
                };
                this.partAction = action;
                this.part = part;
                this.modelErrors = [];
                this.partActionTypeService.getAll().then(function (actions) {
                    _this.actionTypes = angular.copy(actions);
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                });
            }
            PartActionController.$inject = ["app.services.AuthorizationService",
                "$uibModalInstance",
                "toastr",
                "action",
                "part",
                "app.services.PartActionService",
                "app.services.PartActionTypeService"];
            return PartActionController;
        }());
        angular.module("app.partActions")
            .controller("app.partActions.PartActionController", PartActionController);
    })(partActions = app.partActions || (app.partActions = {}));
})(app || (app = {}));
//# sourceMappingURL=partAction.controller.js.map