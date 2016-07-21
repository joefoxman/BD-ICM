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
                this.isInRole = function (roles) {
                    if (!roles)
                        return false;
                    var roleValues = _.map(roles, function (item) {
                        return app.enums.RoleType[item];
                    });
                    return _this.isInRoleType(roleValues);
                };
                this.isInRoleType = function (roles) {
                    if (!roles)
                        return false;
                    _this.user = _this.authenticationService.currentUser();
                    if (_this.user == null)
                        return false;
                    var matches = _.intersection(roles, _this.user.roles);
                    return matches.length > 0;
                };
            }
            AuthorizationService.$inject = ["app.services.AuthenticationService"];
            return AuthorizationService;
        }());
        function factory(authenticationService) {
            return new AuthorizationService(authenticationService);
        }
        factory.$inject = ["app.services.AuthenticationService"];
        angular.module("app.services")
            .factory("app.services.AuthorizationService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=authorization.service.js.map