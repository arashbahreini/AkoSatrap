mainApp.factory('messageFactory', ['$rootScope', function ($rootScope) {

    var messageObj = {};

    messageObj.showAlert = function (message, messageType) {

        $rootScope.message = message;
        $rootScope.messageType = 'alert-' + messageType;
        $rootScope.messageIcon = 'fa-' + messageType;//'success';
        $('#messageModal').modal();
    }

    messageObj.showConfirmModal = function (message, callback) {
        
        $rootScope.message = message;
        $('#confirmModal').modal().one('click', '.btn-success', function (e) {
            $('#confirmModal').modal('hide');
            callback();
        }).one('click', '.btn-danger', function () { $('#confirmModal').modal('hide'); });
    }

    return messageObj;
}]);