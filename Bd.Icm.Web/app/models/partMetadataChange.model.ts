// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartMetadataChangeDto {
        partId: number;
        name: string;
        description: string;
        metaValue: string;
        metaKey: string;
        id: number;
        modifiedDate: string;
        createdDate: string;
        creator: string;
        modifier: string;
        level: number;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
    }

    export interface IPartMetadataChange {
        partId: number;
        name: string;
        metaValue: string;
        metaKey: string;
        description: string;
        id: number;
        modifiedDate: Date;
        createdDate: Date;
        creator: string;
        modifier: string;
        level: number;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
        isSelected: boolean;
    }

    export class PartMetadataChangeDto implements IPartMetadataChangeDto {
        partId: number;
        name: string;
        metaValue: string;
        metaKey: string;
        description: string;
        id: number;
        modifiedDate: string;
        createdDate: string;
        creator: string;
        modifier: string;
        level: number;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;

        static mapToObj(source: IPartMetadataChangeDto): IPartMetadataChange {
            if (source == null) return null;
            var target = new PartMetadataChange();
            target.partId = source.partId;
            target.name = source.name;
            target.description = source.description;
            target.metaKey = source.metaKey;
            target.metaValue = source.metaValue;
            target.modifier = source.modifier;
            target.creator = source.creator;
            target.modifiedDate = moment(source.modifiedDate).toDate();
            target.createdDate = moment(source.createdDate).toDate();
            target.modificationType = source.modificationType;
            target.effectiveFrom = source.effectiveFrom;
            target.effectiveTo = source.effectiveTo;
            return target;
        }

    }

    export class PartMetadataChange implements IPartMetadataChange {
        partId: number;
        instrumentId: number;
        name: string;
        metaValue: string;
        metaKey: string;
        description: string;
        id: number;
        modifiedDate: Date;
        createdDate: Date;
        creator: string;
        modifier: string;
        level: number;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
        isSelected: boolean;

        constructor() {
            this.isSelected = false;
        }

        static mapToDto(source: IPartMetadataChange): IPartMetadataChangeDto {
            if (source == null) return null;
            var target = new PartMetadataChangeDto();
            target.partId = source.partId;
            target.name = source.name;
            target.description = source.description;
            target.metaKey = source.metaKey;
            target.metaValue = source.metaValue;
            target.modifier = source.modifier;
            target.creator = source.creator;
            target.modifiedDate = source.modifiedDate.toDateString();
            target.createdDate = source.createdDate.toDateString();
            target.modificationType = source.modificationType;
            target.effectiveFrom = source.effectiveFrom;
            target.effectiveTo = source.effectiveTo;
            return target;
        }
    }
} 