// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IUserDto {
        id: number;
        userName: string;
        password: string;
        firstName: string;
        lastName: string;
        email: string;
        isDisabled: boolean;
        isNew: boolean;
        roles: number[];
        removePassword: boolean;
    }

    export interface IUser {
        id: number;
        userName: string;
        password: string;
        firstName: string;
        lastName: string;
        email: string;
        roles: enums.RoleType[];
        isDisabled: boolean;
        isNew: boolean;
        removePassword: boolean;
    }

    export class UserDto implements IUserDto {
        id: number;
        userName: string;
        password: string;
        firstName: string;
        lastName: string;
        email: string;
        roles: number[];
        isDisabled: boolean;
        isNew: boolean;
        removePassword: boolean;

        constructor() {
            this.isNew = true;
        }

        static mapToObj(source: IUserDto): IUser {
            var dest = new User();
            dest.id = source.id;
            dest.userName = source.userName;
            dest.password = source.password;
            dest.roles = source.roles;
            dest.firstName = source.firstName;
            dest.lastName = source.lastName;
            dest.email = source.email;
            dest.isNew = source.isNew;
            dest.removePassword = source.removePassword;
            return dest;
        }
    }

    export class User implements IUser {
        id: number;
        userName: string;
        password: string;
        firstName: string;
        lastName: string;
        email: string;
        roles: enums.RoleType[];
        isDisabled: boolean;
        isNew: boolean;
        removePassword: boolean;

        constructor() {
            this.isNew = true;
        }

        static mapToDto(source: IUser): IUserDto {
            var dest = new UserDto();
            dest.id = source.id;
            dest.userName = source.userName;
            dest.password = source.password;
            dest.firstName = source.firstName;
            dest.lastName = source.lastName;
            dest.roles = source.roles;
            dest.email = source.email;
            dest.isNew = source.isNew;
            dest.removePassword = source.removePassword;
            return dest;
        }

    }
} 