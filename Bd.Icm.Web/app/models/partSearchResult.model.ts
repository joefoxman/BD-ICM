// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartSearchResultDto {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        sapPartNumber: number;
        mfgPartNumber: string;
        parentPartId: number;
    }

    export interface IPartSearchResult {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        sapPartNumber: number;
        mfgPartNumber: string;
        parentPartId: number;
    }

    export class PartSearchResultDto implements IPartSearchResultDto {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        sapPartNumber: number;
        mfgPartNumber: string;
        parentPartId: number;

        static mapToObj(source: IPartSearchResultDto): IPartSearchResult {
            if (source == null) return null;
            var target = new PartSearchResult();
            target.partId = source.partId;
            target.instrumentId = source.instrumentId;
            target.name = source.name;
            target.description = source.description;
            target.documentNumber = source.documentNumber;
            target.sapPartNumber = source.sapPartNumber;
            target.serialNumber = source.serialNumber;
            target.parentPartId = source.parentPartId;
            target.mfgPartNumber = source.mfgPartNumber;
            return target;
        }

    }

    export class PartSearchResult implements IPartSearchResult {
        partId: number;
        instrumentId: number;
        name: string;
        serialNumber: string;
        description: string;
        documentNumber: string;
        sapPartNumber: number;
        mfgPartNumber: string;
        parentPartId: number;

        static mapToDto(source: IPartSearchResult): IPartSearchResultDto {
            if (source == null) return null;
            var target = new PartSearchResultDto();
            target.partId = source.partId;
            target.instrumentId = source.instrumentId;
            target.parentPartId = source.parentPartId;
            target.name = source.name;
            target.description = source.description;
            target.documentNumber = source.documentNumber;
            target.serialNumber = source.serialNumber;
            target.sapPartNumber = source.sapPartNumber;
            target.mfgPartNumber = source.mfgPartNumber;
            return target;
        }
    }
} 