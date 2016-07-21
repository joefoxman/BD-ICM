// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        var PartType = (function () {
            function PartType() {
            }
            PartType.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartType();
                target.partTypeId = source.partTypeId;
                target.name = source.name;
                target.isDisabled = source.isDisabled;
                return target;
            };
            return PartType;
        }());
        models.PartType = PartType;
        var PartTypeDto = (function () {
            function PartTypeDto() {
            }
            PartTypeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartType();
                target.partTypeId = source.partTypeId;
                target.name = source.name;
                target.isDisabled = source.isDisabled;
                return target;
            };
            return PartTypeDto;
        }());
        models.PartTypeDto = PartTypeDto;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partType.model.js.map