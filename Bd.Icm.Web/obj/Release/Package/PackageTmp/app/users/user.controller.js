// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var users;
    (function (users) {
        "use strict";
        var UserController = (function () {
            function UserController($state, authService, userService, $stateParams, $location, toastr, dialogService) {
                var _this = this;
                this.$state = $state;
                this.authService = authService;
                this.userService = userService;
                this.$stateParams = $stateParams;
                this.$location = $location;
                this.toastr = toastr;
                this.dialogService = dialogService;
                this.mapRoles = function (user) {
                    _.each(user.roles, function (role) {
                        var found = _.find(_this.roles, function (r) {
                            return r.value === role;
                        });
                        if (found) {
                            found.isSelected = true;
                        }
                    });
                };
                this.setRoles = function () {
                    if (!_this.user)
                        return;
                    _this.user.roles.length = 0;
                    _.each(_this.roles, function (role) {
                        if (role.isSelected) {
                            _this.user.roles.push(role.value);
                        }
                    });
                };
                this.save = function () {
                    _this.setRoles();
                    _this.userService.save(_this.user).then(function (user) {
                        var newUser = _this.user.isNew;
                        if (_this.user.isNew) {
                            _this.tempPassword = user.password;
                        }
                        _this.user = user;
                        _this.toastr.success("User saved.");
                        if (!newUser) {
                            _this.$state.go("root.security.users");
                        }
                    }, function (reason) {
                        if (reason.data && reason.data.modelState) {
                            var msg = "Object is not valid: ";
                            _.each(reason.data.modelState, function (item) {
                                msg = msg + item;
                            });
                            _this.toastr.error(msg);
                        }
                        else {
                            _this.toastr.error("Error saving user.");
                        }
                    });
                };
                this.delete = function () {
                    _this.dialogService.askYesNo("Delete User", "Are you sure you want to delete this user?").then(function (response) {
                        if (response === app.services.DialogResult.Yes) {
                            _this.userService.delete(_this.user).then(function () {
                                _this.toastr.success("User deleted.");
                                _this.$state.go("root.security.users");
                            }, function () {
                                _this.toastr.error("Error deleting this user.");
                            });
                        }
                    });
                };
                var vm = this;
                this.authService.currentUser();
                this.roles = [];
                this.roles.push(new app.models.Selector("Read-only", app.enums.RoleType.ReadOnly));
                this.roles.push(new app.models.Selector("Contributor", app.enums.RoleType.Contributor));
                this.roles.push(new app.models.Selector("Administrator", app.enums.RoleType.Administrator));
                this.roles.push(new app.models.Selector("Committer", app.enums.RoleType.Committer));
                var id = $stateParams["id"];
                if (id === "new") {
                    userService.create().then(function (data) {
                        vm.user = data;
                    }, function () {
                        _this.toastr.error("Error creating a new user.");
                    });
                }
                else {
                    userService.get(id).then(function (data) {
                        vm.user = data;
                        _this.mapRoles(vm.user);
                    }, function () {
                        _this.toastr.error("Error retrieving user.");
                    });
                }
            }
            UserController.$inject = ["$state",
                "app.services.AuthenticationService",
                "app.services.UserService",
                "$stateParams",
                "$location",
                "toastr",
                "app.services.DialogService"
            ];
            return UserController;
        }());
        angular.module("app.users")
            .controller("app.users.UserController", UserController);
    })(users = app.users || (app.users = {}));
})(app || (app = {}));
//# sourceMappingURL=user.controller.js.map