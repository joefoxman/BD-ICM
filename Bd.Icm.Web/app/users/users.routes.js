/// <reference path="../../Scripts/typings/angular-ui-router/angular-ui-router.d.ts"/>
(function () {
    "use strict";
    angular.module("app.users")
        .config(config);
    config.$inject = ["$stateProvider"];
    function config($stateProvider) {
        $stateProvider
            .state("root.security", {
            url: "/security",
            template: "<ui-view/>",
            data: { authorizedRoles: [app.enums.RoleType.Administrator] }
        })
            .state("root.security.users", {
            url: "/users",
            templateUrl: "/app/users/users.view.html",
            controller: "app.users.UsersController as ulvm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator] }
        })
            .state("root.security.user", {
            url: "/user/:id",
            templateUrl: "/app/users/user.view.html",
            controller: "app.users.UserController as uvm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator] }
        });
    }
})();
//# sourceMappingURL=users.routes.js.map