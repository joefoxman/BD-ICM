// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var InstrumentDto = (function () {
            function InstrumentDto() {
                this.parts = [];
            }
            InstrumentDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new Instrument();
                target.instrumentId = source.instrumentId;
                target.type = source.type;
                target.majorRevision = source.majorRevision;
                target.minorRevision = source.minorRevision;
                target.sapPartType = models.InstrumentTypeDto.mapToObj(source.sapPartType);
                target.nickName = source.nickName;
                target.serialNumber = source.serialNumber;
                target.isNew = source.isNew;
                _.each(source.parts, function (part) {
                    target.parts.push(models.PartDto.mapToObj(part));
                });
                return target;
            };
            return InstrumentDto;
        }());
        models.InstrumentDto = InstrumentDto;
        var Instrument = (function () {
            function Instrument() {
                this.parts = new Array();
            }
            Object.defineProperty(Instrument.prototype, "name", {
                get: function () {
                    return this.nickName;
                },
                enumerable: true,
                configurable: true
            });
            Instrument.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new InstrumentDto();
                target.instrumentId = source.instrumentId;
                target.type = source.type;
                target.majorRevision = source.majorRevision;
                target.minorRevision = source.minorRevision;
                target.sapPartType = models.InstrumentType.mapToDto(source.sapPartType);
                target.nickName = source.nickName;
                target.serialNumber = source.serialNumber;
                target.isNew = source.isNew;
                target.parts = _.map(source.parts, function (part) {
                    return models.Part.mapToDto(part);
                });
                return target;
            };
            return Instrument;
        }());
        models.Instrument = Instrument;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=instrument.model.js.map