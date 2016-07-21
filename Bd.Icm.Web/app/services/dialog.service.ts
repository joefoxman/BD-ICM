module app.services {
    export enum DialogResult {
        Yes,
        No,
        Cancel,
        Ok
    }

    export interface IDialogService {
        askYesNo(title: string, message: string): ng.IPromise<DialogResult>;
    }

    class DialogService implements IDialogService {
        
        static $inject = ["$q", "$uibModal"];

        constructor(private $q: ng.IQService,
            private $modal: angular.ui.bootstrap.IModalService) {
            
        }

        askYesNo = (title: string, message: string): ng.IPromise<DialogResult> => {
            var deferred = this.$q.defer();

            var modalInstance = this.$modal.open({
                templateUrl: '/app/dialog/yesNoCancel.view.html',
                controller: 'app.dialog.DialogController as vm',
                resolve: {
                    message: () => message,
                    title: () => title
                }
            });

            modalInstance.result.then((response: DialogResult) => {
                deferred.resolve(response);
            });

            return deferred.promise;
        }
    }

    factory.$inject = ["$q", "$uibModal"];
    function factory($q: ng.IQService,
        $modal: angular.ui.bootstrap.IModalService): IDialogService {
        return new DialogService($q, $modal);
    }

    angular.module("app.services")
        .factory("app.services.DialogService", factory);

} 