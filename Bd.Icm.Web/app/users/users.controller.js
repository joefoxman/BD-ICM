// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var users;
    (function (users) {
        "use strict";
        var UsersController = (function () {
            function UsersController($state, userService, authService, dialogService, toastr) {
                var _this = this;
                this.$state = $state;
                this.userService = userService;
                this.authService = authService;
                this.dialogService = dialogService;
                this.toastr = toastr;
                this.delete = function (user) {
                    _this.dialogService.askYesNo("Delete User", "Are you sure you want to delete this user?").then(function (response) {
                        if (response === app.services.DialogResult.Yes) {
                            _this.userService.delete(user).then(function () {
                                _this.users.splice(_this.users.indexOf(user));
                                _this.toastr.success("User deleted.");
                            }, function () {
                                _this.toastr.error("Error deleting this user.");
                            });
                        }
                    });
                };
                this.add = function () {
                    _this.$state.go("root.security.user", { id: "new" });
                };
                this.isBusy = true;
                this.userService.getAll().then(function (data) {
                    _this.users = angular.copy(data);
                    _this.isBusy = false;
                }).catch(function (reason) {
                    _this.toastr.error(reason.statusText);
                    _this.isBusy = false;
                });
            }
            UsersController.$inject = ["$state",
                "app.services.UserService",
                "app.services.AuthenticationService",
                "app.services.DialogService",
                "toastr"];
            return UsersController;
        }());
        angular.module("app.users")
            .controller("app.users.UsersController", UsersController);
    })(users = app.users || (app.users = {}));
})(app || (app = {}));
//# sourceMappingURL=users.controller.js.map