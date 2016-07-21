(() => {
    "use strict";

    angular
        .module("app")
        .filter("userRole", userRole);

    function userRole() {
        return input => app.enums.RoleType[input];
    }
})();