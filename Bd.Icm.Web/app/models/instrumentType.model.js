// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        var InstrumentType = (function () {
            function InstrumentType() {
            }
            InstrumentType.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new InstrumentType();
                target.instrumentTypeId = source.instrumentTypeId;
                target.name = source.name;
                target.isDisabled = source.isDisabled;
                return target;
            };
            return InstrumentType;
        }());
        models.InstrumentType = InstrumentType;
        var InstrumentTypeDto = (function () {
            function InstrumentTypeDto() {
            }
            InstrumentTypeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new InstrumentType();
                target.instrumentTypeId = source.instrumentTypeId;
                target.name = source.name;
                target.isDisabled = source.isDisabled;
                return target;
            };
            return InstrumentTypeDto;
        }());
        models.InstrumentTypeDto = InstrumentTypeDto;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=instrumentType.model.js.map