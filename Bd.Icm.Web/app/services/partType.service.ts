// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IPartTypeService {
        getAll(): ng.IPromise<models.IPartType[]>;
    }

    class PartTypeService implements IPartTypeService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        getAll(): ng.IPromise<models.IPartType[]> {
            return this.$http.get("/api/partTypes")
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartTypeDto[]>): models.IPartType[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IPartTypeDto) => {
                        items.push(models.PartTypeDto.mapToObj(item));
                    });
                    return items;
                });
        }
    }

    function factory($http: ng.IHttpService): IPartTypeService {
        return new PartTypeService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.PartTypeService", factory);
}