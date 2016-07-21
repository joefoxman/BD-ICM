// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var AuthenticationService = (function () {
            function AuthenticationService($q, $http, localStorageService, userService) {
                var _this = this;
                this.$q = $q;
                this.$http = $http;
                this.localStorageService = localStorageService;
                this.userService = userService;
                this.currentUserKey = "currentUser";
                this.loadUser = function () {
                    _this.logout();
                    return _this.userService.getCurrent().then(function (user) {
                        _this.localStorageService.set(_this.currentUserKey, user);
                        return user;
                    });
                };
                this.currentUser = function () {
                    return _this.localStorageService.get(_this.currentUserKey);
                };
                this.logout = function () {
                    _this.localStorageService.remove(_this.currentUserKey);
                };
            }
            AuthenticationService.$inject = [
                "$q",
                "$http",
                "localStorageService",
                "app.services.UserService"
            ];
            return AuthenticationService;
        }());
        function factory($q, $http, localStorageService, userService) {
            return new AuthenticationService($q, $http, localStorageService, userService);
        }
        factory.$inject = [
            "$q", "$http",
            "localStorageService",
            "app.services.UserService"
        ];
        angular.module("app.services")
            .factory("app.services.AuthenticationService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=authentication.service.js.map