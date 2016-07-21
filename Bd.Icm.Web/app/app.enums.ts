// ReSharper disable once InconsistentNaming
module app.enums {
    "use strict";

    export enum InstrumentType {
        FERT = 1
    };

    export enum RoleType {
        Unknown = 0,
        ReadOnly = 1,
        Contributor = 2,
        Administrator = 3,
        Committer = 4
    }

    export enum PartType {
        None = 0,
        HALB = 1,
        ROH = 2,
        ZICP = 3,
    }

    export enum ModificationType {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3
    }

    export enum RecordType {
        Instrument = 1,
        Part = 2,
        PartMetadata = 3,
        PartAction = 4
    }

    export class AppPermissions {
        static get Default(): any {
            return {
                MaintainSecurity: "MaintainSecurity"
            };
        } 
    }

    export class SelectOption<T> {
        name: string;
        value: T;

        constructor(name: string, value: T) {
            this.name = name;
            this.value = value;
        }

    }
}