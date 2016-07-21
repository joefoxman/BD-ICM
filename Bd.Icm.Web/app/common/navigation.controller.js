// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var common;
    (function (common) {
        "use strict";
        var NavigationController = (function () {
            function NavigationController(authenticationService, authorizationService) {
                var _this = this;
                this.authenticationService = authenticationService;
                this.authorizationService = authorizationService;
                this.isInRole = function (role) {
                    return _this.authorizationService.isInRole(role);
                };
                this.isReadOnly = function () {
                    return _this.isInRole(['ReadOnly']);
                };
                this.cacheBuster = Date.now().toString();
                this.permissions = app.enums.AppPermissions.Default;
                this.authenticationService.loadUser().then(function (user) {
                    _this.currentUser = angular.copy(user);
                    _this.currentDate = new Date();
                });
            }
            NavigationController.$inject = [
                "app.services.AuthenticationService",
                "app.services.AuthorizationService",
            ];
            return NavigationController;
        }());
        angular.module("app.common")
            .controller("app.common.NavigationController", NavigationController);
    })(common = app.common || (app.common = {}));
})(app || (app = {}));
//# sourceMappingURL=navigation.controller.js.map