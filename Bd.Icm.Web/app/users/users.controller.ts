// ReSharper disable once InconsistentNaming
module app.users {
    "use strict";

    interface IUsersScope {
        delete(user: models.IUser): void;
        add(): void;
        isBusy: boolean;
    }

    class UsersController implements IUsersScope {
        users: models.IUser[];
        isBusy: boolean;

        static $inject = ["$state",
            "app.services.UserService",
            "app.services.AuthenticationService",
            "app.services.DialogService",
            "toastr"];

        constructor(private $state: angular.ui.IStateService,
            private userService: services.IUserService,
            private authService: services.IAuthenticationService,
            private dialogService: services.IDialogService,
            private toastr: angular.toastr.IToastrService) {

            this.isBusy = true;
            this.userService.getAll().then((data: models.IUser[]): void => {
                this.users = angular.copy(data);
                this.isBusy = false;
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
                this.isBusy = false;
            });
        }

        delete = (user: models.IUser) => {
            this.dialogService.askYesNo("Delete User", "Are you sure you want to delete this user?").then((response: services.DialogResult) => {
                if (response === services.DialogResult.Yes) {
                    this.userService.delete(user).then(() => {
                        this.users.splice(this.users.indexOf(user));
                        this.toastr.success("User deleted.");
                    },
                        () => {
                            this.toastr.error("Error deleting this user.");
                        });
                }
            });
        };

        add = () => {
            this.$state.go("root.security.user", { id: "new" });
        }
    }

    angular.module("app.users")
        .controller("app.users.UsersController", UsersController);
} 