/// <reference path="../Scripts/typings/angularjs/angular.d.ts"/>
(function () {
    "use strict";
    var app = angular.module("app", [
        "app.filters",
        "app.core",
        "app.users",
        "app.common",
        "app.models",
        "app.services",
        "app.dialog",
        "app.layout",
        "app.parts",
        "app.partActions",
        "app.partMetadata",
        "app.instruments",
        "app.instrumentCommit",
        "toastr"
    ]);
    var serviceBase = '/';
    app.constant('ngAuthSettings', {
        apiServiceBaseUri: serviceBase,
        clientId: 'ngAuthApp'
    });
})();
//# sourceMappingURL=app.module.js.map