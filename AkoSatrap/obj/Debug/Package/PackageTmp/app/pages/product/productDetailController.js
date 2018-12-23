mainApp.lazy.controller('productCategoryController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar'
    , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar) {

        $rootScope.pageTitle = 'مدیریت جزییات محصولات';

        $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
        $scope.englishPattern = /^[a-z\u0590-\u05fe\s]+$/i;
        $scope.numberPattern = /^\+?\d+$/;
        $scope.phonePattern = /^[0-9-]*$/;
        $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
        $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
        $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;
        $scope.isUpdate = false;


        $scope.myPromise = invokeServerService.Post('/PProduct/GetProductListForDropDown', {}).success(function (result) {
            $scope.productDataSource = result;
            $scope.product = result[0];
        });

        $scope.deleteFeature = function (id) {
            messageFactory.showConfirmModal("آیا برای حذف سیستمی مطمئن هستید؟",
                function () {
                    $scope.myPromise = invokeServerService.Post('/PProduct/DeleteFeature', { id: id }).success(function (result) {
                        messageFactory.showAlert('عملیات با موفقیت انجام شد', 'success');
                        $scope.getProductDetails();
                    });
                });
        }

        $scope.addProductFeature = function () {
            $scope.productDetail.ProductId = $scope.product.Id;
            $scope.myPromise = invokeServerService.Post('/PProduct/AddProductFeature', { productFeature: $scope.productDetail }).success(function (result) {

                if (result.Success) {
                    messageFactory.showAlert(result.Message, 'success');

                    $("#tabInsertUpdate").removeClass("active");
                    $("#navInsertUpdate").removeClass("active");

                    $("#tabInformation").addClass("active");
                    $("#navInformation").addClass("active");

                    $scope.productDetail = null;
                    $scope.getProductDetails();
                }
                else {
                    messageFactory.showAlert(result.Message, 'danger');
                }
            });
        }

        $scope.updateProductFeature = function () {
            $scope.productDetail.ProductId = $scope.product.Id;
            $scope.myPromise = invokeServerService.Post('/PProduct/UpdateProductDetail', { productFeature: $scope.productDetail }).success(function (result) {

                if (result.Success) {
                    messageFactory.showAlert(result.Message, 'success');

                    $("#tabInsertUpdate").removeClass("active");
                    $("#navInsertUpdate").removeClass("active");

                    $("#tabInformation").addClass("active");
                    $("#navInformation").addClass("active");

                    $scope.isUpdate = false;
                    $scope.productDetail = null;
                    $scope.getProductDetails();
                }
                else {
                    messageFactory.showAlert(result.Message, 'danger');
                }
            });
        }

        $scope.newProductFeature = function () {
            $scope.productDetail = null;
            $scope.isUpdate = false;
        }

        $scope.getProductDetails = function () {
            $scope.productDetails = [];
            $scope.myPromise = invokeServerService.Post('/PProduct/GetProductDetail', { productId: $scope.product.Id }).success(function (result) {
                if (result.Data) {
                    $scope.productDetails = result.Data;
                }
            })
        }

        $scope.editProductDetails = function (item) {
            $scope.productDetail = item;
            $("#tabInsertUpdate").addClass("active");
            $("#navInsertUpdate").addClass("active");

            $("#tabInformation").removeClass("active");
            $("#navInformation").removeClass("active");

            $scope.isUpdate = true;
        }
    }]);