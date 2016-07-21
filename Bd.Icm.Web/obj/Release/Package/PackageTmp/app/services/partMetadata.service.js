// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var PartMetadataService = (function () {
            function PartMetadataService($http) {
                var _this = this;
                this.$http = $http;
                this.save = function (partId, partMetadata) {
                    var dto = app.models.PartMetadata.mapToDto(partMetadata);
                    return _this.$http.post("/api/partMetadata/save/" + partId, dto).then(function (response) {
                        return app.models.PartMetadataDto.mapToObj(response.data);
                    });
                };
                this.delete = function (partId, partMetadata) {
                    return _this.$http.delete("/api/partMetadata/delete/" + partId + "/" + partMetadata.id).success(function (response) {
                        return true;
                    }).error(function () {
                        return false;
                    });
                };
            }
            PartMetadataService.prototype.getAll = function (partId) {
                return this.$http.get("/api/partMetadata/getAll/" + partId)
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.PartMetadataDto.mapToObj(item));
                    });
                    return items;
                });
            };
            PartMetadataService.prototype.create = function () {
                return this.$http.get("/api/partMetadata/new")
                    .then(function (response) {
                    return app.models.PartMetadataDto.mapToObj(response.data);
                });
            };
            PartMetadataService.prototype.get = function (partId, partMetadataId) {
                return this.$http.get("/api/partMetadata/" + partId + "/" + partMetadataId)
                    .then(function (response) {
                    return app.models.PartMetadataDto.mapToObj(response.data);
                });
            };
            PartMetadataService.$inject = ["$http"];
            return PartMetadataService;
        }());
        function factory($http) {
            return new PartMetadataService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.PartMetadataService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=partMetadata.service.js.map