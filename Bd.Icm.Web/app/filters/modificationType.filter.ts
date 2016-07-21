(() => {
    function ModificationTypeFilter() {
        return (value: app.enums.ModificationType): string => {
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
        }
    }

    angular.module("app").filter("modificationType", [ModificationTypeFilter]);
   
})();
