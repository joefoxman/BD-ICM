// ReSharper disable once InconsistentNaming
module app.models {
    export interface IPartActionType {
        partActionTypeId: number;
        name: string;
        isDisabled: boolean;
    }

    export class PartActionType implements IPartActionType {
        partActionTypeId: number;
        name: string;
        isDisabled: boolean;
        
        static mapToDto(source: IPartActionType): IPartActionTypeDto {
            if (source == null) return null;
            var target = new PartActionType();
            target.partActionTypeId = source.partActionTypeId;
            target.name = source.name;
            target.isDisabled = source.isDisabled;
            return target;
        }
    }

    export interface IPartActionTypeDto {
        partActionTypeId: number;
        name: string;
        isDisabled: boolean;
    }

    export class PartActionTypeDto implements IPartActionTypeDto {
        partActionTypeId: number;
        name: string;
        isDisabled: boolean;

        static mapToObj(source: IPartActionTypeDto): IPartActionType {
            if (source == null) return null;
            var target = new PartActionType();
            target.partActionTypeId = source.partActionTypeId;
            target.name = source.name;
            target.isDisabled = source.isDisabled;
            return target;
        }
    }
}