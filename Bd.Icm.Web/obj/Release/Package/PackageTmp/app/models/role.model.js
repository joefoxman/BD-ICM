// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var models;
    (function (models) {
        "use strict";
        var Role = (function () {
            function Role() {
                this.isNew = true;
            }
            Role.prototype.isValid = function () {
                return (this.name != null) && (this.name !== "") && (this.name.length <= 50);
            };
            Role.mapToDto = function (obj) {
                if (obj == null)
                    return null;
                var dto = new RoleDto();
                dto.roleId = obj.roleId;
                dto.name = obj.name;
                dto.description = obj.description;
                dto.isNew = obj.isNew;
                return dto;
            };
            return Role;
        }());
        models.Role = Role;
        var RoleDto = (function () {
            function RoleDto() {
                this.isNew = true;
            }
            RoleDto.mapToObj = function (dto) {
                if (dto == null)
                    return null;
                var obj = new Role();
                obj.roleId = dto.roleId;
                obj.name = dto.name;
                obj.description = dto.description;
                obj.isNew = dto.isNew;
                return obj;
            };
            return RoleDto;
        }());
        models.RoleDto = RoleDto;
    })(models = app.models || (app.models = {}));
})(app || (app = {}));
//# sourceMappingURL=role.model.js.map