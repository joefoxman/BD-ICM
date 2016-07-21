// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        var InstrumentDiff = (function () {
            function InstrumentDiff() {
                this.deletedParts = [];
                this.addedParts = [];
                this.modifiedParts = [];
            }
            return InstrumentDiff;
        }());
        models.InstrumentDiff = InstrumentDiff;
        var InstrumentDiffDto = (function () {
            function InstrumentDiffDto() {
                this.deletedParts = [];
                this.addedParts = [];
                this.modifiedParts = [];
            }
            InstrumentDiffDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new InstrumentDiff();
                target.fromInstrument = models.InstrumentDto.mapToObj(source.fromInstrument);
                target.toInstrument = models.InstrumentDto.mapToObj(source.toInstrument);
                target.fromCommit = models.InstrumentCommitDto.mapToObj(source.fromCommit);
                target.toCommit = models.InstrumentCommitDto.mapToObj(source.toCommit);
                _.each(source.deletedParts, function (obj) {
                    target.deletedParts.push(models.PartDto.mapToObj(obj));
                });
                _.each(source.addedParts, function (obj) {
                    target.addedParts.push(models.PartDto.mapToObj(obj));
                });
                _.each(source.modifiedParts, function (obj) {
                    target.modifiedParts.push(models.PartVersionDto.mapToObj(obj));
                });
                return target;
            };
            return InstrumentDiffDto;
        }());
        models.InstrumentDiffDto = InstrumentDiffDto;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=instrumentDiff.model.js.map