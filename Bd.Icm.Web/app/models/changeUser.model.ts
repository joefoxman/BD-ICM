// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface IChangeUserDto {
        userId: number;
        firstName: string;
        lastName: string;
        changeCount: number;
    }

    export interface IChangeUser {
        userId: number;
        firstName: string;
        lastName: string;
        changeCount: number;
    }

    export class ChangeUserDto implements IChangeUserDto {
        userId: number;
        firstName: string;
        lastName: string;
        changeCount: number;

        static mapToObj(source: IChangeUserDto): IChangeUser {
            if (source == null) return null;
            var target = new ChangeUser();
            target.userId = source.userId;
            target.firstName = source.firstName;
            target.lastName = source.lastName;
            target.changeCount = source.changeCount;
            return target;
        }
    }

    export class ChangeUser implements IChangeUser {
        userId: number;
        firstName: string;
        lastName: string;
        changeCount: number;
    }
} 