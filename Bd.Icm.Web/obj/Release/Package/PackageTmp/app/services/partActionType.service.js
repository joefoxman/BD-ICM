// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var PartActionTypeService = (function () {
            function PartActionTypeService($http) {
                this.$http = $http;
            }
            PartActionTypeService.prototype.getAll = function () {
                return this.$http.get("/api/partActionTypes")
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.PartActionTypeDto.mapToObj(item));
                    });
                    return items;
                });
            };
            PartActionTypeService.$inject = ["$http"];
            return PartActionTypeService;
        }());
        function factory($http) {
            return new PartActionTypeService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.PartActionTypeService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=partActionType.service.js.map