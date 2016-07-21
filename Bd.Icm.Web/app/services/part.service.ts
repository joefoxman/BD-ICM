// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IPartService {
        create(): ng.IPromise<models.IPart>;
        searchNames(key: string): ng.IPromise<models.IPart[]>;
        getAll(parentId: number): ng.IPromise<models.IPart[]>;
        get(id: number): ng.IPromise<models.IPart>;
        save(part: models.IPart): ng.IPromise<models.IPart>;
        delete(part: models.IPart): ng.IPromise<boolean>;
        getNodes(instrumentId: number, partId: number): ng.IPromise<models.IPartNode[]>;
    }

    class PartService implements IPartService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        searchNames(key: string): ng.IPromise<models.IPart[]> {
            var url = "/api/parts/searchNames";
            if (key !== "") {
                url += "/" + key;
            }
            return this.$http.get(url)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPart[]>): models.IPart[] => {
                    return response.data;
                });
        }

        getAll(parentId: number): ng.IPromise<models.IPart[]> {
            return this.$http.get("/api/parts")
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartDto[]>): models.IPart[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IPartDto) => {
                        items.push(models.PartDto.mapToObj(item));
                    });
                    return items;
                });
        }

        getNodes(instrumentId: number, partId: number): ng.IPromise<models.IPartNode[]> {
            return this.$http.get("/api/parts/nodes?instrumentId=" + instrumentId + "&parentPartId=" + partId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartNodeDto[]>): models.IPartNode[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IPartNodeDto) => {
                        items.push(models.PartNodeDto.mapToObj(item));
                    });
                    return items;
                });
        }

        create(): ng.IPromise<models.IPart> {
            return this.$http.get("/api/parts/new")
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartDto>): models.IPart => {
                    return models.PartDto.mapToObj(response.data);
                });
        }

        get(id: number): ng.IPromise<models.IPart> {
            return this.$http.get("/api/parts/" + id)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartDto>): models.IPart => {
                    return models.PartDto.mapToObj(response.data);
                });
        }

        save = (part: models.IPart) => {
            var dto = models.Part.mapToDto(part);
            return this.$http.post("/api/parts/save", dto).then((response: ng.IHttpPromiseCallbackArg<models.IPartDto>) => {
                return models.PartDto.mapToObj(response.data);
            });
        }

        delete = (part: models.IPart) => {
            return this.$http.delete("/api/parts/delete/" + part.id).success((response: ng.IHttpPromiseCallbackArg<boolean>) => {
                return true;
            }).error(() => {
                return false;
            });
        }

    }

    function factory($http: ng.IHttpService): IPartService {
        return new PartService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.PartService", factory);
}