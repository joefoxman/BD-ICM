// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IRoleDto {
        roleId: number;
        name: string;
        description: string;
        isDisabled: boolean;
        isNew: boolean;
    }

    export interface IRole {
        roleId: number;
        name: string;
        description: string;
        isDisabled: boolean;
        isNew: boolean;
        isValid: () => boolean;
    }

    export class Role implements IRole {
        roleId: number;
        name: string;
        description: string;
        isDisabled: boolean;
        isNew: boolean;
        isValid(): boolean {
            return (this.name != null) && (this.name !== "") && (this.name.length <= 50);
        }

        constructor() {
            this.isNew = true;
        }

        static mapToDto(obj: IRole): IRoleDto {
            if (obj == null) return null;
            var dto = new RoleDto();
            dto.roleId = obj.roleId;
            dto.name = obj.name;
            dto.description = obj.description;
            dto.isNew = obj.isNew;
            return dto;
        }
    }

    export class RoleDto implements IRoleDto {
        roleId: number;
        name: string;
        description: string;
        isDisabled: boolean;
        isNew: boolean;

        constructor() {
            this.isNew = true;
        }

        static mapToObj(dto: IRoleDto): IRole {
            if (dto == null) return null;
            var obj = new Role();
            obj.roleId = dto.roleId;
            obj.name = dto.name;
            obj.description = dto.description;
            obj.isNew = dto.isNew;
            return obj;
        }

    }


} 