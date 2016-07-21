// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        var PartVersion = (function () {
            function PartVersion() {
            }
            return PartVersion;
        }());
        models.PartVersion = PartVersion;
        var PartVersionDto = (function () {
            function PartVersionDto() {
            }
            PartVersionDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartVersion();
                target.from = models.PartDto.mapToObj(source.from);
                target.to = models.PartDto.mapToObj(source.to);
                return target;
            };
            return PartVersionDto;
        }());
        models.PartVersionDto = PartVersionDto;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partVersion.model.js.map