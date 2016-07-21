// ReSharper disable once InconsistentNaming
module app.models {
    "use strict";

    export class Selector<T> {
        isSelected: boolean;
        value: T;
        text: string;

        constructor(text: string, value: T) {
            this.value = value;
            this.text = text;
        }
   }
}