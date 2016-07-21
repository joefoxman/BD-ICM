// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var AuthorizationService = (function () {
            function AuthorizationService(authenticationService) {
                var _this = this;
                this.authenticationService = authenticationService;
                this.isAuthorized = function (permission) {
                    _this.user = _this.authenticationService.currentUser();
                    if (!_this.user)
                        return false;
                    switch (permission) {
                        case app.enums.AppPermissions.MaintainSecurity:
                            return _this.user.role === app.enums.RoleType.Administrator;
                        default:
                            return false;
                    }
                };
            }
            AuthorizationService.$inject = ["app.services.AuthenticationService"];
            return AuthorizationService;
        })();
        function factory(authenticationService) {
            return new AuthorizationService(authenticationService);
        }
        factory.$inject = ["app.services.AuthenticationService"];
        angular.module("app.services")
            .factory("app.services.AuthorizationService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
