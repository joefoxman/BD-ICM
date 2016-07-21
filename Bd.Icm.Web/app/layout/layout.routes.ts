((): void => {
    "use strict";

    angular.module("app.layout")
        .config(config);

    config.$inject = ["$stateProvider"];
    function config($stateProvider: angular.ui.IStateProvider): void {
        //$stateProvider
        //    .state("root", {
        //        templateUrl: "/app/layout/layout.view.html",
        //        controller: "app.layout.LayoutController as lc"
        //    });
    }
})(); 