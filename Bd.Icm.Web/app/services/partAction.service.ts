// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IPartActionService {
        create(): ng.IPromise<models.IPartAction>;
        getAll(partId: number): ng.IPromise<models.IPartAction[]>;
        get(partId: number, partActionId: number): ng.IPromise<models.IPartAction>;
        save(partId: number, partAction: models.IPartAction): ng.IPromise<models.IPartAction>;
        delete(partId: number, partAction: models.IPartAction): ng.IPromise<boolean>;
    }

    class PartActionService implements IPartActionService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        getAll(partId: number): ng.IPromise<models.IPartAction[]> {
            return this.$http.get("/api/partActions/getAll/" + partId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartActionDto[]>): models.IPartAction[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IPartActionDto) => {
                        items.push(models.PartActionDto.mapToObj(item));
                    });
                    return items;
                });
        }

        create(): ng.IPromise<models.IPartAction> {
            return this.$http.get("/api/partActions/new")
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartActionDto>): models.IPartAction => {
                    return models.PartActionDto.mapToObj(response.data);
                });
        }

        get(partId: number, partActionId: number): ng.IPromise<models.IPartAction> {
            return this.$http.get(`/api/partActions/${partId}/${partActionId}`)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartActionDto>): models.IPartAction => {
                    return models.PartActionDto.mapToObj(response.data);
                });
        }

        save = (partId: number, partAction: models.IPartAction) => {
            var dto = models.PartAction.mapToDto(partAction);
            return this.$http.post("/api/partActions/save/" + partId, dto).then((response: ng.IHttpPromiseCallbackArg<models.IPartActionDto>) => {
                return models.PartActionDto.mapToObj(response.data);
            });
        }

        delete = (partId: number, partAction: models.IPartAction) => {
            return this.$http.delete(`/api/partActions/delete/${partId}/${partAction.id}`).success((response: ng.IHttpPromiseCallbackArg<boolean>) => {
                return true;
            }).error(() => {
                return false;
            });
        }

    }

    function factory($http: ng.IHttpService): IPartActionService {
        return new PartActionService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.PartActionService", factory);
}