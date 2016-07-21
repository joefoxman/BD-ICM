// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var PartService = (function () {
            function PartService($http) {
                var _this = this;
                this.$http = $http;
                this.save = function (part) {
                    var dto = app.models.Part.mapToDto(part);
                    return _this.$http.post("/api/parts/save", dto).then(function (response) {
                        return app.models.PartDto.mapToObj(response.data);
                    });
                };
                this.delete = function (part) {
                    return _this.$http.delete("/api/parts/delete/" + part.id).success(function (response) {
                        return true;
                    }).error(function () {
                        return false;
                    });
                };
            }
            PartService.prototype.searchNames = function (key) {
                var url = "/api/parts/searchNames";
                if (key !== "") {
                    url += "/" + key;
                }
                return this.$http.get(url)
                    .then(function (response) {
                    return response.data;
                });
            };
            PartService.prototype.getAll = function (parentId) {
                return this.$http.get("/api/parts")
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.PartDto.mapToObj(item));
                    });
                    return items;
                });
            };
            PartService.prototype.getNodes = function (instrumentId, partId) {
                return this.$http.get("/api/parts/nodes?instrumentId=" + instrumentId + "&parentPartId=" + partId)
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.PartNodeDto.mapToObj(item));
                    });
                    return items;
                });
            };
            PartService.prototype.create = function () {
                return this.$http.get("/api/parts/new")
                    .then(function (response) {
                    return app.models.PartDto.mapToObj(response.data);
                });
            };
            PartService.prototype.get = function (id) {
                return this.$http.get("/api/parts/" + id)
                    .then(function (response) {
                    return app.models.PartDto.mapToObj(response.data);
                });
            };
            PartService.$inject = ["$http"];
            return PartService;
        }());
        function factory($http) {
            return new PartService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.PartService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=part.service.js.map