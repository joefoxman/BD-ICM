(function () {
    "use strict";

    angular
        .module("app")
        .directive("ngIncludeNoCache", noCache);

    noCache.$inject = ["$templateCache"];

    function noCache($templateCache) {
        var directive = {
            restrict: 'A',
            scope: false,
            link: function (scope, element, attributes) {
                scope.$parent.$watch(attributes.ngInclude, function (newValue, oldValue) {
                    $templateCache.remove(oldValue);
                });
            }
        };
        return directive;
    }
})();