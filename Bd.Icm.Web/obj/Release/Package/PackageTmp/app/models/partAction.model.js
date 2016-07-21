// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var PartActionDto = (function () {
            function PartActionDto() {
            }
            PartActionDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new PartAction();
                target.id = source.id;
                target.action = models.PartActionTypeDto.mapToObj(source.action);
                target.description = source.description;
                target.actionDate = moment(source.actionDate).toDate();
                target.isNew = source.isNew;
                if (source.modifier) {
                    target.modifier = models.UserDto.mapToObj(source.modifier);
                    target.modifiedDate = moment(source.modifiedDate).toDate();
                }
                return target;
            };
            return PartActionDto;
        }());
        models.PartActionDto = PartActionDto;
        var PartAction = (function () {
            function PartAction() {
            }
            PartAction.mapToDto = function (source) {
                if (source == null)
                    return null;
                var target = new PartActionDto();
                target.id = source.id;
                target.action = models.PartActionType.mapToDto(source.action);
                target.description = source.description;
                target.actionDate = source.actionDate.toDateString();
                target.isNew = source.isNew;
                if (source.modifier) {
                    target.modifier = models.User.mapToDto(source.modifier);
                    target.modifiedDate = source.modifiedDate.toDateString();
                }
                return target;
            };
            return PartAction;
        }());
        models.PartAction = PartAction;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=partAction.model.js.map