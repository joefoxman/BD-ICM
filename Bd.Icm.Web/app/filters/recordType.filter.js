var app;
(function (app) {
    var filters;
    (function (filters) {
        function RecordTypeFilter() {
            return function (value) {
                switch (value) {
                    case app.enums.RecordType.Instrument:
                        return "Instrument";
                    case app.enums.RecordType.Part:
                        return "Part";
                    case app.enums.RecordType.PartMetadata:
                        return "Setting";
                    case app.enums.RecordType.PartAction:
                        return "Action";
                    default:
                        return "Unknown";
                }
            };
        }
        filters.RecordTypeFilter = RecordTypeFilter;
        angular.module("app.filters", []).filter("recordType", [RecordTypeFilter]);
    })(filters = app.filters || (app.filters = {}));
})(app || (app = {}));
//# sourceMappingURL=recordType.filter.js.map