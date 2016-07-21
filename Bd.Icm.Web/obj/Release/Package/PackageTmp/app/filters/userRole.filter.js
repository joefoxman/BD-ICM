(function () {
    "use strict";
    angular
        .module("app")
        .filter("userRole", userRole);
    function userRole() {
        return function (input) { return app.enums.RoleType[input]; };
    }
})();
//# sourceMappingURL=userRole.filter.js.map