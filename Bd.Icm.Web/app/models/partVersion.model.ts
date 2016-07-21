// ReSharper disable once InconsistentNaming
module app.models {
    export interface IPartVersion {
        from: models.IPart;
        to: models.IPart;
    }

    export class PartVersion implements IPartVersion {
        from: models.IPart;
        to: models.IPart;
    }

    export interface IPartVersionDto {
        from: models.IPartDto;
        to: models.IPartDto;
    }

    export class PartVersionDto implements IPartVersionDto {
        from: models.IPartDto;
        to: models.IPartDto;

        static mapToObj(source: IPartVersionDto): IPartVersion {
            if (source == null) return null;
            var target = new PartVersion();
            target.from = models.PartDto.mapToObj(source.from);
            target.to = models.PartDto.mapToObj(source.to);
            return target;
        }
    }
}