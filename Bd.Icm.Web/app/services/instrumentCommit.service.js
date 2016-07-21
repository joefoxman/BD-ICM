// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var InstrumentCommitService = (function () {
            function InstrumentCommitService($http) {
                var _this = this;
                this.$http = $http;
                this.getUncommittedChanges = function (instrumentId) {
                    return _this.$http.get("/api/instrumentCommit/getUncommittedChanges/" + instrumentId)
                        .then(function (response) {
                        return _.map(response.data, function (item) {
                            return app.models.UncommittedChangeDto.mapToObj(item);
                        });
                    });
                };
                this.commitChanges = function (instrumentCommit, lastChange) {
                    var data = {
                        instrumentCommit: instrumentCommit,
                        lastChange: lastChange
                    };
                    return _this.$http.post("/api/instrumentCommit/commitChanges", data).then(function (response) {
                        return app.models.InstrumentDto.mapToObj(response.data);
                    });
                };
            }
            InstrumentCommitService.prototype.getAll = function (instrumentId) {
                return this.$http.get("/api/instrumentCommits/" + instrumentId)
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.InstrumentCommitDto.mapToObj(item));
                    });
                    return items;
                });
            };
            InstrumentCommitService.$inject = ["$http"];
            return InstrumentCommitService;
        }());
        function factory($http) {
            return new InstrumentCommitService($http);
        }
        factory.$inject = ["$http"];
        angular.module("app.services")
            .factory("app.services.InstrumentCommitService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=instrumentCommit.service.js.map