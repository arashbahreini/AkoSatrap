var mainApp = angular.module('mainApp', ['ui.router', 'kendo.directives', 'ngMessages', 'ui.mask', 'ngAnimate'
        , 'chieffancypants.loadingBar', 'cgBusy', 'angular-iran-national-id']);
    


//angular.module('mainApp').run(['$rootScope', function ($rootScope) {

//    $rootScope.showMessage = function(message, messageType) {
//        
//        $rootScope.message = message;
//        $rootScope.messageType = 'alert-' + messageType;
//        $rootScope.messageIcon = 'fa-' + messageType;//'success';
//        $('#messageModal').modal();
//    }

//}]);