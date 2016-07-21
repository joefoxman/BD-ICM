// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var UserDto = (function () {
            function UserDto() {
                this.isNew = true;
            }
            UserDto.mapToObj = function (source) {
                var dest = new User();
                dest.id = source.id;
                dest.userName = source.userName;
                dest.password = source.password;
                dest.roles = source.roles;
                dest.firstName = source.firstName;
                dest.lastName = source.lastName;
                dest.email = source.email;
                dest.isNew = source.isNew;
                dest.removePassword = source.removePassword;
                return dest;
            };
            return UserDto;
        }());
        models.UserDto = UserDto;
        var User = (function () {
            function User() {
                this.isNew = true;
            }
            User.mapToDto = function (source) {
                var dest = new UserDto();
                dest.id = source.id;
                dest.userName = source.userName;
                dest.password = source.password;
                dest.firstName = source.firstName;
                dest.lastName = source.lastName;
                dest.roles = source.roles;
                dest.email = source.email;
                dest.isNew = source.isNew;
                dest.removePassword = source.removePassword;
                return dest;
            };
            return User;
        }());
        models.User = User;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=user.model.js.map