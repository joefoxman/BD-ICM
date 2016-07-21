// ReSharper disable once InconsistentNaming
module app.common {
    "use strict";

    interface INavigationScope {
        currentUser: models.IUser;
        currentDate: Date;
        isInRole(role: string[]): boolean;
        isReadOnly(): boolean;
        permissions: any;
        cacheBuster: string;
    }

    class NavigationController implements INavigationScope {
        currentUser: models.IUser;
        currentDate: Date;
        permissions: enums.AppPermissions;
        cacheBuster: string;

        static $inject = [
            "app.services.AuthenticationService",
            "app.services.AuthorizationService",
        ];

        constructor(private authenticationService: services.IAuthenticationService,
            private authorizationService: services.IAuthorizationService) {

            this.cacheBuster = Date.now().toString();
            this.permissions = enums.AppPermissions.Default;
            this.authenticationService.loadUser().then((user: models.IUser) => {
                this.currentUser = angular.copy(user);
                this.currentDate = new Date();
            });
        }

        isInRole = (role: string[]): boolean => {
            return this.authorizationService.isInRole(role);
        }

        isReadOnly = (): boolean => {
            return this.isInRole(['ReadOnly']);
        }
    }

    angular.module("app.common")
        .controller("app.common.NavigationController", NavigationController);

}
