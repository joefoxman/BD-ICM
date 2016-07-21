// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartDto = (function () {
            function PartDto() {
                this.parts = [];
                this.actions = [];
                this.metadata = [];
            }
            PartDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new Part();
                target.id = source.id;
                target.instrumentId = source.instrumentId;
                target.name = source.name;
                target.description = source.description;
                target.documentNumber = source.documentNumber;
                target.dashNumber = source.dashNumber;
                target.revisionNumber = source.revisionNumber;
                target.sapPartNumber = source.sapPartNumber;
                target.sapPartType = models.PartType.mapToDto(source.sapPartType);
                target.serialNumber = source.serialNumber;
                target.lotCode = source.lotCode;
                target.dateCode = source.dateCode;
                target.isNew = source.isNew;
                target.parentPartId = source.parentPartId;
                target.mfgPartNumber = source.mfgPartNumber;
                target.manufacturer = source.manufacturer;
                target.modifier = source.modifier;
                target.modifiedDate = moment(source.modifiedDate).toDate();
                target.creator = source.creator;
                target.createdDate = moment(source.createdDate).toDate();
                target.modificationType = source.modificationType;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                _.each(source.parts, function (dto) {
                    target.parts.push(PartDto.mapToObj(dto));
                });
                _.each(source.actions, function (dto) {
                    target.actions.push(models.PartActionDto.mapToObj(dto));
                });
                _.each(source.metadata, function (dto) {
                    target.metadata.push(models.PartMetadataDto.mapToObj(dto));
                });
                return target;
            };
            return PartDto;
        }());
        models.PartDto = PartDto;
        var Part = (function () {
            function Part() {
                this.parts = [];
                this.actions = [];
                this.metadata = [];
            }
            Part.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartDto();
                target.id = source.id;
                target.instrumentId = source.instrumentId;
                target.parentPartId = source.parentPartId;
                target.name = source.name;
                target.description = source.description;
                target.documentNumber = source.documentNumber;
                target.dashNumber = source.dashNumber;
                target.revisionNumber = source.revisionNumber;
                target.serialNumber = source.serialNumber;
                target.sapPartNumber = source.sapPartNumber;
                target.sapPartType = models.PartType.mapToDto(source.sapPartType);
                target.lotCode = source.lotCode;
                target.dateCode = source.dateCode;
                target.mfgPartNumber = source.mfgPartNumber;
                target.manufacturer = source.manufacturer;
                target.isNew = source.isNew;
                target.modifier = source.modifier;
                target.modifiedDate = source.modifiedDate.toDateString();
                target.modificationType = source.modificationType;
                target.creator = source.creator;
                target.createdDate = source.createdDate.toDateString();
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                _.each(source.parts, function (obj) {
                    target.parts.push(Part.mapToDto(obj));
                });
                _.each(source.actions, function (obj) {
                    target.actions.push(models.PartAction.mapToDto(obj));
                });
                _.each(source.metadata, function (obj) {
                    target.metadata.push(models.PartMetadata.mapToDto(obj));
                });
                return target;
            };
            return Part;
        }());
        models.Part = Part;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=part.model.js.map