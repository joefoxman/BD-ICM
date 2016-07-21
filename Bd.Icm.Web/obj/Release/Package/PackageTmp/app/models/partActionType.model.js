// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        var PartActionType = (function () {
            function PartActionType() {
            }
            PartActionType.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartActionType();
                target.partActionTypeId = source.partActionTypeId;
                target.name = source.name;
                target.isDisabled = source.isDisabled;
                return target;
            };
            return PartActionType;
        }());
        models.PartActionType = PartActionType;
        var PartActionTypeDto = (function () {
            function PartActionTypeDto() {
            }
            PartActionTypeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartActionType();
                target.partActionTypeId = source.partActionTypeId;
                target.name = source.name;
                target.isDisabled = source.isDisabled;
                return target;
            };
            return PartActionTypeDto;
        }());
        models.PartActionTypeDto = PartActionTypeDto;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partActionType.model.js.map