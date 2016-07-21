// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IPartActionTypeService {
        getAll(): ng.IPromise<models.IPartActionType[]>;
    }

    class PartActionTypeService implements IPartActionTypeService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        getAll(): ng.IPromise<models.IPartActionType[]> {
            return this.$http.get("/api/partActionTypes")
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartActionTypeDto[]>): models.IPartActionType[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IPartActionTypeDto) => {
                        items.push(models.PartActionTypeDto.mapToObj(item));
                    });
                    return items;
                });
        }
    }

    function factory($http: ng.IHttpService): IPartActionTypeService {
        return new PartActionTypeService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.PartActionTypeService", factory);
}