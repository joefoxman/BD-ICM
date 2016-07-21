// ReSharper disable once InconsistentNaming
module app.layout {
    "use strict";

    interface ILayoutScope {
    }

    class LayoutController implements ILayoutScope {

        static $inject = [
            "$state",
            "$q",
            "$location",
            "$scope"
        ];

        constructor(private $state: angular.ui.IStateService,
            private instrumentService: services.IInstrumentService,
            private $q: ng.IQService,
            private $location: ng.ILocationService,
            private $scope: ng.IScope) {

        }
    }

    angular.module("app.layout")
        .controller("app.layout.LayoutController", LayoutController);

}