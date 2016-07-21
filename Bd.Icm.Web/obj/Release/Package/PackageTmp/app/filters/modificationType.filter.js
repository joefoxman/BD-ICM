(function () {
    function ModificationTypeFilter() {
        return function (value) {
            switch (value) {
                case app.enums.ModificationType.None:
                    return "None";
                case app.enums.ModificationType.Insert:
                    return "Added";
                case app.enums.ModificationType.Update:
                    return "Modified";
                case app.enums.ModificationType.Delete:
                    return "Deleted";
                default:
                    return "Unknown";
            }
        };
    }
    angular.module("app").filter("modificationType", [ModificationTypeFilter]);
})();
//# sourceMappingURL=modificationType.filter.js.map