// ReSharper disable once InconsistentNaming
module app.partMetadatas {
    "use strict";

    interface IPartMetadataScope {
        partMetadata: models.IPartMetadata;
        cancel(): void;
        save(): void;
        modelErrors: string[];
        isReadOnly(): boolean;
    }

    class PartMetadataController implements IPartMetadataScope {
        partMetadata: models.IPartMetadata;
        modelErrors: string[];
        part: models.IPart;

        static $inject = ["app.services.AuthorizationService",
            "$uibModalInstance",
            "toastr",
            "metadata",
            "part",
            "app.services.PartMetadataService"];

        constructor(private authorizationService: services.IAuthorizationService,
            private $modalInstance: angular.ui.bootstrap.IModalServiceInstance,
            private toastr: angular.toastr.IToastrService,
            metadata: models.IPartMetadata, 
            part: models.IPart,
            private partMetadataService: services.IPartMetadataService) {

            this.partMetadata = metadata;
            this.part = part;
            this.modelErrors = [];
        }

        save = () => {
            this.partMetadataService.save(this.part.id, this.partMetadata).then((metadata: models.IPartMetadata) => {
                this.partMetadata = metadata;
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

    angular.module("app.partMetadata")
        .controller("app.partMetadata.PartMetadataController", PartMetadataController);

}
