// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export interface ILogin {
        userName: string;
        password: string;
    }

    export class Login implements ILogin {
        userName: string;
        password: string;
    }
} 