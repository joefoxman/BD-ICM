// ReSharper disable once InconsistentNaming
module app.models {
    export interface IInstrumentDiff {
        fromInstrument: models.IInstrument;
        toInstrument: models.IInstrument;
        fromCommit: models.IInstrumentCommit;
        toCommit: models.IInstrumentCommit;
        deletedParts: models.IPart[];
        addedParts: models.IPart[];
        modifiedParts: models.IPartVersion[];
    }

    export class InstrumentDiff implements IInstrumentDiff {
        fromInstrument: models.IInstrument;
        toInstrument: models.IInstrument;
        fromCommit: models.IInstrumentCommit;
        toCommit: models.IInstrumentCommit;
        deletedParts: models.IPart[];
        addedParts: models.IPart[];
        modifiedParts: models.IPartVersion[];
        constructor() {
            this.deletedParts = [];
            this.addedParts = [];
            this.modifiedParts = [];
        }
    }

    export interface IInstrumentDiffDto {
        fromInstrument: models.IInstrumentDto;
        toInstrument: models.IInstrumentDto;
        fromCommit: models.IInstrumentCommitDto;
        toCommit: models.IInstrumentCommitDto;
        deletedParts: models.IPartDto[];
        addedParts: models.IPartDto[];
        modifiedParts: models.IPartVersionDto[];
    }

    export class InstrumentDiffDto implements IInstrumentDiffDto {
        fromInstrument: models.IInstrumentDto;
        toInstrument: models.IInstrumentDto;
        fromCommit: models.IInstrumentCommitDto;
        toCommit: models.IInstrumentCommitDto;
        deletedParts: models.IPartDto[];
        addedParts: models.IPartDto[];
        modifiedParts: models.IPartVersionDto[];

        constructor() {
            this.deletedParts = [];
            this.addedParts = [];
            this.modifiedParts = [];
        }

        static mapToObj(source: IInstrumentDiffDto): IInstrumentDiff {
            if (source == null) return null;
            var target = new InstrumentDiff();
            target.fromInstrument = models.InstrumentDto.mapToObj(source.fromInstrument);
            target.toInstrument = models.InstrumentDto.mapToObj(source.toInstrument);
            target.fromCommit = models.InstrumentCommitDto.mapToObj(source.fromCommit);
            target.toCommit = models.InstrumentCommitDto.mapToObj(source.toCommit);
            _.each(source.deletedParts, (obj: IPartDto) => {
                target.deletedParts.push(models.PartDto.mapToObj(obj));
            });
            _.each(source.addedParts, (obj: IPartDto) => {
                target.addedParts.push(models.PartDto.mapToObj(obj));
            });
            _.each(source.modifiedParts, (obj: IPartVersionDto) => {
                target.modifiedParts.push(models.PartVersionDto.mapToObj(obj));
            });
            return target;
        }
    }
}