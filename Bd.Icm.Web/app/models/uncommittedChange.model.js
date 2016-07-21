// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var UncommittedChangeDto = (function () {
            function UncommittedChangeDto() {
            }
            UncommittedChangeDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new UncommittedChange();
                target.id = source.id;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                target.location = source.location;
                target.recordType = source.recordType;
                target.modifier = source.modifier;
                target.modifiedDate = moment(source.modifiedDate).toDate();
                target.creator = source.creator;
                target.createdDate = moment(source.createdDate).toDate();
                target.modificationType = source.modificationType;
                return target;
            };
            return UncommittedChangeDto;
        }());
        models.UncommittedChangeDto = UncommittedChangeDto;
        var UncommittedChange = (function () {
            function UncommittedChange() {
                this.isSelected = false;
            }
            UncommittedChange.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new UncommittedChangeDto();
                target.id = source.id;
                target.effectiveFrom = source.effectiveFrom;
                target.effectiveTo = source.effectiveTo;
                target.location = source.location;
                target.recordType = source.recordType;
                target.modifier = source.modifier;
                target.modifiedDate = source.modifiedDate.toDateString();
                target.creator = source.creator;
                target.createdDate = source.createdDate.toDateString();
                target.modificationType = source.modificationType;
                return target;
            };
            return UncommittedChange;
        }());
        models.UncommittedChange = UncommittedChange;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=uncommittedChange.model.js.map