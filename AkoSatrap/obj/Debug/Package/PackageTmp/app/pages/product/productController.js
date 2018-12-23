mainApp.lazy.controller('productController', ['$rootScope', '$scope', 'invokeServerService', 'arrayHelperFactory', 'messageFactory', 'cfpLoadingBar', 'fileUploaderService'
    , function ($rootScope, $scope, invokeServerService, arrayHelperFactory, messageFactory, cfpLoadingBar, fileUploaderService) {

        $rootScope.pageTitle = 'مدیریت محصولات';

        $scope.persianPattern = /^[\u0600-\u06FF\s]+$/;
        $scope.englishPattern = /^[a-z\u0590-\u05fe\s]+$/i;
        $scope.numberPattern = /^\+?\d+$/;
        $scope.phonePattern = /^[0-9-]*$/;
        $scope.englishWithNumber = /^[a-zA-Z0-9]*$/;
        $scope.sitePattern = /https?:\/\/www\.[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,3}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/;
        $scope.emailPattern = /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/;
        $scope.isUpdate = false;

        setup_file_input();

        $scope.myPromise = invokeServerService.Post('/PProduct/GetProductCategoryListForDropDown', {}).success(function (result) {

            $scope.productCategoryDataSource = result;

        });

        $scope.deleteProduct = function (id) {
            messageFactory.showConfirmModal("آیا برای حذف سیستمی مطمئن هستید؟",
                function () {
                    $scope.myPromise = invokeServerService.Post('/PProduct/DeleteProduct', { id: id }).success(function (result) {
                        messageFactory.showAlert('عملیات با موفقیت انجام شد', 'success');
                        $scope.getProducts();
                    });
                });
        }

        $scope.addProduct = function () {
            $scope.myPromise = invokeServerService.Post('/PProduct/AddProduct', { product: $scope.product }).success(function (result) {
                if (result.Success) {
                    messageFactory.showAlert(result.Message, 'success');

                    $("#tabInsertUpdate").removeClass("active");
                    $("#navInsertUpdate").removeClass("active");

                    $("#tabAllProduct").addClass("active");
                    $("#navAllProduct").addClass("active");


                    gridDataSource.read();

                    $scope.product = null;

                }
                else {
                    messageFactory.showAlert(result.Message, 'danger');
                }
            });
        };

        $scope.updateProduct = function () {

            $scope.myPromise = invokeServerService.Post('/PProduct/UpdateProduct', { product: $scope.product }).success(function (result) {
                if (result.Success) {
                    messageFactory.showAlert(result.Message, 'success');
                    $scope.product = null;
                    $("#tabInsertUpdate").removeClass("active");
                    $("#navInsertUpdate").removeClass("active");

                    $("#tabAllProduct").addClass("active");
                    $("#navAllProduct").addClass("active");


                    gridDataSource.read();

                }
                else {
                    messageFactory.showAlert(result.Message, 'danger');
                }
            });
        };

        $scope.newProduct = function () {
            $scope.product = null;
            $scope.isUpdate = false;
        }

        $scope.confirmDelete = function () {

        };

        $scope.getProducts = function () {
            $scope.products = [];
            $scope.myPromise = invokeServerService.Get('/PProduct/GetProductList').success(function (result) {
                if (result.Data) {
                    $scope.products = result.Data;
                }
            })
        }
        $scope.getProducts();

        $scope.editProduct = function (item) {
            $scope.product = item;
            $("#tabInsertUpdate").addClass("active");
            $("#navInsertUpdate").addClass("active");
            $scope.isUpdate = true;
            $("#tabAllProduct").removeClass("active");
            $("#navAllProduct").removeClass("active");
        }

        $scope.uploadFile = function () {
            $scope.myPromise = fileUploaderService.uploadFileToUrl($scope.fileAttachment, '/PProduct/AddImage', $scope.product.Id)
                .then(function (result) {
                    if (result.Success) {

                        messageFactory.showAlert(result.Message, 'success');
                        updateProductImage();

                    }
                    else {
                        messageFactory.showAlert(result.Message, 'success');
                    }
                }
                    , function () {

                    });
        };

        $scope.deletePhoto = function (photoName) {
            $scope.myPromise = invokeServerService.Post('/PProduct/DeleteImage', { imageFolderName: $scope.product.ImageFolderName, fileName: photoName }).success(function (result) {
                if (result.Success) {
                    updateProductImage();
                }
            });
        }

        function updateProductImage() {
            $scope.myPromise = invokeServerService.Post('/PProduct/GetProductImages', { imageFolderName: $scope.product.ImageFolderName }).success(function (result) {
                if (result.Success) {
                    $scope.product.Images = result.Data;
                }
            });
        }

    }]);