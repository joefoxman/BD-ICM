// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var services;
    (function (services) {
        "use strict";
        var InstrumentService = (function () {
            function InstrumentService($http, FileSaver, Blob) {
                var _this = this;
                this.$http = $http;
                this.FileSaver = FileSaver;
                this.Blob = Blob;
                this.save = function (instrument) {
                    var dto = app.models.Instrument.mapToDto(instrument);
                    return _this.$http.post("/api/instruments/save", dto).then(function (response) {
                        return app.models.InstrumentDto.mapToObj(response.data);
                    });
                };
                this.copy = function (instrument) {
                    var dto = app.models.Instrument.mapToDto(instrument);
                    return _this.$http.post("/api/instruments/copy", dto).then(function (response) {
                        return app.models.InstrumentDto.mapToObj(response.data);
                    });
                };
                this.checkForChanges = function (instrumentId) {
                    return _this.$http.get("/api/instruments/changeUsers/" + instrumentId)
                        .then(function (response) {
                        return response.data;
                    });
                };
                this.getHistory = function (instrumentId) {
                    return _this.$http.get("/api/instruments/getHistory/" + instrumentId)
                        .then(function (response) {
                        return _.map(response.data, function (item) {
                            return app.models.PartChangeDto.mapToObj(item);
                        });
                    });
                };
                this.getDiff = function (instrumentId, fromVersion, toVersion) {
                    return _this.$http.get("/api/instruments/compare/" + instrumentId + "/" + fromVersion + "/" + toVersion)
                        .then(function (response) {
                        return app.models.InstrumentDiffDto.mapToObj(response.data);
                    });
                };
                this.searchParts = function (instrumentId, searchKey) {
                    return _this.$http.get("/api/instruments/searchParts/" + instrumentId + "/" + searchKey)
                        .then(function (response) {
                        return _.map(response.data, function (item) {
                            return app.models.PartSearchResultDto.mapToObj(item);
                        });
                    });
                };
            }
            InstrumentService.prototype.exportToExcel = function (instrument, version) {
                var _this = this;
                return this.$http.get("/api/instruments/export/" + instrument.instrumentId + "/" + version, { responseType: "arraybuffer" })
                    .then(function (response) {
                    var data = new Blob([response.data], { type: 'application/vnd.ms-excel' });
                    _this.FileSaver.saveAs(data, instrument.type + '.xlsx');
                });
            };
            InstrumentService.prototype.getAll = function () {
                return this.$http.get("/api/instruments")
                    .then(function (response) {
                    var items = [];
                    _.each(response.data, function (item) {
                        items.push(app.models.InstrumentDto.mapToObj(item));
                    });
                    return items;
                });
            };
            InstrumentService.prototype.get = function (id) {
                return this.$http.get("/api/instruments/" + id)
                    .then(function (response) {
                    return app.models.InstrumentDto.mapToObj(response.data);
                });
            };
            InstrumentService.prototype.create = function () {
                return this.$http.get("/api/instruments/new")
                    .then(function (response) {
                    return app.models.InstrumentDto.mapToObj(response.data);
                });
            };
            InstrumentService.$inject = ["$http", "FileSaver", "Blob"];
            return InstrumentService;
        }());
        function factory($http, FileSaver, Blob) {
            return new InstrumentService($http, FileSaver, Blob);
        }
        factory.$inject = ["$http", "FileSaver", "Blob"];
        angular.module("app.services")
            .factory("app.services.InstrumentService", factory);
    })(services = app.services || (app.services = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.service.js.map