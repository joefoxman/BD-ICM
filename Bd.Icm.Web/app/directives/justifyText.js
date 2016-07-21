(function () {
    'use strict';

    angular
        .module('app')
        .directive('justifyText', justifyText);

    justifyText.$inject = ['$filter', '$locale'];

    function justifyText($filter, $locale) {
        var directive = {
            terminal: true,
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                var addOn = null;

                var setSuffix = function(value) {
                    if (value) {
                        if (addOn == null) {
                            var outer = angular.element('<div />').addClass('input-group');
                            outer.insertBefore(element);
                            element.appendTo(outer).addClass('numeric');
                            addOn = angular.element('<span />');
                            addOn.addClass('input-group-addon').appendTo(outer);
                        }
                        addOn.html(value);
                    }
                }

                scope.$watch(attrs.suffix, function (v) {
                    setSuffix(v);
                });

                element.addClass('text-right');

                setSuffix(attrs.suffix);

                // Listen for change events to enable binding
                element.bind('blur', function () {
                    element.addClass('text-right');
                });

                // Listen for change events to enable binding
                element.bind('focus', function () {
                    element.removeClass('text-right');
                });

            }
        };
        return directive;
    }
})();