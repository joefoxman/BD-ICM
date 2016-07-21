// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IUncommittedChangeDto {
        id: number;
        effectiveFrom: number;
        effectiveTo: number;
        location: string;
        recordType: enums.RecordType;
        modifiedDate: string;
        modifier: string;
        createdDate: string;
        creator: string;
        modificationType: enums.ModificationType;
    }

    export interface IUncommittedChange {
        id: number;
        effectiveFrom: number;
        effectiveTo: number;
        location: string;
        recordType: enums.RecordType;
        modifiedDate: Date;
        modifier: string;
        createdDate: Date;
        creator: string;
        modificationType: enums.ModificationType;
        isSelected: boolean;
    }

    export class UncommittedChangeDto implements IUncommittedChangeDto {
        id: number;
        effectiveFrom: number;
        effectiveTo: number;
        location: string;
        recordType: enums.RecordType;
        modifiedDate: string;
        modifier: string;
        createdDate: string;
        creator: string;
        modificationType: enums.ModificationType;

        static mapToObj(source: IUncommittedChangeDto): IUncommittedChange {
            if (source == null) return null;
            var target = new UncommittedChange();
            target.id = source.id;
            target.effectiveFrom = source.effectiveFrom;
            target.effectiveTo = source.effectiveTo;
            target.location = source.location;
            target.recordType = source.recordType;
            target.modifier = source.modifier;
            target.modifiedDate = moment(source.modifiedDate).toDate();
            target.creator = source.creator;
            target.createdDate = moment(source.createdDate).toDate();
            target.modificationType = source.modificationType;
            return target;
        }

    }

    export class UncommittedChange implements IUncommittedChange {
        id: number;
        effectiveFrom: number;
        effectiveTo: number;
        location: string;
        recordType: enums.RecordType;
        modifiedDate: Date;
        modifier: string;
        createdDate: Date;
        creator: string;
        modificationType: enums.ModificationType;
        isSelected: boolean;

        constructor() {
            this.isSelected = false;
        }

        static mapToDto(source: IUncommittedChange): IUncommittedChangeDto {
            if (source == null) return null;
            var target = new UncommittedChangeDto();
            target.id = source.id;
            target.effectiveFrom = source.effectiveFrom;
            target.effectiveTo = source.effectiveTo;
            target.location = source.location;
            target.recordType = source.recordType;
            target.modifier = source.modifier;
            target.modifiedDate = source.modifiedDate.toDateString()
            target.creator = source.creator;
            target.createdDate = source.createdDate.toDateString();
            target.modificationType = source.modificationType;
            return target;
        }

    }
} 