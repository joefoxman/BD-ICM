// ReSharper disable once InconsistentNaming
module app.models {
    export interface IBreadcrumb {
        title: string;
        route: string;
        sref: string;
        params: any;
    }

    export class Breadcrumb implements IBreadcrumb {
        title: string;
        route: string;
        sref: string;
        params: any;
    }
}