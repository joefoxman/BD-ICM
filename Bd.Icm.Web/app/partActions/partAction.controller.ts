// ReSharper disable once InconsistentNaming
module app.partActions {
    "use strict";

    interface IPartActionScope {
        partAction: models.IPartAction;
        cancel(): void;
        actionTypes: models.IPartActionType[];
        modelErrors: string[];
        isReadOnly(): boolean;
        actionDateOpened: boolean;
        openActionDate();
    }

    class PartActionController implements IPartActionScope {
        partAction: models.IPartAction;
        actionTypes: models.IPartActionType[];
        modelErrors: string[];
        part: models.IPart;
        actionDateOpened: boolean;

        static $inject = ["app.services.AuthorizationService",
            "$uibModalInstance",
            "toastr",
            "action",
            "part",
            "app.services.PartActionService",
            "app.services.PartActionTypeService"];

        constructor(private authorizationService: services.IAuthorizationService,
            private $modalInstance: angular.ui.bootstrap.IModalServiceInstance,
            private toastr: angular.toastr.IToastrService,
            action: models.IPartAction, 
            part: models.IPart,
            private partActionService: services.IPartActionService,
            private partActionTypeService: services.IPartActionTypeService) {

            this.partAction = action;
            this.part = part;
            this.modelErrors = [];
            this.partActionTypeService.getAll().then((actions: models.IPartActionType[]) => {
                this.actionTypes = angular.copy(actions);
            }).catch((reason) => {
                this.toastr.error(reason.statusText);
            });
        }

        openActionDate = () => {
            this.actionDateOpened = true;
        }

        save = () => {
            this.partActionService.save(this.part.id, this.partAction).then((action: models.IPartAction) => {
                this.partAction = action;
                this.$modalInstance.close();
            }, (response) => {
                if (response.data.modelState) {
                    this.modelErrors.length = 0;
                    _.each(response.data.modelState, (error: string) => {
                        this.modelErrors.push(error);
                    });
                } else {
                    this.toastr.error(response.data.message);
                }

            });
        };

        cancel = () => {
            this.$modalInstance.dismiss();
        };

        isReadOnly = (): boolean => {
            return this.authorizationService.isInRole(["ReadOnly"]);
        }
    }

    angular.module("app.partActions")
        .controller("app.partActions.PartActionController", PartActionController);

}
