// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartDto {
        id: number;
        instrumentId: number;
        name: string;
        sapPartType: IPartTypeDto;
        serialNumber: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        revisionNumber: number;
        sapPartNumber: number;
        manufacturer: string;
        mfgPartNumber: string;
        lotCode: string;
        dateCode: string;
        parts: IPartDto[];
        actions: IPartActionDto[];
        metadata: IPartMetadataDto[];
        isNew: boolean;
        parentPartId: number;
        createdDate: string;
        creator: string;
        modifiedDate: string;
        modifier: string;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
    }

    export interface IPart {
        id: number;
        instrumentId: number;
        parentPartId: number;
        sapPartType: IPartType;
        name: string;
        description: string;
        serialNumber: string;
        documentNumber: string;
        dashNumber: number;
        revisionNumber: number;
        sapPartNumber: number;
        manufacturer: string;
        mfgPartNumber: string;
        lotCode: string;
        dateCode: string;
        parts: Array<IPart>;
        actions: IPartAction[];
        metadata: IPartMetadata[];
        isNew: boolean;
        modifiedDate: Date;
        modifier: string;
        createdDate: Date;
        creator: string;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
    }

    export class PartDto implements IPartDto {
        id: number;
        instrumentId: number;
        parentPartId: number;
        name: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        serialNumber: string;
        manufacturer: string;
        mfgPartNumber: string;
        revisionNumber: number;
        sapPartNumber: number;
        sapPartType: IPartTypeDto;
        lotCode: string;
        dateCode: string;
        parts: IPartDto[];
        actions: IPartActionDto[];
        metadata: IPartMetadataDto[];
        isNew: boolean;
        modifier: string;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
        modifiedDate: string;
        createdDate: string;
        creator: string;

        constructor() {
            this.parts = [];
            this.actions = [];
            this.metadata = [];
        }

        static mapToObj(source: IPartDto): IPart {
            if (source == null) return null;
            var target = new Part();
            target.id = source.id;
            target.instrumentId = source.instrumentId;
            target.name = source.name;
            target.description = source.description;
            target.documentNumber = source.documentNumber;
            target.dashNumber = source.dashNumber;
            target.revisionNumber = source.revisionNumber;
            target.sapPartNumber = source.sapPartNumber;
            target.sapPartType = PartType.mapToDto(source.sapPartType);
            target.serialNumber = source.serialNumber;
            target.lotCode = source.lotCode;
            target.dateCode = source.dateCode;
            target.isNew = source.isNew;
            target.parentPartId = source.parentPartId;
            target.mfgPartNumber = source.mfgPartNumber;
            target.manufacturer = source.manufacturer;
            target.modifier = source.modifier;
            target.modifiedDate = moment(source.modifiedDate).toDate();
            target.creator = source.creator;
            target.createdDate = moment(source.createdDate).toDate();
            target.modificationType = source.modificationType;
            target.effectiveFrom = source.effectiveFrom;
            target.effectiveTo = source.effectiveTo;
            _.each(source.parts, (dto: IPartDto) => {
                target.parts.push(PartDto.mapToObj(dto));
            });
            _.each(source.actions, (dto: IPartActionDto) => {
                target.actions.push(PartActionDto.mapToObj(dto));
            });
            _.each(source.metadata, (dto: IPartMetadataDto) => {
                target.metadata.push(PartMetadataDto.mapToObj(dto));
            });
            return target;
        }

    }

    export class Part implements IPart, IPartsContainer {
        id: number;
        instrumentId: number;
        parentPartId: number;
        name: string;
        description: string;
        documentNumber: string;
        dashNumber: number;
        revisionNumber: number;
        serialNumber: string;
        sapPartNumber: number;
        sapPartType: IPartType;
        manufacturer: string;
        mfgPartNumber: string;
        lotCode: string;
        dateCode: string;
        parts: IPart[];
        actions: IPartAction[];
        metadata: IPartMetadata[];
        isNew: boolean;
        modifier: string;
        modificationType: enums.ModificationType;
        effectiveFrom: number;
        effectiveTo: number;
        modifiedDate: Date;
        createdDate: Date;
        creator: string;

        constructor() {
            this.parts = [];
            this.actions = [];
            this.metadata = [];
        }

        static mapToDto(source: IPart): IPartDto {
            if (source == null) return null;
            var target = new PartDto();
            target.id = source.id;
            target.instrumentId = source.instrumentId;
            target.parentPartId = source.parentPartId;
            target.name = source.name;
            target.description = source.description;
            target.documentNumber = source.documentNumber;
            target.dashNumber = source.dashNumber;
            target.revisionNumber = source.revisionNumber;
            target.serialNumber = source.serialNumber;
            target.sapPartNumber = source.sapPartNumber;
            target.sapPartType = PartType.mapToDto(source.sapPartType);
            target.lotCode = source.lotCode;
            target.dateCode = source.dateCode;
            target.mfgPartNumber = source.mfgPartNumber;
            target.manufacturer = source.manufacturer;
            target.isNew = source.isNew;
            target.modifier = source.modifier;
            target.modifiedDate = source.modifiedDate.toDateString();
            target.modificationType = source.modificationType;
            target.creator = source.creator;
            target.createdDate = source.createdDate.toDateString();
            target.effectiveFrom = source.effectiveFrom;
            target.effectiveTo = source.effectiveTo;
            _.each(source.parts, (obj: IPart) => {
                target.parts.push(Part.mapToDto(obj));
            });
            _.each(source.actions, (obj: IPartAction) => {
                target.actions.push(PartAction.mapToDto(obj));
            });
            _.each(source.metadata, (obj: IPartMetadata) => {
                target.metadata.push(PartMetadata.mapToDto(obj));
            });
            return target;
        }
    }
} 