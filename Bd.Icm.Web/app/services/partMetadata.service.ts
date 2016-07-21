// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IPartMetadataService {
        create(): ng.IPromise<models.IPartMetadata>;
        getAll(partId: number): ng.IPromise<models.IPartMetadata[]>;
        get(partId: number, partMetadataId: number): ng.IPromise<models.IPartMetadata>;
        save(partId: number, partMetadata: models.IPartMetadata): ng.IPromise<models.IPartMetadata>;
        delete(partId: number, partMetadata: models.IPartMetadata): ng.IPromise<boolean>;
    }

    class PartMetadataService implements IPartMetadataService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) {
        }

        getAll(partId: number): ng.IPromise<models.IPartMetadata[]> {
            return this.$http.get("/api/partMetadata/getAll/" + partId)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartMetadataDto[]>): models.IPartMetadata[]=> {
                    var items = [];
                    _.each(response.data, (item: models.IPartMetadataDto) => {
                        items.push(models.PartMetadataDto.mapToObj(item));
                    });
                    return items;
                });
        }

        create(): ng.IPromise<models.IPartMetadata> {
            return this.$http.get("/api/partMetadata/new")
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartMetadataDto>): models.IPartMetadata => {
                    return models.PartMetadataDto.mapToObj(response.data);
                });
        }

        get(partId: number, partMetadataId: number): ng.IPromise<models.IPartMetadata> {
            return this.$http.get(`/api/partMetadata/${partId}/${partMetadataId}`)
                .then((response: ng.IHttpPromiseCallbackArg<models.IPartMetadataDto>): models.IPartMetadata => {
                    return models.PartMetadataDto.mapToObj(response.data);
                });
        }

        save = (partId: number, partMetadata: models.IPartMetadata) => {
            var dto = models.PartMetadata.mapToDto(partMetadata);
            return this.$http.post("/api/partMetadata/save/" + partId, dto).then((response: ng.IHttpPromiseCallbackArg<models.IPartMetadataDto>) => {
                return models.PartMetadataDto.mapToObj(response.data);
            });
        }

        delete = (partId: number, partMetadata: models.IPartMetadata) => {
            return this.$http.delete(`/api/partMetadata/delete/${partId}/${partMetadata.id}`).success((response: ng.IHttpPromiseCallbackArg<boolean>) => {
                return true;
            }).error(() => {
                return false;
            });
        }

    }

    function factory($http: ng.IHttpService): IPartMetadataService {
        return new PartMetadataService($http);
    }

    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.PartMetadataService", factory);
}