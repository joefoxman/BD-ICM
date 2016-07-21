// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartNodeDto = (function () {
            function PartNodeDto() {
            }
            PartNodeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartNode();
                target.partId = source.partId;
                target.instrumentId = source.instrumentId;
                target.name = source.name;
                target.description = source.description;
                target.level = source.level;
                target.parentPartId = source.parentPartId;
                return target;
            };
            return PartNodeDto;
        }());
        models.PartNodeDto = PartNodeDto;
        var PartNode = (function () {
            function PartNode() {
            }
            return PartNode;
        }());
        models.PartNode = PartNode;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partNode.model.js.map