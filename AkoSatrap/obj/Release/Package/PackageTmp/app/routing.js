mainApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

  $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', {
            url: '/home',
            templateUrl: '../app/pages/home/home.html',
            resolve: {
                fileDeps: ['$q', '$rootScope', function ($q, $rootScope) {
                    var deferred = $q.defer();
                    var deps = [
                        '../app/pages/home/homeController.js',
                    ];
                    $script(deps, function () {
                        $rootScope.$apply(function () {
                            deferred.resolve();
                        });
                    });
                    return deferred.promise;
                }]
            }
        })
        .state('product', {
            url: '/product',
            templateUrl: '../app/pages/product/product.html',
            resolve: {
                fileDeps: ['$q', '$rootScope', function ($q, $rootScope) {
                    var deferred = $q.defer();
                    var deps = [
                        '../app/pages/product/productController.js',
                    ];
                    $script(deps, function () {
                        $rootScope.$apply(function () {
                            deferred.resolve();
                        });
                    });
                    return deferred.promise;
                }]
            }
        }).state('productCategory', {
            url: '/productCategory',
            templateUrl: '../app/pages/product/productCategory.html',
            resolve: {
                fileDeps: ['$q', '$rootScope', function ($q, $rootScope) {
                    var deferred = $q.defer();
                    var deps = [
                        '../app/pages/product/productCategoryController.js',
                    ];
                    $script(deps, function () {
                        $rootScope.$apply(function () {
                            deferred.resolve();
                        });
                    });
                    return deferred.promise;
                }]
            }
        }).state('productFeature', {
            url: '/productFeature',
            templateUrl: '../app/pages/product/productDetail.html',
            resolve: {
                fileDeps: ['$q', '$rootScope', function ($q, $rootScope) {
                    var deferred = $q.defer();
                    var deps = [
                        '../app/pages/product/productDetailController.js',
                    ];
                    $script(deps, function () {
                        $rootScope.$apply(function () {
                            deferred.resolve();
                        });
                    });
                    return deferred.promise;
                }]
            }
        });
}]);
