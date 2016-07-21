// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IInstrumentService {
        getAll(): ng.IPromise<models.IInstrument[]>;
        get(id: number): ng.IPromise<models.IInstrument>;
        create(): ng.IPromise<models.IInstrument>;
        save(instrument: models.IInstrument);
        copy(instrument: models.IInstrument);
        getHistory(instrumentId: number): ng.IPromise<models.IPartChange[]>;
        checkForChanges(instrumentId: number): ng.IPromise<models.IChangeUser[]>;
        searchParts(instrumentId: number, searchKey: string): ng.IPromise<models.IPartSearchResult[]>;
        exportToExcel(instrument: models.IInstrument, version: number): ng.IPromise<any>;
        getDiff(instrumentId: number, fromVersion: number, toVersion: number): ng.IPromise<models.IInstrumentDiff>;
    }

    class InstrumentService implements IInstrumentService {
        static $inject = ["$http", "FileSaver", "Blob"];

        constructor(private $http: ng.IHttpService,
            private FileSaver: any,
        private Blob: Blob) {
        }

        exportToExcel(instrument: models.IInstrument, version: number): ng.IPromise<any> {
            return this.$http.get("/api/instruments/export/" + instrument.instrumentId + "/" + version, { responseType: "arraybuffer" })
                .then((response: ng.IHttpPromiseCallbackArg<any>): any => {
                    var data = new Blob([response.data], { type: 'application/vnd.ms-excel' });
                    this.FileSaver.saveAs(data, instrument.type + '.xlsx');
                });
        }

        getAll(): ng.IPromise<models.IInstrument[]> {
            return this.$http.get("/api/instruments")
                .then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDto[]>): models.IInstrument[] => {
                    var items = [];
                    _.each(response.data, (item: models.IInstrumentDto) => {
                        items.push(models.InstrumentDto.mapToObj(item));
                    });
                    return items;
                });
        }

        get(id: number): ng.IPromise<models.IInstrument> {
            return this.$http.get("/api/instruments/" + id)
                .then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDto>): models.IInstrument => {
                    return models.InstrumentDto.mapToObj(response.data);
                });
        }

        create(): ng.IPromise<models.IInstrument> {
            return this.$http.get("/api/instruments/new")
                .then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDto>): models.IInstrument => {
                    return models.InstrumentDto.mapToObj(response.data);
                });
        }

        save = (instrument: models.IInstrument) => {
            var dto = models.Instrument.mapToDto(instrument);
            return this.$http.post("/api/instruments/save", dto).then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDto>) => {
                return models.InstrumentDto.mapToObj(response.data);
            });
        }

        copy = (instrument: models.IInstrument) => {
            var dto = models.Instrument.mapToDto(instrument);
            return this.$http.post("/api/instruments/copy", dto).then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDto>) => {
                return models.InstrumentDto.mapToObj(response.data);
            });
        }

        checkForChanges = (instrumentId: number): ng.IPromise<models.IChangeUser[]> => {
            return this.$http.get("/api/instruments/changeUsers/" + instrumentId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IChangeUserDto[]>): models.IChangeUser[] => {
                    return response.data;
                });
        }

        getHistory = (instrumentId: number): ng.IPromise<models.IPartChange[]> => {
            return this.$http.get("/api/instruments/getHistory/" + instrumentId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartChangeDto[]>): models.IPartChange[] => {
                    return _.map(response.data, (item: models.IPartChangeDto) => {
                        return models.PartChangeDto.mapToObj(item);
                    });
                });
        }

        getDiff = (instrumentId: number, fromVersion: number, toVersion: number): ng.IPromise<models.IInstrumentDiff> => {
            return this.$http.get("/api/instruments/compare/" + instrumentId + "/" + fromVersion + "/" + toVersion)
                .then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDiffDto>): models.IInstrumentDiff => {
                    return models.InstrumentDiffDto.mapToObj(response.data);
                });
        }

        searchParts = (instrumentId: number, searchKey: string): ng.IPromise<models.IPartSearchResult[]> => {
            return this.$http.get("/api/instruments/searchParts/" + instrumentId + "/" + searchKey)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartSearchResultDto[]>): models.IPartSearchResult[] => {
                    return _.map(response.data, (item: models.IPartSearchResultDto) => {
                        return models.PartSearchResultDto.mapToObj(item);
                    });
                });
        }
    }

    function factory($http: ng.IHttpService, FileSaver: FileSaver, Blob: Blob): IInstrumentService {
        return new InstrumentService($http, FileSaver, Blob);
    }

    factory.$inject = ["$http", "FileSaver", "Blob"];

    angular.module("app.services")
        .factory("app.services.InstrumentService", factory);
}