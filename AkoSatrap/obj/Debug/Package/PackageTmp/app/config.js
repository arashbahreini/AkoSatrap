mainApp.config([
        '$controllerProvider',
        '$compileProvider',
        '$filterProvider',
        '$provide',
        'cfpLoadingBarProvider',
        '$httpProvider',

        function ($controllerProvider, $compileProvider, $filterProvider, $provide, cfpLoadingBarProvider, $httpProvider) {
            $httpProvider.defaults.headers.common["FROM-ANGULAR"] = "true";
            $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
            //برای رجیستر کردن غیر همزمان اجزای انگیولاری در آینده
            mainApp.lazy =
            {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                filter: $filterProvider.register,
                factory: $provide.factory,
                service: $provide.service,                
            };
            cfpLoadingBarProvider.includeSpinner = true;
        }
]);

mainApp.animation('.shuffle-animation', function () {
    return {
        enter: function (element, done) {
            element.css('display', 'none');
            element.fadeIn(1000, done);
            return function () {
                element.stop();
            }
        },
        leave: function (element, done) {
            element.fadeOut(0, done)
            return function () {
                element.stop();
            }
        }
    }
});

