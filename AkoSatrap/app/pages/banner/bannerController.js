mainApp.lazy.controller('projectController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar', 'fileUploaderService'
    , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar, fileUploaderService) {

        $rootScope.pageTitle = 'مدیریت بنر ها';
        $scope.fileAttachment;

        $scope.getBanners = function () {
            $scope.banners = [];
            $scope.myPromise = invokeServerService.Get('/Banner/GetBanners').success(function (result) {
                if (result.Data) {
                    $scope.banners = result.Data;
                }
            })
        }
        $scope.getBanners();

        $scope.uploadFile = function (bannerName, attachment) {
            if (!attachment) return;
            $scope.myPromise = fileUploaderService.uploadFileToUrl(attachment, '/Banner/SetBanner', bannerName)
                .then(function (result) {
                    if (result.Success) {
                        messageFactory.showAlert(result.Message, 'success');
                        $scope.getBanners();
                        location.reload(true);
                    } else {
                        messageFactory.showAlert(result.Message, 'danger');
                    }}, function () {
                    });
        };
    }]);