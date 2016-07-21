// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartMetadataDto {
        id: number;
        metaKey: string;
        metaValue: string;
        isNew: boolean;
        modifiedDate: string;
        modifier: models.IUserDto;
    }

    export interface IPartMetadata {
        id: number;
        metaKey: string;
        metaValue: string;
        isNew: boolean;
        modifiedDate: Date;
        modifier: models.IUser;
    }

    export class PartMetadataDto implements IPartMetadataDto {
        id: number;
        metaKey: string;
        metaValue: string;
        isNew: boolean;
        modifier: models.IUserDto;
        modifiedDate: string;

        static mapToObj(source: IPartMetadataDto): IPartMetadata {
            if (source == null) return null;
            var target = new PartMetadata();
            target.id = source.id;
            target.metaKey = source.metaKey;
            target.metaValue = source.metaValue;
            target.isNew = source.isNew;
            if (source.modifier) {
                target.modifier = models.UserDto.mapToObj(source.modifier);
                target.modifiedDate = moment(source.modifiedDate).toDate();
            }
            return target;
        }

    }

    export class PartMetadata implements IPartMetadata {
        id: number;
        partId: number;
        metaKey: string;
        metaValue: string;
        isNew: boolean;
        modifier: models.IUser;
        modifiedDate: Date;

        static mapToDto(source: IPartMetadata): IPartMetadataDto {
            if (source == null) return null;
            var target = new PartMetadataDto();
            target.id = source.id;
            target.metaKey = source.metaKey;
            target.metaValue = source.metaValue;
            target.isNew = source.isNew;
            if (source.modifier) {
                target.modifier = models.User.mapToDto(source.modifier);
                target.modifiedDate = source.modifiedDate.toDateString();
            }
            return target;
        }
    }
} 