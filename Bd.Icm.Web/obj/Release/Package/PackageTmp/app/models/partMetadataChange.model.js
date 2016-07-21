// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartMetadataChangeDto = (function () {
            function PartMetadataChangeDto() {
            }
            PartMetadataChangeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartMetadataChange();
                target.partId = source.partId;
                target.name = source.name;
                target.description = source.description;
                target.metaKey = source.metaKey;
                target.metaValue = source.metaValue;
                target.modifier = source.modifier;
                target.creator = source.creator;
                target.modifiedDate = moment(source.modifiedDate).toDate();
                target.createdDate = moment(source.createdDate).toDate();
                target.modificationType = source.modificationType;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                return target;
            };
            return PartMetadataChangeDto;
        }());
        models.PartMetadataChangeDto = PartMetadataChangeDto;
        var PartMetadataChange = (function () {
            function PartMetadataChange() {
                this.isSelected = false;
            }
            PartMetadataChange.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartMetadataChangeDto();
                target.partId = source.partId;
                target.name = source.name;
                target.description = source.description;
                target.metaKey = source.metaKey;
                target.metaValue = source.metaValue;
                target.modifier = source.modifier;
                target.creator = source.creator;
                target.modifiedDate = source.modifiedDate.toDateString();
                target.createdDate = source.createdDate.toDateString();
                target.modificationType = source.modificationType;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                return target;
            };
            return PartMetadataChange;
        }());
        models.PartMetadataChange = PartMetadataChange;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partMetadataChange.model.js.map