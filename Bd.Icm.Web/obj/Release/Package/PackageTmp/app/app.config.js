(function () {
    "use strict";
    angular
        .module("app")
        .config(config);
    config.$inject = ["$locationProvider",
        "$stateProvider",
        "$urlRouterProvider"];
    function config($locationProvider, $stateProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: false,
            requireBase: false
        });
        $stateProvider
            .state("root", {
            abstract: true,
            templateUrl: "/app/layout/layout.view.html",
            controller: "app.common.NavigationController as nav"
        });
        $urlRouterProvider.otherwise("instruments");
    }
    function configureTemplateFactory($provide) {
        // Set a suffix outside the decorator function 
        var cacheBuster = Date.now().toString();
        function templateFactoryDecorator($delegate) {
            var fromUrl = angular.bind($delegate, $delegate.fromUrl);
            $delegate.fromUrl = function (url, params) {
                if (url !== null && angular.isDefined(url) && angular.isString(url)) {
                    url += (url.indexOf("?") === -1 ? "?" : "&");
                    url += "v=" + cacheBuster;
                }
                return fromUrl(url, params);
            };
            return $delegate;
        }
        $provide.decorator('$templateFactory', ['$delegate', templateFactoryDecorator]);
    }
    angular
        .module("app")
        .config(["$provide", configureTemplateFactory]);
})();
//# sourceMappingURL=app.config.js.map