// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IAuthenticationService {
        currentUser(): models.IUser;
        loadUser(): ng.IPromise<models.IUser>;
    }

    class AuthenticationService implements IAuthenticationService {

        static $inject = [
            "$q",
            "$http",
            "localStorageService",
            "app.services.UserService"
        ];

        constructor(private $q: ng.IQService,
            private $http: ng.IHttpService,
            private localStorageService: angular.localStorage.ILocalStorageService,
            private userService: services.IUserService) {
        }

        currentUserKey = "currentUser";

        loadUser = () => {
            this.logout();
            return this.userService.getCurrent().then((user: models.IUser) => {
                this.localStorageService.set(this.currentUserKey, user);
                return user;
            });
        }

        currentUser = () => {
            return this.localStorageService.get(this.currentUserKey);
        }

        logout = () => {
            this.localStorageService.remove(this.currentUserKey);
        }
    }

    function factory($q: ng.IQService,
        $http: ng.IHttpService,
        localStorageService: angular.localStorage.ILocalStorageService,
        userService: services.IUserService) {
        return new AuthenticationService($q,
            $http,
            localStorageService,
            userService);
    }

    factory.$inject = [
        "$q", "$http",
        "localStorageService",
        "app.services.UserService"
    ];

    angular.module("app.services")
        .factory("app.services.AuthenticationService", factory);

}
 