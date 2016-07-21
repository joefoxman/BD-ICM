// ReSharper disable once InconsistentNaming
var app;
(function (app) {
    var partMetadatas;
    (function (partMetadatas) {
        "use strict";
        var PartMetadataController = (function () {
            function PartMetadataController(authorizationService, $modalInstance, toastr, metadata, part, partMetadataService) {
                var _this = this;
                this.authorizationService = authorizationService;
                this.$modalInstance = $modalInstance;
                this.toastr = toastr;
                this.partMetadataService = partMetadataService;
                this.save = function () {
                    _this.partMetadataService.save(_this.part.id, _this.partMetadata).then(function (metadata) {
                        _this.partMetadata = metadata;
                        _this.$modalInstance.close();
                    }, function (response) {
                        if (response.data.modelState) {
                            _this.modelErrors.length = 0;
                            _.each(response.data.modelState, function (error) {
                                _this.modelErrors.push(error);
                            });
                        }
                        else {
                            _this.toastr.error(response.data.message);
                        }
                    });
                };
                this.cancel = function () {
                    _this.$modalInstance.dismiss();
                };
                this.isReadOnly = function () {
                    return _this.authorizationService.isInRole(["ReadOnly"]);
                };
                this.partMetadata = metadata;
                this.part = part;
                this.modelErrors = [];
            }
            PartMetadataController.$inject = ["app.services.AuthorizationService",
                "$uibModalInstance",
                "toastr",
                "metadata",
                "part",
                "app.services.PartMetadataService"];
            return PartMetadataController;
        }());
        angular.module("app.partMetadata")
            .controller("app.partMetadata.PartMetadataController", PartMetadataController);
    })(partMetadatas = app.partMetadatas || (app.partMetadatas = {}));
})(app || (app = {}));
//# sourceMappingURL=partMetadata.controller.js.map