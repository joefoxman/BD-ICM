// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var InstrumentTypeService = (function () {
            function InstrumentTypeService($http) {
                this.$http = $http;
            }
            InstrumentTypeService.prototype.getAll = function () {
                return this.$http.get("/api/instrumentTypes")
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.InstrumentTypeDto.mapToObj(item));
                    });
                    return items;
                });
            };
            InstrumentTypeService.$inject = ["$http"];
            return InstrumentTypeService;
        }());
        function factory($http) {
            return new InstrumentTypeService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.InstrumentTypeService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=instrumentType.service.js.map