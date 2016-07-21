/// <reference path="../../Scripts/typings/angular-ui-router/angular-ui-router.d.ts"/>
(function () {
    "use strict";
    angular.module("app.instruments")
        .config(config);
    config.$inject = ["$stateProvider"];
    function config($stateProvider) {
        $stateProvider
            .state("root.instruments", {
            url: "/instruments",
            templateUrl: "/app/instruments/instruments.index.view.html",
            controller: "app.instruments.InstrumentIndexController as ivm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
        }).state("root.instrument", {
            url: "/instrument/:instrumentId",
            templateUrl: "/app/instruments/instrument.layout.view.html?" + Math.random().toString(36).slice(2),
            controller: "app.instruments.InstrumentLayoutController as ilvm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
        }).state("root.instrument.edit", {
            url: "/edit",
            templateUrl: "/app/instruments/instrument.view.html",
            controller: "app.instruments.InstrumentController as ivm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
        }).state("root.instrument.history", {
            url: "/history",
            templateUrl: "/app/instruments/instrument.history.view.html",
            controller: "app.instruments.InstrumentHistoryController as ihvm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
        }).state("root.instrument.compare", {
            url: "/compare/:fromVersion/:toVersion",
            templateUrl: "/app/instruments/instrument.compare.view.html",
            controller: "app.instruments.InstrumentCompareController as icvm",
            data: { authorizedRoles: [app.enums.RoleType.Administrator, app.enums.RoleType.Contributor, app.enums.RoleType.ReadOnly] }
        });
    }
})();
//# sourceMappingURL=instruments.routes.js.map