// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartNodeDto {
        partId: number;
        instrumentId: number;
        name: string;
        description: string;
        level: number;
        parentPartId: number;
    }

    export interface IPartNode {
        partId: number;
        instrumentId: number;
        name: string;
        description: string;
        level: number;
        parentPartId: number;
    }

    export class PartNodeDto implements IPartNodeDto {
        partId: number;
        instrumentId: number;
        name: string;
        description: string;
        level: number;
        parentPartId: number;

        static mapToObj(source: IPartNodeDto): IPartNode {
            if (source == null) return null;
            var target = new PartNode();
            target.partId = source.partId;
            target.instrumentId = source.instrumentId;
            target.name = source.name;
            target.description = source.description;
            target.level = source.level;
            target.parentPartId = source.parentPartId;
            return target;
        }
    }

    export class PartNode implements IPartNode {
        partId: number;
        instrumentId: number;
        parentPartId: number;
        name: string;
        description: string;
        level: number;
    }
} 