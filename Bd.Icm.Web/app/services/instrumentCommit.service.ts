// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IInstrumentCommitService {
        getAll(instrumentId: number): ng.IPromise<models.IInstrumentCommit[]>;
        getUncommittedChanges(instrumentId: number): ng.IPromise<models.IUncommittedChange[]>;
        commitChanges(instrument: models.IInstrumentCommit, lastChange: models.IUncommittedChange): ng.IPromise<models.IInstrument>;
    }

    class InstrumentCommitService implements IInstrumentCommitService {
        static $inject = ["$http"];

        constructor(private $http: ng.IHttpService) {
        }

        getAll(instrumentId: number): ng.IPromise<models.IInstrumentCommit[]> {
            return this.$http.get("/api/instrumentCommits/" + instrumentId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentCommitDto[]>): models.IInstrumentCommit[] => {
                    var items = [];
                    _.each(response.data, (item: models.IInstrumentCommitDto) => {
                        items.push(models.InstrumentCommitDto.mapToObj(item));
                    });
                    return items;
                });
        }

        getUncommittedChanges = (instrumentId: number): ng.IPromise<models.IUncommittedChange[]> => {
            return this.$http.get("/api/instrumentCommit/getUncommittedChanges/" + instrumentId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IUncommittedChangeDto[]>): models.IUncommittedChange[] => {
                    return _.map(response.data, (item: models.IUncommittedChangeDto) => {
                        return models.UncommittedChangeDto.mapToObj(item);
                    });
                });
        }

        commitChanges = (instrumentCommit: models.IInstrumentCommit, lastChange: models.IUncommittedChange): ng.IPromise<models.IInstrument> => {
            var data = {
                instrumentCommit: instrumentCommit,
                lastChange: lastChange
            };
            return this.$http.post("/api/instrumentCommit/commitChanges", data).then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentDto>) => {
                return models.InstrumentDto.mapToObj(response.data);
            });
        }
    }

    function factory($http: ng.IHttpService): IInstrumentCommitService {
        return new InstrumentCommitService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.InstrumentCommitService", factory);
}