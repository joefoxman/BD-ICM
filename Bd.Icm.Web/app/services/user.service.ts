// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IUserService {
        getAll(): ng.IPromise<models.IUser[]>;
        get(id: number): ng.IPromise<models.IUser>;
        getByUsername(username: string): ng.IPromise<models.IUser>;
        getCurrent(): ng.IPromise<models.IUser>;
        create(): ng.IPromise<models.IUser>;
        delete(user: models.IUser): ng.IPromise<models.IUser>;
        save(user: models.IUser): ng.IPromise<models.IUser>;
    }

    class UserService implements IUserService {
        static $inject = ["$http"];
        current: models.IUser;

        constructor(private $http: ng.IHttpService) {
        }

        getAll(): ng.IPromise<models.IUser[]> {
            return this.$http.get("/api/users")
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto[]>): models.IUser[]=> {
                    return _.map(response.data, (dto: models.IUserDto) => {
                        return models.UserDto.mapToObj(dto);
                    });
                });
        }

        get(id: number): ng.IPromise<models.IUser> {
            return this.$http.get("/api/users/" + id)
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto>): models.IUser => {
                    return models.UserDto.mapToObj(response.data);
                });
        }

        getCurrent(): ng.IPromise<models.IUser> {
            return this.$http.get("/api/users/current")
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto>): models.IUser => {
                    return models.UserDto.mapToObj(response.data);
                });
        }

        getByUsername(username: string): ng.IPromise<models.IUser> {
            return this.$http.get("/api/users/" + encodeURIComponent(username))
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto>): models.IUser => {
                return models.UserDto.mapToObj(response.data);
            });
        }

        save = (user: models.IUser): ng.IPromise<models.IUser> => {
            return this.$http.post("/api/users/save", user)
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto>): models.IUser => {
                return models.UserDto.mapToObj(response.data);
            });
        }

        create = (): ng.IPromise<models.IUser> => {
            return this.$http.get("/api/users/create")
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto>): models.IUser => {
                return models.UserDto.mapToObj(response.data);
            });
        }

        delete = (user: models.IUser): ng.IPromise<models.IUser> => {
            return this.$http.delete("/api/users/delete/" + user.id)
                .then((response: ng.IHttpPromiseCallbackArg<models.IUserDto>): models.IUser => {
                    return models.UserDto.mapToObj(response.data);
            });
        }
    }

    function factory($http: ng.IHttpService): IUserService {
        return new UserService($http);
    }
    factory.$inject = ["$http"];

    angular.module("app.services")
        .factory("app.services.UserService", factory);
}