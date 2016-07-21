module app.filters {
    export function RecordTypeFilter() {
        return (value: enums.RecordType): string => {
            switch (value) {
                case enums.RecordType.Instrument:
                    return "Instrument";
                case enums.RecordType.Part:
                    return "Part";
                case enums.RecordType.PartMetadata:
                    return "Setting";
                case enums.RecordType.PartAction:
                    return "Action";
                default:
                    return "Unknown";
            }
        }
    }

    angular.module("app.filters", []).filter("recordType", [RecordTypeFilter]);
}