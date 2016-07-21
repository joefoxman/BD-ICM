/// <reference path="../../Scripts/typings/angular-ui-router/angular-ui-router.d.ts"/>

((): void => {
    "use strict";

    angular.module("app.parts")
        .config(config);

    config.$inject = ["$stateProvider"];
    function config($stateProvider: angular.ui.IStateProvider): void {
        $stateProvider
            .state("root.parts", {
                url: "/parts",
                templateUrl: "/app/parts/parts.index.view.html",
                controller: "app.parts.PartIndexController as pvm",
                data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
            })
            .state("root.instrument.part", {
                url: "/part/:id/:parentPartId",
                templateUrl: "/app/parts/part.view.html",
                controller: "app.parts.PartController as pvm",
                data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
        });
    }
})(); 