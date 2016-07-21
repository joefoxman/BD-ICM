// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var ChangeUserDto = (function () {
            function ChangeUserDto() {
            }
            ChangeUserDto.mapToObj = function (source) {
                if (source == null)
                    return null;
                var target = new ChangeUser();
                target.userId = source.userId;
                target.firstName = source.firstName;
                target.lastName = source.lastName;
                target.changeCount = source.changeCount;
                return target;
            };
            return ChangeUserDto;
        }());
        models.ChangeUserDto = ChangeUserDto;
        var ChangeUser = (function () {
            function ChangeUser() {
            }
            return ChangeUser;
        }());
        models.ChangeUser = ChangeUser;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=changeUser.model.js.map