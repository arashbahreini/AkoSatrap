mainApp.lazy.controller('siteContentController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar', 'fileUploaderService'
    , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar, fileUploaderService) {

        $rootScope.pageTitle = 'مدیریت محتوا';

        $scope.pages = [
            { name: 'پیمانکاری', PageId: 1 },
            { name: 'آتش نشانی', PageId: 2 },
            { name: 'نظام مهندسی', PageId: 3 },
            { name: 'پیام مدیر عامل', PageId: 4 },
            { name: 'معرفی شرکت', PageId: 5 },
            { name: 'صلاحیت ها', PageId: 6 },
        ];
        $scope.page = {};
        $scope.content = {};
        setup_file_input();

        $scope.getSiteContent = function (page) {
            if (page.Page.PageId === 6) {
                $scope.getImages();
            }
            else {
                $scope.myPromise = invokeServerService.Post('/SiteContent/GetContent', { PageId: page.Page.PageId }).success(function (result) {
                    $scope.content.Body = result.Body;
                    $scope.content.Title = result.Title;
                });
            }
        };

        $scope.getImages = function () {
            $scope.myPromise = invokeServerService.Post('/SiteContent/GetImages').success(function (result) {
                $scope.pageImages = result.Data;
            });
        };

        $scope.deletePhoto = function (x) {
            $scope.myPromise = invokeServerService.Post('/SiteContent/DeleteImage', { fileName: x }).success(function (result) {
                $scope.pageImages = result.Data;
                $scope.getImages();
            });
        };

        $scope.uploadFile = function (attachment) {
            $scope.myPromise = fileUploaderService.uploadFileToUrl(attachment, '/SiteContent/AddImage', $scope.content.Page.PageId)
                .then(function (result) {
                    if (result.Success) {
                        messageFactory.showAlert(result.Message, 'success');
                        $scope.getImages();
                    }
                    else {
                        messageFactory.showAlert(result.Message, 'dangers');
                    }
                }, function () { });
        };

        $scope.saveContent = function () {
            $scope.content.PageId = $scope.content.Page.PageId;
            $scope.myPromise = invokeServerService.Post('/SiteContent/SaveContent', $scope.content).success(function (result) {
                messageFactory.showAlert(result, 'success');
            });
        };
    }]);