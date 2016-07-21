/// <reference path="../../Scripts/typings/angular-ui-router/angular-ui-router.d.ts"/>
(function () {
    "use strict";
    angular.module("app.common")
        .config(config);
    config.$inject = ["$stateProvider"];
    function config($stateProvider) {
        $stateProvider
            .state("root.unauthorized", {
            url: "/unauthorized",
            templateUrl: "/app/common/unauthorized.view.html",
            data: { isPublic: true }
        });
    }
})();
//# sourceMappingURL=common.routes.js.map