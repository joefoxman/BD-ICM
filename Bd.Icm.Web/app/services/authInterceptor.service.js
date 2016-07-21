// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var AuthInterceptorService = (function () {
            function AuthInterceptorService($q, $location, localStorageService) {
                var _this = this;
                this.$q = $q;
                this.$location = $location;
                this.localStorageService = localStorageService;
                this.request = function (config) {
                    config.headers = config.headers || {};
                    var authData = _this.localStorageService.get("authorizationData");
                    if (authData) {
                        config.headers["Authorization"] = "Bearer " + authData.token;
                    }
                    return config;
                };
                this.responseError = function (rejection) {
                    if (rejection.status === 401) {
                        _this.$location.path("/Account/Login");
                    }
                    return _this.$q.reject(rejection);
                };
            }
            AuthInterceptorService.$inject = ["$q", "$location", "localStorageService"];
            return AuthInterceptorService;
        })();
        function factory($q, $location, localStorageService) {
            return new AuthInterceptorService($q, $location, localStorageService);
        }
        factory.$inject = ["$q", "$location", "localStorageService"];
        angular.module("app.services")
            .factory("app.services.AuthInterceptorService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
