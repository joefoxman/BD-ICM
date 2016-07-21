// ReSharper disable once InconsistentNaming
module app.services {
    "use strict";

    export interface IAuthorizationService {
        isInRole(roles: string[]): boolean;
        isInRoleType(roles: enums.RoleType[]): boolean;
        user: models.IUser;
    }

    class AuthorizationService implements IAuthorizationService {
        user: models.IUser;

        static $inject = ["app.services.AuthenticationService"];

        constructor(private authenticationService: services.IAuthenticationService) {
        }

        isInRole = (roles: string[]): boolean => {
            if (!roles) return false;
            var roleValues = _.map(roles, (item: string) => {
                return enums.RoleType[item];
            });
            return this.isInRoleType(roleValues);
        }

        isInRoleType = (roles: enums.RoleType[]): boolean => {
            if (!roles) return false;
            this.user = this.authenticationService.currentUser();
            if (this.user == null) return false;
            var matches = _.intersection(roles, this.user.roles);
            return matches.length > 0;
        }
    }

    function factory(authenticationService: services.IAuthenticationService): IAuthorizationService {
        return new AuthorizationService(authenticationService);
    }

    factory.$inject = ["app.services.AuthenticationService"];

    angular.module("app.services")
        .factory("app.services.AuthorizationService", factory);

}