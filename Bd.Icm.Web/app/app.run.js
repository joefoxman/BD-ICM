(function () {
    "use strict";
    function run($rootScope, $state, authenticationService, authorizationService) {
        authenticationService.loadUser().then(function (user) {
            $rootScope.$on("$stateChangeStart", function (event, toState) {
                if (toState.name === "root.unauthorized")
                    return;
                var currentUser = authenticationService.currentUser();
                if (currentUser && (toState.data)) {
                    if (toState.data.isPublic)
                        return;
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
//# sourceMappingURL=app.run.js.map