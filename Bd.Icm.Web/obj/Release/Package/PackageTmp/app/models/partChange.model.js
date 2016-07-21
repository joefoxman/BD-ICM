// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartChangeDto = (function () {
            function PartChangeDto() {
            }
            PartChangeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartChange();
                target.partId = source.partId;
                target.instrumentId = source.instrumentId;
                target.name = source.name;
                target.description = source.description;
                target.documentNumber = source.documentNumber;
                target.dashNumber = source.dashNumber;
                target.sapPartNumber = source.sapPartNumber;
                target.serialNumber = source.serialNumber;
                target.modifier = source.modifier;
                target.creator = source.creator;
                target.modifiedDate = moment(source.modifiedDate).toDate();
                target.createdDate = moment(source.createdDate).toDate();
                target.modificationType = source.modificationType;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                return target;
            };
            return PartChangeDto;
        }());
        models.PartChangeDto = PartChangeDto;
        var PartChange = (function () {
            function PartChange() {
                this.isSelected = false;
            }
            PartChange.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartChangeDto();
                target.partId = source.partId;
                target.instrumentId = source.instrumentId;
                target.name = source.name;
                target.description = source.description;
                target.documentNumber = source.documentNumber;
                target.dashNumber = source.dashNumber;
                target.serialNumber = source.serialNumber;
                target.sapPartNumber = source.sapPartNumber;
                target.modifier = source.modifier;
                target.creator = source.creator;
                target.modifiedDate = source.modifiedDate.toDateString();
                target.createdDate = source.createdDate.toDateString();
                target.modificationType = source.modificationType;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                return target;
            };
            return PartChange;
        }());
        models.PartChange = PartChange;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partChange.model.js.map