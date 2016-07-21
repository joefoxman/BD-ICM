// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var enums;
    (function (enums) {
        "use strict";
        (function (InstrumentType) {
            InstrumentType[InstrumentType["FERT"] = 1] = "FERT";
        })(enums.InstrumentType || (enums.InstrumentType = {}));
        var InstrumentType = enums.InstrumentType;
        ;
        (function (RoleType) {
            RoleType[RoleType["Unknown"] = 0] = "Unknown";
            RoleType[RoleType["ReadOnly"] = 1] = "ReadOnly";
            RoleType[RoleType["Contributor"] = 2] = "Contributor";
            RoleType[RoleType["Administrator"] = 3] = "Administrator";
            RoleType[RoleType["Committer"] = 4] = "Committer";
        })(enums.RoleType || (enums.RoleType = {}));
        var RoleType = enums.RoleType;
        (function (PartType) {
            PartType[PartType["None"] = 0] = "None";
            PartType[PartType["HALB"] = 1] = "HALB";
            PartType[PartType["ROH"] = 2] = "ROH";
            PartType[PartType["ZICP"] = 3] = "ZICP";
        })(enums.PartType || (enums.PartType = {}));
        var PartType = enums.PartType;
        (function (ModificationType) {
            ModificationType[ModificationType["None"] = 0] = "None";
            ModificationType[ModificationType["Insert"] = 1] = "Insert";
            ModificationType[ModificationType["Update"] = 2] = "Update";
            ModificationType[ModificationType["Delete"] = 3] = "Delete";
        })(enums.ModificationType || (enums.ModificationType = {}));
        var ModificationType = enums.ModificationType;
        (function (RecordType) {
            RecordType[RecordType["Instrument"] = 1] = "Instrument";
            RecordType[RecordType["Part"] = 2] = "Part";
            RecordType[RecordType["PartMetadata"] = 3] = "PartMetadata";
            RecordType[RecordType["PartAction"] = 4] = "PartAction";
        })(enums.RecordType || (enums.RecordType = {}));
        var RecordType = enums.RecordType;
        var AppPermissions = (function () {
            function AppPermissions() {
            }
            Object.defineProperty(AppPermissions, "Default", {
                get: function () {
                    return {
                        MaintainSecurity: "MaintainSecurity"
                    };
                },
                enumerable: true,
                configurable: true
            });
            return AppPermissions;
        }());
        enums.AppPermissions = AppPermissions;
        var SelectOption = (function () {
            function SelectOption(name, value) {
                this.name = name;
                this.value = value;
            }
            return SelectOption;
        }());
        enums.SelectOption = SelectOption;
    })(enums = app.enums || (app.enums = {}));
})(app || (app = {}));
//# sourceMappingURL=app.enums.js.map