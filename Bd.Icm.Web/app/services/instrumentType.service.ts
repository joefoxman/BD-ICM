// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IInstrumentTypeService {
        getAll(): ng.IPromise<models.IInstrumentType[]>;
    }

    class InstrumentTypeService implements IInstrumentTypeService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        getAll(): ng.IPromise<models.IInstrumentType[]> {
            return this.$http.get("/api/instrumentTypes")
                .then((response: ng.IHttpPromiseCallbackArg<models.IInstrumentTypeDto[]>): models.IInstrumentType[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IInstrumentTypeDto) => {
                        items.push(models.InstrumentTypeDto.mapToObj(item));
                    });
                    return items;
                });
        }
    }

    function factory($http: ng.IHttpService): IInstrumentTypeService {
        return new InstrumentTypeService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.InstrumentTypeService", factory);
}