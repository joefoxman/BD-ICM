// ReSharper disable once InconsistentNaming
module app.models {
    export interface IInstrumentType {
        instrumentTypeId: number;
        name: string;
        isDisabled: boolean;
    }

    export class InstrumentType implements IInstrumentType {
        instrumentTypeId: number;
        name: string;
        isDisabled: boolean;
        
        static mapToDto(source: IInstrumentType): IInstrumentTypeDto {
            if (source == null) return null;
            var target = new InstrumentType();
            target.instrumentTypeId = source.instrumentTypeId;
            target.name = source.name;
            target.isDisabled = source.isDisabled;
            return target;
        }
    }

    export interface IInstrumentTypeDto {
        instrumentTypeId: number;
        name: string;
        isDisabled: boolean;
    }

    export class InstrumentTypeDto implements IInstrumentTypeDto {
        instrumentTypeId: number;
        name: string;
        isDisabled: boolean;

        static mapToObj(source: IInstrumentTypeDto): IInstrumentType {
            if (source == null) return null;
            var target = new InstrumentType();
            target.instrumentTypeId = source.instrumentTypeId;
            target.name = source.name;
            target.isDisabled = source.isDisabled;
            return target;
        }
    }
}