// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartChangeDto {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        sapPartNumber: number;
        parentPartId: number;
        modifiedDate: string;
        createdDate: string;
        creator: string;
        modifier: string;
        level: number;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
    }

    export interface IPartChange {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        sapPartNumber: number;
        parentPartId: number;
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

    export class PartChangeDto implements IPartChangeDto {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        sapPartNumber: number;
        parentPartId: number;
        modifiedDate: string;
        createdDate: string;
        creator: string;
        modifier: string;
        level: number;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;

        static mapToObj(source: IPartChangeDto): IPartChange {
            if (source == null) return null;
            var target = new PartChange();
            target.partId = source.partId;
            target.instrumentId = source.instrumentId;
            target.name = source.name;
            target.description = source.description;
            target.documentNumber = source.documentNumber;
            target.dashNumber = source.dashNumber;
            target.sapPartNumber = source.sapPartNumber;
            target.serialNumber = source.serialNumber;
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

    export class PartChange implements IPartChange {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        sapPartNumber: number;
        parentPartId: number;
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

        static mapToDto(source: IPartChange): IPartChangeDto {
            if (source == null) return null;
            var target = new PartChangeDto();
            target.partId = source.partId;
            target.instrumentId = source.instrumentId;
            target.name = source.name;
            target.description = source.description;
            target.documentNumber = source.documentNumber;
            target.dashNumber = source.dashNumber;
            target.serialNumber = source.serialNumber;
            target.sapPartNumber = source.sapPartNumber;
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