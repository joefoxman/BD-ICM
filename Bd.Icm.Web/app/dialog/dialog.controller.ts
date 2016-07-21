module app.dialog {
    "use strict";

    interface IDialogScope {
        title: string;
        message: string;
        yes(): void;
        no(): void;
        cancel(): void;
    }

    class DialogController implements IDialogScope {
        message: string;
        title: string;

        static $inject = ['message', 'title', '$uibModalInstance'];

        constructor(message: string, title: string,
            private $modalInstance: angular.ui.bootstrap.IModalServiceInstance) {
            this.message = message;
            this.title = title;
        }

        yes = () => {
            this.$modalInstance.close(services.DialogResult.Yes);
        };

        no = () => {
            this.$modalInstance.close(services.DialogResult.No);
        };

        cancel = () => {
            this.$modalInstance.dismiss(services.DialogResult.Cancel);
        };
    }

    angular.module("app.dialog")
        .controller("app.dialog.DialogController", DialogController);

}
