// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var Selector = (function () {
            function Selector(text, value) {
                this.value = value;
                this.text = text;
            }
            return Selector;
        }());
        models.Selector = Selector;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=selector.model.js.map