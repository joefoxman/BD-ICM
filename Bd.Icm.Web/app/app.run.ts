((): void => {
    "use strict";

    function run($rootScope: ng.IRootScopeService,
        $state: angular.ui.IStateService,
        authenticationService: app.services.IAuthenticationService,
        authorizationService: app.services.IAuthorizationService): void {

        authenticationService.loadUser().then((user: app.models.IUser) => {
            $rootScope.$on("$stateChangeStart", (event, toState: angular.ui.IState) => {
                if (toState.name === "root.unauthorized") return;
                var currentUser = authenticationService.currentUser();
                if (currentUser && (toState.data)) {
                    if (toState.data.isPublic) return;
                    if (toState.data.authorizedRoles) {
                        if (authorizationService.isInRoleType(toState.data.authorizedRoles)) {
                            return;
                        }
                    }
                }
                event.preventDefault();
                $state.go("root.unauthorized");
            });
        });
    }
    run.$inject = [
        "$rootScope",
        "$state",
        "app.services.AuthenticationService",
        "app.services.AuthorizationService"];

    angular
        .module("app")
        .run(run);

})();