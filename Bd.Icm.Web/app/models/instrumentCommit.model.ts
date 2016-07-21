// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IInstrumentCommitDto {
        id: number;
        instrumentId: number;
        notes: string;
        revision: number;
        effectiveTo: number;
        createdDate: string;
        createdBy: number;
        creator: string;
        isNew: boolean;
    }

    export interface IInstrumentCommit {
        id: number;
        instrumentId: number;
        notes: string;
        revision: number;
        effectiveTo: number;
        createdDate: Date;
        createdBy: number;
        creator: string;
        isNew: boolean;
        isSelected: boolean;
    }

    export class InstrumentCommitDto implements IInstrumentCommitDto {
        id: number;
        instrumentId: number;
        notes: string;
        revision: number;
        effectiveTo: number;
        createdDate: string;
        createdBy: number;
        creator: string;
        isNew: boolean;

        static mapToObj(source: IInstrumentCommitDto): IInstrumentCommit {
            if (source == null) return null;
            var target = new InstrumentCommit();
            target.id = source.id;
            target.instrumentId = source.instrumentId;
            target.notes = source.notes;
            target.createdDate = moment(source.createdDate).toDate();
            target.creator = source.creator;
            target.effectiveTo = source.effectiveTo;
            target.revision = source.revision;
            target.isNew = source.isNew;
            return target;
        }

    }

    export class InstrumentCommit implements IInstrumentCommit {
        id: number;
        instrumentId: number;
        notes: string;
        revision: number;
        effectiveTo: number;
        createdDate: Date;
        createdBy: number;
        creator: string;
        isNew: boolean;
        isSelected: boolean;

        static mapToDto(source: IInstrumentCommit): IInstrumentCommitDto {
            if (source == null) return null;
            var target = new InstrumentCommitDto();
            target.id = source.id;
            target.instrumentId = source.instrumentId;
            target.notes = source.notes;
            target.createdDate = source.createdDate.toDateString();
            target.creator = source.creator;
            target.effectiveTo = source.effectiveTo;
            target.revision = source.revision;
            target.isNew = source.isNew;
            return target;
        }
    }
} 