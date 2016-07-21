// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var UserService = (function () {
            function UserService($http) {
                var _this = this;
                this.$http = $http;
                this.save = function (user) {
                    return _this.$http.post("/api/users/save", user)
                        .then(function (response) {
                        return app.models.UserDto.mapToObj(response.data);
                    });
                };
                this.create = function () {
                    return _this.$http.get("/api/users/create")
                        .then(function (response) {
                        return app.models.UserDto.mapToObj(response.data);
                    });
                };
                this.delete = function (user) {
                    return _this.$http.delete("/api/users/delete/" + user.id)
                        .then(function (response) {
                        return app.models.UserDto.mapToObj(response.data);
                    });
                };
            }
            UserService.prototype.getAll = function () {
                return this.$http.get("/api/users")
                    .then(function (response) {
                    return _.map(response.data, function (dto) {
                        return app.models.UserDto.mapToObj(dto);
                    });
                });
            };
            UserService.prototype.get = function (id) {
                return this.$http.get("/api/users/" + id)
                    .then(function (response) {
                    return app.models.UserDto.mapToObj(response.data);
                });
            };
            UserService.prototype.getCurrent = function () {
                return this.$http.get("/api/users/current")
                    .then(function (response) {
                    return app.models.UserDto.mapToObj(response.data);
                });
            };
            UserService.prototype.getByUsername = function (username) {
                return this.$http.get("/api/users/" + encodeURIComponent(username))
                    .then(function (response) {
                    return app.models.UserDto.mapToObj(response.data);
                });
            };
            UserService.$inject = ["$http"];
            return UserService;
        }());
        function factory($http) {
            return new UserService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.UserService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=user.service.js.map