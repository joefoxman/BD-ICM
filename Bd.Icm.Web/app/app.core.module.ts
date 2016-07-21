((): void => {
    "use strict";

    angular
        .module("app.core", [
            "ui.router",
            "ui.bootstrap",
            "toastr",
            "LocalStorageModule",
            "ngFileSaver"
        ]);
})();