/// <reference path="../../Scripts/typings/angular-ui-router/angular-ui-router.d.ts"/>

((): void => {
    "use strict";

    angular.module("app.common")
        .config(config);

    config.$inject = ["$stateProvider"];
    function config($stateProvider: angular.ui.IStateProvider): void {
        $stateProvider
            .state("root.unauthorized", {
                url: "/unauthorized",
                templateUrl: "/app/common/unauthorized.view.html",
                data: { isPublic: true }
        });
    }
})(); 