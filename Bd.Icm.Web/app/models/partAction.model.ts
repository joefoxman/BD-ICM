// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IPartActionDto {
        id: number;
        action: IPartActionTypeDto;
        description: string;
        actionDate: string;
        isNew: boolean;
        modifiedDate: string;
        modifier: models.IUserDto;
    }

    export interface IPartAction {
        id: number;
        action: IPartActionType;
        description: string;
        actionDate: Date;
        isNew: boolean;
        modifier: models.IUser;
        modifiedDate: Date;
    }

    export class PartActionDto implements IPartActionDto {
        id: number;
        action: IPartActionTypeDto;
        description: string;
        actionDate: string;
        isNew: boolean;
        modifier: models.IUserDto;
        modifiedDate: string;

        static mapToObj(source: IPartActionDto): IPartAction {
            if (source == null) return null;
            var target = new PartAction();
            target.id = source.id;
            target.action = PartActionTypeDto.mapToObj(source.action);
            target.description = source.description;
            target.actionDate = moment(source.actionDate).toDate();
            target.isNew = source.isNew;
            if (source.modifier) {
                target.modifier = models.UserDto.mapToObj(source.modifier);
                target.modifiedDate = moment(source.modifiedDate).toDate();
            }
            return target;
        }

    }

    export class PartAction implements IPartAction {
        id: number;
        partId: number;
        action: IPartActionType;
        description: string;
        actionDate: Date;
        isNew: boolean;
        modifier: models.IUser;
        modifiedDate: Date;

        static mapToDto(source: IPartAction): IPartActionDto {
            if (source == null) return null;
            var target = new PartActionDto();
            target.id = source.id;
            target.action = PartActionType.mapToDto(source.action);
            target.description = source.description;
            target.actionDate = source.actionDate.toDateString();
            target.isNew = source.isNew;
            if (source.modifier) {
                target.modifier = models.User.mapToDto(source.modifier);
                target.modifiedDate = source.modifiedDate.toDateString();
            }
            return target;
        }
    }
} 