// ReSharper disable once InconsistentNaming
module app.models {
    export interface IPartType {
        partTypeId: number;
        name: string;
        isDisabled: boolean;
    }

    export class PartType implements IPartType {
        partTypeId: number;
        name: string;
        isDisabled: boolean;
        
        static mapToDto(source: IPartType): IPartTypeDto {
            if (source == null) return null;
            var target = new PartType();
            target.partTypeId = source.partTypeId;
            target.name = source.name;
            target.isDisabled = source.isDisabled;
            return target;
        }
    }

    export interface IPartTypeDto {
        partTypeId: number;
        name: string;
        isDisabled: boolean;
    }

    export class PartTypeDto implements IPartTypeDto {
        partTypeId: number;
        name: string;
        isDisabled: boolean;

        static mapToObj(source: IPartTypeDto): IPartType {
            if (source == null) return null;
            var target = new PartType();
            target.partTypeId = source.partTypeId;
            target.name = source.name;
            target.isDisabled = source.isDisabled;
            return target;
        }
    }
}