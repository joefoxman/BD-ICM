// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var InstrumentCommitDto = (function () {
            function InstrumentCommitDto() {
            }
            InstrumentCommitDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new InstrumentCommit();
                target.id = source.id;
                target.instrumentId = source.instrumentId;
                target.notes = source.notes;
                target.createdDate = moment(source.createdDate).toDate();
                target.creator = source.creator;
                target.effectiveTo = source.effectiveTo;
                target.revision = source.revision;
                target.isNew = source.isNew;
                return target;
            };
            return InstrumentCommitDto;
        }());
        models.InstrumentCommitDto = InstrumentCommitDto;
        var InstrumentCommit = (function () {
            function InstrumentCommit() {
            }
            InstrumentCommit.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new InstrumentCommitDto();
                target.id = source.id;
                target.instrumentId = source.instrumentId;
                target.notes = source.notes;
                target.createdDate = source.createdDate.toDateString();
                target.creator = source.creator;
                target.effectiveTo = source.effectiveTo;
                target.revision = source.revision;
                target.isNew = source.isNew;
                return target;
            };
            return InstrumentCommit;
        }());
        models.InstrumentCommit = InstrumentCommit;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=instrumentCommit.model.js.map