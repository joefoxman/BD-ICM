// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var PartTypeService = (function () {
            function PartTypeService($http) {
                this.$http = $http;
            }
            PartTypeService.prototype.getAll = function () {
                return this.$http.get("/api/partTypes")
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.PartTypeDto.mapToObj(item));
                    });
                    return items;
                });
            };
            PartTypeService.$inject = ["$http"];
            return PartTypeService;
        }());
        function factory($http) {
            return new PartTypeService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.PartTypeService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=partType.service.js.map