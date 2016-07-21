// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartMetadataDto = (function () {
            function PartMetadataDto() {
            }
            PartMetadataDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartMetadata();
                target.id = source.id;
                target.metaKey = source.metaKey;
                target.metaValue = source.metaValue;
                target.isNew = source.isNew;
                if (source.modifier) {
                    target.modifier = models.UserDto.mapToObj(source.modifier);
                    target.modifiedDate = moment(source.modifiedDate).toDate();
                }
                return target;
            };
            return PartMetadataDto;
        }());
        models.PartMetadataDto = PartMetadataDto;
        var PartMetadata = (function () {
            function PartMetadata() {
            }
            PartMetadata.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartMetadataDto();
                target.id = source.id;
                target.metaKey = source.metaKey;
                target.metaValue = source.metaValue;
                target.isNew = source.isNew;
                if (source.modifier) {
                    target.modifier = models.User.mapToDto(source.modifier);
                    target.modifiedDate = source.modifiedDate.toDateString();
                }
                return target;
            };
            return PartMetadata;
        }());
        models.PartMetadata = PartMetadata;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partMetadata.model.js.map