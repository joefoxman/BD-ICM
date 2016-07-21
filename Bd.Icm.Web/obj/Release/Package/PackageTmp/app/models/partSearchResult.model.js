// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartSearchResultDto = (function () {
            function PartSearchResultDto() {
            }
            PartSearchResultDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartSearchResult();
                target.partId = source.partId;
                target.instrumentId = source.instrumentId;
                target.name = source.name;
                target.description = source.description;
                target.documentNumber = source.documentNumber;
                target.sapPartNumber = source.sapPartNumber;
                target.serialNumber = source.serialNumber;
                target.parentPartId = source.parentPartId;
                target.mfgPartNumber = source.mfgPartNumber;
                return target;
            };
            return PartSearchResultDto;
        }());
        models.PartSearchResultDto = PartSearchResultDto;
        var PartSearchResult = (function () {
            function PartSearchResult() {
            }
            PartSearchResult.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartSearchResultDto();
                target.partId = source.partId;
                target.instrumentId = source.instrumentId;
                target.parentPartId = source.parentPartId;
                target.name = source.name;
                target.description = source.description;
                target.documentNumber = source.documentNumber;
                target.serialNumber = source.serialNumber;
                target.sapPartNumber = source.sapPartNumber;
                target.mfgPartNumber = source.mfgPartNumber;
                return target;
            };
            return PartSearchResult;
        }());
        models.PartSearchResult = PartSearchResult;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partSearchResult.model.js.map