// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IInstrumentDto {
        instrumentId: number;
        type: string;
        majorRevision: number;
        minorRevision: number;
        sapPartType: IInstrumentTypeDto;
        nickName: string;
        serialNumber: string;
        parts: models.IPartDto[];
        isNew: boolean;
    }

    export interface IInstrument extends IPartsContainer {
        instrumentId: number;
        type: string;
        majorRevision: number;
        minorRevision: number;
        sapPartType: IInstrumentType;
        nickName: string;
        serialNumber: string;
        parts: Array<IPart>;
        isNew: boolean;
    }

    export class InstrumentDto implements IInstrumentDto {
        instrumentId: number;
        type: string;
        majorRevision: number;
        minorRevision: number;
        sapPartType: IInstrumentTypeDto;
        nickName: string;
        serialNumber: string;
        parts: models.IPartDto[];
        isNew: boolean;

        constructor() {
            this.parts = [];
        }

        static mapToObj(source: IInstrumentDto): IInstrument {
            if (source == null) return null;
            var target = new Instrument();
            target.instrumentId = source.instrumentId;
            target.type = source.type;
            target.majorRevision = source.majorRevision;
            target.minorRevision = source.minorRevision;
            target.sapPartType = InstrumentTypeDto.mapToObj(source.sapPartType);
            target.nickName = source.nickName;
            target.serialNumber = source.serialNumber;
            target.isNew = source.isNew;
            _.each(source.parts, (part: IPartDto) => {
                target.parts.push(PartDto.mapToObj(part));
            });
            return target;
        }

    }

    export class Instrument implements IInstrument {
        instrumentId: number;
        type: string;
        majorRevision: number;
        minorRevision: number;
        parent: IPartsContainer;
        sapPartType: IInstrumentType;
        nickName: string;
        serialNumber: string;
        parts: Array<IPart>;
        isNew: boolean;

        constructor() {
            this.parts = new Array<IPart>();
        }

        get name(): string {
            return this.nickName;
        }

        static mapToDto(source: IInstrument): IInstrumentDto {
            if (source == null) return null;
            var target = new InstrumentDto();
            target.instrumentId = source.instrumentId;
            target.type = source.type;
            target.majorRevision = source.majorRevision;
            target.minorRevision = source.minorRevision;
            target.sapPartType = InstrumentType.mapToDto(source.sapPartType);
            target.nickName = source.nickName;
            target.serialNumber = source.serialNumber;
            target.isNew = source.isNew;
            target.parts = _.map(source.parts, (part: IPart) => {
                return Part.mapToDto(part);
            });
            return target;
        }
    }
} 