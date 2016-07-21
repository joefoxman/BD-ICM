// ReSharper disable once InconsistentNaming
module app.users {
    "use strict";

    interface IUserScope {
        user: models.IUser;
        save(): void;
        delete(): void;
        roles: models.Selector<enums.RoleType>[];
        tempPassword: string;
    }

    class UserController implements IUserScope {
        user: models.IUser;
        roles: models.Selector<enums.RoleType>[];
        tempPassword: string;

        static $inject = ["$state",
            "app.services.AuthenticationService",
            "app.services.UserService",
            "$stateParams",
            "$location",
            "toastr",
            "app.services.DialogService"
        ];
        constructor(private $state: angular.ui.IStateService,
            private authService: services.IAuthenticationService,
            private userService: services.IUserService,
            private $stateParams: angular.ui.IStateParamsService,
            private $location: ng.ILocationService,
            private toastr: angular.toastr.IToastrService,
            private dialogService: services.IDialogService) {

            var vm = this;

            this.authService.currentUser();
            this.roles = [];
            this.roles.push(new models.Selector<enums.RoleType>("Read-only", enums.RoleType.ReadOnly));
            this.roles.push(new models.Selector<enums.RoleType>("Contributor", enums.RoleType.Contributor));
            this.roles.push(new models.Selector<enums.RoleType>("Administrator", enums.RoleType.Administrator));
            this.roles.push(new models.Selector<enums.RoleType>("Committer", enums.RoleType.Committer));

            var id = $stateParams["id"];
            if (id === "new") {
                userService.create().then((data: models.IUser): void => {
                    vm.user = data;
                }, () => {
                    this.toastr.error("Error creating a new user.");
                });
            } else {
                userService.get(id).then((data: models.IUser): void => {
                    vm.user = data;
                    this.mapRoles(vm.user);
                }, () => {
                    this.toastr.error("Error retrieving user.");
                });
            }
        }

        private mapRoles = (user: models.IUser) => {
            _.each(user.roles, (role: enums.RoleType) => {
                var found = _.find(this.roles, (r: models.Selector<enums.RoleType>) => {
                    return r.value === role;
                });
                if (found) {
                    found.isSelected = true;
                }
            });
        }

        private setRoles = () => {
            if (!this.user) return;
            this.user.roles.length = 0;
            _.each(this.roles, (role: models.Selector<enums.RoleType>) => {
                if (role.isSelected) {
                    this.user.roles.push(role.value);
                }
            });
        }

        save = () => {
            this.setRoles();
            this.userService.save(this.user).then((user: models.IUser) => {
                var newUser = this.user.isNew;
                if (this.user.isNew) {
                    this.tempPassword = user.password;
                }
                this.user = user;
                this.toastr.success("User saved.");
                if (!newUser) {
                    this.$state.go("root.security.users");
                }
            },(reason) => {
                    if (reason.data && reason.data.modelState) {
                        var msg = "Object is not valid: ";
                        _.each(reason.data.modelState, (item) => {
                            msg = msg + item;
                        });
                    this.toastr.error(msg);
                } else {
                    this.toastr.error("Error saving user.");
                }
            });
        };

        delete = () => {
            this.dialogService.askYesNo("Delete User", "Are you sure you want to delete this user?").then((response: services.DialogResult) => {
                if (response === services.DialogResult.Yes) {
                    this.userService.delete(this.user).then(() => {
                        this.toastr.success("User deleted.");
                        this.$state.go("root.security.users");
                    },
                    () => {
                        this.toastr.error("Error deleting this user.");
                    });
                }
            });
        };

    }

    angular.module("app.users")
        .controller("app.users.UserController", UserController);
} 