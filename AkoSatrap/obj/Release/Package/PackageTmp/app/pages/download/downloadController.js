mainApp.lazy.controller('downloadController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory'
    , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory) {

    $rootScope.pageTitle = 'لیست درخواست های دانلود';

    $scope.myPromise = invokeServerService.Post('/Download/GetDownloadList', {}).success(function (result) {
        if (result.Success) {
            $scope.files = result.Data;
        }
        else {
            messageFactory.showAlert(result.Message, "danger");
        }
    });

    $scope.download = function (fileName)
    {
        debugger;
        $scope.myPromise = invokeServerService.Post('/Download/DownloadFile', { fileName: fileName }).success(function (result) {
            if (result.Success) {
                window.location.href = '../../../../Files/ExportFiles/' + fileName;
            }
            else {
                messageFactory.showAlert(result.Message, "danger");
            }
        });
    }
}]);