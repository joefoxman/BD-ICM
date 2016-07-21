// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var PartActionService = (function () {
            function PartActionService($http) {
                var _this = this;
                this.$http = $http;
                this.save = function (partId, partAction) {
                    var dto = app.models.PartAction.mapToDto(partAction);
                    return _this.$http.post("/api/partActions/save/" + partId, dto).then(function (response) {
                        return app.models.PartActionDto.mapToObj(response.data);
                    });
                };
                this.delete = function (partId, partAction) {
                    return _this.$http.delete("/api/partActions/delete/" + partId + "/" + partAction.id).success(function (response) {
                        return true;
                    }).error(function () {
                        return false;
                    });
                };
            }
            PartActionService.prototype.getAll = function (partId) {
                return this.$http.get("/api/partActions/getAll/" + partId)
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.PartActionDto.mapToObj(item));
                    });
                    return items;
                });
            };
            PartActionService.prototype.create = function () {
                return this.$http.get("/api/partActions/new")
                    .then(function (response) {
                    return app.models.PartActionDto.mapToObj(response.data);
                });
            };
            PartActionService.prototype.get = function (partId, partActionId) {
                return this.$http.get("/api/partActions/" + partId + "/" + partActionId)
                    .then(function (response) {
                    return app.models.PartActionDto.mapToObj(response.data);
                });
            };
            PartActionService.$inject = ["$http"];
            return PartActionService;
        }());
        function factory($http) {
            return new PartActionService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.PartActionService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=partAction.service.js.map